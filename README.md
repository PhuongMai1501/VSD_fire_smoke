# Live Camera Processing System

Hệ thống xử lý đồng thời 12 luồng camera RTSP với kiến trúc đa tiến trình, phục vụ cho các bài toán giám sát cháy khói theo thời gian thực.

## Tổng quan
- Nền tảng: .NET 8.0, Windows 10/11 x64.
- Mỗi camera chạy trong tiến trình `CameraWorker` độc lập, giảm rủi ro nghẽn/bị treo toàn hệ thống.
- Giao diện điều khiển chính `CameraManager` giám sát tiến trình con, hiển thị ma trận video 4x3 và tự động khởi động lại khi có sự cố.

## Kiến trúc giải pháp
### CameraWorker
- Giải mã luồng RTSP bằng `FFmpeg.AutoGen`, hỗ trợ tăng tốc phần cứng (CUDA/QSV/D3D11VA).
- Ghi frame vào Memory-Mapped File (MMF) để chia sẻ với tiến trình chính mà không sao chép bộ nhớ.
- Có cơ chế retry, ghi log và xử lý graceful shutdown khi nhận tín hiệu.

### CameraManager
- Điều phối danh sách camera, quản lý vòng đời tiến trình `CameraWorker` thông qua `LittleForker`.
- Đọc cấu hình thiết bị, tham số cảnh báo và lưu log vào CSDL MySQL.
- Kết nối API AI (`/predict`) để phân tích khói/lửa và gửi thông báo qua Telegram/Discord/Zalo.

## Luồng hoạt động chính
1. Người dùng cấu hình danh sách camera, ngưỡng cảnh báo và thông tin kết nối trong giao diện `CameraManager`.
2. Với mỗi camera, `CameraManager` sinh tiến trình `CameraWorker`, truyền RTSP URL, tên MMF/Mutex và tham số phụ (STT, connection string).
3. `CameraWorker` giải mã, đẩy frame vào MMF; `CameraManager` đọc frame, render UI và khi phát hiện sự kiện sẽ gọi API AI.
4. Kết quả suy luận trả về được ghi log, đồng thời hệ thống gửi cảnh báo qua các kênh đã bật.

## Tính năng nổi bật
- Kiến trúc đa tiến trình hạn chế ảnh hưởng chéo giữa các camera.
- Hỗ trợ hardware decoding và tự động fallback sang software khi cần.
- Memory-Mapped File giúp truyền dữ liệu hình ảnh dung lượng lớn với độ trễ thấp.
- Cơ chế tự giám sát, tự khởi động lại tiến trình worker.
- Hệ thống cảnh báo đa kênh: Telegram, Discord, SMS/Zalo (thông qua eSMS API).

## Yêu cầu hệ thống
- .NET SDK 8.0 trở lên.
- Windows 10/11 x64.
- Bộ FFmpeg (đã kèm trong repo, chỉ cần đảm bảo quyền đọc).
- GPU hỗ trợ CUDA/Intel QSV/DirectX (khuyến nghị để tăng tốc).
- MySQL Server cho phần lưu trữ log và cấu hình camera.

## Hướng dẫn cài đặt & chạy
1. **Khôi phục dependencies**: mở solution `LiveCameraProcessing.sln` bằng Visual Studio 2022, nhấn `Restore` nếu được yêu cầu.
2. **Cấu hình RTSP**: trong `CameraManager/Form1.cs`, cập nhật danh sách `_rtspUrls` mặc định hoặc nhập trực tiếp qua giao diện "Camera List".
3. **Chuẩn bị MySQL**: tạo database, bảng `camera_list`, `log_event`, … theo nhu cầu; cập nhật connection string (xem phần *Quản lý bí mật*).
4. **Điền thông tin cảnh báo**: nhập token/ID trong form "Config Message" hoặc chỉnh file `Config Setting/MessageSecrets.ini`.
5. **Build**: chọn cấu hình `x64`, `Release` hoặc `Debug`, sau đó `Build Solution`.
6. **Chạy chương trình**: khởi chạy project `CameraManager`. Tiến trình worker sẽ được tạo tự động khi bật camera.

## Quản lý cấu hình & bí mật
### Luồng ẩn key hiện tại
- Khi chạy lần đầu, `MessageSecretProvider` đảm bảo thư mục `Config Setting/` tồn tại và tạo file `MessageSecrets.ini` với placeholder.
- File thực `Config Setting/MessageSecrets.ini` đã được đưa vào `.gitignore`, vì vậy sẽ không bị commit lên Git.
- Tất cả token, API key, channel ID, thông tin eSMS đều được đọc thông qua `MessageSecretProvider.GetSecrets()`; source code không chứa giá trị thật.

### Tạo `MessageSecrets.ini`
File mẫu tối thiểu:
```
[TELEGRAM BOT TOKEN]  123456:ABCDEF
[DISCORD BOT TOKEN]   your-discord-bot-token
[DISCORD CHANNEL ID]  123456789012345678
[ESMS API KEY]        your-esms-api-key
[ESMS SECRET KEY]     your-esms-secret-key
[ESMS OAID]           123456
[ESMS TEMPLATE ID]    TPL_001
[ESMS BRANDNAME]      FireSmoke
[ESMS CALLBACK URL]   https://esms.vn/webhook/
[ESMS CAMPAIGN ID]    FireSmokeAlert
```
Gợi ý quy trình:
1. Sao chép nội dung mẫu vào file mới `Config Setting/MessageSecrets.ini` (đặt cạnh file exe khi release).
2. Cấp quyền NTFS phù hợp để hạn chế truy cập trái phép.
3. Không commit file này vào Git; nếu cần chia sẻ cho teammate, sử dụng kênh bảo mật (ví dụ vault nội bộ).

### Quản lý chuỗi kết nối MySQL
- Tránh hard-code `connectionString` trong `ClassCommon`. Thay vào đó, lưu chuỗi này cùng `MessageSecrets.ini` hoặc sử dụng biến môi trường.
- Ví dụ khai báo trong `MessageSecrets.ini`:
  ```
  [MYSQL CONNECTION STRING]  Server=192.168.1.10;Database=firesmoke;Uid=svc_camera;Pwd=Super!Secret;
  ```
  Sau đó đọc lại trong code và gán cho `ClassSystemConfig.Ins.m_ClsCommon.connectionString` khi khởi động.
- Đối với môi trường CI/CD, khai báo biến môi trường (`MYSQL_CONNECTION_STRING`, `TELEGRAM_BOT_TOKEN`, …) rồi nạp vào runtime để tránh lưu bí mật lên đĩa.

### Checklist trước khi commit
- Kiểm tra `git status` đảm bảo không có file trong `Config Setting/` được stage.
- Kiểm tra source không chứa mật khẩu/URL riêng tư (`rg "Pwd=" -g"*.cs"`).
- Sử dụng `dotnet user-secrets` hoặc secret manager khác cho môi trường dev nếu cần.

## Troubleshooting
- **Không tìm thấy DLL FFmpeg**: xác nhận đang build `x64` và thư mục `FFmpeg/bin` nằm cạnh exe.
- **Camera không kết nối**: kiểm tra RTSP URL, quyền truy cập mạng và firewall.
- **Hiệu năng thấp**: đảm bảo driver GPU mới nhất, bật hardware decoding và tránh chạy song song quá nhiều stream trên cùng GPU.
- **Thiếu token/API key**: xem lại `MessageSecrets.ini` hoặc form cấu hình; ứng dụng sẽ hiện thông báo kèm log chi tiết.

## Tài liệu bổ sung
- `CameraManager/Config/MessageSecretProvider.cs`: cơ chế đọc bí mật.
- `CameraWorker/Program.cs`: vòng đời tiến trình worker và cách mở luồng RTSP.
- `CameraManager/Class/ClassFunction.cs`: helper xử lý log, lưu ảnh, thao tác DB.
- `README (này)`: quy trình tổng quan, hướng dẫn cấu hình và lưu ý bảo mật.

Chúc bạn triển khai hệ thống an toàn và hiệu quả!

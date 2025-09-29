# LiveCameraProcessing – Flow hoạt động hệ thống

## 1. Mục đích dự án
- Giám sát đồng thời nhiều luồng camera RTSP để phát hiện khói/lửa theo thời gian thực.
- Cô lập xử lý từng camera bằng tiến trình riêng, bảo vệ ứng dụng giao diện khỏi bị treo.
- Chia sẻ khung hình tốc độ cao giữa tiến trình con và giao diện thông qua Memory-Mapped File (MMF) với độ trễ thấp.
- Tích hợp AI inference, lưu log sự kiện và phát cảnh báo đa kênh (Telegram, Zalo, …) dựa trên cấu hình người dùng.

## 2. Kiến trúc tổng quát
- **CameraManager** (`CameraManager/Form1.cs`): ứng dụng WinForms chính quản lý UI, cấu hình, lịch sử và vòng đời tiến trình camera.
- **CameraWorker** (`CameraWorker/Program.cs`): tiến trình phụ phát sinh cho mỗi camera, dùng FFmpeg để giải mã RTSP và chia sẻ frame qua MMF.
- **Giao tiếp liên tiến trình**: mỗi camera sử dụng cặp `MemoryMappedFile` + `Mutex` riêng (tên dạng `Cam_{index}_MMF`), payload BGR24 với header 8 byte (width/height).
- **CSDL MySQL**: lưu danh sách camera (`camera_list`), cấu hình người nhận cảnh báo (`alarm_mes`) và lịch sử sự kiện (`camera_log`).
- **Thư mục cấu hình** `Config Setting/`: lưu tham số UI (`DeviceConfig.ini`), thiết lập gửi cảnh báo (`SendMessageConfig.ini`) và bí mật nhắn tin (`MessageSecrets.ini`).
- **Ghi log**: `FileLogger` và `CameraWorkerLogger` lưu file theo tiến trình vào `Logs/` để truy vết.

## 3. Luồng hoạt động chính
### 3.1 Khởi động ứng dụng
1. `CameraManager/Program.Main()` cấp console, khởi tạo cấu hình WinForms rồi `Application.Run(new Form1())`.
2. `Form1` khởi tạo trong constructor: mở form loading, đăng ký handler đóng ứng dụng, bật hệ thống detection (`InitializeDetectionSystem`) và cảnh báo (`InitializeAlertSystem`).
3. Sự kiện `Form1_Load` gọi lần lượt:
   - `InitializeUI()` tải `DeviceConfig.ini`, liên kết form con (`FormCameraList`, `FormParamCamera`).
   - `LoadCameraList()` đọc bảng `camera_list` qua `ClassFunction.GetRtspUrls`; nếu rỗng thì chèn danh sách RTSP mặc định.
   - `LoadThresholdsByStt()` nạp ngưỡng `Flame_Sensitivity` / `Smoke_Sensitivity` để lọc kết quả AI.
   - `LayoutCameraSpreadView()` dựng lưới `Row x Col` phù hợp số camera và gắn handler double-click/fullscreen.

### 3.2 Khởi chạy và giám sát CameraWorker
1. Khi người dùng nhấn “Start” (hoặc auto trong `Form1_Load`), `StartCameraSystem()` được gọi:
   - Kiểm tra tồn tại `CameraWorker.exe`, chuẩn bị danh sách RTSP đã tải.
   - Cho từng camera: tạo MMF, mutex và instance `ProcessSupervisor` (thư viện LittleForker) với tham số `<rtsp> <mmf> <mutex> <stt> <connStr>` rồi `Start()`.
   - Bật `DisplayTimer` (chu kỳ 5 ms) để đọc frame từ MMF.
2. `DisplayTimer_Tick` thực thi:
   - `UpdatePictureBox(i)` khóa `Mutex`, đọc width/height + payload BGR24, dựng `Bitmap`, hiển thị UI và lưu bản sao `_latestFrames` cho pipeline AI.
   - `UpdateNoSignalOverlay(i)` theo dõi thời gian frame cuối; nếu quá `NO_SIGNAL_TIMEOUT_MS` thì hiện overlay “No Signal”.
3. Cơ chế tự phục hồi:
   - `TryRestartStalledCamera()` kích hoạt khi camera không gửi frame >7s và không vi phạm `RESTART_COOLDOWN_MS`.
   - `RestartWorker()` hủy tiến trình cũ (dò theo `mmf`/`mutex` trong command line), tạo `ProcessSupervisor` mới và reset đồng hồ.

### 3.3 Pipeline phân tích AI và cảnh báo
1. `InitializeDetectionSystem()` bật 3 timer:
   - `_detectionTimer` (150 ms) gom frame mới và gọi `ProcessDetectionAsync` theo camera.
   - `_detectionCleanupTimer` (100 ms) loại bỏ bounding box quá hạn 2s.
   - `_alertBlinkTimer` (400 ms) điều khiển hiệu ứng nháy cảnh báo.
2. `ProcessDetectionAsync(cameraIndex, frame)`:
   - Hạn chế song song bằng `_detectionConcurrency` (tối đa 4 request).
   - `ResizeToSquare()` đưa frame về kích thước `DetectionInputSize`, nén JPEG (quality 75) và POST tới API `http://127.0.0.1:8000/predict`.
   - Ánh xạ kết quả về tọa độ ảnh gốc, lọc theo ngưỡng camera (`GetThresholdsForIndex` + `FilterDetectionsByThreshold`).
   - Lưu ảnh sự kiện `SaveDetectionImage()` nếu có, chèn log DB `AddCameraLogData()` và bật cảnh báo UI `ActivateCameraAlert()`.
   - Gửi thông điệp nếu không bypass: `SendAlarmToActiveRecipientsAsync` → `SendTelegramAlarmAsync` (HTTP GET bot API) hoặc `SendZaloAlarmAsync` (gọi eSMS REST API).
3. Người vận hành có thể xác nhận cảnh báo qua dialog `FormConfirmVision` (kích hoạt trong `ActivateCameraAlert`), quyết định tắt nháy và ghi nhận `Acknowledged`.

### 3.4 Tương tác người dùng & form phụ
- **Danh sách camera** (`FormCameraList`):
  - `UpdateDataGridView()` đọc `camera_list`, hiển thị `DataGridView`. Cho phép thêm/sửa/xóa bản ghi → DB, sau đó gọi `main.ReloadCameraList()`.
- **Thiết lập cảnh báo** (`FormConfigMessage`):
  - `InitComboBoxes()` chọn ứng dụng đích (Telegram/Discord/WhatsApp/Zalo) và mẫu nội dung.
  - `SaveDeviceConfig()` ghi `SendMessageConfig.ini` (`[SEND MESSAGE MODE]`, `[BYPASS ALERT]`).
  - Các hàm CRUD `UpdateAlarmMes()`, `AddRecipientAsync`, `UpdateRecipient`, `DeleteRecipient` thao tác bảng `alarm_mes`.
- **Log view** (`FormLogView`): đọc bảng `camera_log`, hỗ trợ lọc thời gian và xuất CSV.
- **Form loading** (`FormLoading`, `FormStartupLoading`): hiển thị splash trong lúc tải cấu hình.

## 4. Thành phần CameraWorker
- `Program.Main` nhận tham số từ CameraManager. Nếu có `STT` và connection string, hàm `ResolveRtspUrl` truy vấn DB để lấy URL mới nhất (cho phép cập nhật hot).
- `RegisterFFmpegBinaries()` định vị `FFmpeg/bin` tương ứng, cấu hình `ffmpeg.RootPath`.
- `ProcessRTSPStream()` thực hiện chu trình:
  1. `avformat_open_input` với options (RTSP TCP, timeout 5s, `nobuffer`).
  2. Tìm video stream tốt nhất, tạo `AVCodecContext`, ưu tiên hardware (nếu codec hỗ trợ).
  3. Thiết lập scaler `sws_getContext` để chuyển về `AV_PIX_FMT_BGR24`.
  4. Mở MMF + Mutex, ghi trước width/height; mỗi frame đọc được thì `sws_scale` sang BGR và copy tuyến tính vào MMF (bỏ qua stride).
  5. Theo dõi `_shouldExit` để shutdown mềm khi nhận tín hiệu `Ctrl+C`, `ProcessExit` hoặc lệnh kill từ manager.
- Khi xảy ra lỗi, worker ghi log, ngủ 5s rồi retry cho đến khi CameraManager yêu cầu dừng.

## 5. Cấu hình & bí mật
| File | Công dụng chính |
| --- | --- |
| `Config Setting/DeviceConfig.ini` | Tên chương trình, tài khoản đăng nhập, cờ lưu ảnh/log, đường dẫn lưu trữ, auto reconnect, v.v. Đọc/ghi bởi `Form1.LoadDeviceConfig` & `SaveDeviceConfig`. |
| `Config Setting/SendMessageConfig.ini` | Chế độ gửi cảnh báo và cờ bypass, quản lý qua `FormConfigMessage`. |
| `Config Setting/MessageSecrets.ini` | Token/ID (Telegram, Discord, eSMS). Tự tạo nếu thiếu bởi `MessageSecretProvider.GetSecrets()`. Không commit Git. |
| `FFmpeg/bin/` | Thư viện giải mã. Worker tìm và đăng ký khi khởi động. |

## 6. Cách chạy hệ thống
1. **Chuẩn bị môi trường**
   - Windows 10/11 x64, .NET SDK 8.0, Visual Studio 2022.
   - MySQL server cùng schema chứa bảng `camera_list`, `camera_log`, `alarm_mes`, `alarm_mes_history` (nếu dùng) với cột tương ứng.
   - FFmpeg binaries sẵn trong repo (`FFmpeg/bin`).
   - Triển khai service AI tại `http://127.0.0.1:8000/predict` (có thể chỉnh trong `Form1` nếu cần).
2. **Cấu hình**
   - Mở `Config Setting/DeviceConfig.ini` để cập nhật `connectionString`, đường lưu ảnh, chế độ lưu trữ.
   - Khởi tạo `camera_list` bằng `FormCameraList` hoặc import SQL.
   - Tạo `Config Setting/MessageSecrets.ini` với token thật và phân quyền đọc.
   - Qua `FormConfigMessage`, thiết lập người nhận trong bảng `alarm_mes` và chọn ứng dụng gửi.
3. **Build & chạy**
   - Mở `LiveCameraProcessing.sln`, set startup project `CameraManager`, build ở cấu hình `x64`.
   - Run `CameraManager`; form chính hiển thị 4x3 camera. Double-click hoặc phím `F1`…`F12` để fullscreen, `Esc` thoát.
4. **Vận hành**
   - Kiểm tra log realtime trên console hoặc file trong `Logs/`.
   - Sử dụng phím tắt: `Ctrl+R` reload danh sách camera, `Ctrl+Alt+X` emergency shutdown, `Ctrl+Shift+K` kill toàn bộ worker.
   - Theo dõi bảng cảnh báo và xác nhận qua popup `FormConfirmVision` khi cần.

## 7. Các hàm trọng tâm và vai trò
| Thành phần | Hàm/Thuộc tính chính | Vai trò |
| --- | --- | --- |
| `CameraManager/Program.cs` | `Main()` | Khởi động WinForms, mở console, log exception toàn cục. |
| `Form1` | `InitializeUI()`, `LoadCameraList()`, `StartCameraSystem()`, `UpdatePictureBox()`, `DetectionTimer_Tick()`, `ProcessDetectionAsync()`, `SendTelegramAlarmAsync()` | Thiết lập giao diện, nạp cấu hình, tạo/giám sát worker, vòng lặp hiển thị, pipeline AI & cảnh báo. |
|  | `TryRestartStalledCamera()`, `RestartWorker()` | Watchdog tự khởi động lại camera lỗi. |
|  | `ActivateCameraAlert()`, `UpdateAlertAcknowledgementState()` | Điều khiển UI cảnh báo, popup xác nhận. |
| `ClassFunction` | `GetRtspUrls()`, `FormatDataGridView()`, `SaveLog()` | Tiện ích thao tác DB và hiển thị. |
| `ClassCommon` | Thuộc tính cấu hình (RTSP list, lưu ảnh, token mode…) | Lưu trạng thái toàn cục/tùy chọn hệ thống. |
| `FormCameraList` | `UpdateDataGridView()`, CRUD handler | Quản lý bảng `camera_list`, đồng bộ danh sách camera đang chạy. |
| `FormConfigMessage` | `InitComboBoxes()`, `SaveDeviceConfig()`, `UpdateAlarmMes()` và CRUD | Thiết lập ứng dụng gửi tin, danh sách người nhận, bypass. |
| `CameraWorker/Program.cs` | `ResolveRtspUrl()`, `ProcessRTSPStream()` | Fetch RTSP động, giải mã FFmpeg, ghi frame vào MMF. |
| `MessageSecretProvider` | `GetSecrets()` | Đảm bảo bí mật tồn tại, cache token để gửi cảnh báo. |
| `FileLogger`, `CameraWorkerLogger` | `Log()`, `LogException()` | Ghi nhật ký hoạt động từng tiến trình. |

## 8. Ghi chú mở rộng
- **Tăng tốc phần cứng**: có thể bổ sung decoder HW bằng cách đổi codec name trong `ProcessRTSPStream` hoặc chỉnh `av_dict_set` option.
- **Thay đổi API AI**: sửa hằng `API_URL` và logic parse JSON trong `ProcessDetectionAsync` để phù hợp payload mới.
- **Bảo mật**: xem xét nạp `connectionString` từ `MessageSecrets.ini` hoặc biến môi trường thay vì hard-code mặc định trong `ClassCommon`.
- **Giám sát hệ thống**: `FileLogger.GetLogFilePath()` hỗ trợ mở file log; có thể gửi log sang hệ thống khác bằng cách hook vào các hàm `Log` hiện có.

Tài liệu này nhằm giúp onboarding nhanh và tra cứu khi bảo trì/triển khai hệ thống LiveCameraProcessing.

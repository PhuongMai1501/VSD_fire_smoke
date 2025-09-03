# Live Camera Processing System

H? th?ng x? lý 12 camera RTSP ??ng th?i v?i ki?n trúc ?a ti?n trình ?? ??m b?o hi?u n?ng và ?n ??nh cao.

## C?u trúc d? án

### CameraWorker
- Ti?n trình chuyên d?ng ?? gi?i mã m?t lu?ng camera RTSP
- S? d?ng FFmpeg.AutoGen cho vi?c decode video v?i h? tr? t?ng t?c ph?n c?ng (CUDA/QSV/D3D11VA)
- Ghi d? li?u frame vào Memory-Mapped File ?? chia s? v?i ti?n trình chính

### CameraManager  
- Giao di?n chính qu?n lý 12 camera
- S? d?ng LittleForker ?? giám sát và kh?i ??ng l?i các ti?n trình CameraWorker
- Hi?n th? video t? 12 camera trong layout 4x3
- T? ??ng kh?i ??ng l?i worker khi g?p s? c?

## Yêu c?u h? th?ng

- .NET 8.0
- Windows 10/11 (x64)
- FFmpeg libraries (?ã ???c bao g?m trong d? án)
- GPU h? tr? CUDA, Intel QSV ho?c DirectX (tùy ch?n, cho t?ng t?c ph?n c?ng)

## Cách s? d?ng

1. **C?u hình URL camera**: M? `CameraManager\Form1.cs` và thay th? các URL RTSP trong m?ng `_rtspUrls` b?ng ??a ch? th?c t? c?a camera.

```csharp
private readonly List<string> _rtspUrls = new List<string>
{
    "rtsp://192.168.1.100:554/stream1",
    "rtsp://192.168.1.101:554/stream1",
    // ... thêm 10 URL khác
};
```

2. **Build d? án**: Ch?n c?u hình **x64** (quan tr?ng cho FFmpeg libraries) và build solution.

3. **Ch?y ?ng d?ng**: Kh?i ch?y CameraManager.exe

## Tính n?ng chính

- **Ki?n trúc ?a ti?n trình**: M?i camera ch?y trong ti?n trình riêng, tránh ?nh h??ng l?n nhau
- **T?ng t?c ph?n c?ng**: H? tr? CUDA, Intel QSV, DirectX cho decode video
- **T? ph?c h?i**: T? ??ng kh?i ??ng l?i worker khi g?p l?i
- **Hi?u n?ng cao**: S? d?ng Memory-Mapped File ?? chia s? d? li?u nhanh chóng
- **?? tr? th?p**: T?i ?u hóa cho ?ng d?ng real-time

## Ghi chú k? thu?t

- Frame size m?c ??nh: 1920x1080 BGR24
- Frame rate hi?n th?: ~30 FPS
- Memory-mapped file size: ~6MB per camera
- Hardware fallback: T? ??ng chuy?n sang software decoder n?u hardware không kh? d?ng

## Troubleshooting

1. **L?i FFmpeg DLL**: ??m b?o build d? án ? mode x64
2. **Camera không k?t n?i**: Ki?m tra URL RTSP và k?t n?i m?ng
3. **Hi?u n?ng th?p**: Ki?m tra driver GPU và b?t hardware acceleration
4. **Memory usage cao**: ?i?u ch?nh s? l??ng camera ho?c resolution
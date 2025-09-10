﻿using LittleForker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Threading;
using System.IO;
using System.Management;

namespace CameraManager
{
    public partial class Form1 : Form
    {
        #region Camera System Variables
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        // Dynamic camera count based on database
        private int NumCameras => Math.Max(1, ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam?.Count ?? 12);

        private readonly List<ProcessSupervisor> _supervisors = new List<ProcessSupervisor>();
        private readonly List<MemoryMappedFile> _mmfs = new List<MemoryMappedFile>();
        private readonly List<Mutex> _mutexes = new List<Mutex>();

        private readonly List<PictureBox> _pictureboxes = new List<PictureBox>();
        private volatile bool _isShuttingDown = false;

        private const int MaxFrameWidth = 3840;
        private const int MaxFrameHeight = 2160;
        private const long MaxFrameSize = (long)MaxFrameWidth * MaxFrameHeight * 3;

        // FPS Configuration
        private const int TARGET_FPS = 50;
        private const int TIMER_INTERVAL = 1000 / TARGET_FPS;

        // Dynamic Layout Variables
        private TableLayoutPanel tableLayoutPanelCamera;
        private TableLayoutPanel[] tableLayoutPanelDevice;
        public int Row = 3; // 3 rows
        public int Col = 4; // 4 columns per row

        // Fullscreen state tracking
        private bool _isFullscreen = false;
        private int _fullscreenCameraIndex = -1;
        private readonly object _fullscreenLock = new object();

        // AI Detection Variables
        private static readonly HttpClient httpClient = CreateHttpClient();
        private readonly Dictionary<string, string> predictionCache = new Dictionary<string, string>();
        private readonly object cacheLock = new object();
        private DateTime lastCacheCleanup = DateTime.Now;
        private const int CACHE_CLEANUP_INTERVAL_MS = 30000;
        private const string API_URL = "http://127.0.0.1:8000/predict";

        // Detection overlay
        private readonly List<List<Detection>> _cameraDetections = new List<List<Detection>>();
        private readonly System.Windows.Forms.Timer _detectionTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer _detectionCleanupTimer = new System.Windows.Forms.Timer();

        // Reconnect monitoring (per-camera via DisplayTimer)
        private readonly Dictionary<int, DateTime> _lastFrameAt = new Dictionary<int, DateTime>();
        private readonly Dictionary<int, DateTime> _lastStallDetectedAt = new Dictionary<int, DateTime>();
        private readonly Dictionary<int, DateTime> _lastRestartAt = new Dictionary<int, DateTime>();
        private readonly HashSet<int> _restartingCameras = new HashSet<int>();
        private readonly object _reconnectLock = new object();
        private const int NO_SIGNAL_TIMEOUT_MS = 2000;
        private const int RESTART_STALL_MS = 7000;
        private const int RESTART_COOLDOWN_MS = 10000;

        // Per-camera thresholds loaded from DB (key: STT)
        private readonly Dictionary<int, (double flame, double smoke)> _thresholdsByStt = new Dictionary<int, (double flame, double smoke)>();
        private readonly object _thresholdsLock = new object();

        // Store frame for AI detection processing
        private readonly Dictionary<int, Bitmap> _latestFrames = new Dictionary<int, Bitmap>();
        private readonly object _frameStoreLock = new object();

        // Detection processing flags to prevent recursion
        private readonly HashSet<int> _processsingDetection = new HashSet<int>();
        private readonly object _detectionProcessLock = new object();

        // Detection throttling and concurrency
        private readonly SemaphoreSlim _detectionConcurrency = new SemaphoreSlim(4); // limit concurrent requests
        private readonly Dictionary<int, DateTime> _lastDetectAt = new Dictionary<int, DateTime>();
        private const int DETECT_MIN_INTERVAL_MS = 100; // per-camera min interval (≈10 FPS); adjust down to 50 for ~20 FPS
        private const int DETECTION_TIMER_INTERVAL_MS = 150; // drive scheduling loop cadence
        // Detection input config: use square input size from global config
        private int DETECT_INPUT_SIZE => Math.Max(1, ClassSystemConfig.Ins?.m_ClsCommon?.DetectionInputSize ?? 1280);
        private const long JPEG_QUALITY = 75L; // JPEG quality for request payload

        // MySQL Connection
        MySqlConnection connection = new MySqlConnection(ClassSystemConfig.Ins.m_ClsCommon.connectionString);

        #endregion

        public Form1()
        {
            ClassSystemConfig.Ins.m_ClsCommon.StartupLoadingForm();

            InitializeComponent();
            // Enable keyboard events
            this.KeyPreview = true;
            this.WindowState = FormWindowState.Maximized;

            // Handle process exit events to ensure cleanup
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ApplicationExit += Application_ApplicationExit;

            // Handle Windows shutdown/logoff
            Microsoft.Win32.SystemEvents.SessionEnding += SystemEvents_SessionEnding;

            // Initialize detection components
            InitializeDetectionSystem();
        }

        #region Form Event Handlers
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("\n=== CameraManager Form1_Load Started ===");

                InitializeUI();
                LoadCameraList();
                // Load per-camera thresholds from database after camera list
                LoadThresholdsByStt();

                Console.WriteLine($"Camera Configuration: {NumCameras} cameras, {Row}x{Col} grid");

                LayoutCameraSpreadView();
                UpdateCameraLogInvoke(this);
                Thread.Sleep(1000);
                Console.WriteLine("? Form1_Load completed");
                ClassSystemConfig.Ins.m_ClsCommon.StopStartupLoadingForm();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Form1_Load error: {ex.Message}");
                FileLogger.LogException(ex, "Form1_Load");
            }
        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            if (_isShuttingDown) return;

            try
            {
                for (int i = 0; i < NumCameras && i < _mmfs.Count && i < _pictureboxes.Count; i++)
                {
                    UpdatePictureBox(i);
                    UpdateNoSignalOverlay(i);
                }
            }
            catch (Exception ex)
            {
                if (!_isShuttingDown)
                {
                    FileLogger.LogException(ex, "DisplayTimer_Tick");
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                FileLogger.Log($"?? Key pressed: {e.KeyCode}, Fullscreen: {_isFullscreen}, Camera: {_fullscreenCameraIndex}");
                Console.WriteLine($"?? Key pressed: {e.KeyCode}, Fullscreen: {_isFullscreen}, Camera: {_fullscreenCameraIndex}");

                // Handle ESC first - always exit fullscreen if active
                if (e.KeyCode == Keys.Escape)
                {
                    if (_isFullscreen)
                    {
                        FileLogger.Log("?? ESC pressed - Forcing exit fullscreen mode");
                        Console.WriteLine("?? ESC pressed - Forcing exit fullscreen mode");

                        lock (_fullscreenLock)
                        {
                            ExitFullscreen();
                        }
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        FileLogger.Log("?? ESC pressed but not in fullscreen mode");
                        Console.WriteLine("?? ESC pressed but not in fullscreen mode");
                    }
                    e.Handled = true;
                    return;
                }

                // Handle F1-F12 keys for camera selection
                else if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
                {
                    int cameraIndex = e.KeyCode - Keys.F1;
                    if (cameraIndex < NumCameras)
                    {
                        FileLogger.Log($"?? F{cameraIndex + 1} pressed - Toggling camera {cameraIndex + 1}");
                        Console.WriteLine($"?? F{cameraIndex + 1} pressed - Toggling camera {cameraIndex + 1}");

                        lock (_fullscreenLock)
                        {
                            ToggleFullscreen(cameraIndex);
                        }
                        e.Handled = true;
                    }
                    else
                    {
                        Console.WriteLine($"?? F{cameraIndex + 1} pressed but only {NumCameras} cameras available");
                    }
                }

                // Handle number keys 1-9 for camera selection
                else if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
                {
                    int cameraIndex = e.KeyCode - Keys.D1;

                    if (cameraIndex < NumCameras)
                    {
                        FileLogger.Log($"?? Number {cameraIndex + 1} pressed - Toggling camera {cameraIndex + 1}");
                        Console.WriteLine($"?? Number {cameraIndex + 1} pressed - Toggling camera {cameraIndex + 1}");

                        lock (_fullscreenLock)
                        {
                            if (_isFullscreen && _fullscreenCameraIndex == cameraIndex)
                            {
                                Console.WriteLine($"?? Camera {cameraIndex + 1} already in fullscreen - exiting");
                                ExitFullscreen();
                            }
                            else
                            {
                                Console.WriteLine($"?? Switching to Camera {cameraIndex + 1} fullscreen");
                                EnterFullscreen(cameraIndex);
                            }
                        }
                        e.Handled = true;
                    }
                    else
                    {
                        Console.WriteLine($"?? Number {cameraIndex + 1} pressed but only {NumCameras} cameras available");
                        Console.WriteLine($"?? Available cameras: 1-{NumCameras} (Press F1-F{NumCameras} or 1-{Math.Min(NumCameras, 9)})");
                    }
                }

                // Handle numpad keys 1-9 for camera selection
                else if (e.KeyCode >= Keys.NumPad1 && e.KeyCode <= Keys.NumPad9)
                {
                    int cameraIndex = e.KeyCode - Keys.NumPad1;

                    if (cameraIndex < NumCameras)
                    {
                        FileLogger.Log($"?? Numpad {cameraIndex + 1} pressed - Toggling camera {cameraIndex + 1}");
                        Console.WriteLine($"?? Numpad {cameraIndex + 1} pressed - Toggling camera {cameraIndex + 1}");

                        lock (_fullscreenLock)
                        {
                            if (_isFullscreen && _fullscreenCameraIndex == cameraIndex)
                            {
                                Console.WriteLine($"?? Camera {cameraIndex + 1} already in fullscreen - exiting");
                                ExitFullscreen();
                            }
                            else
                            {
                                Console.WriteLine($"?? Switching to Camera {cameraIndex + 1} fullscreen");
                                EnterFullscreen(cameraIndex);
                            }
                        }
                        e.Handled = true;
                    }
                    else
                    {
                        Console.WriteLine($"?? Numpad {cameraIndex + 1} pressed but only {NumCameras} cameras available");
                    }
                }

                // Show help with H key
                else if (e.KeyCode == Keys.H || (e.Control && e.KeyCode == Keys.H))
                {
                    ShowKeyboardShortcuts();
                    e.Handled = true;
                }

                // (Removed) Test detection overlay via T key

                // Emergency shutdown with Ctrl+Alt+X
                else if (e.Control && e.Alt && e.KeyCode == Keys.X)
                {
                    FileLogger.Log("?? EMERGENCY SHUTDOWN HOTKEY PRESSED (Ctrl+Alt+X)");
                    EmergencyShutdown();
                    e.Handled = true;
                    Application.Exit();
                }

                // Force kill workers with Ctrl+Shift+K
                else if (e.Control && e.Shift && e.KeyCode == Keys.K)
                {
                    FileLogger.Log("?? FORCE KILL WORKERS HOTKEY PRESSED (Ctrl+Shift+K)");
                    ForceKillAllCameraWorkers();
                    e.Handled = true;
                }

                // Reload camera list with Ctrl+R
                else if (e.Control && e.KeyCode == Keys.R)
                {
                    FileLogger.Log("?? RELOAD CAMERA LIST HOTKEY PRESSED (Ctrl+R)");
                    ReloadCameraList();
                    e.Handled = true;
                }

                // Debug fullscreen state with Ctrl+D
                else if (e.Control && e.KeyCode == Keys.D)
                {
                    DebugFullscreenState();
                    e.Handled = true;
                }

                // Force exit fullscreen with Ctrl+E
                else if (e.Control && e.KeyCode == Keys.E)
                {
                    Console.WriteLine("?? FORCE EXIT FULLSCREEN HOTKEY PRESSED (Ctrl+E)");
                    FileLogger.Log("?? FORCE EXIT FULLSCREEN HOTKEY PRESSED (Ctrl+E)");
                    ForceExitFullscreen();
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "Form1_KeyDown");
                Console.WriteLine($"? Form1_KeyDown error: {ex.Message}");
            }
        }

        #endregion

        #region Button Event Handlers

        private void btnHideSetting_Click(object sender, EventArgs e)
        {
            try
            {
                panelSetting.Visible = !panelSetting.Visible;
                FileLogger.Log($"Setting panel visibility toggled: {panelSetting.Visible}");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "btnHideSetting_Click");
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
                FileLogger.Log("Window minimized");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "btnMinimize_Click");
            }
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                    FileLogger.Log("Window restored to normal");
                }
                else
                {
                    this.WindowState = FormWindowState.Maximized;
                    FileLogger.Log("Window maximized");
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "btnMaximize_Click");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                FileLogger.Log("Exit button clicked - Closing application");
                this.Close();
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "btnExit_Click");
            }
        }

        private void btnStartCamera_Click(object sender, EventArgs e)
        {
            try
            {
                FileLogger.Log("?? Start Camera button clicked");
                ClassSystemConfig.Ins.m_ClsCommon.StartLoadingForm();

                btnStartCamera.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                StartCameraSystem();

                btnStopCamera.Enabled = true;
                this.Cursor = Cursors.Default;

                FileLogger.Log("? Camera system start initiated");
                ClassSystemConfig.Ins.m_ClsCommon.StopLoadingForm();
                MessageBox.Show("Camera system started successfully!", "Start Camera",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "btnStartCamera_Click");

                btnStartCamera.Enabled = true;
                btnStopCamera.Enabled = false;
                this.Cursor = Cursors.Default;

                MessageBox.Show($"Error starting camera system: {ex.Message}", "Start Camera Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStopCamera_Click(object sender, EventArgs e)
        {
            try
            {
                FileLogger.Log("?? Stop Camera button clicked");

                btnStopCamera.Enabled = false;
                btnStartCamera.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                DisplayTimer?.Stop();
                _detectionTimer?.Stop();
                _detectionCleanupTimer?.Stop();

                lock (_cameraDetections)
                {
                    for (int i = 0; i < _cameraDetections.Count; i++)
                    {
                        _cameraDetections[i].Clear();
                    }
                }

                lock (_frameStoreLock)
                {
                    foreach (var frame in _latestFrames.Values)
                    {
                        try { frame?.Dispose(); } catch { }
                    }
                    _latestFrames.Clear();
                }

                foreach (var pictureBox in _pictureboxes)
                {
                    try
                    {
                        if (!pictureBox.IsDisposed)
                        {
                            var oldImage = pictureBox.Image;
                            pictureBox.Image = null;
                            oldImage?.Dispose();

                            if (pictureBox.Controls.Count > 0)
                            {
                                pictureBox.Controls[0].Visible = true;
                            }

                            pictureBox.Invalidate();
                        }
                    }
                    catch (Exception ex)
                    {
                        FileLogger.LogException(ex, $"Clear PictureBox");
                    }
                }

                EmergencyShutdown();

                this.Cursor = Cursors.Default;
                btnStartCamera.Enabled = true;
                btnStopCamera.Enabled = false;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                FileLogger.Log("?? Camera system successfully stopped");
                MessageBox.Show("Camera system stopped successfully!", "Stop Camera",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "btnStopCamera_Click");

                this.Cursor = Cursors.Default;
                btnStartCamera.Enabled = true;
                btnStopCamera.Enabled = false;

                MessageBox.Show($"Error stopping camera system: {ex.Message}", "Stop Camera Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeButtonColorClick(sender);
                ClassSystemConfig.Ins.m_FrmParamCamera.Show();
                ClassSystemConfig.Ins.m_FrmParamCamera.ShowOnScreen();
                ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.HANDLER_CLICKED,
                                                        "Clicked Camera Setting",
                                                        ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
                FileLogger.Log("Camera settings opened");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "btnSetting_Click");
            }
        }

        private void ChangeButtonColorClick(object sender)
        {
            try
            {
                if ((Button)sender == btnAlarm)
                {
                    btnAlarm.BackColor = Color.DeepSkyBlue;
                    btnSetting.BackColor = Color.Aquamarine;
                    btnLogView.BackColor = Color.Aquamarine;
                }
                else if ((Button)sender == btnSetting)
                {
                    btnAlarm.BackColor = Color.Aquamarine;
                    btnSetting.BackColor = Color.DeepSkyBlue;
                    btnLogView.BackColor = Color.Aquamarine;
                }
                else if ((Button)sender == btnLogView)
                {
                    btnAlarm.BackColor = Color.Aquamarine;
                    btnSetting.BackColor = Color.Aquamarine;
                    btnLogView.BackColor = Color.DeepSkyBlue;
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "ChangeButtonColorClick");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _isShuttingDown = true;
                FileLogger.Log("\n=== Application Shutdown Started ===");

                DisplayTimer?.Stop();
                _detectionTimer?.Stop();
                _detectionTimer?.Dispose();
                _detectionCleanupTimer?.Stop();
                _detectionCleanupTimer?.Dispose();

                lock (_cameraDetections)
                {
                    for (int i = 0; i < _cameraDetections.Count; i++)
                    {
                        _cameraDetections[i].Clear();
                    }
                }

                lock (_frameStoreLock)
                {
                    foreach (var frame in _latestFrames.Values)
                    {
                        try { frame?.Dispose(); } catch { }
                    }
                    _latestFrames.Clear();
                }

                ForceKillAllCameraWorkers();

                foreach (var supervisor in _supervisors)
                {
                    try { supervisor?.Dispose(); } catch { }
                }

                foreach (var mmf in _mmfs)
                {
                    try { mmf?.Dispose(); } catch { }
                }
                foreach (var mutex in _mutexes)
                {
                    try { mutex?.Close(); mutex?.Dispose(); } catch { }
                }

                foreach (var pictureBox in _pictureboxes)
                {
                    try { pictureBox?.Image?.Dispose(); } catch { }
                }

                FileLogger.Log("? Application shutdown completed successfully");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "Form1_FormClosing");
            }
        }

        #endregion

        #region AI Detection Classes
        public class Detection
        {
            public string label { get; set; }
            public double x1 { get; set; }
            public double y1 { get; set; }
            public double x2 { get; set; }
            public double y2 { get; set; }
            public double score { get; set; }
            public DateTime timestamp { get; set; } = DateTime.Now;
        }

        private void InitializeDetectionSystem()
        {
            try
            {
                // Do not assume camera count at startup; cameras load later.
                EnsureCameraDetectionsSize(Math.Max(1, NumCameras));

                _detectionTimer.Interval = DETECTION_TIMER_INTERVAL_MS;
                _detectionTimer.Tick += DetectionTimer_Tick;
                _detectionTimer.Start();

                _detectionCleanupTimer.Interval = 100;
                _detectionCleanupTimer.Tick += DetectionCleanupTimer_Tick;
                _detectionCleanupTimer.Start();

                FileLogger.Log("? AI Detection system initialized");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "InitializeDetectionSystem");
            }
        }

        private void EnsureCameraDetectionsSize(int target)
        {
            try
            {
                if (target <= 0) target = 1;
                lock (_cameraDetections)
                {
                    while (_cameraDetections.Count < target)
                    {
                        _cameraDetections.Add(new List<Detection>());
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "EnsureCameraDetectionsSize");
            }
        }

        private void DetectionCleanupTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_isShuttingDown) return;

                var currentTime = DateTime.Now;

                lock (_cameraDetections)
                {
                    EnsureCameraDetectionsSize(Math.Max(NumCameras, _pictureboxes.Count));
                    for (int cameraIndex = 0; cameraIndex < _cameraDetections.Count; cameraIndex++)
                    {
                        var detections = _cameraDetections[cameraIndex];
                        int originalCount = detections.Count;

                        detections.RemoveAll(d => (currentTime - d.timestamp).TotalMilliseconds > 2000);

                        if (detections.Count != originalCount)
                        {
                            TriggerPaintEvent(cameraIndex, detections.Count);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "DetectionCleanupTimer_Tick");
            }
        }

        private async void DetectionTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_isShuttingDown) return;
                // Schedule detection tasks per camera with throttling and concurrency limits
                var now = DateTime.Now;
                int maxIndex;
                lock (_frameStoreLock) { maxIndex = _latestFrames.Count; }

                for (int i = 0; i < maxIndex; i++)
                {
                    Bitmap frame = null;
                    lock (_frameStoreLock)
                    {
                        if (!_latestFrames.ContainsKey(i)) continue;
                        frame = (Bitmap)_latestFrames[i].Clone();
                    }

                    bool shouldProcess = false;
                    lock (_detectionProcessLock)
                    {
                        if (!_processsingDetection.Contains(i))
                        {
                            if (!_lastDetectAt.TryGetValue(i, out var last) || (now - last).TotalMilliseconds >= DETECT_MIN_INTERVAL_MS)
                            {
                                _processsingDetection.Add(i);
                                _lastDetectAt[i] = now;
                                shouldProcess = true;
                            }
                        }
                    }

                    if (!shouldProcess)
                    {
                        frame.Dispose();
                        continue;
                    }

                    _ = ProcessDetectionAsync(i, frame);
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "DetectionTimer_Tick");
            }
        }

        private async Task ProcessDetectionAsync(int cameraIndex, Bitmap frame)
        {
            try
            {
                // Limit global concurrency; skip if at capacity to keep realtime
                if (!await _detectionConcurrency.WaitAsync(0))
                {
                    frame.Dispose();
                    lock (_detectionProcessLock)
                    {
                        _processsingDetection.Remove(cameraIndex);
                    }
                    return;
                }

                // Resize frame to reduce payload
                using var resized = ResizeToSquare(frame, DETECT_INPUT_SIZE);

                // Encode JPEG with quality and to base64
                var jpegBytes = EncodeJpeg(resized, JPEG_QUALITY);
                string base64Image = Convert.ToBase64String(jpegBytes);

                var payload = new { image = base64Image };
                var json = JsonConvert.SerializeObject(payload);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var response = await httpClient.PostAsync(API_URL, content);
                if (!response.IsSuccessStatusCode)
                {
                    return;
                }

                var result = await response.Content.ReadAsStringAsync();
                var detectionsRaw = JsonConvert.DeserializeObject<List<Detection>>(result) ?? new List<Detection>();

                // Debug raw
                if (detectionsRaw.Count > 0)
                {
                    var d = detectionsRaw[0];
                    FileLogger.Log($"Detect RAW cam {cameraIndex + 1}: cnt={detectionsRaw.Count} first=({d.label},{d.score:F2}) x1={d.x1:F3},y1={d.y1:F3},x2={d.x2:F3},y2={d.y2:F3}");
                }

                // Normalize detections to original frame coordinates [0..1]
                var detections = NormalizeDetectionsToOriginalFrame(detectionsRaw, frame.Width, frame.Height, DETECT_INPUT_SIZE);

                if (detections.Count > 0)
                {
                    var d = detections[0];
                    FileLogger.Log($"Detect NORM cam {cameraIndex + 1}: cnt={detections.Count} first=({d.label},{d.score:F2}) x1={d.x1:F3},y1={d.y1:F3},x2={d.x2:F3},y2={d.y2:F3}");
                }

                // Apply per-camera thresholds by STT (cameraIndex + 1)
                var (thrFlame, thrSmoke) = GetThresholdsForIndex(cameraIndex);
                var filtered = FilterDetectionsByThreshold(detections, thrFlame, thrSmoke);

                EnsureCameraDetectionsSize(cameraIndex + 1);
                lock (_cameraDetections)
                {
                    if (cameraIndex < _cameraDetections.Count)
                    {
                        _cameraDetections[cameraIndex].Clear();
                        _cameraDetections[cameraIndex].AddRange(filtered);
                    }
                }

                if (filtered.Count > 0)
                {
                    try { SaveDetectionImage(cameraIndex, frame, filtered); } catch (Exception exSave) { FileLogger.LogException(exSave, "ProcessDetectionAsync -> SaveDetectionImage"); }

                    // Gọi cảnh báo alarm nếu bật và có người nhận active
                    try
                    {
                        var labels = filtered
                            .Where(d => d != null && !string.IsNullOrWhiteSpace(d.label))
                            .Select(d => d.label?.Trim() ?? "")
                            .ToList();
                        bool hasFire = labels.Any(l => l.Equals("fire", StringComparison.OrdinalIgnoreCase) || l.Equals("flame", StringComparison.OrdinalIgnoreCase) || l.Contains("fire", StringComparison.OrdinalIgnoreCase) || l.Contains("flame", StringComparison.OrdinalIgnoreCase));
                        bool hasSmoke = labels.Any(l => l.Equals("smoke", StringComparison.OrdinalIgnoreCase) || l.Contains("smoke", StringComparison.OrdinalIgnoreCase));
                        string eventText = hasFire ? "FIRE" : (hasSmoke ? "SMOKE" : "FIRE");

                        _ = SendAlarmToActiveRecipientsAsync(eventText);
                    }
                    catch (Exception exAlarm)
                    {
                        FileLogger.LogException(exAlarm, "ProcessDetectionAsync -> SendAlarm");
                    }
                }
                TriggerPaintEvent(cameraIndex, filtered.Count);
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "ProcessDetectionAsync");
            }
            finally
            {
                try { frame.Dispose(); } catch { }
                _detectionConcurrency.Release();
                lock (_detectionProcessLock)
                {
                    _processsingDetection.Remove(cameraIndex);
                }
            }
        }

        // Gửi cảnh báo theo cấu hình (chỉ Telegram hiện tại) tới các ChatID đang IsActive=1
        private async Task SendAlarmToActiveRecipientsAsync(string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message)) return;
                // ByPass: nếu bật (1) thì bỏ qua không gửi
                if (ClassSystemConfig.Ins?.m_ClsCommon?.b_ByPassAlarm == 1) return;

                // 0: Telegram (theo form config)
                if (ClassSystemConfig.Ins?.m_ClsCommon?.m_iFormatSendMessage != 0) return;

                string botToken = "7918989769:AAEAH2IAU91rJ3pBGGGLhuE2SDm03Q4-TH4";
                var recipients = new List<(string Name, string SDT, string ChatID)>();

                string connStr = ClassSystemConfig.Ins?.m_ClsCommon?.connectionString;
                if (string.IsNullOrWhiteSpace(connStr))
                {
                    FileLogger.Log("SendAlarmToActiveRecipientsAsync: Missing DB connection string");
                    return;
                }

                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
                {
                    await conn.OpenAsync();
                    string sql = "SELECT Name, SDT, ChatID FROM alarm_mes WHERE IsActive = 1 AND ChatID IS NOT NULL AND TRIM(ChatID) <> ''";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var name = reader["Name"]?.ToString()?.Trim() ?? string.Empty;
                            var sdt = reader["SDT"]?.ToString()?.Trim() ?? string.Empty;
                            var raw = reader["ChatID"]?.ToString()?.Trim();
                            if (string.IsNullOrWhiteSpace(raw)) continue;

                            var parts = raw
                                .Split(new[] { ',', ';', ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(s => s.Trim())
                                .Where(s => !string.IsNullOrWhiteSpace(s));
                            foreach (var id in parts)
                            {
                                recipients.Add((name, sdt, id));
                            }
                        }
                    }
                }

                if (recipients.Count == 0) return;
                recipients = recipients
                    .GroupBy(r => (r.ChatID, r.Name, r.SDT))
                    .Select(g => g.First())
                    .ToList();

                using (var client = new HttpClient())
                {
                    foreach (var r in recipients)
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(r.ChatID))
                            {
                                ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.DATA,
                                    $"TELEGRAM SEND | Name={r.Name} | ChatID=<EMPTY> | SDT={r.SDT} | Status=FAIL (empty)",
                                    ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
                                continue;
                            }

                            string url = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={r.ChatID}&text={Uri.EscapeDataString(message)}";
                            var resp = await client.GetAsync(url);
                            var ok = resp.IsSuccessStatusCode;

                            ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.DATA,
                                $"TELEGRAM SEND | Name={r.Name} | ChatID={r.ChatID} | SDT={r.SDT} | Status={(ok ? "SUCCESS" : "FAIL (HTTP)")}",
                                ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
                        }
                        catch (Exception exSend)
                        {
                            ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.DATA,
                                $"TELEGRAM SEND | Name={r.Name} | ChatID={r.ChatID} | SDT={r.SDT} | Status=FAIL (EXCEPTION: {exSend.Message})",
                                ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
                            FileLogger.LogException(exSend, $"SendAlarmToActiveRecipientsAsync -> ChatID={r.ChatID}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "SendAlarmToActiveRecipientsAsync");
            }
        }

        // Convert detections (either normalized or pixel wrt square input) to normalized [0..1] on original frame
        private List<Detection> NormalizeDetectionsToOriginalFrame(List<Detection> input, int frameW, int frameH, int squareSize)
        {
            try
            {
                if (input == null || input.Count == 0) return new List<Detection>();
                if (frameW <= 0 || frameH <= 0 || squareSize <= 0) return new List<Detection>(input);

                double scale = Math.Min((double)squareSize / frameW, (double)squareSize / frameH);
                double newW = frameW * scale;
                double newH = frameH * scale;
                double offX = (squareSize - newW) / 2.0;
                double offY = (squareSize - newH) / 2.0;

                var list = new List<Detection>(input.Count);
                foreach (var d in input)
                {
                    if (d == null) continue;

                    bool squareNormalized = d.x2 <= 1.5 && d.y2 <= 1.5 && d.x1 >= 0 && d.y1 >= 0;

                    // Convert to square pixel coordinates first
                    double sx1 = squareNormalized ? d.x1 * squareSize : d.x1;
                    double sy1 = squareNormalized ? d.y1 * squareSize : d.y1;
                    double sx2 = squareNormalized ? d.x2 * squareSize : d.x2;
                    double sy2 = squareNormalized ? d.y2 * squareSize : d.y2;

                    // Map back to original frame pixel coordinates (undo letterbox + scale)
                    double ox1 = (sx1 - offX) / scale;
                    double oy1 = (sy1 - offY) / scale;
                    double ox2 = (sx2 - offX) / scale;
                    double oy2 = (sy2 - offY) / scale;

                    // Clamp
                    ox1 = Math.Max(0, Math.Min(frameW, ox1));
                    oy1 = Math.Max(0, Math.Min(frameH, oy1));
                    ox2 = Math.Max(0, Math.Min(frameW, ox2));
                    oy2 = Math.Max(0, Math.Min(frameH, oy2));

                    // Normalize to [0..1]
                    double nx1 = frameW > 0 ? ox1 / frameW : 0;
                    double ny1 = frameH > 0 ? oy1 / frameH : 0;
                    double nx2 = frameW > 0 ? ox2 / frameW : 0;
                    double ny2 = frameH > 0 ? oy2 / frameH : 0;

                    list.Add(new Detection
                    {
                        label = d.label,
                        score = d.score,
                        x1 = nx1,
                        y1 = ny1,
                        x2 = nx2,
                        y2 = ny2,
                        timestamp = DateTime.Now
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "NormalizeDetectionsToOriginalFrame");
                return new List<Detection>(input);
            }
        }

        private void TriggerPaintEvent(int cameraIndex, int detectionCount)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => TriggerPaintEvent(cameraIndex, detectionCount)));
                    return;
                }

                if (cameraIndex < _pictureboxes.Count && !_pictureboxes[cameraIndex].IsDisposed)
                {
                    var pictureBox = _pictureboxes[cameraIndex];
                    // Force refresh when we have detections to ensure overlay appears promptly
                    if (detectionCount > 0)
                    {
                        pictureBox.Refresh();
                    }
                    else
                    {
                        pictureBox.Invalidate();
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, $"TriggerPaintEvent - Camera {cameraIndex + 1}");
            }
        }

        private void DrawDetectionBoxes(Graphics graphics, List<Detection> detections, int imageWidth, int imageHeight)
        {
            try
            {
                if (graphics == null || detections == null) return;

                foreach (var detection in detections)
                {
                    if (detection.score < 0.3) continue;

                    // Support both normalized [0,1] and absolute coordinates
                    // If any coordinate > 1, assume absolute pixels already
                    bool isNormalized = detection.x2 <= 1.5 && detection.y2 <= 1.5 && detection.x1 >= 0 && detection.y1 >= 0;

                    int x1 = (int)((isNormalized ? detection.x1 : detection.x1 / Math.Max(1, imageWidth)) * imageWidth);
                    int y1 = (int)((isNormalized ? detection.y1 : detection.y1 / Math.Max(1, imageHeight)) * imageHeight);
                    int x2 = (int)((isNormalized ? detection.x2 : detection.x2 / Math.Max(1, imageWidth)) * imageWidth);
                    int y2 = (int)((isNormalized ? detection.y2 : detection.y2 / Math.Max(1, imageHeight)) * imageHeight);

                    // Order and clamp to bounds
                    int left = Math.Max(0, Math.Min(imageWidth - 1, Math.Min(x1, x2)));
                    int top = Math.Max(0, Math.Min(imageHeight - 1, Math.Min(y1, y2)));
                    int right = Math.Max(0, Math.Min(imageWidth - 1, Math.Max(x1, x2)));
                    int bottom = Math.Max(0, Math.Min(imageHeight - 1, Math.Max(y1, y2)));
                    int w = Math.Max(1, right - left);
                    int h = Math.Max(1, bottom - top);

                    using (var pen = new Pen(Color.Red, 2))
                    {
                        Rectangle rect = new Rectangle(left, top, w, h);
                        graphics.DrawRectangle(pen, rect);
                    }

                    string labelText = $"{detection.label} ({detection.score:F2})";
                    using (var font = new Font("Arial", 11, FontStyle.Bold))
                    {
                        using (var textBrush = new SolidBrush(Color.White))
                        {
                            graphics.DrawString(labelText, font, textBrush, new PointF(left + 3, Math.Max(0, top - 18)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "DrawDetectionBoxes");
            }
        }

        // Save annotated image to folder structure yyyy/MM/dd/image_#.jpg
        private void SaveDetectionImage(int cameraIndex, Bitmap frame, List<Detection> detections)
        {
            try
            {
                if (frame == null || detections == null || detections.Count == 0) return;

                using (var annotated = (Bitmap)frame.Clone())
                using (var g = Graphics.FromImage(annotated))
                {
                    DrawDetectionBoxes(g, detections, annotated.Width, annotated.Height);

                    string baseRoot = null;
                    try { baseRoot = ClassSystemConfig.Ins?.m_ClsCommon?.m_CommonPath; } catch { baseRoot = null; }
                    if (string.IsNullOrWhiteSpace(baseRoot))
                    {
                        baseRoot = Environment.CurrentDirectory;
                    }
                    string imagesRoot = Path.Combine(baseRoot, "Images");

                    var now = DateTime.Now;
                    string saveDir = Path.Combine(
                        imagesRoot,
                        now.ToString("yyyy"),        // 2025
                        now.ToString("MM_yyyy"),     // 09_2025
                        now.ToString("dd_MM_yyyy"),  // 03_09_2025
                        "graphic");
                    Directory.CreateDirectory(saveDir);

                    int nextIdx = GetNextImageIndex(saveDir);
                    string fileName = $"cam_{cameraIndex + 1}_{now:HH_mm_ss_fff}_{nextIdx}.jpg";
                    string filePath = Path.Combine(saveDir, fileName);

                    annotated.Save(filePath, ImageFormat.Jpeg);
                    FileLogger.Log($"Saved detection image (cam {cameraIndex + 1}): {filePath}");

                    // Also log to database for tracking
                    try
                    {
                        var labels = detections
                            .Where(d => d != null && !string.IsNullOrWhiteSpace(d.label))
                            .Select(d => d.label?.Trim() ?? "")
                            .ToList();

                        // Map labels to only FIRE or SMOKE
                        bool hasFire = labels.Any(l => l.Equals("fire", StringComparison.OrdinalIgnoreCase) || l.Equals("flame", StringComparison.OrdinalIgnoreCase) || l.Contains("fire", StringComparison.OrdinalIgnoreCase) || l.Contains("flame", StringComparison.OrdinalIgnoreCase));
                        bool hasSmoke = labels.Any(l => l.Equals("smoke", StringComparison.OrdinalIgnoreCase) || l.Contains("smoke", StringComparison.OrdinalIgnoreCase));
                        string eventText = hasFire ? "FIRE" : (hasSmoke ? "SMOKE" : "FIRE");

                        AddCameraLogData(cameraIndex + 1, DateTime.Now, eventText, filePath);

                        // Tự động refresh bảng log sau khi có detection mới
                        try { UpdateCameraLogInvoke(this); } catch { }
                    }
                    catch (Exception exAddLog)
                    {
                        FileLogger.LogException(exAddLog, "SaveDetectionImage -> AddCameraLogData");
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "SaveDetectionImage");
            }
        }

        private void AddCameraLogData(int cameraNumber, DateTime time, string eventText, string imagePath)
        {
            try
            {
                string connStr = null;
                try { connStr = ClassSystemConfig.Ins?.m_ClsCommon?.connectionString; } catch { connStr = null; }
                if (string.IsNullOrWhiteSpace(connStr))
                {
                    FileLogger.Log("AddCameraLogData: Missing connection string");
                    return;
                }

                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "INSERT INTO camera_log (`Camera`, `Time`, `Event`, `image_Path`) VALUES (@cam, @time, @event, @path)";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                    {
                        string camName = $"CAM{cameraNumber}";
                        cmd.Parameters.AddWithValue("@cam", camName);
                        cmd.Parameters.AddWithValue("@time", time);
                        cmd.Parameters.AddWithValue("@event", string.IsNullOrWhiteSpace(eventText) ? "FIRE" : eventText);
                        cmd.Parameters.AddWithValue("@path", imagePath ?? string.Empty);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "AddCameraLogData");
            }
        }

        private int GetNextImageIndex(string directory)
        {
            try
            {
                if (!Directory.Exists(directory)) return 1;
                int maxIdx = 0;

                foreach (var file in Directory.GetFiles(directory, "*.jpg"))
                {
                    var name = Path.GetFileNameWithoutExtension(file);

                    // Pattern 1: image_<idx>.jpg
                    if (name.StartsWith("image_", StringComparison.OrdinalIgnoreCase))
                    {
                        if (int.TryParse(name.Substring(6), out int idx1))
                            if (idx1 > maxIdx) maxIdx = idx1;
                        continue;
                    }

                    // Pattern 2: cam_<camIdx>_<HH_mm_ss_fff>_<idx>.jpg
                    if (name.StartsWith("cam_", StringComparison.OrdinalIgnoreCase))
                    {
                        var parts = name.Split('_');
                        if (parts.Length >= 5)
                        {
                            // last part should be index
                            if (int.TryParse(parts[parts.Length - 1], out int idx2))
                                if (idx2 > maxIdx) maxIdx = idx2;
                        }
                        continue;
                    }
                }

                return maxIdx + 1;
            }
            catch
            {
                return 1;
            }
        }

        private static HttpClient CreateHttpClient()
        {
            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2),
                PooledConnectionIdleTimeout = TimeSpan.FromSeconds(30),
                MaxConnectionsPerServer = 16,
                AutomaticDecompression = DecompressionMethods.All,
                EnableMultipleHttp2Connections = false
            };

            var client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(2)
            };
            client.DefaultRequestVersion = HttpVersion.Version11;
            client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;
            return client;
        }

        private static Bitmap ResizeToMaxWidth(Bitmap src, int maxWidth)
        {
            if (src == null) return null;
            if (maxWidth <= 0 || src.Width <= maxWidth) return (Bitmap)src.Clone();

            double scale = (double)maxWidth / src.Width;
            int newW = maxWidth;
            int newH = Math.Max(1, (int)Math.Round(src.Height * scale));

            var dst = new Bitmap(newW, newH);
            using (var g = Graphics.FromImage(dst))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(src, new Rectangle(0, 0, newW, newH));
            }
            return dst;
        }

        private static byte[] EncodeJpeg(Bitmap bmp, long quality)
        {
            using var ms = new MemoryStream();
            var encoder = ImageCodecInfo.GetImageEncoders().FirstOrDefault(e => e.MimeType == "image/jpeg");
            if (encoder == null)
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
            var encParams = new EncoderParameters(1);
            encParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            bmp.Save(ms, encoder, encParams);
            return ms.ToArray();
        }

        // TestDetectionOverlay removed per request

        #endregion

        #region Core Implementation Methods

        private void InitializeUI()
        {
            try
            {
                LoadDeviceConfig(false);

                ClassSystemConfig.Ins.m_CameraList.InitializeUI(this);
                ClassSystemConfig.Ins.m_FrmParamCamera.Innit(this);
                FileLogger.Log("UI initialized successfully");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "InitializeUI");
            }
        }

        private void LoadCameraList()
        {
            try
            {
                Console.WriteLine("?? Loading camera list from database...");

                ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.Clear();
                ClassSystemConfig.Ins.m_ClsFunc.GetRtspUrls(connection, ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam);

                if (ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.Count > 0)
                {
                    Console.WriteLine($"?? Found {ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.Count} RTSP URLs in database");
                    UpdateLayoutForCameraCount();
                }
                else
                {
                    Console.WriteLine("?? No RTSP URLs found in database, using default configuration");
                    AddDefaultRtspUrls();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"? Error in LoadCameraList: {ex.Message}");
                FileLogger.LogException(ex, "LoadCameraList");
                AddDefaultRtspUrls();
            }
        }

        private void AddDefaultRtspUrls()
        {
            try
            {
                ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.Clear();
                ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.AddRange(new List<string>
                {
                    "rtsp://admin:infiniq2025@10.29.98.55:554/cam/realmonitor?channel=1&subtype=1",
                    "rtsp://admin:infiniq2025@10.29.98.56:554/cam/realmonitor?channel=1&subtype=1",
                    "rtsp://admin:infiniq2025@10.29.98.57:554/cam/realmonitor?channel=1&subtype=1",
                    "rtsp://admin:infiniq2025@10.29.98.58:554/cam/realmonitor?channel=1&subtype=1",
                    "rtsp://admin:infiniq2025@10.29.98.59:554/cam/realmonitor?channel=1&subtype=1",
                    "rtsp://admin:infiniq2025@10.29.98.60:554/cam/realmonitor?channel=1&subtype=1",
                    "rtsp://admin:infiniq2025@10.29.98.61:554/cam/realmonitor?channel=1&subtype=1",
                    "rtsp://admin:infiniq2025@10.29.98.62:554/cam/realmonitor?channel=1&subtype=1",
                    "rtsp://admin:infiniq2025@10.29.98.63:554/cam/realmonitor?channel=1&subtype=1",
                    "rtsp://admin:infiniq2025@10.29.98.64:554/cam/realmonitor?channel=1&subtype=1",
                    "rtsp://admin:infiniq2025@10.29.98.53:554/cam/realmonitor?channel=1&subtype=1",
                    "rtsp://admin:infiniq2025@10.29.98.54:554/cam/realmonitor?channel=1&subtype=1"
                });

                Console.WriteLine($"? Added {ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.Count} default RTSP URLs");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "AddDefaultRtspUrls");
            }
        }

        private void UpdateLayoutForCameraCount()
        {
            try
            {
                int cameraCount = ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.Count;

                if (cameraCount <= 4)
                {
                    Row = 2; Col = 2;
                }
                else if (cameraCount <= 6)
                {
                    Row = 2; Col = 3;
                }
                else if (cameraCount <= 9)
                {
                    Row = 3; Col = 3;
                }
                else if (cameraCount <= 12)
                {
                    Row = 3; Col = 4;
                }
                else if (cameraCount <= 16)
                {
                    Row = 4; Col = 4;
                }
                else
                {
                    int sqrt = (int)Math.Ceiling(Math.Sqrt(cameraCount));
                    Row = sqrt;
                    Col = sqrt;
                }

                Console.WriteLine($"?? Updated layout for {cameraCount} cameras: {Row}x{Col} grid");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "UpdateLayoutForCameraCount");
            }
        }

        public void LayoutCameraSpreadView()
        {
            try
            {
                if (panelMain.InvokeRequired)
                {
                    panelMain.Invoke(new Action(() => LayoutCameraSpreadView()));
                    return;
                }

                FileLogger.Log("Setting up dynamic camera layout...");

                panelMain.Controls.Clear();
                _pictureboxes.Clear();

                tableLayoutPanelCamera = new TableLayoutPanel();
                tableLayoutPanelCamera.Dock = DockStyle.Fill;
                tableLayoutPanelCamera.ColumnCount = 1;
                tableLayoutPanelCamera.RowCount = Row;
                tableLayoutPanelCamera.ColumnStyles.Clear();
                tableLayoutPanelCamera.RowStyles.Clear();

                panelMain.Controls.Add(tableLayoutPanelCamera);

                tableLayoutPanelDevice = new TableLayoutPanel[Row];

                for (int i = 0; i < Row; i++)
                {
                    tableLayoutPanelCamera.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / Row));

                    tableLayoutPanelDevice[i] = new TableLayoutPanel();
                    tableLayoutPanelCamera.Controls.Add(tableLayoutPanelDevice[i], 0, i);
                    tableLayoutPanelDevice[i].Dock = DockStyle.Fill;
                    tableLayoutPanelDevice[i].ColumnCount = Col;
                    tableLayoutPanelDevice[i].RowCount = 1;

                    for (int j = 0; j < Col; j++)
                    {
                        tableLayoutPanelDevice[i].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / Col));
                    }
                    tableLayoutPanelDevice[i].RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
                }

                int indexCam = 0;
                for (int row = 0; row < Row; row++)
                {
                    for (int col = 0; col < Col; col++)
                    {
                        if (indexCam < NumCameras)
                        {
                            var pictureBox = new PictureBox();
                            pictureBox.Name = $"pictureBox{indexCam + 1}";
                            pictureBox.Dock = DockStyle.Fill;
                            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBox.BackColor = Color.Black;
                            pictureBox.BorderStyle = BorderStyle.FixedSingle;

                            var label = new Label();
                            label.Text = $"CAM {indexCam + 1}\n(Press {indexCam + 1}, F{indexCam + 1}, Double-click for fullscreen)\n(ESC to exit fullscreen)";
                            label.ForeColor = Color.White;
                            label.BackColor = Color.Transparent;
                            label.Font = new Font("Arial", 9, FontStyle.Bold);
                            label.TextAlign = ContentAlignment.MiddleCenter;
                            label.Dock = DockStyle.Fill;
                            label.Anchor = AnchorStyles.None;

                            pictureBox.Controls.Add(label);

                            int cameraIndex = indexCam;
                            pictureBox.DoubleClick += (sender, e) => ToggleFullscreen(cameraIndex);
                            pictureBox.Cursor = Cursors.Hand;

                            pictureBox.Click += (sender, e) =>
                            {
                                if (!_isFullscreen)
                                {
                                    Console.WriteLine($"Camera {cameraIndex + 1} selected");
                                }
                            };

                            pictureBox.MouseEnter += (sender, e) =>
                            {
                                if (!_isFullscreen || _fullscreenCameraIndex != cameraIndex)
                                {
                                    pictureBox.BorderStyle = BorderStyle.Fixed3D;
                                }
                            };

                            pictureBox.MouseLeave += (sender, e) =>
                            {
                                if (!_isFullscreen || _fullscreenCameraIndex != cameraIndex)
                                {
                                    pictureBox.BorderStyle = BorderStyle.FixedSingle;
                                }
                            };

                            pictureBox.Paint += (sender, e) => OnPictureBoxPaint(sender, e, cameraIndex);

                            tableLayoutPanelDevice[row].Controls.Add(pictureBox, col, 0);
                            _pictureboxes.Add(pictureBox);

                            indexCam++;
                        }
                    }
                }

                // Ensure detection slots match number of picture boxes
                EnsureCameraDetectionsSize(_pictureboxes.Count);

                FileLogger.Log($"? Created dynamic layout with {_pictureboxes.Count} camera PictureBoxes");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "LayoutCameraSpreadView");
            }
        }

        private void OnPictureBoxPaint(object sender, PaintEventArgs e, int cameraIndex)
        {
            try
            {
                EnsureCameraDetectionsSize(cameraIndex + 1);
                if (_isShuttingDown || cameraIndex >= _cameraDetections.Count) return;

                var pictureBox = sender as PictureBox;
                if (pictureBox == null) return;

                // PictureBox will draw its Image by itself. We only draw overlay.

                List<Detection> detections = null;
                lock (_cameraDetections)
                {
                    if (cameraIndex < _cameraDetections.Count && _cameraDetections[cameraIndex].Count > 0)
                    {
                        detections = new List<Detection>(_cameraDetections[cameraIndex]);
                    }
                }

                if (detections?.Count > 0)
                {
                    DrawDetectionBoxes(e.Graphics, detections, pictureBox.Width, pictureBox.Height);
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, $"OnPictureBoxPaint - Camera {cameraIndex}");
            }
        }

        public void ChangeCameraSpreadView(int cameraIndex, bool spreadOut)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => ChangeCameraSpreadView(cameraIndex, spreadOut)));
                    return;
                }

                if (spreadOut && cameraIndex >= 0)
                {
                    int row = cameraIndex / Col;
                    int col = cameraIndex % Col;

                    for (int i = 0; i < tableLayoutPanelCamera.RowCount; i++)
                    {
                        if (i == row)
                        {
                            tableLayoutPanelCamera.RowStyles[i] = new RowStyle(SizeType.Percent, 100f);
                        }
                        else
                        {
                            tableLayoutPanelCamera.RowStyles[i] = new RowStyle(SizeType.Absolute, 0f);
                        }
                    }

                    for (int i = 0; i < tableLayoutPanelDevice[row].ColumnCount; i++)
                    {
                        if (i == col)
                        {
                            tableLayoutPanelDevice[row].ColumnStyles[i] = new ColumnStyle(SizeType.Percent, 100f);
                        }
                        else
                        {
                            tableLayoutPanelDevice[row].ColumnStyles[i] = new ColumnStyle(SizeType.Absolute, 0f);
                        }
                    }

                    if (cameraIndex < _pictureboxes.Count)
                    {
                        _pictureboxes[cameraIndex].BorderStyle = BorderStyle.Fixed3D;
                    }
                }
                else
                {
                    for (int i = 0; i < tableLayoutPanelCamera.RowCount; i++)
                    {
                        tableLayoutPanelCamera.RowStyles[i] = new RowStyle(SizeType.Percent, 100f / tableLayoutPanelCamera.RowCount);
                    }

                    for (int row = 0; row < Row; row++)
                    {
                        for (int i = 0; i < tableLayoutPanelDevice[row].ColumnCount; i++)
                        {
                            tableLayoutPanelDevice[row].ColumnStyles[i] = new ColumnStyle(SizeType.Percent, 100f / tableLayoutPanelDevice[row].ColumnCount);
                        }
                    }

                    foreach (var pictureBox in _pictureboxes)
                    {
                        pictureBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                }

                tableLayoutPanelCamera.PerformLayout();
                for (int i = 0; i < Row; i++)
                {
                    tableLayoutPanelDevice[i].PerformLayout();
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "ChangeCameraSpreadView");
            }
        }

        #endregion

        #region Save/Load Config
        public void SaveDeviceConfig(bool ShowMessage)
        {
            string file_name = Directory.GetCurrentDirectory() + @"\Config Setting\DeviceConfig.ini";

            if (!System.IO.File.Exists(file_name))
            {
                System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Config Setting");
            }

            try
            {
                using (StreamWriter objWriter = new StreamWriter(file_name))
                {
                    objWriter.WriteLine("[PROGRAM NAME]  " + ClassCommon.ProgramName);
                    objWriter.WriteLine("[MACHINE NAME]  " + ClassCommon.MachineName);

                    // IP CAM VISION
                    //for (int iControl = 0; iControl < ClassCommon.MaxDevice; iControl++)
                    //    objWriter.WriteLine(String.Format("[IP CAM VISION {0}]  {1}", iControl + 1, ClassSystemConfig.Ins.m_ClsCommon.m_ListIPCAM[iControl]));
                    //for (int i = 0; i < ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.Count; i++)
                    //{
                    //    // Ghi vào t?p v?i tên camera t? 1 d?n 9
                    //    objWriter.WriteLine("[RTSP CAMERA " + (i + 1) + "]  " + ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam[i]);
                    //}

                    // LOGIN
                    objWriter.WriteLine("[LOGIN USER]  " + ClassSystemConfig.Ins.m_ClsCommon.m_ListUserLogin[0]);
                    objWriter.WriteLine("[LOGIN PASS]  " + "***");

                    // SAVING
                    objWriter.WriteLine("[IS SAVE IMG_LOCAL]  " + (ClassSystemConfig.Ins.m_ClsCommon.IsSaveImgOKNG_Local ? 1 : 0));
                    objWriter.WriteLine("[IS SAVE GRAPHIC IMG LOCAL]  " + (ClassSystemConfig.Ins.m_ClsCommon.IsSaveImgGraphic_Local ? 1 : 0));
                    objWriter.WriteLine("[IS SAVE LOG LOCAL]  " + (ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local ? 1 : 0));
                    objWriter.WriteLine("[IS SAVE IMG_FTP]  " + (ClassSystemConfig.Ins.m_ClsCommon.IsSaveImgOKNG_FTP ? 1 : 0));
                    objWriter.WriteLine("[IS SAVE GRAPHIC IMG FTP]  " + (ClassSystemConfig.Ins.m_ClsCommon.IsSaveImgGraphic_FTP ? 1 : 0));
                    objWriter.WriteLine("[IS SAVE LOG FTP]  " + (ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_FTP ? 1 : 0));

                    objWriter.WriteLine("[IS DELETE AUTO]  " + (ClassSystemConfig.Ins.m_ClsCommon.m_bAutoDeleteImg ? 1 : 0));
                    objWriter.WriteLine("[PERIOD DELETE]  " + ClassSystemConfig.Ins.m_ClsCommon.m_iPeriodDelete);
                    objWriter.WriteLine("[COMMON PATH]  " + ClassSystemConfig.Ins.m_ClsCommon.m_CommonPath);

                    objWriter.WriteLine("[SHOW GRAPHIC]  " + (ClassSystemConfig.Ins.m_ClsCommon.m_bShowGraphic ? "1" : "0"));
                    objWriter.WriteLine("[SHOW ORIGIN]  " + (ClassSystemConfig.Ins.m_ClsCommon.m_bShowOrigin ? "1" : "0"));
                    objWriter.WriteLine("[SHOW PROGRESS STATUS]  " + (ClassSystemConfig.Ins.m_ClsCommon.m_bShowProgressStatus ? "1" : "0"));

                    objWriter.Close();
                }
                if (ShowMessage)
                {
                    MessageBox.Show("Saved Configurations");
                }
            }
            catch
            {
                if (ShowMessage)
                {
                    MessageBox.Show("Save Fail");
                }
            }

        }
        private void LoadDeviceConfig(bool ShowMessage)
        {
            string file_name = Directory.GetCurrentDirectory() + @"\Config Setting\DeviceConfig.ini";

            if (!System.IO.Directory.Exists(Directory.GetCurrentDirectory() + @"\Config Setting"))
            {
                System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Config Setting");
            }
            else
            {
                try
                {
                    try
                    {
                        ClassCommon.ProgramName = ClassCommon.GetConfig(file_name, "PROGRAM NAME", "UT ALIGNMENT");
                        ClassCommon.MachineName = ClassCommon.GetConfig(file_name, "MACHINE NAME", "MC #2");

                        //for (int i = 0; i < 9; i++)
                        //{
                        //    // Ghi vào t?p v?i tên camera t? 1 d?n 9
                        //    ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.Add("");
                        //    ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam[i] = ClassCommon.GetConfig(file_name, "RTSP CAMERA " + (i + 1), "");
                        //}

                        ClassSystemConfig.Ins.m_ClsCommon.m_ListUserLogin[0] = ClassCommon.GetConfig(file_name, "LOGIN USER", "Admin");
                        ClassSystemConfig.Ins.m_ClsCommon.m_ListPasswordLogin[0] = ClassCommon.GetConfig(file_name, "LOGIN PASS", "");

                        ClassSystemConfig.Ins.m_ClsCommon.IsSaveImgOKNG_Local = (ClassCommon.GetConfig(file_name, "IS SAVE IMG_LOCAL", "1").Trim() == "1") ? true : false;
                        ClassSystemConfig.Ins.m_ClsCommon.IsSaveImgGraphic_Local = (ClassCommon.GetConfig(file_name, "IS SAVE GRAPHIC IMG LOCAL", "1").Trim() == "1") ? true : false;
                        ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local = (ClassCommon.GetConfig(file_name, "IS SAVE LOG LOCAL", "1").Trim() == "1") ? true : false;
                        ClassSystemConfig.Ins.m_ClsCommon.IsSaveImgGraphic_FTP = (ClassCommon.GetConfig(file_name, "IS SAVE GRAPHIC IMG FTP", "1").Trim() == "1") ? true : false;

                        ClassSystemConfig.Ins.m_ClsCommon.m_bAutoDeleteImg = (ClassCommon.GetConfig(file_name, "IS DELETE AUTO", "1").Trim() == "1") ? true : false;
                        ClassSystemConfig.Ins.m_ClsCommon.m_iPeriodDelete = ClassSystemConfig.Ins.m_ClsCommon.ConvertStringToInt(ClassCommon.GetConfig(file_name, "PERIOD DELETE", "30"), 15);
                        ClassSystemConfig.Ins.m_ClsCommon.m_CommonPath = ClassCommon.GetConfig(file_name, "COMMON PATH", Directory.GetCurrentDirectory());

                        ClassSystemConfig.Ins.m_ClsCommon.m_bShowGraphic = (ClassCommon.GetConfig(file_name, "SHOW GRAPHIC", "1").Trim() == "1") ? true : false;
                        ClassSystemConfig.Ins.m_ClsCommon.m_bShowOrigin = false; // (ClassCommon.GetConfig(file_name, "SHOW ORIGIN", "1").Trim() == "1") ? true : false;
                        ClassSystemConfig.Ins.m_ClsCommon.m_bShowProgressStatus = (ClassCommon.GetConfig(file_name, "SHOW PROGRESS STATUS", "0").Trim() == "1") ? true : false;

                        // Always enable auto-reconnect regardless of config
                        ClassSystemConfig.Ins.m_ClsCommon.m_bAutoReconnect = true;
                        ClassSystemConfig.Ins.m_ClsCommon.IsSaveByFTP = (ClassCommon.GetConfig(file_name, "IS_FTP_SAVING", "1").Trim() == "1") ? true : false;

                        ClassSystemConfig.Ins.m_ClsCommon.m_iTimeBetween2Trigger = ClassSystemConfig.Ins.m_ClsCommon.ConvertStringToInt(ClassCommon.GetConfig(file_name, "TIMEOUT GET IMAGE", "1000"), 1000);
                        ClassSystemConfig.Ins.m_ClsCommon.m_iModeTriggerLight = ClassSystemConfig.Ins.m_ClsCommon.ConvertStringToInt(ClassCommon.GetConfig(file_name, "TRIGGER LIGHT MODE", "1"), 1);
                        ClassSystemConfig.Ins.m_ClsCommon.m_iTimeDelayTriggerCAM = ClassSystemConfig.Ins.m_ClsCommon.ConvertStringToInt(ClassCommon.GetConfig(file_name, "DELAY TRIGGER CAM MS", "100"), 100);
                        ClassSystemConfig.Ins.m_ClsCommon.m_iTimeDelaySendReady = ClassSystemConfig.Ins.m_ClsCommon.ConvertStringToInt(ClassCommon.GetConfig(file_name, "DELAY SEND READY MS", "50"), 50);
                        ClassSystemConfig.Ins.m_ClsCommon.m_iTimeoutCheckReady = ClassSystemConfig.Ins.m_ClsCommon.ConvertStringToInt(ClassCommon.GetConfig(file_name, "TIMEOUT CHECK READY MS", "2000"), 2000);
                        ClassSystemConfig.Ins.m_ClsCommon.m_iFormatSavingMode = ClassSystemConfig.Ins.m_ClsCommon.ConvertStringToInt(ClassCommon.GetConfig(file_name, "SAVING JPG MODE", "0"), 0);
                        ClassSystemConfig.Ins.m_ClsCommon.m_bCheckTimeoutReady = (ClassCommon.GetConfig(file_name, "CHECK TIMEOUT READY", "0").Trim() == "1") ? true : false;

                    }
                    catch { }

                }
                catch
                {
                    if (ShowMessage)
                    {
                        MessageBox.Show("Load Fail");
                    }
                }
            }

        }
        #endregion

        #region Fullscreen Control Methods
        private void ToggleFullscreen(int cameraIndex)
        {
            try
            {
                if (_isShuttingDown) return;

                if (cameraIndex < 0 || cameraIndex >= NumCameras)
                {
                    Console.WriteLine($"? Invalid camera index {cameraIndex + 1} (Available: 1-{NumCameras})");
                    return;
                }

                lock (_fullscreenLock)
                {
                    if (_isFullscreen)
                    {
                        if (_fullscreenCameraIndex == cameraIndex)
                        {
                            ExitFullscreen();
                        }
                        else
                        {
                            EnterFullscreen(cameraIndex);
                        }
                    }
                    else
                    {
                        EnterFullscreen(cameraIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "ToggleFullscreen");
                Console.WriteLine($"? ToggleFullscreen error: {ex.Message}");
            }
        }

        private void EnterFullscreen(int cameraIndex)
        {
            try
            {
                if (cameraIndex < 0 || cameraIndex >= NumCameras) return;

                _isFullscreen = true;
                _fullscreenCameraIndex = cameraIndex;

                ChangeCameraSpreadView(cameraIndex, true);

                Console.WriteLine($"Camera {cameraIndex + 1} expanded to fullscreen (Press ESC to exit)");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "EnterFullscreen");
                Console.WriteLine($"? EnterFullscreen error: {ex.Message}");

                _isFullscreen = false;
                _fullscreenCameraIndex = -1;
            }
        }

        public void ExitFullscreen()
        {
            try
            {
                if (!_isFullscreen)
                {
                    return;
                }

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => ExitFullscreen()));
                    return;
                }

                _isFullscreen = false;
                _fullscreenCameraIndex = -1;

                ChangeCameraSpreadView(-1, false);

                Console.WriteLine($"? Exited fullscreen - Returned to grid view");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "ExitFullscreen");
                Console.WriteLine($"? ExitFullscreen error: {ex.Message}");

                _isFullscreen = false;
                _fullscreenCameraIndex = -1;
            }
        }
        #endregion

        #region Helper and Utility Methods

        public void ShowKeyboardShortcuts()
        {
            try
            {
                string shortcuts = $@"
?? CAMERA MANAGER - KEYBOARD SHORTCUTS:

?? CAMERA CONTROLS ({NumCameras} cameras available):
• 1-9: Toggle fullscreen for Camera 1-9 (OPTIMIZED)
  - Press number to enter fullscreen
  - Press same number again to exit fullscreen
  - Press different number to switch cameras
• F1-F12: Toggle fullscreen for Camera 1-12
• Numpad 1-9: Same as number keys 1-9
• ESC: Exit fullscreen mode (ALWAYS WORKS)
• Double-click: Toggle fullscreen for selected camera

?? SYSTEM CONTROLS:
• Ctrl+R: Reload camera list from database
• Ctrl+Alt+X: Emergency shutdown
• Ctrl+Shift+K: Force kill camera workers
• Ctrl+E: Force exit fullscreen (emergency)
• H: Show this help


?? OPTIMIZED FEATURES:
• Smart camera switching - no need to exit first
• Reliable ESC key - always exits fullscreen
• Number keys 1-{Math.Min(NumCameras, 9)} for quick access
";

                MessageBox.Show(shortcuts, "?? Camera Manager - Keyboard Shortcuts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FileLogger.Log("?? Optimized keyboard shortcuts displayed to user");

                Console.WriteLine($"?? Help shown - Current config: {NumCameras} cameras in {Row}x{Col} grid");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "ShowKeyboardShortcuts");
            }
        }

        public void DebugFullscreenState()
        {
            try
            {
                Console.WriteLine("?? === FULLSCREEN DEBUG INFO ===");
                Console.WriteLine($"?? _isFullscreen: {_isFullscreen}");
                Console.WriteLine($"?? _fullscreenCameraIndex: {_fullscreenCameraIndex}");
                Console.WriteLine($"?? NumCameras: {NumCameras}");
                Console.WriteLine("?? === END DEBUG INFO ===");

                FileLogger.Log($"DEBUG - Fullscreen: {_isFullscreen}, Camera: {_fullscreenCameraIndex}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"? Debug error: {ex.Message}");
                FileLogger.LogException(ex, "DebugFullscreenState");
            }
        }

        public void ForceExitFullscreen()
        {
            try
            {
                Console.WriteLine("?? FORCE EXIT FULLSCREEN CALLED");
                FileLogger.Log("?? FORCE EXIT FULLSCREEN CALLED");

                _isFullscreen = false;
                _fullscreenCameraIndex = -1;

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => ForceExitFullscreen()));
                    return;
                }

                LayoutCameraSpreadView();

                Console.WriteLine("? FORCE EXIT FULLSCREEN COMPLETED");
                FileLogger.Log("? FORCE EXIT FULLSCREEN COMPLETED");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"? ForceExitFullscreen error: {ex.Message}");
                FileLogger.LogException(ex, "ForceExitFullscreen");
            }
        }

        public void EmergencyShutdown()
        {
            try
            {
                FileLogger.Log("?? EMERGENCY SHUTDOWN INITIATED");
                _isShuttingDown = true;

                DisplayTimer?.Stop();
                _detectionTimer?.Stop();
                _detectionCleanupTimer?.Stop();

                // reconnect timer not used in per-camera monitoring

                ForceKillAllCameraWorkers();

                // Ensure all resources are disposed and cleared
                CleanupResources();

                FileLogger.Log("? Emergency shutdown completed");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "EmergencyShutdown");
            }
        }

        private static Bitmap ResizeToSquare(Bitmap src, int size)
        {
            if (src == null) return null;
            if (size <= 0) return (Bitmap)src.Clone();

            // Maintain aspect ratio, letterbox into square canvas
            double scale = Math.Min((double)size / src.Width, (double)size / src.Height);
            int newW = Math.Max(1, (int)Math.Round(src.Width * scale));
            int newH = Math.Max(1, (int)Math.Round(src.Height * scale));
            int offsetX = (size - newW) / 2;
            int offsetY = (size - newH) / 2;

            var dst = new Bitmap(size, size);
            using (var g = Graphics.FromImage(dst))
            {
                g.Clear(Color.Black);
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(src, new Rectangle(offsetX, offsetY, newW, newH));
            }
            return dst;
        }

        // === Thresholds by STT ===
        private void LoadThresholdsByStt()
        {
            try
            {
                string connStr = null;
                try { connStr = ClassSystemConfig.Ins?.m_ClsCommon?.connectionString; } catch { connStr = null; }
                if (string.IsNullOrWhiteSpace(connStr))
                {
                    FileLogger.Log("LoadThresholdsByStt: Missing connection string");
                    return;
                }

                var temp = new Dictionary<int, (double flame, double smoke)>();
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "SELECT STT, Flame_Sensitivity, Smoke_Sensitivity FROM camera_list";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int stt = 0;
                            double flame = 0, smoke = 0;
                            try { stt = Convert.ToInt32(reader["STT"]); } catch { stt = 0; }
                            try { flame = Convert.ToDouble(reader["Flame_Sensitivity"]); } catch { flame = 0; }
                            try { smoke = Convert.ToDouble(reader["Smoke_Sensitivity"]); } catch { smoke = 0; }
                            if (stt > 0)
                            {
                                temp[stt] = (flame, smoke);
                            }
                        }
                    }
                }

                lock (_thresholdsLock)
                {
                    _thresholdsByStt.Clear();
                    foreach (var kv in temp) _thresholdsByStt[kv.Key] = kv.Value;
                }

                FileLogger.Log($"Loaded thresholds for {_thresholdsByStt.Count} cameras (by STT)");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "LoadThresholdsByStt");
            }
        }

        private (double flame, double smoke) GetThresholdsForIndex(int cameraIndex)
        {
            try
            {
                int stt = cameraIndex + 1; // mapping by STT
                lock (_thresholdsLock)
                {
                    if (_thresholdsByStt.TryGetValue(stt, out var t))
                    {
                        return t;
                    }
                }
            }
            catch { }
            return (0.0, 0.0);
        }

        private static bool IsFireLabel(string label)
        {
            if (string.IsNullOrWhiteSpace(label)) return false;
            var l = label.Trim();
            return l.Equals("fire", StringComparison.OrdinalIgnoreCase)
                   || l.Equals("flame", StringComparison.OrdinalIgnoreCase)
                   || l.IndexOf("fire", StringComparison.OrdinalIgnoreCase) >= 0
                   || l.IndexOf("flame", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool IsSmokeLabel(string label)
        {
            if (string.IsNullOrWhiteSpace(label)) return false;
            var l = label.Trim();
            return l.Equals("smoke", StringComparison.OrdinalIgnoreCase)
                   || l.IndexOf("smoke", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static List<Detection> FilterDetectionsByThreshold(List<Detection> detections, double thrFlame, double thrSmoke)
        {
            if (detections == null || detections.Count == 0) return new List<Detection>();
            var list = new List<Detection>();
            foreach (var d in detections)
            {
                if (d == null) continue;
                if (IsFireLabel(d.label))
                {
                    if (d.score >= thrFlame) list.Add(d);
                }
                else if (IsSmokeLabel(d.label))
                {
                    if (d.score >= thrSmoke) list.Add(d);
                }
            }
            return list;
        }

        private void CleanupResources()
        {
            try
            {
                // Dispose supervisors
                if (_supervisors != null && _supervisors.Count > 0)
                {
                    foreach (var sup in _supervisors)
                    {
                        try { sup?.Dispose(); } catch { }
                    }
                    _supervisors.Clear();
                }

                // Dispose MMFs
                if (_mmfs != null && _mmfs.Count > 0)
                {
                    foreach (var mmf in _mmfs)
                    {
                        try { mmf?.Dispose(); } catch { }
                    }
                    _mmfs.Clear();
                }

                // Dispose Mutexes
                if (_mutexes != null && _mutexes.Count > 0)
                {
                    foreach (var m in _mutexes)
                    {
                        try { m?.Dispose(); } catch { }
                    }
                    _mutexes.Clear();
                }

                // Dispose latest frames
                lock (_frameStoreLock)
                {
                    foreach (var kv in _latestFrames)
                    {
                        try { kv.Value?.Dispose(); } catch { }
                    }
                    _latestFrames.Clear();
                }

                // Clear last frame/reconnect state
                lock (_reconnectLock)
                {
                    _lastFrameAt.Clear();
                    _lastStallDetectedAt.Clear();
                    _lastRestartAt.Clear();
                    _restartingCameras.Clear();
                }

                // Clear picture boxes and dispose old images
                foreach (var pictureBox in _pictureboxes)
                {
                    try
                    {
                        if (pictureBox != null && !pictureBox.IsDisposed)
                        {
                            var old = pictureBox.Image;
                            pictureBox.Image = null;
                            try { old?.Dispose(); } catch { }

                            if (pictureBox.Controls.Count > 0)
                            {
                                pictureBox.Controls[0].Visible = true;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "CleanupResources");
            }
        }

        private void ForceKillAllCameraWorkers()
        {
            try
            {
                FileLogger.Log("?? EMERGENCY: Force killing all CameraWorker processes...");

                var processes = System.Diagnostics.Process.GetProcessesByName("CameraWorker");
                foreach (var process in processes)
                {
                    try
                    {
                        FileLogger.Log($"?? Force killing CameraWorker process ID: {process.Id}");
                        process.Kill();
                        process.WaitForExit(1000);
                        process.Dispose();
                    }
                    catch (Exception ex)
                    {
                        FileLogger.LogException(ex, $"ForceKill process {process.Id}");
                    }
                }

                FileLogger.Log("? Force kill completed");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "ForceKillAllCameraWorkers");
            }
        }

        private void UpdateNoSignalOverlay(int cameraIndex)
        {
            try
            {
                if (cameraIndex < 0 || cameraIndex >= _pictureboxes.Count) return;
                var pictureBox = _pictureboxes[cameraIndex];
                if (pictureBox == null || pictureBox.IsDisposed) return;

                var now = DateTime.Now;
                bool isStale = true;
                DateTime last;
                lock (_reconnectLock)
                {
                    if (_lastFrameAt.TryGetValue(cameraIndex, out last))
                    {
                        isStale = (now - last).TotalMilliseconds > NO_SIGNAL_TIMEOUT_MS;
                    }
                }

                if (isStale)
                {
                    if (pictureBox.Controls.Count > 0 && pictureBox.Controls[0] is Label lbl)
                    {
                        lbl.Text = $"CAM {cameraIndex + 1}\nNo Signal (reconnecting...)";
                        lbl.Visible = true;
                    }

                    TryRestartStalledCamera(cameraIndex, now);
                }
                else
                {
                    // On fresh signal, ensure overlay hidden (UpdatePictureBox also hides it when setting Image)
                    if (pictureBox.Controls.Count > 0)
                    {
                        pictureBox.Controls[0].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, $"UpdateNoSignalOverlay - Camera {cameraIndex}");
            }
        }

        private void TryRestartStalledCamera(int cameraIndex, DateTime now)
        {
            try
            {
                bool shouldRestart = false;
                lock (_reconnectLock)
                {
                    if (_restartingCameras.Contains(cameraIndex)) return;

                    // Require stall sustained beyond RESTART_STALL_MS
                    if (!_lastStallDetectedAt.TryGetValue(cameraIndex, out var firstSeen))
                    {
                        _lastStallDetectedAt[cameraIndex] = now;
                        return;
                    }
                    if ((now - firstSeen).TotalMilliseconds < RESTART_STALL_MS)
                    {
                        return;
                    }

                    // Cooldown between restarts
                    if (_lastRestartAt.TryGetValue(cameraIndex, out var lastRestart))
                    {
                        if ((now - lastRestart).TotalMilliseconds < RESTART_COOLDOWN_MS)
                        {
                            return;
                        }
                    }

                    shouldRestart = true;
                }

                if (shouldRestart)
                {
                    _ = Task.Run(() => RestartWorker(cameraIndex));
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, $"TryRestartStalledCamera - Camera {cameraIndex}");
            }
        }

        private void RestartWorker(int cameraIndex)
        {
            if (cameraIndex < 0) return;
            lock (_reconnectLock)
            {
                if (_restartingCameras.Contains(cameraIndex)) return;
                _restartingCameras.Add(cameraIndex);
            }

            try
            {
                FileLogger.Log($"?? RestartWorker: restarting camera {cameraIndex + 1}");

                // Try to kill the specific CameraWorker process bound to this camera's mutex/MMF
                TryKillWorkerProcess(cameraIndex);

                // Dispose old supervisor if exists
                if (cameraIndex < _supervisors.Count && _supervisors[cameraIndex] != null)
                {
                    try { _supervisors[cameraIndex]?.Dispose(); } catch { }
                    _supervisors[cameraIndex] = null;
                }

                // Build new supervisor with current RTSP config
                string cameraWorkerPath = Path.Combine(Environment.CurrentDirectory, "CameraWorker.exe");
                if (!File.Exists(cameraWorkerPath))
                {
                    FileLogger.Log($"RestartWorker: CameraWorker.exe not found at {cameraWorkerPath}");
                    return;
                }

                if (ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam == null || cameraIndex >= ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.Count)
                {
                    FileLogger.Log($"RestartWorker: invalid RTSP index {cameraIndex}");
                    return;
                }

                string rtspUrl = ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam[cameraIndex];
                string mmfName = $"Cam_{cameraIndex}_MMF";
                string mutexName = $"Global\\Cam_{cameraIndex}_Mutex";
                string sttArg = (cameraIndex + 1).ToString();
                string connStrArg = ClassSystemConfig.Ins?.m_ClsCommon?.connectionString ?? string.Empty;
                string arguments = $"\"{rtspUrl}\" {mmfName} {mutexName} {sttArg} \"{connStrArg}\"";

                var supervisor = new ProcessSupervisor(
                    loggerFactory: NullLoggerFactory.Instance,
                    processRunType: ProcessRunType.NonTerminating,
                    processPath: cameraWorkerPath,
                    arguments: arguments,
                    workingDirectory: Environment.CurrentDirectory
                );

                // Ensure list capacity
                while (_supervisors.Count <= cameraIndex) _supervisors.Add(null);
                _supervisors[cameraIndex] = supervisor;
                supervisor.Start();

                // Reset timers for this camera
                lock (_reconnectLock)
                {
                    _lastRestartAt[cameraIndex] = DateTime.Now;
                    _lastStallDetectedAt.Remove(cameraIndex);
                    _lastFrameAt.Remove(cameraIndex);
                }

                FileLogger.Log($"? RestartWorker: camera {cameraIndex + 1} restarted");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, $"RestartWorker - Camera {cameraIndex}");
            }
            finally
            {
                lock (_reconnectLock)
                {
                    _restartingCameras.Remove(cameraIndex);
                }
            }
        }

        private void TryKillWorkerProcess(int cameraIndex)
        {
            try
            {
                string mmfName = $"Cam_{cameraIndex}_MMF";
                string mutexName = $"Global\\Cam_{cameraIndex}_Mutex";
                var searcher = new ManagementObjectSearcher("SELECT ProcessId, CommandLine FROM Win32_Process WHERE Name='CameraWorker.exe'");
                foreach (ManagementObject proc in searcher.Get())
                {
                    string cmd = proc["CommandLine"]?.ToString() ?? string.Empty;
                    if (cmd.Contains(mmfName) || cmd.Contains(mutexName))
                    {
                        try
                        {
                            int pid = Convert.ToInt32(proc["ProcessId"]);
                            var p = System.Diagnostics.Process.GetProcessById(pid);
                            FileLogger.Log($"RestartWorker: killing old worker PID {pid} for camera {cameraIndex + 1}");
                            p.Kill();
                            p.WaitForExit(1000);
                            p.Dispose();
                        }
                        catch (Exception ex)
                        {
                            FileLogger.LogException(ex, $"TryKillWorkerProcess - Camera {cameraIndex}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, $"TryKillWorkerProcess(root) - Camera {cameraIndex}");
            }
        }
        // === Auto Reconnect (per-camera) implemented via UpdateNoSignalOverlay/TryRestartStalledCamera/RestartWorker ===

        public void ReloadCameraList()
        {
            try
            {
                Console.WriteLine("?? Reloading camera list from database...");
                LoadCameraList();
                // Reload thresholds as well when camera list changes
                LoadThresholdsByStt();

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => LayoutCameraSpreadView()));
                }
                else
                {
                    LayoutCameraSpreadView();
                }

                Console.WriteLine("? Camera list reloaded successfully");
                FileLogger.Log($"Camera list reloaded: {NumCameras} cameras found");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"? Error reloading camera list: {ex.Message}");
                FileLogger.LogException(ex, "ReloadCameraList");
            }
        }

        public void UpdateCameraLogInvoke(System.Windows.Forms.Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate
                {
                    UpdateCameraLog();
                }));
            }
            else
            {
                UpdateCameraLog();
            }
        }

        public void UpdateCameraLog()
        {
            try
            {
                connection.Open();
                string query = "SELECT Camera, Time, Event FROM camera_log ORDER BY STT DESC";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dgviewLog.DataSource = dataTable;
                ClassSystemConfig.Ins.m_ClsFunc.FormatDataGridView(dgviewLog);

                dgviewLog.Columns["Camera"].HeaderText = "Camera";
                dgviewLog.Columns["Camera"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgviewLog.Columns["Camera"].Width = 80;
                dgviewLog.Columns["Time"].HeaderText = "Time";
                dgviewLog.Columns["Time"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgviewLog.Columns["Event"].HeaderText = "Event";
                dgviewLog.Columns["Event"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgviewLog.Columns["Event"].Width = 80;
            }
            catch (Exception ex)
            {
                MessageBox.Show("L?i k?t n?i: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        // Process exit handlers
        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            FileLogger.Log("?? ProcessExit event triggered - Emergency cleanup");
            EmergencyShutdown();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            FileLogger.LogException((Exception)e.ExceptionObject, "UnhandledException");
            FileLogger.Log("?? Unhandled exception - Emergency cleanup");
            EmergencyShutdown();
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            FileLogger.Log("?? ApplicationExit event triggered - Emergency cleanup");
            EmergencyShutdown();
        }

        private void SystemEvents_SessionEnding(object sender, Microsoft.Win32.SessionEndingEventArgs e)
        {
            FileLogger.Log($"?? Windows session ending: {e.Reason} - Emergency cleanup");
            EmergencyShutdown();
        }

        // Camera system methods
        private void StartCameraSystem()
        {
            try
            {
                // Cho phép chạy lại sau khi đã Stop
                _isShuttingDown = false;
                string cameraWorkerPath = Path.Combine(Environment.CurrentDirectory, "CameraWorker.exe");
                if (!File.Exists(cameraWorkerPath))
                {
                    string errorMsg = $"? CameraWorker.exe not found at: {cameraWorkerPath}";
                    FileLogger.Log(errorMsg);
                    Console.WriteLine(errorMsg);
                    return;
                }

                if (ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.Count == 0)
                {
                    LoadCameraList();
                }

                int actualCameraCount = ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.Count;
                FileLogger.Log($"?? Starting {actualCameraCount} camera workers");

                for (int i = 0; i < actualCameraCount; i++)
                {
                    string rtspUrl = ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam[i];
                    string mmfName = $"Cam_{i}_MMF";
                    string mutexName = $"Global\\Cam_{i}_Mutex";

                    try
                    {
                        var mmf = MemoryMappedFile.CreateOrOpen(mmfName, MaxFrameSize, MemoryMappedFileAccess.ReadWrite);
                        _mmfs.Add(mmf);

                        using (var accessor = mmf.CreateViewAccessor())
                        {
                            accessor.Write(0, 0);
                            accessor.Write(4, 0);
                        }

                        _mutexes.Add(new Mutex(false, mutexName));

                        string sttArg = (i + 1).ToString();
                        string connStrArg = ClassSystemConfig.Ins?.m_ClsCommon?.connectionString ?? string.Empty;
                        string arguments = $"\"{rtspUrl}\" {mmfName} {mutexName} {sttArg} \"{connStrArg}\"";
                        var supervisor = new ProcessSupervisor(
                            loggerFactory: NullLoggerFactory.Instance,
                            processRunType: ProcessRunType.NonTerminating,
                            processPath: cameraWorkerPath,
                            arguments: arguments,
                            workingDirectory: Environment.CurrentDirectory
                        );

                        _supervisors.Add(supervisor);
                        supervisor.Start();

                        FileLogger.Log($"?? Camera {i + 1} worker started");
                        Thread.Sleep(200);
                    }
                    catch (Exception ex)
                    {
                        FileLogger.LogException(ex, $"StartCameraSystem - Camera {i + 1}");
                        continue;
                    }
                }

                DisplayTimer.Interval = 5;
                DisplayTimer.Start();

                // Đảm bảo bật lại các timer detection sau khi đã Stop
                try
                {
                    _detectionTimer.Interval = DETECTION_TIMER_INTERVAL_MS;
                    _detectionTimer.Start();
                    _detectionCleanupTimer.Interval = 100;
                    _detectionCleanupTimer.Start();
                }
                catch (Exception exTimer)
                {
                    FileLogger.LogException(exTimer, "StartCameraSystem -> restart detection timers");
                }

                // Per-camera reconnect is handled in DisplayTimer via UpdateNoSignalOverlay

                FileLogger.Log($"? Camera system started with {_supervisors.Count}/{actualCameraCount} workers");
            }
            catch (Exception ex)
            {
                FileLogger.LogException(ex, "StartCameraSystem");
            }
        }

        private void UpdatePictureBox(int index)
        {
            if (_isShuttingDown || index >= _pictureboxes.Count) return;

            try
            {
                var mutex = _mutexes[index];
                var mmf = _mmfs[index];
                var pictureBox = _pictureboxes[index];

                if (mutex?.WaitOne(0) == true)
                {
                    try
                    {
                        using (var accessor = mmf.CreateViewAccessor())
                        {
                            int width = accessor.ReadInt32(0);
                            int height = accessor.ReadInt32(4);

                            if (width > 0 && height > 0 && width <= MaxFrameWidth && height <= MaxFrameHeight)
                            {
                                long frameSize = (long)width * height * 3;
                                byte[] frameData = new byte[frameSize];
                                accessor.ReadArray(8, frameData, 0, frameData.Length);

                                bool hasData = false;
                                for (int i = 0; i < Math.Min(1000, frameData.Length); i++)
                                {
                                    if (frameData[i] != 0)
                                    {
                                        hasData = true;
                                        break;
                                    }
                                }

                                if (hasData)
                                {
                                    var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                                    BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),
                                        ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                                    try
                                    {
                                        int stride = bmpData.Stride;
                                        IntPtr scan0 = bmpData.Scan0;
                                        int srcLineSize = width * 3;

                                        for (int y = 0; y < height; y++)
                                        {
                                            int srcOffset = y * srcLineSize;
                                            IntPtr dstLineStart = scan0 + (y * stride);
                                            Marshal.Copy(frameData, srcOffset, dstLineStart, Math.Min(srcLineSize, stride));
                                        }
                                    }
                                    finally
                                    {
                                        bmp.UnlockBits(bmpData);
                                    }

                                    if (!_isShuttingDown && !pictureBox.IsDisposed)
                                    {
                                        if (pictureBox.Controls.Count > 0)
                                        {
                                            pictureBox.Controls[0].Visible = false;
                                        }

                                        var oldImage = pictureBox.Image;
                                        pictureBox.Image = bmp;
                                        oldImage?.Dispose();

                                        // 👉 Thêm đoạn này để DetectionTimer có dữ liệu
                                        lock (_frameStoreLock)
                                        {
                                            if (_latestFrames.ContainsKey(index))
                                            {
                                                _latestFrames[index]?.Dispose();
                                                _latestFrames[index] = (Bitmap)bmp.Clone();
                                            }
                                            else
                                            {
                                                _latestFrames.Add(index, (Bitmap)bmp.Clone());
                                            }
                                        }

                                        // Track last frame time for reconnect monitoring
                                        lock (_reconnectLock)
                                        {
                                            _lastFrameAt[index] = DateTime.Now;
                                        }

                                        pictureBox.Invalidate();
                                    }
                                    else
                                    {
                                        bmp.Dispose();
                                    }
                                }
                            }
                        }
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }
            catch (Exception ex)
            {
                if (!_isShuttingDown)
                {
                    FileLogger.LogException(ex, $"updating picture box {index}");
                }
            }
        }

        #endregion

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            ChangeButtonColorClick(sender);
            if (ClassSystemConfig.Ins.m_FrmConfigMessage == null || ClassSystemConfig.Ins.m_FrmConfigMessage.IsDisposed)
            {
                ClassSystemConfig.Ins.m_FrmConfigMessage = new FormConfigMessage();
            }
            ClassSystemConfig.Ins.m_FrmConfigMessage.Show();
            ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.HANDLER_CLICKED,
                                                    "Clicked Config Message View",
                                                    ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
        }

        private void btnLogView_Click(object sender, EventArgs e)
        {
            ChangeButtonColorClick(sender);
            ClassSystemConfig.Ins.m_FrmLogView.Show();
            ClassSystemConfig.Ins.m_FrmLogView.ShowOnScreen();
            ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.HANDLER_CLICKED,
                                                    "Clicked Log View",
                                                    ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
        }
    }
}

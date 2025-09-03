using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
using Org.BouncyCastle.Ocsp;

namespace CameraManager
{
    public class ClassFunction
    {
        //#region Save Image
        //public void CognexSaveImage(string file_path, ICogImage cogImage, bool is_save_log = true)
        //{
        //    //try
        //    //{
        //    //    CogImageFileTool vpFileTool = new CogImageFileTool();
        //    //    vpFileTool.InputImage = cogImage;
        //    //    vpFileTool.Operator.Open(file_path, CogImageFileModeConstants.Write);
        //    //    vpFileTool.Run();

        //    //    if (is_save_log)
        //    //    SaveLog("Saved Raw Image " + file_path, ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local);
                
        //    //}
        //    //catch (System.Exception ex)
        //    //{
        //    //    if (is_save_log)
        //    //    SaveLog("Save Raw Image Fail (" + ex.Message + ")", ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local);
        //    //}
        //}
        //public ICogImage CognexOpenImage(string file_path)
        //{
        //    ICogImage cogImage = null;
        //    try
        //    {

        //        //CogImageFileTool vpFileTool = new CogImageFileTool();
        //        //vpFileTool.Operator.Open(file_path, CogImageFileModeConstants.Read);
        //        //vpFileTool.Run();

        //        //cogImage = vpFileTool.OutputImage;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        cogImage = null;
        //    }
        //    return cogImage;
        //}
        //public void CognexDeleteImage(string file_path)
        //{
        //    try
        //    {
                
        //    }
        //    catch (System.Exception ex)
        //    {
                
        //    }
        //}
        //public void DeleteFile(string path)
        //{
        //    try
        //    {
        //        if (File.Exists(path))
        //            File.Delete(path);
        //    }
        //    catch { }
        //}
        //public void SaveImageRaw(string name, Mat imageRaw, Bitmap graphicImg, ref string img_graphic_path)
        //{
        //    try
        //    {
        //        // Kiểm tra tính hợp lệ của Mat trước khi chuyển đổi
        //        if (imageRaw == null || imageRaw.IsDisposed || imageRaw.Empty())
        //        {
        //            ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.EXCEPTION,
        //                                    "EX -> SaveImageRaw: imageRaw is null, disposed, or empty",
        //                                    ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
        //            return;
        //        }

        //        // Kiểm tra thêm các thuộc tính cơ bản của Mat
        //        if (imageRaw.Rows <= 0 || imageRaw.Cols <= 0)
        //        {
        //            ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.EXCEPTION,
        //                                    "EX -> SaveImageRaw: imageRaw has invalid dimensions",
        //                                    ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
        //            return;
        //        }

        //        Bitmap img_Raw = null;
        //        try
        //        {
        //            img_Raw = imageRaw.ToBitmap();
        //        }
        //        catch (Exception ex)
        //        {
        //            ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.EXCEPTION,
        //                                    "EX -> ToBitmap conversion failed: " + ex.Message,
        //                                    ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
        //            return;
        //        }

        //        if (img_Raw == null)
        //        {
        //            ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.EXCEPTION,
        //                                    "EX -> ToBitmap returned null",
        //                                    ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
        //            return;
        //        }

        //        string _imagePath = ClassSystemConfig.Ins.m_ClsCommon.m_CommonPath + @"\Images\" + DateTime.Now.ToString("yyyy_MM") + @"\" + DateTime.Now.ToString("yyyy_MM_dd") + @"\";
        //        string _imagePathGraphic = ClassSystemConfig.Ins.m_ClsCommon.m_CommonPath + @"\Images\" + DateTime.Now.ToString("yyyy_MM") + @"\" + DateTime.Now.ToString("yyyy_MM_dd") + @"\"; ;

        //        _imagePath += "Origin";
        //        _imagePathGraphic += "Graphic";

        //        string strFullPath = "";
        //        try
        //        {
        //            if (!Directory.Exists(_imagePath))
        //            {
        //                Directory.CreateDirectory(_imagePath);
        //            }

        //            if (ClassSystemConfig.Ins.m_ClsCommon.m_iFormatSavingMode == 1)
        //            {
        //                strFullPath = _imagePath + "\\" + name + ".jpg";
        //                img_Raw.Save(strFullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        //            }
        //            else if (ClassSystemConfig.Ins.m_ClsCommon.m_iFormatSavingMode == 2)
        //            {
        //                strFullPath = _imagePath + "\\" + name + ".png";
        //                img_Raw.Save(strFullPath, System.Drawing.Imaging.ImageFormat.Png);
        //            }
        //            else
        //            {
        //                strFullPath = _imagePath + "\\" + name + ".jpg";
        //                img_Raw.Save(strFullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.EXCEPTION,
        //                                    "EX -> Save Raw Image Error " + ex.Message,
        //                                    ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
        //        }
        //        finally
        //        {
        //            // Dispose Bitmap để giải phóng bộ nhớ
        //            img_Raw?.Dispose();
        //        }

        //        try
        //        {
        //            if ((ClassSystemConfig.Ins.m_ClsCommon.IsSaveImgGraphic_Local) && graphicImg != null)
        //            {
        //                if (!Directory.Exists(_imagePathGraphic))
        //                {
        //                    Directory.CreateDirectory(_imagePathGraphic);
        //                }

        //                img_graphic_path = _imagePathGraphic + "\\" + name + ".jpg";
        //                graphicImg.Save(img_graphic_path, System.Drawing.Imaging.ImageFormat.Jpeg);

        //                SaveLog("Saved Graphic Image", ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.EXCEPTION,
        //                                    "EX -> Save Graphic Image Error " + ex.Message,
        //                                    ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.EXCEPTION,
        //                                "EX -> SaveImageRaw unexpected error: " + ex.Message,
        //                                ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
        //    }
        //}
        //public string  SaveImage(string strOKNG, string name, Bitmap image, bool IsOriginImg = true)
        //{
        //    string strPathRet = "";
        //    string strFullPath = "";
        //    try
        //    {
        //        string _imagePath = ClassSystemConfig.Ins.m_ClsCommon.m_CommonPath + @"\Images\" + DateTime.Now.ToString("yyyy_MM") + @"\" + DateTime.Now.ToString("yyyy_MM_dd") + @"\";
        //        string _imagePathGraphic = "";
        //        strPathRet = DateTime.Now.ToString("yyyy_MM_dd") + @"\";

        //        if (strOKNG != null && strOKNG.TrimEnd() != "")
        //        {
        //            _imagePath += strOKNG;
        //            strPathRet += strOKNG;
        //        }
        //        _imagePathGraphic = _imagePath + "\\Graphic";

                
        //        try
        //        {
        //            if (IsOriginImg && ClassSystemConfig.Ins.m_ClsCommon.IsSaveImgOKNG_Local && image != null)
        //            {
        //                if (!Directory.Exists(_imagePath))
        //                {
        //                    Directory.CreateDirectory(_imagePath);
        //                }

        //                if (ClassSystemConfig.Ins.m_ClsCommon.m_iFormatSavingMode == 1)
        //                {
        //                    strFullPath = _imagePath + "\\" + name + ".jpg";
        //                    strPathRet += "\\" + name + ".jpg";
        //                    image.Save(strFullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        //                }
        //                else
        //                if (ClassSystemConfig.Ins.m_ClsCommon.m_iFormatSavingMode == 2)
        //                {
        //                    strFullPath = _imagePath + "\\" + name + ".png";
        //                    strPathRet += "\\" + name + ".png";
        //                    image.Save(strFullPath, System.Drawing.Imaging.ImageFormat.Png);
        //                }
        //                else
        //                {
        //                    strFullPath = _imagePath + "\\" + name + ".bmp";
        //                    strPathRet += "\\" + name + ".bmp";
        //                    image.Save(strFullPath, System.Drawing.Imaging.ImageFormat.Bmp);
        //                }
        //                SaveLog("Saved Origin Image " + strFullPath, ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            SaveLog("Save Origin Image Fail " + strFullPath + "(" + ex.Message + ")", ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local);
        //        }

        //        if (IsOriginImg == false && ClassSystemConfig.Ins.m_ClsCommon.IsSaveImgGraphic_Local && image != null)
        //        {
        //            if (!Directory.Exists(_imagePathGraphic))
        //            {
        //                Directory.CreateDirectory(_imagePathGraphic);
        //            }

        //            strFullPath = _imagePathGraphic + "\\" + name + ".jpg";
        //            image.Save(strFullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        //            SaveLog("Saved Graphic Image " + strFullPath, ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //    return strPathRet;
        //}
        //#endregion

        #region Save Log
        Mutex mutex_log = new Mutex();
        public void SaveLog(string strLog, bool isSaveLog)
        {
            mutex_log.WaitOne();
            if (isSaveLog)
            {
                try
                {
                    string _logPath = ClassSystemConfig.Ins.m_ClsCommon.m_CommonPath + @"\Log\" + DateTime.Now.ToString("yyyy_MM") + @"\" + DateTime.Now.ToString("yyyy_MM_dd") + @"\";
                    if (ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local == true)
                    {
                        
                        if (!Directory.Exists(_logPath))
                        {
                            Directory.CreateDirectory(_logPath);
                        }
                        _logPath += @"\PROGRAM.txt";

                        using (StreamWriter objWriter = File.AppendText(_logPath))
                        {
                            objWriter.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " + strLog);
                            objWriter.Close();
                        }
                        
                    }
                }
                catch { }
            }

            //try
            //{
            //    ClassSystemConfig.Ins.m_UserLog.SetLogOnline(strLog);
            //}
            //catch { }
            mutex_log.ReleaseMutex();
        }
        public enum SAVING_LOG_TYPE
        {
            PROGRAM,
            DATA,
            PLC,
            HANDLER_CLICKED,
            EXCEPTION,
            DATA_RECEIVE
        }
        Mutex mutex = new Mutex();
        public void SaveLog(SAVING_LOG_TYPE log_type, string strLog, bool isSaveLog, bool isUpdateLog)
        {
            if (isSaveLog)
            {
                mutex.WaitOne();
                try
                {
                    string _logPath = ClassSystemConfig.Ins.m_ClsCommon.m_CommonPath + @"\Log\" + DateTime.Now.ToString("yyyy_MM") + @"\" + DateTime.Now.ToString("yyyy_MM_dd") + @"\";
                    if (ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local == true)
                    {
                        if (!Directory.Exists(_logPath))
                        {
                            Directory.CreateDirectory(_logPath);
                        }

                        _logPath += log_type.ToString() + ".txt";

                        using (StreamWriter objWriter = File.AppendText(_logPath))
                        {
                            objWriter.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " + strLog);
                            objWriter.Close();
                        }

                    }
                }
                catch { }
                mutex.ReleaseMutex();
            }

            //if (isUpdateLog)
            //    ClassSystemConfig.Ins.m_UserLog.SetLogOnline(strLog);
        }
        public void SaveLog(int IndexCam, string strLog, bool isSaveLog, bool isUpdateLog)
        {
            if (isSaveLog)
            {
                mutex.WaitOne();
                try
                {
                    string _logPath = ClassSystemConfig.Ins.m_ClsCommon.m_CommonPath + @"\Log\" + DateTime.Now.ToString("yyyy_MM") + @"\" + DateTime.Now.ToString("yyyy_MM_dd") + @"\";
                    if (ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local == true)
                    {
                        if (!Directory.Exists(_logPath))
                        {
                            Directory.CreateDirectory(_logPath);
                        }

                        if (IndexCam >= 0 && IndexCam < ClassCommon.MaxDevice && ClassSystemConfig.Ins.m_ClsCommon.m_ListNameControl[IndexCam].Trim().Length > 0)
                            _logPath += ClassSystemConfig.Ins.m_ClsCommon.m_ListNameControl[IndexCam] + ".txt";
                        else
                            _logPath += "CAM" + (IndexCam + 1) + ".txt";

                        using (StreamWriter objWriter = File.AppendText(_logPath))
                        {
                            objWriter.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " + strLog);
                            objWriter.Close();
                        }

                    }
                }
                catch { }
                mutex.ReleaseMutex();
            }

            //if (isUpdateLog)
            //    ClassSystemConfig.Ins.m_UserLog.SetLogOnline(strLog);
        }
        public void SaveCSVLog(int iCamIndex, DateTime dateSaving, string runMode, string model, string logHeader, string logContent)
        {
            if (ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local)
            {
                string logPath = ClassSystemConfig.Ins.m_ClsCommon.m_CommonPath + @"\Log\" + DateTime.Now.ToString("yyyy_MM") + @"\"  + DateTime.Now.ToString("yyyy_MM_dd") + "\\" + model;
                string fileNamePath = logPath + "\\" + string.Format("{0}.csv", ClassSystemConfig.Ins.m_ClsCommon.m_ListNameControl[iCamIndex]);

                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }


                string strLog = dateSaving.ToString("MM-dd-yyyy, HH:mm:ss.fff") + ",";
                strLog += iCamIndex.ToString().ToUpper() + ",";
                strLog += runMode + ",";
                strLog += model + ",";
                strLog += logContent + ",";

                try
                {
                    if (File.Exists(fileNamePath))
                    {
                        FileInfo myFile = new FileInfo(fileNamePath);
                        myFile.IsReadOnly = false;
                        
                    }
                    
                    using (StreamWriter objWriter = new StreamWriter(fileNamePath, true))
                    {
                        if (new FileInfo(fileNamePath).Length == 0)
                        {
                            string strHeader = "DATE, TIME, CAMERA , RUN MODE, MODEL , " + logHeader;

                            objWriter.WriteLine(strHeader);
                        }


                        objWriter.WriteLine(strLog);
                        objWriter.Flush();

                        objWriter.Close();
                    }
                }
                catch (Exception ex)
                {
                    //ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.EXCEPTION,
                    //                                "EX -> SaveCSVLog Error " + ex.Message,
                    //                                ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
                }

            }
        }
        #endregion

        #region Function
        public void ControlTextInvoke(System.Windows.Forms.Control control, string text)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate
                {
                    control.Text = text;
                }));
            }
            else
            {
                control.Text = text;
            }
        }
        public byte[] ImageToByteArray(dynamic img)
        {
            // Lấy thông tin mảng ảnh và chuyển sang byte array
            var shape = img.shape;
            int width = shape[1];
            int height = shape[0];
            int channels = shape[2];

            byte[] byteArray = img.tobytes();
            return byteArray;
        }
        //static byte[] ConvertMatToByteArray(Mat mat)
        //{
        //    return mat.ToBytes(".jpg");
        //}
        public void ConvertBitmapToNumpy()
        {
            // Tải bitmap từ file
            Bitmap bitmap = new Bitmap("image.png");

            // Chuyển đổi Bitmap thành mảng byte
            int width = bitmap.Width;
            int height = bitmap.Height;
            byte[] pixelData;

            // Khóa bitmap để đọc pixel
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            try
            {
                int stride = bitmapData.Stride;
                pixelData = new byte[stride * height];
                Marshal.Copy(bitmapData.Scan0, pixelData, 0, pixelData.Length);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }

            //// Gửi dữ liệu tới Python
            //using (Py.GIL()) // Đảm bảo quản lý GIL khi gọi Python từ C#
            //{
            //    dynamic np = Py.Import("numpy");

            //    // Chuyển đổi mảng byte thành NumPy array
            //    dynamic numpyArray = np.array(pixelData);
            //    numpyArray = numpyArray.reshape(height, width, 3); // Reshape thành mảng 3D (height, width, channels)

            //    Console.WriteLine($"NumPy array shape: {numpyArray.shape}");
            //}
        }
        //public string ImageToBase64(Mat frame)
        //{
        //    byte[] imageBytes = ConvertMatToByteArray(frame);
        //    string base64String = Convert.ToBase64String(imageBytes);
        //    return ValidateBase64(base64String);
        //}
        public static string ValidateBase64(string base64String)
        {
            base64String = base64String.Trim();

            if (base64String.Length % 4 != 0)
            {
                int padding = 4 - (base64String.Length % 4);
                base64String = base64String.PadRight(base64String.Length + padding, '=');
            }
            return base64String;
        }

        #endregion

        #region DataBase
        public void CreateTable(MySqlConnection connection)
        {
            string createTableQuery = @"
            CREATE TABLE `test`.`camera_list` (
              `STT` INT NOT NULL AUTO_INCREMENT,
              `Name` VARCHAR(45) NULL,
              `RTSP URL` VARCHAR(45) NULL,
              `Type` INT NULL,
              `IP_Camera` VARCHAR(45) NULL,
              `UserID` VARCHAR(45) NULL,
              `Password` VARCHAR(45) NULL,
              PRIMARY KEY (`STT`)
            );";

            using (MySqlCommand command = new MySqlCommand(createTableQuery, connection))
            {
                // Thực thi câu lệnh CREATE TABLE
                command.ExecuteNonQuery();
                Console.WriteLine("Table 'camera_list' created successfully.");
            }
        }

        public void ChangeColorRawDataGridView(DataGridView dtView)
        {
            if (dtView.InvokeRequired)
            {
                dtView.Invoke(new MethodInvoker(delegate
                {
                    foreach (DataGridViewRow row in dtView.Rows)
                        if (row.Index % 2 == 0)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = dtView.BackgroundColor;
                            //row.DefaultCellStyle.BackColor = Color.LightGray;
                        }
                }));
            }
            else
            {
                foreach (DataGridViewRow row in dtView.Rows)
                    if (row.Index % 2 == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = dtView.BackgroundColor;
                        //row.DefaultCellStyle.BackColor = Color.LightGray;
                    }
            }
        }

        public void GetRtspUrls(MySqlConnection connection, List<string> m_ListRtspCam)
        {
            string query = "SELECT `RTSP_URL` FROM `camera_list`";  // Truy vấn cột RTSP_URL từ bảng camera_list

            try
            {
                // Mở kết nối đến cơ sở dữ liệu
                connection.Open();

                // Tạo đối tượng MySqlCommand để thực thi câu lệnh truy vấn
                MySqlCommand command = new MySqlCommand(query, connection);

                // Thực thi câu lệnh và lấy dữ liệu từ cơ sở dữ liệu
                MySqlDataReader reader = command.ExecuteReader();

                // Duyệt qua kết quả và thêm từng RTSP URL vào danh sách
                while (reader.Read())
                {
                    // Thêm giá trị của cột RTSP_URL vào m_ListRtspCam
                    m_ListRtspCam.Add(reader.GetString("RTSP_URL"));
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void FormatDataGridView(DataGridView dgv)
        {
            // Căn chỉnh chung
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 73, 94);
            dgv.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv.BackgroundColor = Color.White;

            // Header
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Căn giữa và font cho ô dữ liệu
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Căn giữa tiêu đề dòng
            dgv.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Tùy chọn khác
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true; // Nếu bạn không muốn cho người dùng sửa trực tiếp

            // Tự co giãn cột
            //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tắt lựa chọn toàn dòng nếu không cần
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgv.GridColor = Color.Black;  // Đặt màu viền của bảng

        }

        #endregion

        //#region FFmpeg Integration
        //private static FFmpegStreamManager ffmpegManager;
        //private static readonly object ffmpegLock = new object();

        //public static FFmpegStreamManager GetFFmpegManager()
        //{
        //    if (ffmpegManager == null)
        //    {
        //        lock (ffmpegLock)
        //        {
        //            if (ffmpegManager == null)
        //            {
        //                ffmpegManager = new FFmpegStreamManager();
        //            }
        //        }
        //    }
        //    return ffmpegManager;
        //}

        //public static void DisposeFFmpegManager()
        //{
        //    lock (ffmpegLock)
        //    {
        //        if (ffmpegManager != null)
        //        {
        //            ffmpegManager.Dispose();
        //            ffmpegManager = null;
        //        }
        //    }
        //}

        //// Optimized image conversion methods
        //public string ImageToBase64Optimized(Mat frame)
        //{
        //    try
        //    {
        //        if (frame == null || frame.Empty())
        //            return string.Empty;

        //        // Use optimized JPEG encoding with lower quality for faster processing
        //        var encodeParams = new int[] { 
        //            (int)ImwriteFlags.JpegQuality, 85,  // Lower quality for speed
        //            (int)ImwriteFlags.JpegOptimize, 1   // Enable optimization
        //        };

        //        byte[] imageBytes;
        //        bool success = Cv2.ImEncode(".jpg", frame, out imageBytes, encodeParams);
                
        //        if (success && imageBytes != null)
        //        {
        //            return Convert.ToBase64String(imageBytes);
        //        }
                
        //        return string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        SaveLog(SAVING_LOG_TYPE.EXCEPTION, 
        //               $"ImageToBase64Optimized error: {ex.Message}", 
        //               true, false);
        //        return string.Empty;
        //    }
        //}

        //// Batch processing for multiple frames
        //public List<string> ProcessFramesBatch(List<Mat> frames, int maxConcurrency = 4)
        //{
        //    if (frames == null || frames.Count == 0)
        //        return new List<string>();

        //    var results = new List<string>(frames.Count);
        //    var semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);
        //    var tasks = new List<Task<string>>();

        //    foreach (var frame in frames)
        //    {
        //        var task = ProcessFrameAsync(frame, semaphore);
        //        tasks.Add(task);
        //    }

        //    try
        //    {
        //        Task.WaitAll(tasks.ToArray(), TimeSpan.FromSeconds(30));
        //        // Fix for .NET Framework 4.7.2 compatibility
        //        results.AddRange(tasks.Select(t => t.Status == TaskStatus.RanToCompletion ? t.Result : string.Empty));
        //    }
        //    catch (Exception ex)
        //    {
        //        SaveLog(SAVING_LOG_TYPE.EXCEPTION, 
        //               $"ProcessFramesBatch error: {ex.Message}", 
        //               true, false);
        //    }
        //    finally
        //    {
        //        semaphore.Dispose();
        //    }

        //    return results;
        //}

        //private async Task<string> ProcessFrameAsync(Mat frame, SemaphoreSlim semaphore)
        //{
        //    await semaphore.WaitAsync();
        //    try
        //    {
        //        return ImageToBase64Optimized(frame);
        //    }
        //    finally
        //    {
        //        semaphore.Release();
        //    }
        //}

        //#endregion
    }
}

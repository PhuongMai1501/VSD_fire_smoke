using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using static Mysqlx.Crud.Order.Types;
using MySqlX.XDevAPI;
using System.Net.Http;
using Mysqlx.Notice;
using System.Threading;
using Org.BouncyCastle.Utilities.Collections;
using System.Data.SqlClient;

namespace CameraManager
{
    public partial class MeasureRecipe2 : UserControl
    {
        #region Define Variables
        private int m_IndexRecipe = 0;
        private int m_indexCamera = 0;

        public Results Result = new Results();
        //CogDisplay Cogdisplay = new CogDisplay();
        //CogImage24PlanarColor InputImage = null;

        MySqlConnection connection;
        HttpClient client = new HttpClient();

        private Form1 main;

        #endregion

        public MeasureRecipe2()
        {
            InitializeComponent();
            SetOutputView();
        }
        public void InitPageSetup(Form1 obj, int index, string title = "")
        {
            lbTitleName.Text = (title.Trim() != "") ? title : "SETTING";
            main = obj;
            m_IndexRecipe = index;
            RefreshSetupUI();

        }
        #region database
        public void UpdateDataBase()
        {
            try
            {
                // Mở kết nối
                connection = new MySqlConnection(ClassSystemConfig.Ins.m_ClsCommon.connectionString);
                connection.Open();
                // Viết câu lệnh SQL để truy vấn dữ liệu
                string query = "SELECT `STT`, `Name` FROM `camera_list`";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connection);

                // Tạo DataTable để lưu trữ dữ liệu
                DataTable dataTable = new DataTable();

                // Điền dữ liệu vào DataTable
                dataAdapter.Fill(dataTable);

                // Gán DataTable vào DataGridView
                dgviewCamera.DataSource = dataTable;

                ClassSystemConfig.Ins.m_ClsFunc.FormatDataGridView(dgviewCamera);
                dgviewCamera.Columns["STT"].HeaderText = "STT";
                dgviewCamera.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgviewCamera.Columns["STT"].Width = 80;
                dgviewCamera.Columns["Name"].HeaderText = "Camera";
                dgviewCamera.Columns["Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgviewCamera.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
            finally
            {
                //Đóng kết nối
                connection.Close();
            }
        }

        #endregion

        #region function
        public void RefreshSetupUI()
        {
            btnRefreshRecipe_Click(null, null);
        }


        private void UncheckControl(CheckBox control)
        {
            //for (int i = 0; i < ListCheckControl.Length; i++)
            //{
            //    ListCheckControl[i].Checked = (control == ListCheckControl[i]);
            //}
        }
        public void SetOutputView()
        {
            try
            {
                //panelMain.Controls.Clear();
                //panelMain.Controls.Add(Cogdisplay);
                //Cogdisplay.Dock = DockStyle.Fill;
                //Cogdisplay.BackColor = Color.DimGray;
                //if (Cogdisplay.Image != null)
                //    Cogdisplay.Fit(true);
            }
            catch { }
        }
        public void ShowOnScreen()
        {
            this.BringToFront();
        }

        #endregion

        #region Run and Simulate
        List<string> ListImagePaths = new List<string>();
        int m_indexRun = 0;
        private void toolStripRun_Click(object sender, EventArgs e)
        {
            try
            {
                //if (InputImage != null)
                //{
                //    RunProcess(InputImage);
                //    SetImage(Result.OutputImage, Result.Graphics, true);
                //}
                //else
                if (ListImagePaths.Count > 0)
                {
                    Bitmap bmp = (Bitmap)System.Drawing.Image.FromFile(ListImagePaths[0]);
                    //RunProcess_Image(bmp);
                }
            }
            catch
            {

            }

        }

        private void toolStripRunNext_Click(object sender, EventArgs e)
        {
            if (ListImagePaths.Count == 0)
            {
                ClassCommon.ShowMessageBoxShort("None InputImage", "Null", 500);
            }
            else
            {
                string path = ListImagePaths[0];
                if (ListImagePaths.Count > 1)
                {
                    if (m_indexRun >= 0 && m_indexRun < ListImagePaths.Count)
                    {
                        path = ListImagePaths[m_indexRun];
                        m_indexRun++;
                    }
                    else
                    {
                        m_indexRun = 0;
                    }

                }
                else
                {
                    m_indexRun = 0;
                }
                //CogImage8Grey image = new CogImage8Grey((Bitmap)Image.FromFile(path));
                //SetImage(Result.OutputImage,Result.Graphics,true);
            }
        }
        private void toolStripOpenImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Images |*.bmp;*.png;*.jpg;*.jpeg;";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ListImagePaths.Clear();
                ListImagePaths.Add(fileDialog.FileName);
            }
        }

        private void toolStripopenFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fldDialog = new FolderBrowserDialog();
            if (fldDialog.ShowDialog() == DialogResult.OK)
            {
                ////String[] listImages = FormVisionSetting.GetFilesFrom(fldDialog.SelectedPath, false);
                //if (listImages != null)
                //{
                //    ListImagePaths.Clear();
                //    ListImagePaths.AddRange(listImages);
                //}
            }
        }

        private void toolStripLoad_Click(object sender, EventArgs e)
        {
            //LoadModel(m_IndexRecipe, ClassSystemConfig.Ins.m_ClsCommon.m_strRecipeName, true);
        }
        private void toolStripSave_Click(object sender, EventArgs e)
        {
            //SaveModel(m_IndexRecipe, ClassSystemConfig.Ins.m_ClsCommon.m_strRecipeName, true);
        }
        //public Mat ConvertBitmapToMat(Bitmap bitmap)
        //{
        //    // Tạo một đối tượng Mat từ Bitmap
        //    Mat mat = new Mat(bitmap.Height, bitmap.Width, MatType.CV_8UC3);

        //    // Chuyển đổi Bitmap thành Mat
        //    BitmapConverter.ToMat(bitmap, mat);

        //    return mat;
        //}
        //private async void RunProcess_Image(Bitmap inputImage)
        //{
        //    Mat frame = new Mat();
        //    Mat frame_resize = new Mat();
        //    frame = ConvertBitmapToMat(inputImage);
        //    Cogdisplay.StaticGraphics.Clear();
        //    int width_img = frame.Width;
        //    int height_img = frame.Height;
        //    if (inputImage == null)
        //    {
        //        Console.WriteLine("Lỗi: Không thể đọc frame hoặc hết video.");
        //    }

        //    Cv2.Resize(frame, frame_resize, new OpenCvSharp.Size(1280, 1280));
        //    //byte[] frameBytes = ConvertMatToByteArray(frame);
        //    string image_str = ClassSystemConfig.Ins.m_ClsFunc.ImageToBase64(frame_resize);
        //    int length = image_str.Length;
        //    DateTime _time1 = DateTime.Now;
        //    string predictionResult = await Predict_b64(image_str);
        //    double total_time = (DateTime.Now - _time1).TotalMilliseconds;
        //    Console.WriteLine("Time detect: " + total_time.ToString("F2"));

        //    if (predictionResult.Contains("Error"))
        //        goto END;

        //    var result = JsonConvert.DeserializeObject<List<Detection>>(predictionResult);
        //    if (result == null)
        //        return;
        //    foreach (var detection in result)
        //    {
        //        string label = detection.label;
        //        double x1 = Convert.ToDouble(detection.x1);
        //        double y1 = Convert.ToDouble(detection.y1);
        //        double x2 = Convert.ToDouble(detection.x2);
        //        double y2 = Convert.ToDouble(detection.y2);
        //        double score = Convert.ToDouble(detection.score);

        //        DrawGraphic(label, x1, y1, x2, y2, score, width_img, height_img);
        //    }
        //END:
        //    CogImage24PlanarColor outputImage = new CogImage24PlanarColor(frame.ToBitmap());
        //    SetImage(outputImage);
        //    Thread.Sleep(5);

        //}
        //public void DrawGraphic(string name, double x1, double y1, double x2, double y2, double score, int originalWidth, int originalHeight)
        //{
        //    double sParamFlame = Convert.ToDouble(numFlame_Sen.Value);
        //    double sParamSmoke = Convert.ToDouble(numSmoke_Sen.Value);
        //    if (name == "SMOKE")
        //    {
        //        if (score < sParamSmoke)
        //        {
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        if (score < sParamFlame)
        //        {
        //            return;
        //        }
        //    }
        //        CogColorConstants color;
        //    if (name == "SMOKE")
        //        color = CogColorConstants.Cyan;
        //    else
        //        color = CogColorConstants.Red;

        //    int resizeWidth = 1280, resizeHeight = 1280;

        //    int x1Original = (int)(x1 * originalWidth / (float)resizeWidth);
        //    int y1Original = (int)(y1 * originalHeight / (float)resizeHeight);
        //    int x2Original = (int)(x2 * originalWidth / (float)resizeWidth);
        //    int y2Original = (int)(y2 * originalHeight / (float)resizeHeight);

        //    double width = Math.Abs(x2Original - x1Original);
        //    double height = Math.Abs(y2Original - y1Original);
        //    Console.WriteLine("Width : " + width.ToString("F2") + " Height: " + height.ToString("F2"));

        //    //Resize region detect color
        //    CogRectangleAffine rec = new CogRectangleAffine();
        //    rec.SetOriginLengthsRotationSkew(x1Original, y1Original, width, height, 0, 0);
        //    rec.Color = color;
        //    rec.LineWidthInScreenPixels = 2;
        //    Cogdisplay.StaticGraphics.Add(rec, "rect");

        //    CogGraphicLabel label = new CogGraphicLabel();
        //    label.Alignment = CogGraphicLabelAlignmentConstants.BottomLeft;
        //    label.Font = new Font("Century Gothic", 12, FontStyle.Bold);
        //    label.Color = CogColorConstants.White;
        //    label.BackgroundColor = color;
        //    label.SetXYText(x1Original, y1Original, name.ToUpper() + ": " + score.ToString("F2"));
        //    Cogdisplay.StaticGraphics.Add(label, "label");
        //}
        //public async Task<string> Predict_b64(string base64Image)
        //{
        //    try
        //    {
        //        #region Image
        //        // Thêm ảnh vào yêu cầu với key là "file"
        //        var jsonContent = new StringContent(
        //        $"{{\"image\": \"{base64Image}\"}}",
        //        Encoding.UTF8,
        //        "application/json");
        //        #endregion

        //        DateTime _time1 = DateTime.Now;
        //        // Đóng kết nối khi xong
        //        var response = await client.PostAsync(ClassSystemConfig.Ins.m_ClsCommon.url_Server, jsonContent);
        //        double total_time = (DateTime.Now - _time1).TotalMilliseconds;
        //        Console.WriteLine("Time detect: " + total_time.ToString("F2"));
        //        // Kiểm tra kết quả
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Đọc response body dưới dạng chuỗi JSON
        //            string responseBody = await response.Content.ReadAsStringAsync();

        //            // Deserialize JSON thành danh sách các phát hiện
        //            var result = JsonConvert.DeserializeObject<List<Detection>>(responseBody);

        //            if (result != null)
        //            {
        //                foreach (var detection in result)
        //                {
        //                    Console.WriteLine($"{detection.label}: ({detection.x1}, {detection.y1}) - ({detection.x2}, {detection.y2}), Score: {detection.score}");
        //                }
        //            }

        //            return responseBody;
        //        }
        //        else
        //        {
        //            return ("Error: " + response.StatusCode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi chưa start docker!" + ex);
        //        return ("Error: " + ex);

        //    }
        //}

        #endregion

        #region DataTable1: Manage Tool Base 
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dgviewCamera.CurrentRow == null)
                return;
            int iSelected = dgviewCamera.CurrentRow.Index;
            m_indexCamera = iSelected;
            int STT = iSelected + 1;
            if (iSelected >= 0)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = "SELECT FrameInterval, Flame_Sensitivity, Smoke_Sensitivity FROM camera_list WHERE STT = @STT";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@STT", STT);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            numInterval.Value = reader.GetDecimal(0);
                            numFlame_Sen.Value = reader.GetDecimal(1);
                            numSmoke_Sen.Value = reader.GetDecimal(2);
                        }
                    }
                }
            }
        }

        private void btnRemoveToolBase_Click(object sender, EventArgs e)
        {
            if (dgviewCamera.CurrentRow == null)
                return;
            int iSelected = dgviewCamera.CurrentRow.Index;
            //if (iSelected >= 0 && iSelected < ListBaseTool.Count)
            //{

            //    string tool_name = ListBaseTool[iSelected].Name;
            //    if (MessageBox.Show("Do you want to delete this tool " + tool_name, "Delete Tool", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        dataGridView1.Rows.RemoveAt(iSelected);
            //        ListBaseTool.RemoveAt(iSelected);

            //        ClassCommon.ShowMessageBoxShort("Removed Tool " + tool_name + " Complete", "Remove", 1000);
            //    }
            //}
        }

        private void AddNewRowData1(string name, string type)
        {
            // Add To DataGridView
            if (type != null && type.Contains("Cog"))
                type = type.Replace("Cog", "");
            object[] row = new object[3];
            row[0] = dgviewCamera.Rows.Count;
            row[1] = name;
            row[2] = type;

            dgviewCamera.Rows.Add(row);
        }
        private void btnSortDown1_Click(object sender, EventArgs e)
        {
            if (dgviewCamera.CurrentRow == null)
                return;
            int iSelected = dgviewCamera.CurrentRow.Index;
            //if (iSelected >= 0 && iSelected < ListBaseTool.Count - 1)
            //{
            //    ICogTool tool_select = ListBaseTool[iSelected];
            //    DataGridViewRow row_select = dataGridView1.Rows[iSelected];
            //    dataGridView1.Rows.Remove(row_select);
            //    dataGridView1.Rows.Insert(iSelected + 1, row_select);

            //    ListBaseTool.Remove(tool_select);
            //    ListBaseTool.Insert(iSelected + 1, tool_select);


            //    dataGridView1.Rows[iSelected].Selected = false;

            //    for (int iRow = 0; iRow < dataGridView1.Rows.Count; iRow++)
            //    {
            //        dataGridView1.Rows[iRow].Cells[0].Value = (iRow + 1);
            //    }
            //}
        }
        private void btnSortUp1_Click(object sender, EventArgs e)
        {
            if (dgviewCamera.CurrentRow == null)
                return;
            int iSelected = dgviewCamera.CurrentRow.Index;
            //if (iSelected > 0 && iSelected < ListBaseTool.Count)
            //{
            //    ICogTool tool_select = ListBaseTool[iSelected];
            //    DataGridViewRow row_select = dataGridView1.Rows[iSelected];
            //    dataGridView1.Rows.Remove(row_select);
            //    dataGridView1.Rows.Insert(iSelected - 1, row_select);

            //    ListBaseTool.Remove(tool_select);
            //    ListBaseTool.Insert(iSelected - 1, tool_select);

            //    dataGridView1.Rows[iSelected].Selected = false;

            //    for (int iRow = 0; iRow < dataGridView1.Rows.Count; iRow++)
            //    {
            //        dataGridView1.Rows[iRow].Cells[0].Value = (iRow + 1);
            //    }

            //}
        }
        private void btnDeleteAllToolBase_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete all base tools", "Delete All", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dgviewCamera.Rows.Clear();

                ClassCommon.ShowMessageBoxShort("Deleted All Tool Complete", "Delete All", 1000);
            }
        }
        #endregion

        #region Measure Run Tool

        //public void SetImage(ICogImage image)
        //{
        //    if (Cogdisplay.InvokeRequired)
        //    {
        //        Cogdisplay.Invoke(new MethodInvoker(delegate
        //        {
        //            SetImage(image);

        //        }));
        //    }
        //    else
        //    {
        //        Cogdisplay.Image = image;
        //        Cogdisplay.AutoFit = true;
        //        //if (clear_graphic)
        //        //{
        //        //    try
        //        //    {
        //        //        cogRecordDisplay.StaticGraphics.Clear();
        //        //    }
        //        //    catch { }
        //        //}
        //        //foreach (ICogGraphic g in graphic)
        //        //{
        //        //    cogRecordDisplay.StaticGraphics.Add(g, "");
        //        //}
        //    }
        //}
        //public void ShowGraphicResult(ICogGraphic graphic)
        //{
        //    if (graphic != null)
        //    {
        //        if (Result.Graphics == null)
        //            Result.Graphics = new CogGraphicCollection();
        //        Result.Graphics.Add(graphic);
        //        Cogdisplay.StaticGraphics.Add(graphic, "graphic");
        //    }
        //}
        //private CogGraphicLabel CreateLabel(double x, double y, double rotation, float fontsize, bool isBold, string text, CogColorConstants color, string spacename)
        //{
        //    CogGraphicLabel label = new CogGraphicLabel();
        //    label.SetXYText(x, y, text);
        //    label.Alignment = CogGraphicLabelAlignmentConstants.TopLeft;
        //    label.Font = new Font("Century Gothic", fontsize, isBold ? FontStyle.Bold : FontStyle.Regular);
        //    label.Color = color;
        //    label.Rotation = rotation;
        //    if (spacename.Trim().Length > 0)
        //    {
        //        label.SelectedSpaceName = spacename;
        //    }

        //    return label;
        //}
        //CogPointMarker point_edit = null;
        //private void cbEditGraphic_CheckedChanged(object sender, EventArgs e)
        //{

        //}
        //private void btnShowGrid_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (Cogdisplay.Image != null)
        //    {
        //        if (btnShowGrid.Checked)
        //        {
        //            int grid_size = 0;
        //            int.TryParse(txbGridSize.Text, out grid_size);
        //            if (grid_size < 2)
        //                grid_size = 2;
        //            else
        //                if (grid_size > 40)
        //                grid_size = 40;
        //            ShowGridDisplay(Cogdisplay.Image, grid_size, CogColorConstants.Blue);
        //        }
        //        else
        //        {
        //            ClearGridDisplay();
        //        }
        //    }
        //    else
        //    {
        //        btnShowGrid.Checked = false;
        //    }
        //    btnShowGrid.BackColor = btnShowGrid.Checked ? Color.BlueViolet : Color.Transparent;
        //    txbGridSize.Enabled = !btnShowGrid.Checked;
        //}
        //private void ShowGridDisplay(ICogImage image, int Number = 5, CogColorConstants color = CogColorConstants.Yellow)
        //{
        //    double W = image.Width;
        //    double H = image.Height;
        //    if (Number < 1)
        //        Number = 2;
        //    CogLineSegment[] line_horz = new CogLineSegment[Number];
        //    CogLineSegment[] line_vert = new CogLineSegment[Number];

        //    try
        //    {
        //        for (int i = 0; i < Number; i++)
        //        {
        //            line_horz[i] = new CogLineSegment();
        //            line_vert[i] = new CogLineSegment();

        //            // Create Line
        //            double dx = image.Width / Number;
        //            double dy = image.Height / Number;
        //            line_vert[i].SetStartLengthRotation(dx * i, 0, H, Math.PI / 2);
        //            line_horz[i].SetStartLengthRotation(0, dy * i, W, 0);

        //            line_vert[i].Color = color;
        //            line_horz[i].Color = color;

        //            line_vert[i].LineStyle = (i % 2 == 0) ? CogGraphicLineStyleConstants.Solid : CogGraphicLineStyleConstants.Dot;
        //            line_horz[i].LineStyle = (i % 2 == 0) ? CogGraphicLineStyleConstants.Solid : CogGraphicLineStyleConstants.Dot;

        //            line_vert[i].SelectedSpaceName = "#";
        //            line_horz[i].SelectedSpaceName = "#";

        //            Cogdisplay.InteractiveGraphics.Add(line_vert[i], "grid", false);
        //            Cogdisplay.InteractiveGraphics.Add(line_horz[i], "grid", false);
        //        }
        //    }
        //    catch { }
        //}
        //public void ClearGridDisplay()
        //{
        //    try
        //    {
        //        Cogdisplay.InteractiveGraphics.Remove("grid");

        //    }
        //    catch { }
        //}
        #endregion

        public class Results
        {
            //public ICogImage OutputImage = null;
            //public CogGraphicCollection Graphics = new CogGraphicCollection();
            public List<double> ListData = new List<double>();
            public List<string> ListHeader = new List<string>();
            public bool FinalResult = false;
            public string Status = "";
            public double ProcessingTime = 0;
            public void Clear()
            {
                //OutputImage = null;
                //Graphics = new CogGraphicCollection();
                Status = "";
                FinalResult = false;
                ProcessingTime = 0;
                ListData = new List<double>();
                ListHeader = new List<string>();
            }
        }

        public class RecipeConfig
        {
            public int Index { get; set; }
            public bool IsReady { get; set; }
            public bool IsUse2PM = false;
            public bool IsCropImage = false;
            public bool ShowCommonGraphic = true;
            public bool ShowResultGraphic = true;
            public bool ShowTextGraphic = true;
            public double MaxRefTolerance = 0.03;
            public List<ITool> ListToolMeasure = new List<ITool>();
        }
        public class ITool
        {
            public string Toolname = "";
            public int Mode = 0;
            public bool ShowGraphic = false;
            public List<string[]> Inputs = new List<string[]>();
            public double[] Spec = new double[3] { 0, 0, 0 };
            public double OffsetRatio = 0;
            public PointF TextLocation = new PointF(0, 0);
            public bool IntersecLine = true;
            public double RefInputValue = 0;
            public bool RefInputEnable = false;
        }
        public void UpdateParam(string updateQuery, MySqlConnection connection, int interval, double flame, double smoke, int stt)
        {
            try
            {
                // Mở kết nối
                using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                {
                    // Thêm các tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@Interval", interval);
                    cmd.Parameters.AddWithValue("@Flame", flame);
                    cmd.Parameters.AddWithValue("@Smoke", smoke);
                    cmd.Parameters.AddWithValue("@STT", stt);  // hoặc cmd.Parameters.AddWithValue("@Name", name);

                    // Thực thi câu lệnh UPDATE
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Dữ liệu đã được cập nhật thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu được cập nhật.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string query = @"
                UPDATE camera_list
                SET FrameInterval = @Interval,
                    Flame_Sensitivity = @Flame,
                    Smoke_Sensitivity = @Smoke
                WHERE STT = @STT";  // hoặc WHERE Name = @Name
            int stt = m_indexCamera + 1;
            int interval = Convert.ToInt32(numInterval.Value);
            double flame_sensitivity = Convert.ToDouble(numFlame_Sen.Value);
            double smoke_sensitivity = Convert.ToDouble(numSmoke_Sen.Value);

            UpdateParam(query, connection, interval, flame_sensitivity, smoke_sensitivity, stt);

            // Cập nhật lại DataGridView
            //UpdateDataGridView();

        }

        private void MeasureRecipe2_Load(object sender, EventArgs e)
        {

        }

        private void btnConfigScreen_Click(object sender, EventArgs e)
        {
            main.Col = Convert.ToInt32(numColCam.Value);
            main.Row = Convert.ToInt32(numRowCam.Value);
            main.LayoutCameraSpreadView();
        }

        private void btnAddToolBase_Click(object sender, EventArgs e)
        {
            ClassSystemConfig.Ins.m_CameraList.Show();
            ClassSystemConfig.Ins.m_CameraList.ShowOnScreen();
            ClassSystemConfig.Ins.m_ClsFunc.SaveLog(ClassFunction.SAVING_LOG_TYPE.HANDLER_CLICKED,
                                                    "Clicked Camera List Add",
                                                    ClassSystemConfig.Ins.m_ClsCommon.IsSaveLog_Local, true);
        }

        private void btnRefreshRecipe_Click(object sender, EventArgs e)
        {
            UpdateDataBase();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }

}


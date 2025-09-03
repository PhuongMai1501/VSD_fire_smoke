using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace CameraManager
{
    public partial class FormCameraList : Form
    {
        int m_IndexSelectedRow = 0;
        public Form1 main;
        MySqlConnection connection;
        bool bLastStateNormal = false;

        public FormCameraList()
        {
            InitializeComponent();
        }

        public void InitializeUI(Form1 obj)
        {
            main = obj;
            UpdateDataGridView();

        }

        #region database
        private void UpdateDataGridView()
        {
            try
            {
                connection = new MySqlConnection(ClassSystemConfig.Ins.m_ClsCommon.connectionString);
                // Mở kết nối
                connection.Open();

                // Viết câu lệnh SQL để truy vấn dữ liệu
                string query = "SELECT STT, Name, RTSP_URL, Type, IP_Camera, UserID, Password FROM camera_list";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connection);

                // Tạo DataTable để lưu trữ dữ liệu
                DataTable dataTable = new DataTable();

                // Điền dữ liệu vào DataTable
                dataAdapter.Fill(dataTable);

                // Gán DataTable vào DataGridView
                dgviewCameraDetail.DataSource = dataTable;

                // Căn lề các cột
                dgviewCameraDetail.Columns["STT"].Width = 80;
                dgviewCameraDetail.Columns["Name"].Width = 150;
                dgviewCameraDetail.Columns["RTSP_URL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgviewCameraDetail.Columns["Type"].Width = 80;
                dgviewCameraDetail.Columns["IP_Camera"].Width = 150;
                dgviewCameraDetail.Columns["UserID"].Width = 150;
                dgviewCameraDetail.Columns["Password"].Width = 150;

                dgviewCameraDetail.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgviewCameraDetail.Columns["Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgviewCameraDetail.Columns["RTSP_URL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgviewCameraDetail.Columns["Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgviewCameraDetail.Columns["IP_Camera"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgviewCameraDetail.Columns["UserID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgviewCameraDetail.Columns["Password"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ClassSystemConfig.Ins.m_ClsFunc.FormatDataGridView(dgviewCameraDetail);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối
                connection.Close();
            }

            // Cập nhật danh sách camera
            ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam.Clear();
            ClassSystemConfig.Ins.m_ClsFunc.GetRtspUrls(connection, ClassSystemConfig.Ins.m_ClsCommon.m_ListRtspCam);

        }
        public void AddDataBase(string insertQuery, MySqlConnection connection, string name, string rtspUrl, string ipCamera, string userId, string password, int stt)
        {
            connection.Open();
            // Tạo đối tượng MySqlCommand
            using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
            {
                // Thêm các tham số vào câu lệnh SQL
                command.Parameters.AddWithValue("@STT", stt);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@RTSP_URL", rtspUrl);
                command.Parameters.AddWithValue("@Type", "0");
                command.Parameters.AddWithValue("@IP_Camera", ipCamera);
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@Password", password);
                try
                {
                    // Thực thi câu lệnh INSERT INTO
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Dữ liệu đã được thêm thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu được thêm vào.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        #endregion

        #region Datagridview
        private void InitColumnDataGridview(DataGridView dtGridview, string Header)
        {
            // Create Column Name
            List<string> listHeaderCol = new List<string>();

            string[] strMajor = Header.Split(',');
            listHeaderCol.AddRange(strMajor);


            // Assign to datagridview
            try
            {
                dtGridview.CancelEdit();
                dtGridview.Columns.Clear();
                dtGridview.DataSource = null;

                dtGridview.ColumnCount = listHeaderCol.Count;
                int width_sum = 0;
                for (int i = 0; i < listHeaderCol.Count; i++)
                {
                    string strColName = listHeaderCol[i].Trim().Replace("_", " ");
                    strColName = strColName.ToUpper();
                    dtGridview.Columns[i].Name = strColName;
                    width_sum += dtGridview.Width / listHeaderCol.Count;
                    if (listHeaderCol.Count > 1)
                        dtGridview.Columns[i].Width = dtGridview.Width / (listHeaderCol.Count - 1);
                }

                dtGridview.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9f, FontStyle.Bold);
                dtGridview.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkMagenta; //SlateGray
                dtGridview.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            catch { }
        }
       
        public void ClearDataGridview(DataGridView _DataGridView)
        {
            if (_DataGridView.InvokeRequired)
            {
                _DataGridView.BeginInvoke(new Action(() =>
                {
                    if (_DataGridView.RowCount > 0)
                        _DataGridView.Rows.Clear();
                }));
            }
            else
            {
                if (_DataGridView.RowCount > 0)
                    _DataGridView.Rows.Clear();
            }
           // m_iCountLine = 0;
        }
        #endregion

        #region From Move
        bool MouseDown = false;
        System.Drawing.Point StartPoint;
        private void panelHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDown)
            {
                this.Location = new System.Drawing.Point(this.Location.X + (e.Location.X - StartPoint.X), this.Location.Y + (e.Location.Y - StartPoint.Y));
            }
        }

        private void panelHeader_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDown = false;
        }

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown = true;
            StartPoint = e.Location;
        }
        public void ShowOnScreen()
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = bLastStateNormal ? FormWindowState.Normal : FormWindowState.Maximized;

            this.BringToFront();
        }

        #endregion

        #region Handler
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {

        }

        private void btnAddCamera_Click(object sender, EventArgs e)
        {
            string insertQuery = @"
            INSERT INTO `camera_list` (`STT`,`Name`, `RTSP_URL`, `Type`, `IP_Camera`, `UserID`, `Password`)
            VALUES (@STT, @Name, @RTSP_URL, @Type, @IP_Camera, @UserID, @Password);";
            string name = txbCameraName.Text.Trim();
            string ipCamera = txbIPCamera.Text.Trim();
            string userId = txbUserID.Text.Trim();
            string password = txbPassword.Text.Trim();
            int stt = Convert.ToInt32(txbIDCamera.Text.Trim());
            
            // *** FIXED: Better RTSP URL format with fallback options ***
            string rtspUrl;
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
            {
                rtspUrl = $"rtsp://{ipCamera}:554/cam/realmonitor?channel=1&subtype=1";
            }
            else
            {
                rtspUrl = $"rtsp://{userId}:{password}@{ipCamera}:554/cam/realmonitor?channel=1&subtype=1";
            }
            
            Console.WriteLine($"🔗 Generated RTSP URL: {rtspUrl}");

            //// Test RTSP URL before adding to database
            //Task.Run(async () =>
            //{
            //    Console.WriteLine($"🧪 Testing RTSP URL before adding to database: {rtspUrl}");
            //    bool isValid = await CameraManager.Diagnostics.FFmpegTester.TestRtspUrl(rtspUrl, testDurationSeconds: 3);

            //    if (isValid)
            //    {
            //        Console.WriteLine($"✅ RTSP URL is valid: {rtspUrl}");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"⚠️ RTSP URL might have issues: {rtspUrl}");
            //        // Try alternative URL formats
            //        var altUrl1 = $"rtsp://{userId}:{password}@{ipCamera}:554/cam/realmonitor?channel=1&subtype=0";
            //        var altUrl2 = $"rtsp://{userId}:{password}@{ipCamera}:554/h264/ch1/main/av_stream";

            //        Console.WriteLine($"🧪 Trying alternative URL 1: {altUrl1}");
            //        bool alt1Valid = await CameraManager.Diagnostics.FFmpegTester.TestRtspUrl(altUrl1, testDurationSeconds: 3);

            //        if (alt1Valid)
            //        {
            //            Console.WriteLine($"✅ Alternative URL 1 works: {altUrl1}");
            //            return;
            //        }

            //        Console.WriteLine($"🧪 Trying alternative URL 2: {altUrl2}");
            //        bool alt2Valid = await CameraManager.Diagnostics.FFmpegTester.TestRtspUrl(altUrl2, testDurationSeconds: 3);

            //        if (alt2Valid)
            //        {
            //            Console.WriteLine($"✅ Alternative URL 2 works: {altUrl2}");
            //        }
            //    }
            //});

            // Mở kết nối
            AddDataBase(insertQuery, connection, name, rtspUrl, ipCamera, userId, password, stt);

            // Cập nhật lại DataGridView
            UpdateDataGridView();
        }
        private void dgviewCameraDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iCol = e.ColumnIndex;
            m_IndexSelectedRow = iRow;

            //if (iRow >= 0 && iRow < dgviewCameraDetail.Rows.Count)
            //{
            //    txbIDCamera.Text = dgviewCameraDetail.Rows[iRow].Cells[0].Value.ToString();
            //    txbCameraName.Text = dgviewCameraDetail.Rows[iRow].Cells[1].Value.ToString();
            //    txbRtsp.Text = dgviewCameraDetail.Rows[iRow].Cells[2].Value.ToString();
            //    txbType.Text = dgviewCameraDetail.Rows[iRow].Cells[3].Value.ToString();
            //    txbIPCamera.Text = dgviewCameraDetail.Rows[iRow].Cells[4].Value.ToString();
            //    txbUserID.Text = dgviewCameraDetail.Rows[iRow].Cells[5].Value.ToString();
            //    txbPassword.Text = dgviewCameraDetail.Rows[iRow].Cells[6].Value.ToString();
            //}
        }
        private void btnMaximum_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                bLastStateNormal = true;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                bLastStateNormal = false;
            }
        }
        private void FormConfirmVision_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        #endregion
        private void btnDeleteCamera_Click(object sender, EventArgs e)
        {
            // Câu lệnh SQL để xóa dòng
            string deleteQuery = "DELETE FROM `camera_list` WHERE `STT` = @STT;";
            int stt = Convert.ToInt32(txbIDCamera.Text);
            DeleteCameraData(stt, connection, deleteQuery);
            UpdateDataGridView();
        }
        public void DeleteCameraData(int stt, MySqlConnection connection, string deleteQuery)
        {
            try
            {
                // Mở kết nối
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                {
                    // Thêm tham số vào câu lệnh SQL
                    command.Parameters.AddWithValue("@STT", stt);

                    // Thực thi câu lệnh DELETE
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Dòng đã được xóa thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dòng để xóa.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        public void UpdateCameraData(string updateQuery, MySqlConnection connection, string rtspUrl, string name, string ipCamera, string userId, string password, int stt)
        {
            try
            {
                // Mở kết nối
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    // Thêm các tham số vào câu lệnh SQL
                    command.Parameters.AddWithValue("param1", rtspUrl);
                    command.Parameters.AddWithValue("param2", name);
                    command.Parameters.AddWithValue("param3", ipCamera);
                    command.Parameters.AddWithValue("param4", userId);
                    command.Parameters.AddWithValue("param5", password);
                    command.Parameters.AddWithValue("param6", "0");
                    command.Parameters.AddWithValue("param7", stt);

                    // Thực thi câu lệnh UPDATE
                    int rowsAffected = command.ExecuteNonQuery();
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
        private void dgviewCameraDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (dgviewCameraDetail.CurrentRow == null)
                return;
            int iSelected = dgviewCameraDetail.CurrentRow.Index;

            if (iSelected >= 0 && iSelected < dgviewCameraDetail.Rows.Count)
            {
                txbIDCamera.Text = dgviewCameraDetail.Rows[iSelected].Cells[0].Value.ToString();
                txbCameraName.Text = dgviewCameraDetail.Rows[iSelected].Cells[1].Value.ToString();
                txbRtsp.Text = dgviewCameraDetail.Rows[iSelected].Cells[2].Value.ToString();
                txbType.Text = dgviewCameraDetail.Rows[iSelected].Cells[3].Value.ToString();
                txbIPCamera.Text = dgviewCameraDetail.Rows[iSelected].Cells[4].Value.ToString();
                txbUserID.Text = dgviewCameraDetail.Rows[iSelected].Cells[5].Value.ToString();
                txbPassword.Text = dgviewCameraDetail.Rows[iSelected].Cells[6].Value.ToString();
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "UPDATE camera_list SET `RTSP_URL` = ?, Name = ?, IP_Camera = ?, UserID = ?, Password = ?, Type = ? WHERE STT = ?";
            string name = txbCameraName.Text.Trim();
            string ipCamera = txbIPCamera.Text.Trim();
            string userId = txbUserID.Text.Trim();
            string password = txbPassword.Text.Trim();
            int STT = Convert.ToInt32(txbIDCamera.Text.Trim());
            string rtspUrl = "rtsp://" + userId + ":" + password + "@" + ipCamera + ":554/cam/realmonitor?channel=1&subtype=1";

            UpdateCameraData(query, connection, rtspUrl, name, ipCamera, userId, password, STT);

            // Cập nhật lại DataGridView
            UpdateDataGridView();
        }
    }
}

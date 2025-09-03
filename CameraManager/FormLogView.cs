using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CameraManager
{
    public partial class FormLogView : Form
    {

        bool bLastStateNormal = true;
        MySqlConnection connection;
        private int currentPage = 1;  // Trang hiện tại
        private int pageSize = 42;    // Số lượng bản ghi mỗi trang
        private int totalRecords = 0; // Tổng số bản ghi
        private int totalPages = 0;   // Tổng số trang

        public FormLogView()
        {
            InitializeComponent();
        }
        private void FormLogView_Load(object sender, EventArgs e)
        {
            connection = new MySqlConnection(ClassSystemConfig.Ins.m_ClsCommon.connectionString);
            LoadDataForPage();
            btnMaximum_Click(sender, e);
        }

        #region Handler
        private void FormLogView_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
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
        public void ShowOnScreen()
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = bLastStateNormal ? FormWindowState.Normal : FormWindowState.Maximized;

            this.BringToFront();
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadDataForPage();
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadDataForPage();
            }
        }
        private void dgLogEvent_SelectionChanged(object sender, EventArgs e)
        {
            if (dgLogEvent.CurrentRow == null)
                return;
            int iSelected = dgLogEvent.CurrentRow.Index;

            if (iSelected >= 0 && iSelected < dgLogEvent.Rows.Count)
            {
                string image_path = dgLogEvent.Rows[iSelected].Cells[4].Value.ToString();
                if (File.Exists(image_path))
                {
                    Image picture = Image.FromFile(image_path);
                    if (picture != null)
                    {
                        pictureImageView.Image = Image.FromFile(image_path);
                    }
                }
                else
                {
                    pictureImageView.Image = null;
                }
            }
        }

        #endregion

        #region Enable Drag Winform
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void panelHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panelHeader_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        #endregion

        #region Function
        private void UpdateDataGridView()
        {
            try
            {
                connection = new MySqlConnection(ClassSystemConfig.Ins.m_ClsCommon.connectionString);
                // Mở kết nối
                connection.Open();

                // Viết câu lệnh SQL để truy vấn dữ liệu
                string query = "SELECT * FROM camera_log";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connection);

                // Tạo DataTable để lưu trữ dữ liệu
                DataTable dataTable = new DataTable();

                // Điền dữ liệu vào DataTable
                dataAdapter.Fill(dataTable);

                // Gán DataTable vào DataGridView
                dgLogEvent.DataSource = dataTable;

                dgLogEvent.Columns["STT"].HeaderText = "STT";
                dgLogEvent.Columns["STT"].Width = 80;
                dgLogEvent.Columns["Camera"].HeaderText = "Camera";
                dgLogEvent.Columns["Camera"].Width = 150;
                dgLogEvent.Columns["Time"].HeaderText = "Time";
                dgLogEvent.Columns["Time"].Width = 300;
                dgLogEvent.Columns["Event"].HeaderText = "Event";
                dgLogEvent.Columns["Event"].Width = 150;
                dgLogEvent.Columns["image_Path"].HeaderText = "File Name";
                dgLogEvent.Columns["image_Path"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                ClassSystemConfig.Ins.m_ClsFunc.FormatDataGridView(dgLogEvent);

                // Căn lề các cột
                dgLogEvent.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgLogEvent.Columns["Camera"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgLogEvent.Columns["Time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgLogEvent.Columns["Event"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgLogEvent.Columns["image_Path"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


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
        }

        private void LoadDataForPage()
        {
            if (connection == null)
            {
                connection = new MySqlConnection(ClassSystemConfig.Ins.m_ClsCommon.connectionString);
            }

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // Tính toán chỉ mục bắt đầu và kết thúc cho trang hiện tại
            int startIndex = (currentPage - 1) * pageSize;
            string query = "SELECT STT, Camera, Time, Event, image_Path FROM camera_log " +
                               "ORDER BY STT LIMIT @pageSize OFFSET @startIndex";

            // Tạo MySqlDataAdapter và gán kết nối
            MySqlDataAdapter da = new MySqlDataAdapter(query, connection);

            // Thêm tham số vào câu truy vấn
            da.SelectCommand.Parameters.AddWithValue("@startIndex", startIndex);
            da.SelectCommand.Parameters.AddWithValue("@pageSize", pageSize);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dgLogEvent.DataSource = dt;

            // Cập nhật thông tin phân trang
            UpdatePagingInfo();
            FormatDataView();
            // Đóng kết nối khi xong
            connection.Close();
        }

        private void UpdatePagingInfo()
        {
            // Tính tổng số bản ghi và tổng số trang
            string countQuery = "SELECT COUNT(*) FROM camera_log";
            MySqlCommand cmd = new MySqlCommand(countQuery, connection);
            totalRecords = Convert.ToInt32(cmd.ExecuteScalar());

            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            // Cập nhật các điều khiển phân trang
            lbTotalPages2.Text = "Total: " + totalPages + " Pages";
            lbTotalPages1.Text = "Total: " + totalPages + " Pages";
            lbCurrrentPage.Text = "Page: " + currentPage + " / " + totalPages;

            // Cập nhật các nút điều hướng
            btnNext.Enabled = currentPage < totalPages;
            btnPrevious.Enabled = currentPage > 1;
        }

        private void FormatDataView()
        {
            dgLogEvent.Columns["STT"].HeaderText = "STT";
            dgLogEvent.Columns["STT"].Width = 80;
            dgLogEvent.Columns["Camera"].HeaderText = "Camera";
            dgLogEvent.Columns["Camera"].Width = 150;
            dgLogEvent.Columns["Time"].HeaderText = "Time";
            dgLogEvent.Columns["Time"].Width = 300;
            dgLogEvent.Columns["Event"].HeaderText = "Event";
            dgLogEvent.Columns["Event"].Width = 150;
            dgLogEvent.Columns["image_Path"].HeaderText = "File Name";
            dgLogEvent.Columns["image_Path"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ClassSystemConfig.Ins.m_ClsFunc.FormatDataGridView(dgLogEvent);

            // Căn lề các cột
            dgLogEvent.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgLogEvent.Columns["Camera"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgLogEvent.Columns["Time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgLogEvent.Columns["Event"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgLogEvent.Columns["image_Path"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataForPage();
            btnNext.Visible = true;
            btnPrevious.Visible = true;
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath = System.IO.Path.Combine(ClassSystemConfig.Ins.m_ClsCommon.m_CommonPath, "Images");
                if (System.IO.Directory.Exists(folderPath))
                    System.Diagnostics.Process.Start(folderPath);
                else
                {
                    ClassCommon.ShowMessageBoxShort("Not Found Saving Location", "Message", 2000);
                    System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory());
                }
            }
            catch
            {

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string startDate = dtPickerSelectDateStart.Value.ToString("yyyy-MM-dd");
            string fromHour = numericFromHour.Value.ToString();
            string endDate = dtPickerSelectDateEnd.Value.ToString("yyyy-MM-dd");
            string toHour = numericToHour.Value.ToString();
            SearchByDateTime(startDate, fromHour, endDate, toHour);
            btnPrevious.Visible = false;
            btnNext.Visible = false;

        }
        public void SearchByDateTime(string startDate, string fromHour, string endDate, string toHour)
        {
            if (connection == null)
            {
                connection = new MySqlConnection(ClassSystemConfig.Ins.m_ClsCommon.connectionString);
            }

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // Truy vấn SQL để tìm kiếm theo ngày giờ
            string query = @"
            SELECT * 
            FROM camera_log
            WHERE Time BETWEEN CONCAT(@DateStart, ' ', @FromHour, ':00:00') 
                          AND CONCAT(@DateEnd, ' ', @ToHour, ':59:59')";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                // Thêm các tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@DateStart", startDate);
                cmd.Parameters.AddWithValue("@FromHour", fromHour);
                cmd.Parameters.AddWithValue("@DateEnd", endDate);
                cmd.Parameters.AddWithValue("@ToHour", toHour);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("No data found for the selected time range.");
                        return;  // Nếu không có dữ liệu, thoát hàm
                    }

                    // Tạo DataTable để lưu dữ liệu
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Hiển thị dữ liệu trong DataGridView
                    dgLogEvent.DataSource = dt;
                }
            }
        }

        public void ExportToCSV(DataGridView dgv, string filePath)
        {
            StringBuilder sb = new StringBuilder();

            // Lấy tên cột (header)
            string[] columnNames = new string[dgv.ColumnCount];
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                columnNames[i] = dgv.Columns[i].HeaderText;
            }
            sb.AppendLine(string.Join(",", columnNames)); // Ghi dòng tiêu đề

            // Lấy dữ liệu trong từng dòng
            foreach (DataGridViewRow row in dgv.Rows)
            {
                string[] cellValues = new string[dgv.ColumnCount];
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    cellValues[i] = row.Cells[i].Value?.ToString() ?? string.Empty; // Nếu cell rỗng thì sử dụng chuỗi rỗng
                }
                sb.AppendLine(string.Join(",", cellValues)); // Ghi mỗi dòng dữ liệu
            }

            // Ghi dữ liệu vào file CSV
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

            MessageBox.Show("Export completed successfully!");
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại SaveFileDialog để chọn đường dẫn lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV File (*.csv)|*.csv|All Files (*.*)|*.*";
            saveFileDialog.FileName = "data.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Gọi hàm export với DataGridView và đường dẫn file đã chọn
                ExportToCSV(dgLogEvent, saveFileDialog.FileName);
            }
        }
    }
}

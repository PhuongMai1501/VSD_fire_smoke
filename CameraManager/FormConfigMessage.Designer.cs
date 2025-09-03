namespace CameraManager
{
    partial class FormConfigMessage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            chkEnableAlert = new CheckBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            lblTitle = new Label();
            panel2 = new Panel();
            btnTest = new Button();
            gbSendingMode = new GroupBox();
            lblAppSelect = new Label();
            cmbAppSelect = new ComboBox();
            lblMessageSelect = new Label();
            cmbMessageSelect = new ComboBox();
            lblAlertLevel = new Label();
            cmbAlertLevel = new ComboBox();
            btnSave = new Button();
            lblSubtitle = new Label();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            gbSendingMode.SuspendLayout();
            SuspendLayout();
            // 
            // chkEnableAlert
            // 
            chkEnableAlert.AutoSize = true;
            chkEnableAlert.Checked = true;
            chkEnableAlert.CheckState = CheckState.Checked;
            chkEnableAlert.Location = new Point(7, 37);
            chkEnableAlert.Margin = new Padding(4, 3, 4, 3);
            chkEnableAlert.Name = "chkEnableAlert";
            chkEnableAlert.Size = new Size(183, 19);
            chkEnableAlert.TabIndex = 0;
            chkEnableAlert.Text = "Bật/Tắt gửi cảnh báo tin nhắn";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 13.14387F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 86.85612F));
            tableLayoutPanel1.Size = new Size(541, 563);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblTitle);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(4, 3);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(533, 68);
            panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(119, 16);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(254, 32);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Cài Đặt Gửi Tin Nhắn";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnTest);
            panel2.Controls.Add(gbSendingMode);
            panel2.Controls.Add(lblAppSelect);
            panel2.Controls.Add(cmbAppSelect);
            panel2.Controls.Add(lblMessageSelect);
            panel2.Controls.Add(cmbMessageSelect);
            panel2.Controls.Add(lblAlertLevel);
            panel2.Controls.Add(cmbAlertLevel);
            panel2.Controls.Add(btnSave);
            panel2.Controls.Add(lblSubtitle);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(4, 77);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(533, 483);
            panel2.TabIndex = 1;
            // 
            // btnTest
            // 
            btnTest.Anchor = AnchorStyles.Left;
            btnTest.BackColor = Color.FromArgb(24, 119, 242);
            btnTest.FlatAppearance.BorderSize = 0;
            btnTest.FlatStyle = FlatStyle.Flat;
            btnTest.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTest.ForeColor = Color.White;
            btnTest.Location = new Point(467, 3);
            btnTest.Margin = new Padding(4, 3, 4, 3);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(68, 37);
            btnTest.TabIndex = 13;
            btnTest.Text = "Test";
            btnTest.UseVisualStyleBackColor = false;
            btnTest.Click += btnTest_Click;
            // 
            // gbSendingMode
            // 
            gbSendingMode.Anchor = AnchorStyles.Left;
            gbSendingMode.Controls.Add(chkEnableAlert);
            gbSendingMode.Location = new Point(31, 47);
            gbSendingMode.Margin = new Padding(4, 3, 4, 3);
            gbSendingMode.Name = "gbSendingMode";
            gbSendingMode.Padding = new Padding(4, 3, 4, 3);
            gbSendingMode.Size = new Size(465, 81);
            gbSendingMode.TabIndex = 5;
            gbSendingMode.TabStop = false;
            gbSendingMode.Text = "Chế độ gửi tin nhắn";
            // 
            // lblAppSelect
            // 
            lblAppSelect.Anchor = AnchorStyles.Left;
            lblAppSelect.AutoSize = true;
            lblAppSelect.Location = new Point(31, 151);
            lblAppSelect.Margin = new Padding(4, 0, 4, 0);
            lblAppSelect.Name = "lblAppSelect";
            lblAppSelect.Size = new Size(91, 15);
            lblAppSelect.TabIndex = 7;
            lblAppSelect.Text = "Chọn ứng dụng";
            // 
            // cmbAppSelect
            // 
            cmbAppSelect.Anchor = AnchorStyles.Left;
            cmbAppSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAppSelect.FormattingEnabled = true;
            cmbAppSelect.Location = new Point(31, 180);
            cmbAppSelect.Margin = new Padding(4, 3, 4, 3);
            cmbAppSelect.Name = "cmbAppSelect";
            cmbAppSelect.Size = new Size(465, 23);
            cmbAppSelect.TabIndex = 6;
            // 
            // lblMessageSelect
            // 
            lblMessageSelect.Anchor = AnchorStyles.Left;
            lblMessageSelect.AutoSize = true;
            lblMessageSelect.Location = new Point(31, 232);
            lblMessageSelect.Margin = new Padding(4, 0, 4, 0);
            lblMessageSelect.Name = "lblMessageSelect";
            lblMessageSelect.Size = new Size(104, 15);
            lblMessageSelect.TabIndex = 9;
            lblMessageSelect.Text = "Nội dung tin nhắn";
            // 
            // cmbMessageSelect
            // 
            cmbMessageSelect.Anchor = AnchorStyles.Left;
            cmbMessageSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMessageSelect.FormattingEnabled = true;
            cmbMessageSelect.Location = new Point(31, 261);
            cmbMessageSelect.Margin = new Padding(4, 3, 4, 3);
            cmbMessageSelect.Name = "cmbMessageSelect";
            cmbMessageSelect.Size = new Size(465, 23);
            cmbMessageSelect.TabIndex = 8;
            // 
            // lblAlertLevel
            // 
            lblAlertLevel.Anchor = AnchorStyles.Left;
            lblAlertLevel.AutoSize = true;
            lblAlertLevel.Location = new Point(31, 313);
            lblAlertLevel.Margin = new Padding(4, 0, 4, 0);
            lblAlertLevel.Name = "lblAlertLevel";
            lblAlertLevel.Size = new Size(100, 15);
            lblAlertLevel.TabIndex = 11;
            lblAlertLevel.Text = "Mức độ cảnh báo";
            // 
            // cmbAlertLevel
            // 
            cmbAlertLevel.Anchor = AnchorStyles.Left;
            cmbAlertLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAlertLevel.FormattingEnabled = true;
            cmbAlertLevel.Location = new Point(31, 342);
            cmbAlertLevel.Margin = new Padding(4, 3, 4, 3);
            cmbAlertLevel.Name = "cmbAlertLevel";
            cmbAlertLevel.Size = new Size(465, 23);
            cmbAlertLevel.TabIndex = 10;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Left;
            btnSave.BackColor = Color.FromArgb(24, 119, 242);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(31, 395);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(465, 58);
            btnSave.TabIndex = 12;
            btnSave.Text = "Lưu cấu hình";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Anchor = AnchorStyles.Left;
            lblSubtitle.AutoSize = true;
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.Location = new Point(125, 15);
            lblSubtitle.Margin = new Padding(4, 0, 4, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(255, 15);
            lblSubtitle.TabIndex = 2;
            lblSubtitle.Text = "Thiết lập hệ thống cảnh báo và gửi tin tự động.";
            // 
            // FormConfigMessage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(541, 563);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormConfigMessage";
            Text = "FormConfigMessage";
            Load += FormConfigMessage_Load;
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            gbSendingMode.ResumeLayout(false);
            gbSendingMode.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbSendingMode;
        private System.Windows.Forms.Label lblAppSelect;
        private System.Windows.Forms.ComboBox cmbAppSelect;
        private System.Windows.Forms.Label lblMessageSelect;
        private System.Windows.Forms.ComboBox cmbMessageSelect;
        private System.Windows.Forms.Label lblAlertLevel;
        private System.Windows.Forms.ComboBox cmbAlertLevel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnTest;
    }
}
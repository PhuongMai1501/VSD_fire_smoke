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
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            lblTitle = new Label();
            panel2 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel3 = new Panel();
            dgMessage_Alarm = new DataGridView();
            panel4 = new Panel();
            tableLayoutPanel3 = new TableLayoutPanel();
            panel5 = new Panel();
            btnSave = new Button();
            chkByPass = new CheckBox();
            btnTest = new Button();
            lblSubtitle = new Label();
            gbSendingMode = new GroupBox();
            chkEnableAlert = new CheckBox();
            lblAppSelect = new Label();
            cmbMessageSelect = new ComboBox();
            cmbAppSelect = new ComboBox();
            lblMessageSelect = new Label();
            panel6 = new Panel();
            label4 = new Label();
            txbChatID = new TextBox();
            btnDelete_Mes = new Button();
            btnAdd_Mes = new Button();
            btnUpdate_Mes = new Button();
            label3 = new Label();
            txbPhone_num = new TextBox();
            label2 = new Label();
            txbName = new TextBox();
            label1 = new Label();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgMessage_Alarm).BeginInit();
            panel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel5.SuspendLayout();
            gbSendingMode.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
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
            tableLayoutPanel1.Size = new Size(843, 518);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(lblTitle);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(4, 3);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(835, 62);
            panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(246, 14);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(340, 32);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Setting Send Message Alarm";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(tableLayoutPanel2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(4, 71);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(835, 444);
            panel2.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.8199272F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.1800728F));
            tableLayoutPanel2.Controls.Add(panel3, 0, 0);
            tableLayoutPanel2.Controls.Add(panel4, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(833, 442);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(dgMessage_Alarm);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(409, 436);
            panel3.TabIndex = 0;
            // 
            // dgMessage_Alarm
            // 
            dgMessage_Alarm.BackgroundColor = Color.Gainsboro;
            dgMessage_Alarm.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgMessage_Alarm.Dock = DockStyle.Fill;
            dgMessage_Alarm.Location = new Point(0, 0);
            dgMessage_Alarm.Name = "dgMessage_Alarm";
            dgMessage_Alarm.RowHeadersVisible = false;
            dgMessage_Alarm.Size = new Size(409, 436);
            dgMessage_Alarm.TabIndex = 1;
            dgMessage_Alarm.SelectionChanged += dgMessage_Alarm_SelectionChanged;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(tableLayoutPanel3);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(418, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(412, 436);
            panel4.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(panel5, 0, 1);
            tableLayoutPanel3.Controls.Add(panel6, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 47.6958542F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 52.3041458F));
            tableLayoutPanel3.Size = new Size(410, 434);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(btnSave);
            panel5.Controls.Add(chkByPass);
            panel5.Controls.Add(btnTest);
            panel5.Controls.Add(lblSubtitle);
            panel5.Controls.Add(gbSendingMode);
            panel5.Controls.Add(lblAppSelect);
            panel5.Controls.Add(cmbMessageSelect);
            panel5.Controls.Add(cmbAppSelect);
            panel5.Controls.Add(lblMessageSelect);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(3, 210);
            panel5.Name = "panel5";
            panel5.Size = new Size(404, 221);
            panel5.TabIndex = 0;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Left;
            btnSave.BackColor = Color.FromArgb(24, 119, 242);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(265, 160);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(117, 37);
            btnSave.TabIndex = 30;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // chkByPass
            // 
            chkByPass.Appearance = Appearance.Button;
            chkByPass.BackColor = Color.FromArgb(24, 119, 242);
            chkByPass.Checked = true;
            chkByPass.CheckState = CheckState.Checked;
            chkByPass.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkByPass.ForeColor = SystemColors.ControlLightLight;
            chkByPass.Location = new Point(265, 102);
            chkByPass.Margin = new Padding(4, 3, 4, 3);
            chkByPass.Name = "chkByPass";
            chkByPass.Size = new Size(117, 46);
            chkByPass.TabIndex = 1;
            chkByPass.Text = "By Pass";
            chkByPass.TextAlign = ContentAlignment.MiddleCenter;
            chkByPass.UseVisualStyleBackColor = false;
            chkByPass.CheckedChanged += chkByPass_CheckedChanged;
            // 
            // btnTest
            // 
            btnTest.Anchor = AnchorStyles.Left;
            btnTest.BackColor = Color.FromArgb(24, 119, 242);
            btnTest.FlatAppearance.BorderSize = 0;
            btnTest.FlatStyle = FlatStyle.Flat;
            btnTest.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTest.ForeColor = Color.White;
            btnTest.Location = new Point(265, 50);
            btnTest.Margin = new Padding(4, 3, 4, 3);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(117, 37);
            btnTest.TabIndex = 29;
            btnTest.Text = "Test";
            btnTest.UseVisualStyleBackColor = false;
            btnTest.Click += btnTest_Click;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Anchor = AnchorStyles.Left;
            lblSubtitle.AutoSize = true;
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.Location = new Point(69, 4);
            lblSubtitle.Margin = new Padding(4, 0, 4, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(246, 15);
            lblSubtitle.TabIndex = 22;
            lblSubtitle.Text = "Set up automatic alert and messaging system";
            // 
            // gbSendingMode
            // 
            gbSendingMode.Anchor = AnchorStyles.Left;
            gbSendingMode.Controls.Add(chkEnableAlert);
            gbSendingMode.Location = new Point(7, 41);
            gbSendingMode.Margin = new Padding(4, 3, 4, 3);
            gbSendingMode.Name = "gbSendingMode";
            gbSendingMode.Padding = new Padding(4, 3, 4, 3);
            gbSendingMode.Size = new Size(250, 46);
            gbSendingMode.TabIndex = 23;
            gbSendingMode.TabStop = false;
            gbSendingMode.Text = "Enable send message";
            // 
            // chkEnableAlert
            // 
            chkEnableAlert.AutoSize = true;
            chkEnableAlert.Checked = true;
            chkEnableAlert.CheckState = CheckState.Checked;
            chkEnableAlert.Location = new Point(9, 22);
            chkEnableAlert.Margin = new Padding(4, 3, 4, 3);
            chkEnableAlert.Name = "chkEnableAlert";
            chkEnableAlert.Size = new Size(229, 19);
            chkEnableAlert.TabIndex = 0;
            chkEnableAlert.Text = "Enable/Disable sending message alerts";
            // 
            // lblAppSelect
            // 
            lblAppSelect.Anchor = AnchorStyles.Left;
            lblAppSelect.AutoSize = true;
            lblAppSelect.Location = new Point(7, 96);
            lblAppSelect.Margin = new Padding(4, 0, 4, 0);
            lblAppSelect.Name = "lblAppSelect";
            lblAppSelect.Size = new Size(103, 15);
            lblAppSelect.TabIndex = 25;
            lblAppSelect.Text = "Select applycation";
            // 
            // cmbMessageSelect
            // 
            cmbMessageSelect.Anchor = AnchorStyles.Left;
            cmbMessageSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMessageSelect.FormattingEnabled = true;
            cmbMessageSelect.Location = new Point(7, 174);
            cmbMessageSelect.Margin = new Padding(4, 3, 4, 3);
            cmbMessageSelect.Name = "cmbMessageSelect";
            cmbMessageSelect.Size = new Size(226, 23);
            cmbMessageSelect.TabIndex = 26;
            // 
            // cmbAppSelect
            // 
            cmbAppSelect.Anchor = AnchorStyles.Left;
            cmbAppSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAppSelect.FormattingEnabled = true;
            cmbAppSelect.Location = new Point(7, 117);
            cmbAppSelect.Margin = new Padding(4, 3, 4, 3);
            cmbAppSelect.Name = "cmbAppSelect";
            cmbAppSelect.Size = new Size(226, 23);
            cmbAppSelect.TabIndex = 24;
            // 
            // lblMessageSelect
            // 
            lblMessageSelect.Anchor = AnchorStyles.Left;
            lblMessageSelect.AutoSize = true;
            lblMessageSelect.Location = new Point(7, 148);
            lblMessageSelect.Margin = new Padding(4, 0, 4, 0);
            lblMessageSelect.Name = "lblMessageSelect";
            lblMessageSelect.Size = new Size(104, 15);
            lblMessageSelect.TabIndex = 27;
            lblMessageSelect.Text = "Nội dung tin nhắn";
            // 
            // panel6
            // 
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Add(label4);
            panel6.Controls.Add(txbChatID);
            panel6.Controls.Add(btnDelete_Mes);
            panel6.Controls.Add(btnAdd_Mes);
            panel6.Controls.Add(btnUpdate_Mes);
            panel6.Controls.Add(label3);
            panel6.Controls.Add(txbPhone_num);
            panel6.Controls.Add(label2);
            panel6.Controls.Add(txbName);
            panel6.Controls.Add(label1);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(3, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(404, 201);
            panel6.TabIndex = 1;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(8, 138);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(46, 15);
            label4.TabIndex = 36;
            label4.Text = "Chat ID";
            // 
            // txbChatID
            // 
            txbChatID.Location = new Point(8, 161);
            txbChatID.Name = "txbChatID";
            txbChatID.Size = new Size(225, 23);
            txbChatID.TabIndex = 35;
            // 
            // btnDelete_Mes
            // 
            btnDelete_Mes.Anchor = AnchorStyles.Left;
            btnDelete_Mes.BackColor = Color.FromArgb(24, 119, 242);
            btnDelete_Mes.FlatAppearance.BorderSize = 0;
            btnDelete_Mes.FlatStyle = FlatStyle.Flat;
            btnDelete_Mes.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete_Mes.ForeColor = Color.White;
            btnDelete_Mes.Location = new Point(265, 154);
            btnDelete_Mes.Margin = new Padding(4, 3, 4, 3);
            btnDelete_Mes.Name = "btnDelete_Mes";
            btnDelete_Mes.Size = new Size(117, 30);
            btnDelete_Mes.TabIndex = 34;
            btnDelete_Mes.Text = "Delete";
            btnDelete_Mes.UseVisualStyleBackColor = false;
            btnDelete_Mes.Click += btnDelete_Mes_Click;
            // 
            // btnAdd_Mes
            // 
            btnAdd_Mes.Anchor = AnchorStyles.Left;
            btnAdd_Mes.BackColor = Color.FromArgb(24, 119, 242);
            btnAdd_Mes.FlatAppearance.BorderSize = 0;
            btnAdd_Mes.FlatStyle = FlatStyle.Flat;
            btnAdd_Mes.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd_Mes.ForeColor = Color.White;
            btnAdd_Mes.Location = new Point(265, 105);
            btnAdd_Mes.Margin = new Padding(4, 3, 4, 3);
            btnAdd_Mes.Name = "btnAdd_Mes";
            btnAdd_Mes.Size = new Size(117, 30);
            btnAdd_Mes.TabIndex = 33;
            btnAdd_Mes.Text = "Add";
            btnAdd_Mes.UseVisualStyleBackColor = false;
            btnAdd_Mes.Click += btnAdd_Mes_Click;
            // 
            // btnUpdate_Mes
            // 
            btnUpdate_Mes.Anchor = AnchorStyles.Left;
            btnUpdate_Mes.BackColor = Color.FromArgb(24, 119, 242);
            btnUpdate_Mes.FlatAppearance.BorderSize = 0;
            btnUpdate_Mes.FlatStyle = FlatStyle.Flat;
            btnUpdate_Mes.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpdate_Mes.ForeColor = Color.White;
            btnUpdate_Mes.Location = new Point(265, 49);
            btnUpdate_Mes.Margin = new Padding(4, 3, 4, 3);
            btnUpdate_Mes.Name = "btnUpdate_Mes";
            btnUpdate_Mes.Size = new Size(117, 30);
            btnUpdate_Mes.TabIndex = 30;
            btnUpdate_Mes.Text = "Update";
            btnUpdate_Mes.UseVisualStyleBackColor = false;
            btnUpdate_Mes.Click += btnUpdate_Mes_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(8, 82);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 32;
            label3.Text = "Phone number";
            // 
            // txbPhone_num
            // 
            txbPhone_num.Location = new Point(8, 105);
            txbPhone_num.Name = "txbPhone_num";
            txbPhone_num.Size = new Size(225, 23);
            txbPhone_num.TabIndex = 31;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(8, 31);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 30;
            label2.Text = "Name";
            // 
            // txbName
            // 
            txbName.Location = new Point(8, 49);
            txbName.Name = "txbName";
            txbName.Size = new Size(225, 23);
            txbName.TabIndex = 25;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.ForeColor = Color.Gray;
            label1.Location = new Point(96, 10);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(199, 15);
            label1.TabIndex = 24;
            label1.Text = "Manage warning message recipients";
            // 
            // FormConfigMessage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(843, 518);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormConfigMessage";
            Text = "FormConfigMessage";
            Load += FormConfigMessage_Load;
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgMessage_Alarm).EndInit();
            panel4.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            gbSendingMode.ResumeLayout(false);
            gbSendingMode.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel3;
        private Panel panel4;
        private DataGridView dgMessage_Alarm;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel5;
        private Button btnTest;
        private Label lblSubtitle;
        private GroupBox gbSendingMode;
        private CheckBox chkEnableAlert;
        private Label lblAppSelect;
        private ComboBox cmbMessageSelect;
        private ComboBox cmbAppSelect;
        private Label lblMessageSelect;
        private Panel panel6;
        private Label label2;
        private TextBox txbName;
        private Label label1;
        private Label label3;
        private TextBox txbPhone_num;
        private Button btnDelete_Mes;
        private Button btnAdd_Mes;
        private Button btnUpdate_Mes;
        private Label label4;
        private TextBox txbChatID;
        private CheckBox chkByPass;
        private Button btnSave;
    }
}
namespace CameraManager
{
    partial class FormLogView
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
            panel4 = new Panel();
            lbTitleHeader = new Label();
            btnMinimize = new Button();
            btnMaximum = new Button();
            btnExit = new Button();
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel2 = new Panel();
            tableLayoutPanel3 = new TableLayoutPanel();
            panelControl = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel7 = new Panel();
            btnPrevious = new Button();
            lbCurrrentPage = new Label();
            label4 = new Label();
            lbTotalPages2 = new Label();
            btnNext = new Button();
            lbTotalPages1 = new Label();
            label1 = new Label();
            panel9 = new Panel();
            btnExport = new Button();
            dgLogEvent = new DataGridView();
            panel3 = new Panel();
            panelLeft = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            btnOpenFolder = new Button();
            pictureImageView = new PictureBox();
            panelSearch = new Panel();
            btnSearch = new Button();
            groupBoxDateTime = new GroupBox();
            numericToHour = new NumericUpDown();
            label7 = new Label();
            numericFromHour = new NumericUpDown();
            label6 = new Label();
            dtPickerSelectDateEnd = new DateTimePicker();
            label16 = new Label();
            label17 = new Label();
            dtPickerSelectDateStart = new DateTimePicker();
            panel8 = new Panel();
            btnRefresh = new Button();
            label19 = new Label();
            pictureBox1 = new PictureBox();
            panel4.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panelControl.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel7.SuspendLayout();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgLogEvent).BeginInit();
            panel3.SuspendLayout();
            panelLeft.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureImageView).BeginInit();
            panelSearch.SuspendLayout();
            groupBoxDateTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericToHour).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericFromHour).BeginInit();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel4
            // 
            panel4.BackColor = Color.SteelBlue;
            panel4.Controls.Add(lbTitleHeader);
            panel4.Controls.Add(btnMinimize);
            panel4.Controls.Add(btnMaximum);
            panel4.Controls.Add(btnExit);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Margin = new Padding(4, 3, 4, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(1190, 32);
            panel4.TabIndex = 7;
            panel4.MouseDown += panelHeader_MouseDown;
            panel4.MouseMove += panelHeader_MouseMove;
            panel4.MouseUp += panelHeader_MouseUp;
            // 
            // lbTitleHeader
            // 
            lbTitleHeader.AutoSize = true;
            lbTitleHeader.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTitleHeader.ForeColor = Color.LightSkyBlue;
            lbTitleHeader.Location = new Point(4, 7);
            lbTitleHeader.Margin = new Padding(4, 0, 4, 0);
            lbTitleHeader.Name = "lbTitleHeader";
            lbTitleHeader.Size = new Size(62, 16);
            lbTitleHeader.TabIndex = 12;
            lbTitleHeader.Text = "Log View";
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.BackgroundImageLayout = ImageLayout.Stretch;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnMinimize.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMinimize.ForeColor = Color.White;
            btnMinimize.Image = Properties.Resources.icons8_minimize_window_28;
            btnMinimize.Location = new Point(1088, 3);
            btnMinimize.Margin = new Padding(4, 3, 4, 3);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(28, 25);
            btnMinimize.TabIndex = 8;
            btnMinimize.TextAlign = ContentAlignment.BottomCenter;
            btnMinimize.UseVisualStyleBackColor = true;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnMaximum
            // 
            btnMaximum.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaximum.BackgroundImageLayout = ImageLayout.Stretch;
            btnMaximum.FlatAppearance.BorderSize = 0;
            btnMaximum.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnMaximum.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnMaximum.FlatStyle = FlatStyle.Flat;
            btnMaximum.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMaximum.ForeColor = Color.White;
            btnMaximum.Image = Properties.Resources.icons8_rectangle_28;
            btnMaximum.Location = new Point(1124, 3);
            btnMaximum.Margin = new Padding(4, 3, 4, 3);
            btnMaximum.Name = "btnMaximum";
            btnMaximum.Size = new Size(28, 25);
            btnMaximum.TabIndex = 7;
            btnMaximum.TextAlign = ContentAlignment.BottomCenter;
            btnMaximum.UseVisualStyleBackColor = true;
            btnMaximum.Click += btnMaximum_Click;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExit.BackgroundImageLayout = ImageLayout.Stretch;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnExit.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.ForeColor = Color.White;
            btnExit.Image = Properties.Resources.icons8_close_window_28;
            btnExit.Location = new Point(1158, 3);
            btnExit.Margin = new Padding(4, 3, 4, 3);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(28, 25);
            btnExit.TabIndex = 6;
            btnExit.TextAlign = ContentAlignment.BottomCenter;
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 32);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1190, 746);
            panel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 331F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(panel2, 1, 0);
            tableLayoutPanel2.Controls.Add(panel3, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1190, 746);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(tableLayoutPanel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(335, 3);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(851, 740);
            panel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(panelControl, 0, 1);
            tableLayoutPanel3.Controls.Add(dgLogEvent, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            tableLayoutPanel3.Size = new Size(851, 740);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // panelControl
            // 
            panelControl.Controls.Add(tableLayoutPanel1);
            panelControl.Dock = DockStyle.Fill;
            panelControl.Location = new Point(4, 685);
            panelControl.Margin = new Padding(4, 3, 4, 3);
            panelControl.Name = "panelControl";
            panelControl.Size = new Size(843, 52);
            panelControl.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 89.36464F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.63536F));
            tableLayoutPanel1.Controls.Add(panel7, 0, 0);
            tableLayoutPanel1.Controls.Add(panel9, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(843, 52);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel7
            // 
            panel7.Controls.Add(btnPrevious);
            panel7.Controls.Add(lbCurrrentPage);
            panel7.Controls.Add(label4);
            panel7.Controls.Add(lbTotalPages2);
            panel7.Controls.Add(btnNext);
            panel7.Controls.Add(lbTotalPages1);
            panel7.Controls.Add(label1);
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(4, 3);
            panel7.Margin = new Padding(4, 3, 4, 3);
            panel7.Name = "panel7";
            panel7.Size = new Size(745, 46);
            panel7.TabIndex = 0;
            // 
            // btnPrevious
            // 
            btnPrevious.Location = new Point(182, 8);
            btnPrevious.Margin = new Padding(4, 3, 4, 3);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(89, 29);
            btnPrevious.TabIndex = 13;
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += btnPrevious_Click;
            // 
            // lbCurrrentPage
            // 
            lbCurrrentPage.AutoSize = true;
            lbCurrrentPage.Location = new Point(458, 14);
            lbCurrrentPage.Margin = new Padding(4, 0, 4, 0);
            lbCurrrentPage.Name = "lbCurrrentPage";
            lbCurrrentPage.Size = new Size(13, 15);
            lbCurrrentPage.TabIndex = 12;
            lbCurrrentPage.Text = "1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(481, 14);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(12, 15);
            label4.TabIndex = 11;
            label4.Text = "/";
            // 
            // lbTotalPages2
            // 
            lbTotalPages2.AutoSize = true;
            lbTotalPages2.Location = new Point(497, 14);
            lbTotalPages2.Margin = new Padding(4, 0, 4, 0);
            lbTotalPages2.Name = "lbTotalPages2";
            lbTotalPages2.Size = new Size(13, 15);
            lbTotalPages2.TabIndex = 10;
            lbTotalPages2.Text = "2";
            // 
            // btnNext
            // 
            btnNext.Location = new Point(624, 8);
            btnNext.Margin = new Padding(4, 3, 4, 3);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(91, 29);
            btnNext.TabIndex = 9;
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // lbTotalPages1
            // 
            lbTotalPages1.AutoSize = true;
            lbTotalPages1.Location = new Point(49, 15);
            lbTotalPages1.Margin = new Padding(4, 0, 4, 0);
            lbTotalPages1.Name = "lbTotalPages1";
            lbTotalPages1.Size = new Size(16, 15);
            lbTotalPages1.TabIndex = 8;
            lbTotalPages1.Text = "...";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 15);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 7;
            label1.Text = "Total: ";
            // 
            // panel9
            // 
            panel9.Controls.Add(btnExport);
            panel9.Dock = DockStyle.Fill;
            panel9.Location = new Point(757, 3);
            panel9.Margin = new Padding(4, 3, 4, 3);
            panel9.Name = "panel9";
            panel9.Size = new Size(82, 46);
            panel9.TabIndex = 1;
            // 
            // btnExport
            // 
            btnExport.Dock = DockStyle.Fill;
            btnExport.Location = new Point(0, 0);
            btnExport.Margin = new Padding(4, 3, 4, 3);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(82, 46);
            btnExport.TabIndex = 0;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // dgLogEvent
            // 
            dgLogEvent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgLogEvent.Dock = DockStyle.Fill;
            dgLogEvent.Location = new Point(4, 3);
            dgLogEvent.Margin = new Padding(4, 3, 4, 3);
            dgLogEvent.Name = "dgLogEvent";
            dgLogEvent.ReadOnly = true;
            dgLogEvent.RowHeadersVisible = false;
            dgLogEvent.Size = new Size(843, 676);
            dgLogEvent.TabIndex = 1;
            dgLogEvent.SelectionChanged += dgLogEvent_SelectionChanged;
            // 
            // panel3
            // 
            panel3.AutoScroll = true;
            panel3.Controls.Add(panelLeft);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(4, 3);
            panel3.Margin = new Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(323, 740);
            panel3.TabIndex = 1;
            // 
            // panelLeft
            // 
            panelLeft.AutoScroll = true;
            panelLeft.Controls.Add(panel5);
            panelLeft.Controls.Add(label19);
            panelLeft.Controls.Add(pictureBox1);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            panelLeft.Location = new Point(0, 0);
            panelLeft.Margin = new Padding(4, 3, 4, 3);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(316, 740);
            panelLeft.TabIndex = 1;
            // 
            // panel5
            // 
            panel5.Controls.Add(panel6);
            panel5.Controls.Add(panelSearch);
            panel5.Controls.Add(groupBoxDateTime);
            panel5.Controls.Add(panel8);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 187);
            panel5.Margin = new Padding(4, 3, 4, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(299, 891);
            panel5.TabIndex = 2;
            // 
            // panel6
            // 
            panel6.Controls.Add(btnOpenFolder);
            panel6.Controls.Add(pictureImageView);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 283);
            panel6.Margin = new Padding(4, 3, 4, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(299, 608);
            panel6.TabIndex = 5;
            // 
            // btnOpenFolder
            // 
            btnOpenFolder.Dock = DockStyle.Top;
            btnOpenFolder.Location = new Point(0, 223);
            btnOpenFolder.Margin = new Padding(4, 3, 4, 3);
            btnOpenFolder.Name = "btnOpenFolder";
            btnOpenFolder.Size = new Size(299, 37);
            btnOpenFolder.TabIndex = 8;
            btnOpenFolder.Text = "Open Folder";
            btnOpenFolder.UseVisualStyleBackColor = true;
            btnOpenFolder.Click += btnOpenFolder_Click;
            // 
            // pictureImageView
            // 
            pictureImageView.Dock = DockStyle.Top;
            pictureImageView.Location = new Point(0, 0);
            pictureImageView.Margin = new Padding(4, 3, 4, 3);
            pictureImageView.Name = "pictureImageView";
            pictureImageView.Size = new Size(299, 223);
            pictureImageView.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureImageView.TabIndex = 7;
            pictureImageView.TabStop = false;
            // 
            // panelSearch
            // 
            panelSearch.Controls.Add(btnSearch);
            panelSearch.Dock = DockStyle.Top;
            panelSearch.Location = new Point(0, 237);
            panelSearch.Margin = new Padding(4, 3, 4, 3);
            panelSearch.Name = "panelSearch";
            panelSearch.Size = new Size(299, 46);
            panelSearch.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.Dock = DockStyle.Fill;
            btnSearch.Location = new Point(0, 0);
            btnSearch.Margin = new Padding(4, 3, 4, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(299, 46);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // groupBoxDateTime
            // 
            groupBoxDateTime.Controls.Add(numericToHour);
            groupBoxDateTime.Controls.Add(label7);
            groupBoxDateTime.Controls.Add(numericFromHour);
            groupBoxDateTime.Controls.Add(label6);
            groupBoxDateTime.Controls.Add(dtPickerSelectDateEnd);
            groupBoxDateTime.Controls.Add(label16);
            groupBoxDateTime.Controls.Add(label17);
            groupBoxDateTime.Controls.Add(dtPickerSelectDateStart);
            groupBoxDateTime.Dock = DockStyle.Top;
            groupBoxDateTime.Location = new Point(0, 70);
            groupBoxDateTime.Margin = new Padding(4, 3, 4, 3);
            groupBoxDateTime.Name = "groupBoxDateTime";
            groupBoxDateTime.Padding = new Padding(4, 3, 4, 3);
            groupBoxDateTime.Size = new Size(299, 167);
            groupBoxDateTime.TabIndex = 3;
            groupBoxDateTime.TabStop = false;
            groupBoxDateTime.Text = "Select Date  Time";
            // 
            // numericToHour
            // 
            numericToHour.Location = new Point(119, 128);
            numericToHour.Margin = new Padding(4, 3, 4, 3);
            numericToHour.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            numericToHour.Name = "numericToHour";
            numericToHour.Size = new Size(58, 22);
            numericToHour.TabIndex = 11;
            numericToHour.Value = new decimal(new int[] { 23, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(30, 30, 30);
            label7.Location = new Point(14, 128);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(56, 16);
            label7.TabIndex = 10;
            label7.Text = "To Hour";
            // 
            // numericFromHour
            // 
            numericFromHour.Location = new Point(119, 59);
            numericFromHour.Margin = new Padding(4, 3, 4, 3);
            numericFromHour.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            numericFromHour.Name = "numericFromHour";
            numericFromHour.Size = new Size(58, 22);
            numericFromHour.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(30, 30, 30);
            label6.Location = new Point(13, 61);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(70, 16);
            label6.TabIndex = 8;
            label6.Text = "From Hour";
            // 
            // dtPickerSelectDateEnd
            // 
            dtPickerSelectDateEnd.CustomFormat = "yyyy/M/d";
            dtPickerSelectDateEnd.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtPickerSelectDateEnd.Format = DateTimePickerFormat.Custom;
            dtPickerSelectDateEnd.Location = new Point(119, 92);
            dtPickerSelectDateEnd.Margin = new Padding(4, 3, 4, 3);
            dtPickerSelectDateEnd.Name = "dtPickerSelectDateEnd";
            dtPickerSelectDateEnd.Size = new Size(160, 26);
            dtPickerSelectDateEnd.TabIndex = 7;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label16.ForeColor = Color.FromArgb(30, 30, 30);
            label16.Location = new Point(14, 103);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(63, 16);
            label16.TabIndex = 6;
            label16.Text = "Date End";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label17.ForeColor = Color.FromArgb(30, 30, 30);
            label17.Location = new Point(14, 33);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(66, 16);
            label17.TabIndex = 4;
            label17.Text = "Date Start";
            // 
            // dtPickerSelectDateStart
            // 
            dtPickerSelectDateStart.CustomFormat = "yyyy/M/d";
            dtPickerSelectDateStart.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtPickerSelectDateStart.Format = DateTimePickerFormat.Custom;
            dtPickerSelectDateStart.Location = new Point(119, 23);
            dtPickerSelectDateStart.Margin = new Padding(4, 3, 4, 3);
            dtPickerSelectDateStart.Name = "dtPickerSelectDateStart";
            dtPickerSelectDateStart.Size = new Size(160, 26);
            dtPickerSelectDateStart.TabIndex = 1;
            // 
            // panel8
            // 
            panel8.Controls.Add(btnRefresh);
            panel8.Dock = DockStyle.Top;
            panel8.Location = new Point(0, 0);
            panel8.Margin = new Padding(4, 3, 4, 3);
            panel8.Name = "panel8";
            panel8.Size = new Size(299, 70);
            panel8.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.Dock = DockStyle.Fill;
            btnRefresh.Location = new Point(0, 0);
            btnRefresh.Margin = new Padding(4, 3, 4, 3);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(299, 70);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // label19
            // 
            label19.Dock = DockStyle.Top;
            label19.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label19.ForeColor = Color.Orange;
            label19.Location = new Point(0, 135);
            label19.Margin = new Padding(4, 0, 4, 0);
            label19.Name = "label19";
            label19.Size = new Size(299, 52);
            label19.TabIndex = 0;
            label19.Text = "STATISTIC DATA";
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(299, 135);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // FormLogView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1190, 778);
            Controls.Add(panel1);
            Controls.Add(panel4);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormLogView";
            Text = "FormLogView";
            FormClosing += FormLogView_FormClosing;
            Load += FormLogView_Load;
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            panelControl.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgLogEvent).EndInit();
            panel3.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureImageView).EndInit();
            panelSearch.ResumeLayout(false);
            groupBoxDateTime.ResumeLayout(false);
            groupBoxDateTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericToHour).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericFromHour).EndInit();
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbTitleHeader;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMaximum;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.DataGridView dgLogEvent;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBoxDateTime;
        private System.Windows.Forms.NumericUpDown numericToHour;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericFromHour;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtPickerSelectDateEnd;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dtPickerSelectDateStart;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.PictureBox pictureImageView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lbCurrrentPage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbTotalPages2;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lbTotalPages1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnExport;
    }
}
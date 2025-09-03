namespace CameraManager
{
    partial class FormCameraList
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
            panelHeader = new Panel();
            lbTitleHeader = new Label();
            btnMinimize = new Button();
            btnMaximum = new Button();
            btnExit = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            tableLayoutPanel3 = new TableLayoutPanel();
            panel4 = new Panel();
            btnUpdate = new Button();
            btnDeleteCamera = new Button();
            btnAddCamera = new Button();
            txbType = new TextBox();
            label8 = new Label();
            txbPassword = new TextBox();
            label7 = new Label();
            txbUserID = new TextBox();
            label6 = new Label();
            txbIPCamera = new TextBox();
            label5 = new Label();
            txbRtsp = new TextBox();
            label4 = new Label();
            txbCameraName = new TextBox();
            label3 = new Label();
            txbIDCamera = new TextBox();
            label2 = new Label();
            label1 = new Label();
            panel5 = new Panel();
            dgviewCameraDetail = new DataGridView();
            panelHeader.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgviewCameraDetail).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.SteelBlue;
            panelHeader.Controls.Add(lbTitleHeader);
            panelHeader.Controls.Add(btnMinimize);
            panelHeader.Controls.Add(btnMaximum);
            panelHeader.Controls.Add(btnExit);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1048, 28);
            panelHeader.TabIndex = 6;
            panelHeader.MouseDown += panelHeader_MouseDown;
            panelHeader.MouseMove += panelHeader_MouseMove;
            panelHeader.MouseUp += panelHeader_MouseUp;
            // 
            // lbTitleHeader
            // 
            lbTitleHeader.AutoSize = true;
            lbTitleHeader.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTitleHeader.ForeColor = Color.LightSkyBlue;
            lbTitleHeader.Location = new Point(3, 6);
            lbTitleHeader.Name = "lbTitleHeader";
            lbTitleHeader.Size = new Size(78, 16);
            lbTitleHeader.TabIndex = 12;
            lbTitleHeader.Text = "Camera List";
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
            btnMinimize.Location = new Point(961, 3);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(24, 22);
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
            btnMaximum.Location = new Point(991, 3);
            btnMaximum.Name = "btnMaximum";
            btnMaximum.Size = new Size(24, 22);
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
            btnExit.Location = new Point(1021, 3);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(24, 22);
            btnExit.TabIndex = 6;
            btnExit.TextAlign = ContentAlignment.BottomCenter;
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 28);
            panel2.Name = "panel2";
            panel2.Size = new Size(1048, 512);
            panel2.TabIndex = 7;
            // 
            // panel3
            // 
            panel3.Controls.Add(tableLayoutPanel3);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1048, 512);
            panel3.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = SystemColors.AppWorkspace;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(panel4, 0, 0);
            tableLayoutPanel3.Controls.Add(panel5, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1048, 512);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ControlDark;
            panel4.Controls.Add(btnUpdate);
            panel4.Controls.Add(btnDeleteCamera);
            panel4.Controls.Add(btnAddCamera);
            panel4.Controls.Add(txbType);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(txbPassword);
            panel4.Controls.Add(label7);
            panel4.Controls.Add(txbUserID);
            panel4.Controls.Add(label6);
            panel4.Controls.Add(txbIPCamera);
            panel4.Controls.Add(label5);
            panel4.Controls.Add(txbRtsp);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(txbCameraName);
            panel4.Controls.Add(label3);
            panel4.Controls.Add(txbIDCamera);
            panel4.Controls.Add(label2);
            panel4.Controls.Add(label1);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(244, 506);
            panel4.TabIndex = 0;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = SystemColors.ControlLightLight;
            btnUpdate.Location = new Point(0, 264);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(241, 31);
            btnUpdate.TabIndex = 17;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDeleteCamera
            // 
            btnDeleteCamera.BackColor = SystemColors.ControlLightLight;
            btnDeleteCamera.Location = new Point(0, 338);
            btnDeleteCamera.Name = "btnDeleteCamera";
            btnDeleteCamera.Size = new Size(241, 31);
            btnDeleteCamera.TabIndex = 16;
            btnDeleteCamera.Text = "Delete";
            btnDeleteCamera.UseVisualStyleBackColor = false;
            btnDeleteCamera.Click += btnDeleteCamera_Click;
            // 
            // btnAddCamera
            // 
            btnAddCamera.BackColor = SystemColors.ControlLightLight;
            btnAddCamera.Location = new Point(0, 301);
            btnAddCamera.Name = "btnAddCamera";
            btnAddCamera.Size = new Size(241, 31);
            btnAddCamera.TabIndex = 15;
            btnAddCamera.Text = "Add New";
            btnAddCamera.UseVisualStyleBackColor = false;
            btnAddCamera.Click += btnAddCamera_Click;
            // 
            // txbType
            // 
            txbType.Location = new Point(87, 215);
            txbType.Name = "txbType";
            txbType.Size = new Size(151, 24);
            txbType.TabIndex = 14;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 218);
            label8.Name = "label8";
            label8.Size = new Size(41, 19);
            label8.TabIndex = 13;
            label8.Text = "Type:";
            // 
            // txbPassword
            // 
            txbPassword.Location = new Point(87, 185);
            txbPassword.Name = "txbPassword";
            txbPassword.Size = new Size(151, 24);
            txbPassword.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 188);
            label7.Name = "label7";
            label7.Size = new Size(70, 19);
            label7.TabIndex = 11;
            label7.Text = "Password:";
            // 
            // txbUserID
            // 
            txbUserID.Location = new Point(87, 155);
            txbUserID.Name = "txbUserID";
            txbUserID.Size = new Size(151, 24);
            txbUserID.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 158);
            label6.Name = "label6";
            label6.Size = new Size(54, 19);
            label6.TabIndex = 9;
            label6.Text = "UserID:";
            // 
            // txbIPCamera
            // 
            txbIPCamera.Location = new Point(87, 125);
            txbIPCamera.Name = "txbIPCamera";
            txbIPCamera.Size = new Size(151, 24);
            txbIPCamera.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 128);
            label5.Name = "label5";
            label5.Size = new Size(58, 19);
            label5.TabIndex = 7;
            label5.Text = "IP Addr:";
            // 
            // txbRtsp
            // 
            txbRtsp.Enabled = false;
            txbRtsp.Location = new Point(87, 95);
            txbRtsp.Name = "txbRtsp";
            txbRtsp.Size = new Size(151, 24);
            txbRtsp.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 98);
            label4.Name = "label4";
            label4.Size = new Size(72, 19);
            label4.TabIndex = 5;
            label4.Text = "RTSP URL:";
            // 
            // txbCameraName
            // 
            txbCameraName.Location = new Point(87, 65);
            txbCameraName.Name = "txbCameraName";
            txbCameraName.Size = new Size(151, 24);
            txbCameraName.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 68);
            label3.Name = "label3";
            label3.Size = new Size(49, 19);
            label3.TabIndex = 3;
            label3.Text = "Name:";
            // 
            // txbIDCamera
            // 
            txbIDCamera.Location = new Point(87, 35);
            txbIDCamera.Name = "txbIDCamera";
            txbIDCamera.Size = new Size(151, 24);
            txbIDCamera.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 38);
            label2.Name = "label2";
            label2.Size = new Size(26, 19);
            label2.TabIndex = 1;
            label2.Text = "ID:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 10);
            label1.Name = "label1";
            label1.Size = new Size(85, 19);
            label1.TabIndex = 0;
            label1.Text = "Camera Info";
            // 
            // panel5
            // 
            panel5.Controls.Add(dgviewCameraDetail);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(253, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(792, 506);
            panel5.TabIndex = 1;
            // 
            // dgviewCameraDetail
            // 
            dgviewCameraDetail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgviewCameraDetail.Dock = DockStyle.Fill;
            dgviewCameraDetail.Location = new Point(0, 0);
            dgviewCameraDetail.Name = "dgviewCameraDetail";
            dgviewCameraDetail.ReadOnly = true;
            dgviewCameraDetail.RowHeadersVisible = false;
            dgviewCameraDetail.Size = new Size(792, 506);
            dgviewCameraDetail.TabIndex = 0;
            dgviewCameraDetail.SelectionChanged += dgviewCameraDetail_SelectionChanged;
            // 
            // FormCameraList
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1048, 540);
            Controls.Add(panel2);
            Controls.Add(panelHeader);
            Font = new Font("Microsoft YaHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "FormCameraList";
            StartPosition = FormStartPosition.Manual;
            Text = "FormConfirmVision";
            FormClosing += FormConfirmVision_FormClosing;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgviewCameraDetail).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lbTitleHeader;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMaximum;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnDeleteCamera;
        private System.Windows.Forms.Button btnAddCamera;
        private System.Windows.Forms.TextBox txbType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txbUserID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbIPCamera;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbRtsp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbCameraName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbIDCamera;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView dgviewCameraDetail;
    }
}
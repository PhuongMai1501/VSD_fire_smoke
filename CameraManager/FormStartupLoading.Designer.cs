namespace CameraManager
{
    partial class FormStartupLoading
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
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            panelContainer = new Panel();
            lbProgramName = new Label();
            lbReleasedDate = new Label();
            lbProgramVersion = new Label();
            label2 = new Label();
            pictureBox2 = new PictureBox();
            panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Interval = 50;
            timer1.Tick += timer1_Tick;
            // 
            // panelContainer
            // 
            panelContainer.BackgroundImage = Properties.Resources.img_3734;
            panelContainer.BackgroundImageLayout = ImageLayout.Stretch;
            panelContainer.Controls.Add(lbProgramName);
            panelContainer.Controls.Add(lbReleasedDate);
            panelContainer.Controls.Add(lbProgramVersion);
            panelContainer.Controls.Add(label2);
            panelContainer.Controls.Add(pictureBox2);
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Location = new Point(0, 0);
            panelContainer.Margin = new Padding(4, 3, 4, 3);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(670, 418);
            panelContainer.TabIndex = 4;
            // 
            // lbProgramName
            // 
            lbProgramName.BackColor = Color.Transparent;
            lbProgramName.Font = new Font("Microsoft YaHei UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbProgramName.ForeColor = Color.Blue;
            lbProgramName.Location = new Point(326, 37);
            lbProgramName.Margin = new Padding(4, 0, 4, 0);
            lbProgramName.Name = "lbProgramName";
            lbProgramName.Size = new Size(304, 149);
            lbProgramName.TabIndex = 11;
            lbProgramName.Text = "PROGRAM NAME";
            lbProgramName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbReleasedDate
            // 
            lbReleasedDate.BackColor = Color.Transparent;
            lbReleasedDate.Font = new Font("Microsoft YaHei", 11.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lbReleasedDate.ForeColor = Color.White;
            lbReleasedDate.Location = new Point(435, 384);
            lbReleasedDate.Margin = new Padding(4, 0, 4, 0);
            lbReleasedDate.Name = "lbReleasedDate";
            lbReleasedDate.Size = new Size(231, 25);
            lbReleasedDate.TabIndex = 13;
            lbReleasedDate.Text = "Released Date";
            lbReleasedDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lbProgramVersion
            // 
            lbProgramVersion.BackColor = Color.Transparent;
            lbProgramVersion.Font = new Font("Microsoft YaHei", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbProgramVersion.ForeColor = Color.White;
            lbProgramVersion.Location = new Point(439, 345);
            lbProgramVersion.Margin = new Padding(4, 0, 4, 0);
            lbProgramVersion.Name = "lbProgramVersion";
            lbProgramVersion.Size = new Size(231, 36);
            lbProgramVersion.TabIndex = 12;
            lbProgramVersion.Text = "VER 1.0.0";
            lbProgramVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Blue;
            label2.Location = new Point(485, 9);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(181, 28);
            label2.TabIndex = 4;
            label2.Text = "VISION CAMERA";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = Properties.Resources.load_32_256;
            pictureBox2.Location = new Point(222, 172);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(210, 106);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // FormStartupLoading
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(670, 418);
            Controls.Add(panelContainer);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormStartupLoading";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormStartupLoading";
            Load += FormStartupLoading_Load;
            panelContainer.ResumeLayout(false);
            panelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Label lbProgramName;
        private System.Windows.Forms.Label lbReleasedDate;
        private System.Windows.Forms.Label lbProgramVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
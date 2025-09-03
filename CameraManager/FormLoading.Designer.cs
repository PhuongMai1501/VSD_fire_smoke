namespace CameraManager
{
    partial class FormLoading
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
            lbTitleName = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lbTitleName
            // 
            lbTitleName.Dock = DockStyle.Top;
            lbTitleName.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTitleName.ForeColor = Color.DeepSkyBlue;
            lbTitleName.Location = new Point(0, 0);
            lbTitleName.Name = "lbTitleName";
            lbTitleName.Size = new Size(273, 42);
            lbTitleName.TabIndex = 1;
            lbTitleName.Text = "LOADING";
            lbTitleName.TextAlign = ContentAlignment.MiddleCenter;
            lbTitleName.MouseDown += FormMain_MouseDown;
            lbTitleName.MouseMove += FormMain_MouseMove;
            lbTitleName.MouseUp += FormMain_MouseUp;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 800;
            timer1.Tick += timer1_Tick;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = CameraManager.Properties.Resources.load_32_256;
            pictureBox1.Location = new Point(17, 47);
            pictureBox1.Margin = new Padding(5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(240, 167);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.MouseDown += FormMain_MouseDown;
            pictureBox1.MouseMove += FormMain_MouseMove;
            pictureBox1.MouseUp += FormMain_MouseUp;
            // 
            // FormLoading
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(273, 219);
            Controls.Add(lbTitleName);
            Controls.Add(pictureBox1);
            Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5);
            Name = "FormLoading";
            Opacity = 0.7D;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Loading";
            MouseDown += FormMain_MouseDown;
            MouseMove += FormMain_MouseMove;
            MouseUp += FormMain_MouseUp;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbTitleName;
        private System.Windows.Forms.Timer timer1;
    }
}
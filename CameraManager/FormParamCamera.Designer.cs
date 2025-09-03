namespace CameraManager
{
    partial class FormParamCamera
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
            panelMain = new Panel();
            panelHeader.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(134, 175, 215);
            panelHeader.Controls.Add(lbTitleHeader);
            panelHeader.Controls.Add(btnMinimize);
            panelHeader.Controls.Add(btnMaximum);
            panelHeader.Controls.Add(btnExit);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(4, 3, 4, 3);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1002, 32);
            panelHeader.TabIndex = 13;
            panelHeader.MouseDown += panelHeader_MouseDown;
            panelHeader.MouseMove += panelHeader_MouseMove;
            panelHeader.MouseUp += panelHeader_MouseUp;
            // 
            // lbTitleHeader
            // 
            lbTitleHeader.AutoSize = true;
            lbTitleHeader.Font = new Font("Microsoft YaHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTitleHeader.ForeColor = Color.MidnightBlue;
            lbTitleHeader.Location = new Point(4, 7);
            lbTitleHeader.Margin = new Padding(4, 0, 4, 0);
            lbTitleHeader.Name = "lbTitleHeader";
            lbTitleHeader.Size = new Size(103, 19);
            lbTitleHeader.TabIndex = 11;
            lbTitleHeader.Text = "Program Name";
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
            btnMinimize.Location = new Point(899, 6);
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
            btnMaximum.Location = new Point(934, 6);
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
            btnExit.FlatAppearance.MouseDownBackColor = Color.Tomato;
            btnExit.FlatAppearance.MouseOverBackColor = Color.Red;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.ForeColor = Color.White;
            btnExit.Image = Properties.Resources.icons8_close_window_28;
            btnExit.Location = new Point(969, 6);
            btnExit.Margin = new Padding(4, 3, 4, 3);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(28, 25);
            btnExit.TabIndex = 6;
            btnExit.TextAlign = ContentAlignment.BottomCenter;
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // panelMain
            // 
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 32);
            panelMain.Margin = new Padding(4, 3, 4, 3);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1002, 592);
            panelMain.TabIndex = 14;
            // 
            // FormParamCamera
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1002, 624);
            Controls.Add(panelMain);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormParamCamera";
            Text = "FormParamCamera";
            Load += FormParamCamera_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lbTitleHeader;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMaximum;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panelMain;
    }
}
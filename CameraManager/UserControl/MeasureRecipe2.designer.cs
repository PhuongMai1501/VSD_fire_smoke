namespace CameraManager
{
    partial class MeasureRecipe2
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelPage1Setting = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel3 = new Panel();
            panelBaseTool = new Panel();
            dgviewCamera = new DataGridView();
            panel4 = new Panel();
            btnRefreshRecipe = new Button();
            btnAddToolBase = new Button();
            lbTitleName = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            btnApply = new Button();
            numSmoke_Sen = new NumericUpDown();
            label3 = new Label();
            numFlame_Sen = new NumericUpDown();
            label2 = new Label();
            numInterval = new NumericUpDown();
            label1 = new Label();
            panel2 = new Panel();
            panelMain = new Panel();
            toolStrip1 = new ToolStrip();
            toolStripRunImage = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripOpenImage = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripProcessTime = new ToolStripLabel();
            toolStripSeparator6 = new ToolStripSeparator();
            toolStripSeparator7 = new ToolStripSeparator();
            toolStripStatus = new ToolStripLabel();
            toolStripLabel2 = new ToolStripLabel();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            commonGraphicToolStripMenuItem = new ToolStripMenuItem();
            showResultGraphicToolStripMenuItem = new ToolStripMenuItem();
            showTextGraphicToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator8 = new ToolStripSeparator();
            toolStripLabel3 = new ToolStripLabel();
            toolStripRuntime = new ToolStripButton();
            mySqlDataAdapter1 = new MySql.Data.MySqlClient.MySqlDataAdapter();
            panelPage1Setting.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel3.SuspendLayout();
            panelBaseTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgviewCamera).BeginInit();
            panel4.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSmoke_Sen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numFlame_Sen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numInterval).BeginInit();
            panel2.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panelPage1Setting
            // 
            panelPage1Setting.AutoScroll = true;
            panelPage1Setting.BackColor = SystemColors.ControlLightLight;
            panelPage1Setting.BorderStyle = BorderStyle.FixedSingle;
            panelPage1Setting.Controls.Add(tableLayoutPanel2);
            panelPage1Setting.Controls.Add(lbTitleName);
            panelPage1Setting.Dock = DockStyle.Fill;
            panelPage1Setting.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            panelPage1Setting.Location = new Point(3, 3);
            panelPage1Setting.Name = "panelPage1Setting";
            panelPage1Setting.Size = new Size(294, 817);
            panelPage1Setting.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(panel3, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 32);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(292, 783);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Controls.Add(panelBaseTool);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(286, 777);
            panel3.TabIndex = 0;
            // 
            // panelBaseTool
            // 
            panelBaseTool.Controls.Add(dgviewCamera);
            panelBaseTool.Controls.Add(panel4);
            panelBaseTool.Dock = DockStyle.Fill;
            panelBaseTool.Location = new Point(0, 0);
            panelBaseTool.Name = "panelBaseTool";
            panelBaseTool.Size = new Size(286, 777);
            panelBaseTool.TabIndex = 20;
            // 
            // dgviewCamera
            // 
            dgviewCamera.AllowUserToAddRows = false;
            dgviewCamera.AllowUserToDeleteRows = false;
            dgviewCamera.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgviewCamera.Dock = DockStyle.Fill;
            dgviewCamera.Location = new Point(0, 40);
            dgviewCamera.Margin = new Padding(0);
            dgviewCamera.Name = "dgviewCamera";
            dgviewCamera.ReadOnly = true;
            dgviewCamera.RowHeadersVisible = false;
            dgviewCamera.RowHeadersWidth = 51;
            dgviewCamera.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgviewCamera.Size = new Size(286, 737);
            dgviewCamera.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(btnRefreshRecipe);
            panel4.Controls.Add(btnAddToolBase);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(286, 40);
            panel4.TabIndex = 0;
            // 
            // btnRefreshRecipe
            // 
            btnRefreshRecipe.BackColor = SystemColors.ControlLightLight;
            btnRefreshRecipe.BackgroundImageLayout = ImageLayout.Zoom;
            btnRefreshRecipe.Dock = DockStyle.Left;
            btnRefreshRecipe.FlatAppearance.BorderSize = 0;
            btnRefreshRecipe.FlatAppearance.MouseDownBackColor = Color.Gray;
            btnRefreshRecipe.FlatAppearance.MouseOverBackColor = Color.Gray;
            btnRefreshRecipe.FlatStyle = FlatStyle.Flat;
            btnRefreshRecipe.Image = Properties.Resources.synchronize_28px;
            btnRefreshRecipe.Location = new Point(0, 0);
            btnRefreshRecipe.Name = "btnRefreshRecipe";
            btnRefreshRecipe.Size = new Size(50, 38);
            btnRefreshRecipe.TabIndex = 16;
            btnRefreshRecipe.UseVisualStyleBackColor = false;
            btnRefreshRecipe.Click += btnRefreshRecipe_Click;
            // 
            // btnAddToolBase
            // 
            btnAddToolBase.Dock = DockStyle.Right;
            btnAddToolBase.FlatAppearance.BorderSize = 0;
            btnAddToolBase.FlatStyle = FlatStyle.Flat;
            btnAddToolBase.Image = Properties.Resources.add_new_28px;
            btnAddToolBase.Location = new Point(68, 0);
            btnAddToolBase.Name = "btnAddToolBase";
            btnAddToolBase.Size = new Size(216, 38);
            btnAddToolBase.TabIndex = 1;
            btnAddToolBase.Text = "Manager Camera";
            btnAddToolBase.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnAddToolBase.UseVisualStyleBackColor = true;
            btnAddToolBase.Click += btnAddToolBase_Click;
            // 
            // lbTitleName
            // 
            lbTitleName.BackColor = Color.Lavender;
            lbTitleName.Dock = DockStyle.Top;
            lbTitleName.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTitleName.Location = new Point(0, 0);
            lbTitleName.Name = "lbTitleName";
            lbTitleName.Size = new Size(292, 32);
            lbTitleName.TabIndex = 0;
            lbTitleName.Text = "CAMERA";
            lbTitleName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panelPage1Setting, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Controls.Add(panel2, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1110, 823);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(303, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(294, 817);
            panel1.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnApply);
            groupBox1.Controls.Add(numSmoke_Sen);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(numFlame_Sen);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(numInterval);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Microsoft YaHei", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(288, 198);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Parametter";
            // 
            // btnApply
            // 
            btnApply.BackColor = Color.Gainsboro;
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.Location = new Point(6, 150);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(276, 32);
            btnApply.TabIndex = 7;
            btnApply.Text = "Apply";
            btnApply.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnApply_Click;
            // 
            // numSmoke_Sen
            // 
            numSmoke_Sen.DecimalPlaces = 2;
            numSmoke_Sen.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numSmoke_Sen.Location = new Point(168, 101);
            numSmoke_Sen.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numSmoke_Sen.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numSmoke_Sen.Name = "numSmoke_Sen";
            numSmoke_Sen.Size = new Size(114, 25);
            numSmoke_Sen.TabIndex = 6;
            numSmoke_Sen.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 106);
            label3.Name = "label3";
            label3.Size = new Size(120, 19);
            label3.TabIndex = 5;
            label3.Text = "Smoke_sensitivity:";
            // 
            // numFlame_Sen
            // 
            numFlame_Sen.DecimalPlaces = 2;
            numFlame_Sen.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numFlame_Sen.Location = new Point(168, 68);
            numFlame_Sen.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numFlame_Sen.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numFlame_Sen.Name = "numFlame_Sen";
            numFlame_Sen.Size = new Size(114, 25);
            numFlame_Sen.TabIndex = 4;
            numFlame_Sen.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 73);
            label2.Name = "label2";
            label2.Size = new Size(114, 19);
            label2.TabIndex = 3;
            label2.Text = "Flame_sensitivity:";
            // 
            // numInterval
            // 
            numInterval.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numInterval.Location = new Point(168, 33);
            numInterval.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            numInterval.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numInterval.Name = "numInterval";
            numInterval.Size = new Size(114, 25);
            numInterval.TabIndex = 2;
            numInterval.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(6, 38);
            label1.Name = "label1";
            label1.Size = new Size(58, 19);
            label1.TabIndex = 1;
            label1.Text = "Interval:";
            // 
            // panel2
            // 
            panel2.Controls.Add(panelMain);
            panel2.Controls.Add(toolStrip1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(603, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(504, 817);
            panel2.TabIndex = 5;
            // 
            // panelMain
            // 
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 35);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(504, 782);
            panelMain.TabIndex = 11;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripRunImage, toolStripSeparator1, toolStripOpenImage, toolStripSeparator3, toolStripProcessTime, toolStripSeparator6, toolStripSeparator7, toolStripStatus, toolStripLabel2, toolStripDropDownButton1, toolStripSeparator8, toolStripLabel3, toolStripRuntime });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(504, 35);
            toolStrip1.TabIndex = 10;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripRunImage
            // 
            toolStripRunImage.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripRunImage.Image = Properties.Resources.next_28px;
            toolStripRunImage.ImageScaling = ToolStripItemImageScaling.None;
            toolStripRunImage.ImageTransparentColor = Color.Magenta;
            toolStripRunImage.Name = "toolStripRunImage";
            toolStripRunImage.Size = new Size(32, 32);
            toolStripRunImage.Text = "Run Image";
            toolStripRunImage.Click += toolStripRun_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(10, 0, 10, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 35);
            // 
            // toolStripOpenImage
            // 
            toolStripOpenImage.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripOpenImage.Image = Properties.Resources.image_28px;
            toolStripOpenImage.ImageScaling = ToolStripItemImageScaling.None;
            toolStripOpenImage.ImageTransparentColor = Color.Magenta;
            toolStripOpenImage.Name = "toolStripOpenImage";
            toolStripOpenImage.Size = new Size(32, 32);
            toolStripOpenImage.Text = "Open File";
            toolStripOpenImage.Click += toolStripOpenImage_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Margin = new Padding(5, 0, 5, 0);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 35);
            // 
            // toolStripProcessTime
            // 
            toolStripProcessTime.Alignment = ToolStripItemAlignment.Right;
            toolStripProcessTime.Name = "toolStripProcessTime";
            toolStripProcessTime.Size = new Size(32, 32);
            toolStripProcessTime.Text = "0 ms";
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(6, 35);
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Alignment = ToolStripItemAlignment.Right;
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(6, 35);
            // 
            // toolStripStatus
            // 
            toolStripStatus.Alignment = ToolStripItemAlignment.Right;
            toolStripStatus.Name = "toolStripStatus";
            toolStripStatus.Size = new Size(39, 32);
            toolStripStatus.Text = "Status";
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(48, 32);
            toolStripLabel2.Text = "Graphic";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { commonGraphicToolStripMenuItem, showResultGraphicToolStripMenuItem, showTextGraphicToolStripMenuItem });
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(13, 32);
            toolStripDropDownButton1.Text = "Graphic Option";
            // 
            // commonGraphicToolStripMenuItem
            // 
            commonGraphicToolStripMenuItem.CheckOnClick = true;
            commonGraphicToolStripMenuItem.Name = "commonGraphicToolStripMenuItem";
            commonGraphicToolStripMenuItem.Size = new Size(182, 22);
            commonGraphicToolStripMenuItem.Text = "Common Graphic";
            // 
            // showResultGraphicToolStripMenuItem
            // 
            showResultGraphicToolStripMenuItem.CheckOnClick = true;
            showResultGraphicToolStripMenuItem.Name = "showResultGraphicToolStripMenuItem";
            showResultGraphicToolStripMenuItem.Size = new Size(182, 22);
            showResultGraphicToolStripMenuItem.Text = "Show Result Graphic";
            // 
            // showTextGraphicToolStripMenuItem
            // 
            showTextGraphicToolStripMenuItem.CheckOnClick = true;
            showTextGraphicToolStripMenuItem.Name = "showTextGraphicToolStripMenuItem";
            showTextGraphicToolStripMenuItem.Size = new Size(182, 22);
            showTextGraphicToolStripMenuItem.Text = "Show Text Graphic";
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Margin = new Padding(0, 0, 5, 0);
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(6, 35);
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(86, 32);
            toolStripLabel3.Text = "Runtime Mode";
            // 
            // toolStripRuntime
            // 
            toolStripRuntime.AutoSize = false;
            toolStripRuntime.BackColor = SystemColors.ControlLight;
            toolStripRuntime.CheckOnClick = true;
            toolStripRuntime.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripRuntime.ImageTransparentColor = Color.Magenta;
            toolStripRuntime.Name = "toolStripRuntime";
            toolStripRuntime.Size = new Size(23, 23);
            toolStripRuntime.Text = "OFF";
            // 
            // mySqlDataAdapter1
            // 
            mySqlDataAdapter1.DeleteCommand = null;
            mySqlDataAdapter1.InsertCommand = null;
            mySqlDataAdapter1.SelectCommand = null;
            mySqlDataAdapter1.UpdateCommand = null;
            // 
            // MeasureRecipe2
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MeasureRecipe2";
            Size = new Size(1110, 823);
            Load += MeasureRecipe2_Load;
            panelPage1Setting.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panelBaseTool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgviewCamera).EndInit();
            panel4.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSmoke_Sen).EndInit();
            ((System.ComponentModel.ISupportInitialize)numFlame_Sen).EndInit();
            ((System.ComponentModel.ISupportInitialize)numInterval).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPage1Setting;
        private System.Windows.Forms.Label lbTitleName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private MySql.Data.MySqlClient.MySqlDataAdapter mySqlDataAdapter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripRunImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripOpenImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripProcessTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripLabel toolStripStatus;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem commonGraphicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showResultGraphicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTextGraphicToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton toolStripRuntime;
        private System.Windows.Forms.NumericUpDown numSmoke_Sen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numFlame_Sen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelBaseTool;
        private System.Windows.Forms.DataGridView dgviewCamera;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnRefreshRecipe;
        private System.Windows.Forms.Button btnAddToolBase;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panelPCInfo;
        private System.Windows.Forms.Button btnConfigScreen;
        private System.Windows.Forms.NumericUpDown numRowCam;
        private System.Windows.Forms.NumericUpDown numColCam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

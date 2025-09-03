using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraManager
{
    public partial class FormParamCamera : Form
    {
        bool bLastStateNormal = false;
        Form1 main;
        public FormParamCamera()
        {
            InitializeComponent();
        }

        public void Innit(Form1 obj)
        {
            main = obj;
            ClassSystemConfig.Ins.m_SettingParam.InitPageSetup(obj, 0, "SETTING");
            panelMain.Controls.Clear();
            panelMain.Controls.Add(ClassSystemConfig.Ins.m_SettingParam);
            ClassSystemConfig.Ins.m_SettingParam.Dock = DockStyle.Fill;
        }

        #region UI startup
        public void ShowOnScreen()
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = bLastStateNormal ? FormWindowState.Normal : FormWindowState.Maximized;

            this.BringToFront();
        }
        private void FormParamCamera_Load(object sender, EventArgs e)
        {
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
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

    }
}

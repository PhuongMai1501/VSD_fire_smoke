using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraManager
{
    public partial class FormStartupLoading : Form
    {
        public FormStartupLoading()
        {
            InitializeComponent();
            string str_ProgramName = "demo camera";
            lbProgramName.Text = str_ProgramName.ToUpper();
            lbProgramVersion.Text = "Ver 1.0.0";

            var dt = System.IO.File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location);
            lbReleasedDate.Text = "Released " + dt.ToString("yyyy/MM/dd");
        }


        #region Enable Drag Winform
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void FormStartupLoading_Load(object sender, EventArgs e)
        {
            // Additional insurance
            this.TopMost = true;
            this.BringToFront();
            this.Activate();
            this.Focus();

            // Center and refresh
            this.CenterToScreen();
            this.Refresh();
            Application.DoEvents();

            Console.WriteLine("Loading form is now on top");
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Ensure it stays on top when shown
            this.TopMost = true;
            this.BringToFront();
            this.Activate();
        }
    }
}

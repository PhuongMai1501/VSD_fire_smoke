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
    public partial class FormLoading : Form
    {
        public FormLoading()
        {
            InitializeComponent();
            //this.BackColor = Color.White;
            //this.TransparencyKey = Color.White;
            //ClassCommon.ControlTextInvoke(lbTitleName, ClassCommon.StrLoadingFormName);
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

        int m_iCountIndex = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch(m_iCountIndex)
            {
                case 0:
                    lbTitleName.ForeColor = Color.DeepSkyBlue;
                    break;
                case 1:
                    lbTitleName.ForeColor = Color.Orange;
                    break;
                case 2:
                    lbTitleName.ForeColor = Color.Lime;
                    break;
                default:
                    m_iCountIndex = 0;
                    lbTitleName.ForeColor = Color.DeepSkyBlue;
                    break;
            }
            m_iCountIndex++;
        }
    }
}

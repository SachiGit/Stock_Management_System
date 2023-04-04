using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Common
{
    public partial class FrmContactUs : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public FrmContactUs()
        {
            InitializeComponent(); 
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }

        private void FrmContactUs_Load(object sender, EventArgs e)
        {
        }

        private void FrmContactUs_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void FrmContactUs_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

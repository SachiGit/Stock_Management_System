using StockManageUI.GRN;
using StockManageUI.GRNReturn;
using StockManageUI.IssueNote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageUI.Common
{
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {

        }

        private void pbGRN_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "GRN")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmGRN _grn = new FrmGRN();
                _grn.MdiParent = this.MdiParent;
                _grn.WindowState = FormWindowState.Maximized;
                _grn.Show();
            }
        }

        private void pbIssueNote_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Issue Note")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmIssueNote _issuenote = new FrmIssueNote();
                _issuenote.MdiParent = this.MdiParent;
                _issuenote.WindowState = FormWindowState.Maximized;
                _issuenote.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "GRN Return")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmGRNReturn _grnreturn = new FrmGRNReturn();
                _grnreturn.MdiParent = this.MdiParent;
                _grnreturn.WindowState = FormWindowState.Maximized;
                _grnreturn.Show();
            }
        }
    }
}

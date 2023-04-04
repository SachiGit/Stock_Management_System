using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockManageUI.User;
using StockManageUI.Unit;
using StockManageUI.Item;
using StockManageUI.Department;
using StockManageUI.Vendor;
using StockManageUI.Company;
using StockManageUI.Reports.GRN.Details;
using StockManageUI.Reports.IssueNote.Details;
using StockManageUI.Reports.GRNReturn.Details;
using StockManageUI.Reports.StockValuationSummary;
using StockManageUI.Reports.IssuedItemSummary;
using StockManageUI.Reports.FinalReport;
using StockManageUI.Reports.ReOrderLevel;
using StockManageUI.Search;
using StockManageUI.Reports.ItemDetail;
using StockManageUI.Reports.IssueNoteVoteSummary;

namespace StockManageUI.Common
{
    public partial class FrmMain : Form
    {
        public string UserName = string.Empty;
        public string UserType = string.Empty;

        private bool isLogout = false;

        List<string> iList = new List<string>();
        List<Form> iList2 = new List<Form>();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            FrmHome _home = new FrmHome();
            _home.MdiParent = this;
            _home.Show();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!isLogout)
                {
                    if (MessageBox.Show("Are you sure you want to exit ?", "Closing ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch
            { }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Home")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmHome _home = new FrmHome();
                _home.MdiParent = this;
                _home.Show();
            }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            if (UserType == "Admin")
            {

                bool isAlreadyOpen = false;
                foreach (Form myForm in Application.OpenForms)
                {
                    if (myForm.Text == "User List")
                    {
                        isAlreadyOpen = true;
                        myForm.Focus();
                        break;
                    }
                }

                if (!isAlreadyOpen)
                {
                    FrmUserList _userlist = new FrmUserList();
                    _userlist.MdiParent = this;
                    _userlist.WindowState = FormWindowState.Maximized;
                    _userlist.Show();
                }
            }
            else
            {
                MessageBox.Show("Only Admin User Can Access");
            }
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Item List")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmItemList _item = new FrmItemList();
                _item.MdiParent = this;
                _item.WindowState = FormWindowState.Maximized;
                _item.Show();
            }
        }

        private void btnUnit_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Unit List")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmUnitList _unit = new FrmUnitList();
                _unit.MdiParent = this;
                _unit.WindowState = FormWindowState.Maximized;
                _unit.Show();
            }
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Department List")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmDepartmentList _department = new FrmDepartmentList();
                _department.MdiParent = this;
                _department.WindowState = FormWindowState.Maximized;
                _department.Show();
            }
        }

        private void btnVendors_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Vendor List")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmVendorList _vendor = new FrmVendorList();
                _vendor.MdiParent = this;
                _vendor.WindowState = FormWindowState.Maximized;
                _vendor.Show();
            }
        }

        private void btnCompany_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Company")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmCompany _company = new FrmCompany();
                _company.ShowDialog();
            }
        }

        private void btnWindow_Click(object sender, EventArgs e)
        {
            iList.Clear();
            iList2.Clear();
            FormCollection fc = Application.OpenForms;
            iList.Add("Close All");

            foreach (Form frm in fc)
            {
                string input = frm.Text;
                int firstCommaIndex = input.IndexOf('-');
                string firstPart = frm.Text;

                if (frm.Text != "Login")
                {
                    if (!firstPart.Contains("ERP System"))
                    {
                        iList.Add(frm.Text.ToString());
                        iList2.Add(frm);
                    }
                }
            }

            SubMenu(btnWindow, "Window");
        }

        public void SubMenu(ToolStripMenuItem MnuItems, string var)
        {
            if (var == "Window")
            {
                MnuItems.DropDown.Items.Clear();

                foreach (string rw in iList)
                {
                    ToolStripMenuItem SSMenu = new ToolStripMenuItem(rw, null, ChildClick);

                    try
                    {
                        if (this.ActiveControl.Text.ToString() == SSMenu.ToString())
                        {
                            MnuItems.DropDownItems.Add(SSMenu);
                            SSMenu.Checked = true;
                        }
                        else
                        {
                            if (rw.ToString() == "Close All")
                            {
                                MnuItems.DropDownItems.Add(SSMenu);
                                MnuItems.DropDownItems.Add(new ToolStripSeparator());
                            }
                            else
                            {
                                MnuItems.DropDownItems.Add(SSMenu);
                            }
                        }
                    }
                    catch { }
                }
            }
        }

        public void ChildClick(object sender, System.EventArgs e)
        {

            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == sender.ToString())
                {
                    if (myForm.WindowState == FormWindowState.Minimized)
                    {
                        myForm.WindowState = FormWindowState.Maximized;

                        myForm.Focus();
                        break;
                    }
                    else
                    {
                        myForm.Focus();
                        break;
                    }
                }
                else if ("Close All" == sender.ToString())
                {
                    foreach (Form myForm1 in iList2)
                    {
                        if (myForm1.Text != "Home")
                        {
                            if (myForm1.Text != "FrmLogin")
                            {
                                if (myForm1.Text != "Main")
                                {
                                    myForm1.Close();
                                }
                            }
                        }
                    }
                    break;
                }
            }

        }

        private void btnGRNDetails_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "GRN Details")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmGRNDetails _grndetails = new FrmGRNDetails();
                _grndetails.MdiParent = this;
                _grndetails.WindowState = FormWindowState.Maximized;
                _grndetails.Show();
            }
        }

        private void btnIssueNoteDetails_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Issue Note Details")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmIssueNoteDetails _issuenotedetails = new FrmIssueNoteDetails();
                _issuenotedetails.MdiParent = this;
                _issuenotedetails.WindowState = FormWindowState.Maximized;
                _issuenotedetails.Show();
            }
        }

        private void btnGRNReturnDetails_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "GRN Return Details")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmGRNReturnDetails _grnreturndetails = new FrmGRNReturnDetails();
                _grnreturndetails.MdiParent = this;
                _grnreturndetails.WindowState = FormWindowState.Maximized;
                _grnreturndetails.Show();
            }
        }

        private void stockValuationSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Stock Valuation Summary")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmStockValuationSummaryNew _stockvaluationsummary = new FrmStockValuationSummaryNew();
                _stockvaluationsummary.MdiParent = this;
                _stockvaluationsummary.WindowState = FormWindowState.Maximized;
                _stockvaluationsummary.Show();
            }
        }

        private void btnIssuedItemSummary_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Issued Item Summary")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmIssuedItemSummary _issueditemsummary = new FrmIssuedItemSummary();
                _issueditemsummary.MdiParent = this;
                _issueditemsummary.WindowState = FormWindowState.Maximized;
                _issueditemsummary.Show();
            }
        }

        private void btnFinalReport_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Final Report")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmFinalReport _fianlreport = new FrmFinalReport();
                _fianlreport.MdiParent = this;
                _fianlreport.WindowState = FormWindowState.Maximized;
                _fianlreport.Show();
            }
        }

        private void btnReOrderLevel_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "ReOrder Level Report")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmReOrderLevelReport _reorderlevelreport = new FrmReOrderLevelReport();
                _reorderlevelreport.MdiParent = this;
                _reorderlevelreport.WindowState = FormWindowState.Maximized;
                _reorderlevelreport.Show();
            }
        }

        private void btnContactUs_Click(object sender, EventArgs e)
        {
            FrmContactUs _contact = new FrmContactUs();
            _contact.ShowDialog();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Find")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmFind _find = new FrmFind();
                _find.MdiParent = this;
                _find.WindowState = FormWindowState.Maximized;
                _find.Show();
            }
        }

        private void btnItemDetail_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Item Detail")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmItemDetail _iItemDetailreport = new FrmItemDetail();
                _iItemDetailreport.MdiParent = this;
                _iItemDetailreport.WindowState = FormWindowState.Maximized;
                _iItemDetailreport.Show();
            }
        }

        private void btnIssueNoteVoteSummary_Click(object sender, EventArgs e)
        {
            bool isAlreadyOpen = false;
            foreach (Form myForm in Application.OpenForms)
            {
                if (myForm.Text == "Issue Note Vote Summary")
                {
                    isAlreadyOpen = true;
                    myForm.Focus();
                    break;
                }
            }

            if (!isAlreadyOpen)
            {
                FrmIssueNoteVoteSummary _issueNoteVoteSummaryreport = new FrmIssueNoteVoteSummary();
                _issueNoteVoteSummaryreport.MdiParent = this;
                _issueNoteVoteSummaryreport.WindowState = FormWindowState.Maximized;
                _issueNoteVoteSummaryreport.Show();
            }
        }
    }
}

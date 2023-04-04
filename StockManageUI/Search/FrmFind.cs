using StockManageBLL.Search;
using StockManageUI.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StockManageUI.GRN;
using StockManageUI.IssueNote;
using StockManageUI.GRNReturn;

namespace StockManageUI.Search
{
    public partial class FrmFind : Form
    {
        FindBLL _findBLLObject;

        public FrmFind()
        {
            InitializeComponent();
            _findBLLObject = new FindBLL();
        }

        private void FrmFind_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Text = "2018-01-01";
            LoadDepartment();
            DataGridStyles.styleGridList_2014(dataGridView1);
            cmbFormType.Text = "GRN";
        }

        private void LoadDepartment()
        {
            DataTable dt = _findBLLObject.GetDepartmentList();
            DataTable _dtcopy = dt.Copy();
            DataRow _row = _dtcopy.NewRow();
            _row["department"] = "";
            _dtcopy.Rows.InsertAt(_row, 0);

            cmbDepartment.DataSource = _dtcopy;
            cmbDepartment.DisplayMember = "department";
        }

        private void cmbFormType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFormType.Text == "GRN")
            {
                LoadVendor();
                lblName.Text = "Supplier Name"; 
                pnlDept.Enabled = false;
                pnlGIN.Enabled = false;
            }

            if (cmbFormType.Text == "IssueNote")
            {
                LoadEmployee();
                lblName.Text = "Employee Name";
                pnlDept.Enabled = true;
                pnlGIN.Enabled = true;
            }

            if (cmbFormType.Text == "GRN Return")
            {
                LoadEmployee();
                lblName.Text = "Employee Name";
                pnlDept.Enabled = false;
                pnlGIN.Enabled = false;
            }
        }

        private void LoadVendor()
        {
            DataTable dt = _findBLLObject.GetVendorList();
            DataTable _dtcopy = dt.Copy();
            DataRow _row = _dtcopy.NewRow();
            _row["vendorname"] = "";
            _dtcopy.Rows.InsertAt(_row, 0);

            cmbName.DataSource = _dtcopy;
            cmbName.DisplayMember = "vendorname";
        }

        private void LoadEmployee()
        {
            DataTable dt = _findBLLObject.GetEmployeeList();
            DataTable _dtcopy = dt.Copy();
            DataRow _row = _dtcopy.NewRow();
            _row["vendor"] = "";
            _dtcopy.Rows.InsertAt(_row, 0);

            cmbName.DataSource = _dtcopy;
            cmbName.DisplayMember = "vendor";
        }

        private void brnFind_Click(object sender, EventArgs e)
        {
            string Fdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string Tdate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string FormType = cmbFormType.Text;
            string Name = cmbName.Text;
            string SerialNo = txtSerialNo.Text;
            string Department = cmbDepartment.Text;
            string GINNo = txtGINNo.Text;

            if (!string.IsNullOrEmpty(FormType))
            {
                DataFind(FormType, Fdate, Tdate, Name, SerialNo, Department, GINNo); 
            }
            else
            {
                MessageBox.Show("Please Select Form Type");
            }
        }

        private void DataFind(string FormType, string Fdate, string Tdate, string Name, string SerialNo, string Department, string GINNo)
        {
            DataTable dt = _findBLLObject.GetFormData(FormType, Fdate, Tdate, Name, SerialNo, Department, GINNo);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Total"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string SerialNo = dataGridView1.SelectedCells[1].Value.ToString();

            if (cmbFormType.Text == "GRN")
            {
                FrmGRN _grn = new FrmGRN();
                _grn.MdiParent = this.MdiParent;
                _grn.WindowState = FormWindowState.Maximized;
                _grn.Show();
                _grn.FindTransactionBySerialNumber(SerialNo);
            }

            if (cmbFormType.Text == "IssueNote")
            {
                FrmIssueNote _issuenote = new FrmIssueNote();
                _issuenote.MdiParent = this.MdiParent;
                _issuenote.WindowState = FormWindowState.Maximized;
                _issuenote.Show();
                _issuenote.FindTransactionBySerialNumber(SerialNo);
            }

            if (cmbFormType.Text == "GRN Return")
            {
                FrmGRNReturn _grnreturn = new FrmGRNReturn();
                _grnreturn.MdiParent = this.MdiParent;
                _grnreturn.WindowState = FormWindowState.Maximized;
                _grnreturn.Show();
                _grnreturn.FindTransactionBySerialNumber(SerialNo);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dataGridView1.DataSource = dt;
        }


    }
}

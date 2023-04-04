using StockManageBLL.Department;
using StockManageBLL.Reports.IssueNote.Details;
using StockManageDTO.Reports.IssueNote.Details;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Reports.IssueNote.Details
{
    public partial class FrmIssueNoteDetails : Form
    {
        IssueNoteDetailsBLL _issuenoteDetailsBLLObject;
        DepartmentBLL _departmentBLLObject;
        private string Fdate;
        private string Tdate;

        public FrmIssueNoteDetails()
        {
            InitializeComponent();
            _issuenoteDetailsBLLObject = new IssueNoteDetailsBLL();
            _departmentBLLObject = new DepartmentBLL();
        }

        private void FrmIssueNoteDetails_Load(object sender, EventArgs e)
        {
            Fdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Tdate = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            LoadDepartment();

            LoadData(Fdate, Tdate, cmbDepartment.Text);
        }

        private void LoadDepartment()
        {
            DataTable dt = _departmentBLLObject.LoadDepartmentList();

            #region  Remove extra columns
            string[] existingcol = { "id", "description" };
            foreach (string colName in existingcol)
            {
                dt.Columns.Remove(colName);
            } 
            #endregion

            #region Add new row 
            DataTable dtcopy = dt.Copy();
            DataRow _row = dtcopy.NewRow();
            _row["department"] = "All";
            dtcopy.Rows.InsertAt(_row, 0); 
            #endregion

            cmbDepartment.DataSource = dtcopy;
            cmbDepartment.DisplayMember = "department";
        }

        private void LoadData(string Fdate, string Tdate, string Department)
        {
            try
            {
                IssueNoteDetailsDataSet DS = _issuenoteDetailsBLLObject.GetGRNDetails(Fdate, Tdate, Department);

                IssueNoteDetailsReport _report = new IssueNoteDetailsReport();
                _report.SetDataSource(DS);
                _report.SetParameterValue("Fdate", Fdate);
                _report.SetParameterValue("Tdate", Tdate);
                _report.SetParameterValue("Department", Department);
                crystalReportViewer1.ReportSource = _report;
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "/r/nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Report Data Load Error");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Fdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Tdate = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            LoadData(Fdate, Tdate, cmbDepartment.Text);
        }


    }
}

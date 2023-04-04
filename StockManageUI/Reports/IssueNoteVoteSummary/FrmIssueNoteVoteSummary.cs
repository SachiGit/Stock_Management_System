using StockManageBLL.Reports.IssueNoteVoteSummary;
using StockManageDTO.Reports.IssueNoteVoteSummary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageUI.Reports.IssueNoteVoteSummary
{
    public partial class FrmIssueNoteVoteSummary : Form
    {
        IssueNoteVoteSummaryBLL _issueNoteVoteSummaryBLLObject;

        public FrmIssueNoteVoteSummary()
        {
            InitializeComponent();
            _issueNoteVoteSummaryBLLObject = new IssueNoteVoteSummaryBLL();
        }

        private void FrmIssueNoteVoteSummary_Load(object sender, EventArgs e)
        {
            LoadVoteNo();
            LoadDepartment();
        }

        private void LoadVoteNo()
        {
            try
            {
                DataTable dtVote = _issueNoteVoteSummaryBLLObject.GetDepartmentData();

                string[] existingcol = { "department" };
                foreach (string colName in existingcol)
                {
                    dtVote.Columns.Remove(colName);
                }

                DataTable _dtvotecopy = dtVote.Copy();
                DataRow _newrow = _dtvotecopy.NewRow();
                _newrow["voteNo"] = "All";
                _dtvotecopy.Rows.InsertAt(_newrow, 0);

                DataTable distinctTable = _dtvotecopy.DefaultView.ToTable( /*distinct*/ true);

                cmbVoteNo.DataSource = distinctTable;
                cmbVoteNo.DisplayMember = "voteNo";
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "\r\nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Vote No Load Error");
            }
        }

        private void LoadDepartment()
        {
            try
            {
                DataTable dtDepartment = _issueNoteVoteSummaryBLLObject.GetDepartmentData();

                string[] existingcol = { "voteNo" };
                foreach (string colName in existingcol)
                {
                    dtDepartment.Columns.Remove(colName);
                }

                DataTable _dtDepartmentcopy = dtDepartment.Copy();
                DataRow _newrow = _dtDepartmentcopy.NewRow();
                _newrow["department"] = "All";
                _dtDepartmentcopy.Rows.InsertAt(_newrow, 0);

                cmbDepartment.DataSource = _dtDepartmentcopy;
                cmbDepartment.DisplayMember = "department";
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "\r\nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Department Load Error");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string Fdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string Tdate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                string VoteNo = cmbVoteNo.Text;
                string Department = cmbDepartment.Text;

                IssueNoteVoteSummaryDataSet DS = _issueNoteVoteSummaryBLLObject.GetIssueNoteVoteSummaryData(Fdate, Tdate, VoteNo, Department);

                IssueNoteVoteSummaryReport _report = new IssueNoteVoteSummaryReport();
                _report.SetDataSource(DS);
                _report.SetParameterValue("Fdate", Fdate);
                _report.SetParameterValue("Tdate", Tdate);
                crystalReportViewer1.ReportSource = _report;
            }
            catch (Exception ex)
            {
                Clipboard.SetText("Error Message : " + ex.Message + "\r\nTrace Message : " + ex.StackTrace.ToString());
                MessageBox.Show("Report Data Load Error");
            }
        }


    }
}

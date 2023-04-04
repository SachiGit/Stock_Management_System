using StockManageDAL.Reports.IssueNoteVoteSummary;
using StockManageDTO.Reports.IssueNoteVoteSummary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StockManageBLL.Reports.IssueNoteVoteSummary
{
    public class IssueNoteVoteSummaryBLL
    {
        IssueNoteVoteSummaryDAL _issueNoteVoteSummaryDALObject;

        public IssueNoteVoteSummaryBLL()
        {
            _issueNoteVoteSummaryDALObject = new IssueNoteVoteSummaryDAL();
        }

        public IssueNoteVoteSummaryDataSet GetIssueNoteVoteSummaryData(string _fdate, string _tdate, string _voteNo, string _department)
        {
            return _issueNoteVoteSummaryDALObject.GetIssueNoteVoteSummaryData(_fdate, _tdate, _voteNo, _department);
        }

        public DataTable GetDepartmentData()
        {
            return _issueNoteVoteSummaryDALObject.GetDepartmentData();
        }
    }
}

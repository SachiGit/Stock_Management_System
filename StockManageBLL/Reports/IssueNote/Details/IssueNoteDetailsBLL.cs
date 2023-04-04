using StockManageDAL.Reports.IssueNote.Details;
using StockManageDTO.Reports.IssueNote.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageBLL.Reports.IssueNote.Details
{
    public class IssueNoteDetailsBLL
    {
        IssueNoteDetailsDAL _grnDetailsDALObject;

        public IssueNoteDetailsBLL()
        {
            _grnDetailsDALObject = new IssueNoteDetailsDAL();
        }

        public IssueNoteDetailsDataSet GetGRNDetails(string Fdate, string Tdate, string Department)
        {
            return _grnDetailsDALObject.GetIssueNoteDetailsData(Fdate, Tdate, Department);
        }
    }
}

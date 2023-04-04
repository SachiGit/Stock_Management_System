using StockManageDAL.IssueNote.Print;
using StockManageDTO.IssueNote.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageBLL.IssueNote.Print
{
    public class IssueNotePrintBLL
    {
        IssueNotePrintDAL _pritDALObject;

        public IssueNotePrintBLL()
        {
            _pritDALObject = new IssueNotePrintDAL();
        }

        public IssueNoteDataSet GetIssueNoteData(string _serialNO)
        {
            return _pritDALObject.GetIssueNoteData(_serialNO);
        }
    }
}

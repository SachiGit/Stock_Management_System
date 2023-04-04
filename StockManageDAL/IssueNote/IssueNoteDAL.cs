using StockManageDAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDAL.IssueNote
{
    public class IssueNoteDAL : CommonDAL
    {
        public IssueNoteDAL()
        {
            SetSerialNumberListBehaviour(FormType.issuenote);
            SetFormBehaviour(FormType.issuenote);
            setVendorNames();
            setDepartment();
            setItems();
            setUnit();
            setGINNo();
        }
    }
}

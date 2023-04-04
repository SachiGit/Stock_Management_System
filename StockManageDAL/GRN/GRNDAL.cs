using StockManageDAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDAL.GRN
{
    public class GRNDAL : CommonDAL
    {
        public GRNDAL()
        {
            SetSerialNumberListBehaviour(FormType.grn);
            SetFormBehaviour(FormType.grn);
            setVendorNames();
            setUnit();
            setDepartment();
            setItems();
        }
    }
}

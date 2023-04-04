using StockManageDAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageDAL.GRNReturn
{
    public class GRNReturnDAL : CommonDAL
    {
        public GRNReturnDAL()
        {
            SetSerialNumberListBehaviour(FormType.grnreturn);
            SetFormBehaviour(FormType.grnreturn);
            setVendorNames();
            setUnit();
            setDepartment();
            setItems();
        }
    }
}

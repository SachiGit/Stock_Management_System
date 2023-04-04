using StockManageDAL.GRN.Print;
using StockManageDTO.GRN.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageBLL.GRN.Print
{
    public class GRNPrintBLL
    {
        GRNPrintDAL _pritDALObject;

        public GRNPrintBLL()
        {
            _pritDALObject = new GRNPrintDAL();
        }

        public GRNPrintDataSet GetGRNData(string _serialNO)
        {
            return _pritDALObject.GetGRNData(_serialNO);
        }
    }
}

using StockManageDAL.GRNReturn.Print;
using StockManageDTO.GRNReturn.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageBLL.GRNReturn.Print
{
    public class GRNReturnPrintBLL
    {
        GRNReturnPrintDAL _pritDALObject;

        public GRNReturnPrintBLL()
        {
            _pritDALObject = new GRNReturnPrintDAL();
        }

        public GRNReturnPrintDataSet GetGRNData(string _serialNO)
        {
            return _pritDALObject.GetGRNReturnData(_serialNO);
        }
    }
}

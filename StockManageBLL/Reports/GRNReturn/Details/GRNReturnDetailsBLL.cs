using StockManageDAL.Reports.GRNReturn.Details;
using StockManageDTO.Reports.GRNReturn.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageBLL.Reports.GRNReturn.Details
{
    public class GRNReturnDetailsBLL
    {
        GRNReturnDetailsDAL _grnDetailsDALObject;

        public GRNReturnDetailsBLL()
        {
            _grnDetailsDALObject = new GRNReturnDetailsDAL();
        }

        public GRNReturnDetailsDataSet GetGRNReturnDetails(string Fdate, string Tdate)
        {
            return _grnDetailsDALObject.GetGRNReturnDetailsData(Fdate, Tdate);
        }
    }
}

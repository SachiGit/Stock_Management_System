using StockManageDAL.Reports.GRN.Details;
using StockManageDTO.Reports.GRN.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageBLL.Reports.GRN.Details
{
    public class GRNDetailsBLL
    {
        GRNDetailsDAL _grnDetailsDALObject;

        public GRNDetailsBLL()
        {
            _grnDetailsDALObject = new GRNDetailsDAL();
        }

        public GRNDetailsDataSet GetGRNDetails(string Fdate, string Tdate)
        {
            return _grnDetailsDALObject.GetGRNDetailsData(Fdate, Tdate);
        }
    }
}

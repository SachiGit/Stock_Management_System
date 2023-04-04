using StockManageDAL.Reports.IssuedItemSummary;
using StockManageDTO.Reports.IssuedItemSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageBLL.Reports.IssuedItemSummary
{
    public class IssuedItemSummaryBLL
    {
        IssuedItemSummaryDAL _issueditemsummaryDALObject;

        public IssuedItemSummaryBLL()
        {
            _issueditemsummaryDALObject = new IssuedItemSummaryDAL();
        }

        public IssuedItemSummaryDataSet GetIssuedItemSummaryData(string Fdate,string Tdate,string Department,string ItemCode)
        {
            return _issueditemsummaryDALObject.GetIssuedItemSummaryData(Fdate, Tdate, Department, ItemCode);
        }
    }
}

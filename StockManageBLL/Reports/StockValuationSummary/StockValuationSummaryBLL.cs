using StockManageDAL.Reports.StockValuationSummary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StockManageBLL.Reports.StockValuationSummary
{
    public class StockValuationSummaryBLL
    {
        StockValuationSummaryDAL _stockValuationSummaryDALObject;

        public StockValuationSummaryBLL()
        {
            _stockValuationSummaryDALObject = new StockValuationSummaryDAL();
        }

        public DataTable GetStockValuationSummaryData(string Fdate, string Tdate)
        {
            return _stockValuationSummaryDALObject.GetStockValuationSummaryData(Fdate, Tdate);
        }

        public string GetCompanyname()
        {
            return _stockValuationSummaryDALObject.GetCompanyname();
        }
    }
}

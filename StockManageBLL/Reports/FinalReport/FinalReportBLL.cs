using StockManageDAL.Reports.FinalReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StockManageBLL.Reports.FinalReport
{
    public class FinalReportBLL
    {
        FinalReportDAL _finalreportDALObject;

        public FinalReportBLL()
        {
            _finalreportDALObject = new FinalReportDAL();
        }

        public DataTable GetFinalReportData(string Fdate, string Tdate)
        {
            return _finalreportDALObject.GetFinalReportData(Fdate, Tdate);
        }

        public string GetCompanyname()
        {
            return _finalreportDALObject.GetCompanyname();
        }
    }
}

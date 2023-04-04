using StockManageDAL.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace StockManageDAL.Reports.StockValuationSummary
{
    public class StockValuationSummaryDAL
    {
        DBAccess _dbObject;

        public StockValuationSummaryDAL()
        {
            _dbObject = new DBAccess();
        }

        public DataTable GetStockValuationSummaryData(string Fdate, string Tdate)
        {
            DataTable dt = new DataTable();

            using (var con = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "CALL `StockValuationFIFO_Report`(@fdate,@tdate)";
                    cmd.Parameters.AddWithValue("@fdate", Fdate);
                    cmd.Parameters.AddWithValue("@tdate", Tdate);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public string GetCompanyname ()
        {
            DataTable dt = new DataTable();
            string CompanyName = string.Empty;

            using (var con = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT `companyName` FROM `company`";
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            if (dt.Rows.Count > 0)
            {
                CompanyName = dt.Rows[0][0].ToString();
            }

            return CompanyName;
        }
    }
}

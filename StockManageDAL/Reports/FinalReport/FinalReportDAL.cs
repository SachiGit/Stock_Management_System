using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Reports.FinalReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StockManageDAL.Reports.FinalReport
{
    public class FinalReportDAL
    {
        DBAccess _dbObject;

        public FinalReportDAL()
        {
            _dbObject = new DBAccess();
        }

        public DataTable GetFinalReportData(string Fdate, string Tdate)
        {
            DataTable dt = new DataTable();

            using (var con = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "CALL `FinalReport` (@fdate,@tdate)";
                    cmd.Parameters.AddWithValue("@fdate", Fdate);
                    cmd.Parameters.AddWithValue("@tdate", Tdate);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public string GetCompanyname()
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

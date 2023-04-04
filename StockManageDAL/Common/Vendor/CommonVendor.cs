using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDAL.Common.Vendor
{
    public class CommonVendor : CommonVendorInterface
    {
        DBAccess _DBConnection;

        public CommonVendor()
        {
            _DBConnection = new DBAccess();
        }

        public DataTable GetVendor()
        {
            DataTable dt = new DataTable();
            using (var con = new MySqlConnection(_DBConnection.Sqlconnection()))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT `vendorname` FROM `vendor` ORDER BY `vendorname` ASC";
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}

using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDAL.Common.Items
{
    public class CommonItems : CommonItemInterface
    {
        DBAccess _DBConnection;

        public CommonItems()
        {
            _DBConnection = new DBAccess();
        }

        public DataTable GetItems()
        {
            DataTable dt = new DataTable();
            using (var con = new MySqlConnection(_DBConnection.Sqlconnection()))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT `itemCode`,`itemName` FROM `item`";
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}

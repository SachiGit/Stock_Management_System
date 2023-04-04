using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDAL.Common.Unit
{
    public class CommonUnit : CommonUnitInterface
    {
        DBAccess _DBConnection;

        public CommonUnit()
        {
            _DBConnection = new DBAccess();
        }

        public DataTable GetUnit()
        {
            DataTable dt = new DataTable();
            using (var con = new MySqlConnection(_DBConnection.Sqlconnection()))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT  `unit` FROM `unit`";
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}

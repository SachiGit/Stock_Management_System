using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDAL.Common.Department
{
    public class CommonDepartment : CommonDepartmentInterface
    {
        DBAccess _DBConnection;

        public CommonDepartment()
        {
            _DBConnection = new DBAccess();
        }

        public DataTable GetDepartment()
        {
            DataTable dt = new DataTable();
            using (var con = new MySqlConnection(_DBConnection.Sqlconnection()))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT `department`,`voteNo` FROM `department` ORDER BY `department` ASC";
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}

using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Reports.ReOrderLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageDAL.Reports.ReOrderLevel
{
    public class ReOrderLevelDAL
    {
        DBAccess _dbObject;

        public ReOrderLevelDAL()
        {
            _dbObject = new DBAccess();
        }

        public  ReOrderLevelDataSet GetReOrderLevelData()
        {
            ReOrderLevelDataSet DS = new ReOrderLevelDataSet();

            using (var conn = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "CALL `ReorderLevelReport`()";
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(DS, "DataTable1");
                }

                using (var cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = @"SELECT `companyName`,`address`,`phone`,`fax`,`email`,`web` FROM `company`";
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(DS, "Company");
                }
            }

            return DS;
        }
    }
}

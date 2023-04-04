using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Reports.GRNReturn.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageDAL.Reports.GRNReturn.Details
{
    public class GRNReturnDetailsDAL
    {
         DBAccess _dbObject;

        public GRNReturnDetailsDAL()
        {
            _dbObject = new DBAccess();
        }

        public GRNReturnDetailsDataSet GetGRNReturnDetailsData(string Fdate, string Tdate)
        {
            GRNReturnDetailsDataSet DS = new GRNReturnDetailsDataSet();

            using (var conn = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT gr1.`serialNo`,gr1.`date`,gr1.`vendor`,gr1.`memo`,gr1.`total`,gr2.`itemCode`,
                                               gr2.`itemName`,gr2.`description`,gr2.`qty`,gr2.`rate`,gr2.`amount`,gr2.`unit`
                                         FROM `grnreturnsave1` gr1
                                         INNER JOIN `grnreturnsave2` gr2 ON gr1.`id`=gr2.`id`
                                         WHERE gr1.`date` BETWEEN @fdate AND @tdate";

                    cmd.Parameters.AddWithValue("@fdate", Fdate);
                    cmd.Parameters.AddWithValue("@tdate", Tdate);
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

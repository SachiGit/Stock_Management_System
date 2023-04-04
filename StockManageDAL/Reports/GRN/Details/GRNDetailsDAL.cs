using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Reports.GRN.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageDAL.Reports.GRN.Details
{
    public class GRNDetailsDAL
    {
        DBAccess _dbObject;

        public GRNDetailsDAL()
        {
            _dbObject = new DBAccess();
        }

        public GRNDetailsDataSet GetGRNDetailsData(string Fdate, string Tdate)
        {
            GRNDetailsDataSet DS = new GRNDetailsDataSet();

            using (var conn = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT g1.`serialNo`,g1.`date`,g1.`vendor`,g1.`memo`,g1.`total`,g2.`itemCode`,
                                               g2.`itemName`,g2.`description`,g2.`qty`,g2.`rate`,g2.`amount`,g2.`unit`
                                         FROM `grnsave1` g1
                                         INNER JOIN `grnsave2` g2 ON g1.`id`=g2.`id`
                                         WHERE g1.`date` BETWEEN @fdate AND @tdate
                                         ORDER BY g1.`date`,g1.`serialNo`";

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

using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.GRNReturn.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageDAL.GRNReturn.Print
{
    public class GRNReturnPrintDAL
    {
        DBAccess _dbConnection;

        public GRNReturnPrintDAL()
        {
            _dbConnection = new DBAccess();
        }

        public GRNReturnPrintDataSet GetGRNReturnData(string SerialNo) 
        {
            GRNReturnPrintDataSet DS = new GRNReturnPrintDataSet();

            using (var conn = new MySqlConnection(_dbConnection.Sqlconnection()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT `serialNo`,`date`,`vendor`,`memo`,`total`,`department`,`voteNo` FROM `grnreturnsave1` WHERE `serialNo` = @sno";
                    cmd.Parameters.AddWithValue("@sno", SerialNo);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(DS, "Fields");
                }

                using (var cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = "SELECT `itemCode`,`itemName`,`description`,`qty`,`rate`,`amount`,`unit` FROM `grnreturnsave2` WHERE `serialNo` = @sno";
                    cmd1.Parameters.AddWithValue("@sno", SerialNo);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(DS, "Grid");
                }

                using (var cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "SELECT `companyName`,`address`,`phone`,`fax`,`email`,`web` FROM `company`";
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    da2.Fill(DS, "Company");
                }
            }

            return DS;
        }
    }
}

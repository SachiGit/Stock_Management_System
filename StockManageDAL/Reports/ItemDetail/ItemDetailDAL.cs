using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Reports.ItemDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageDAL.Reports.ItemDetail
{
    public class ItemDetailDAL
    {
        DBAccess _dbObject;

        public ItemDetailDAL()
        {
            _dbObject = new DBAccess();
        }

        public ItemDetailDataSet GetItemDetailData()
        {
            ItemDetailDataSet DS = new ItemDetailDataSet();

            using (var conn = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "CALL `onhandWithItemDetails`()";
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

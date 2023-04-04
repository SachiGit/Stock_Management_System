using StockManageDAL.DBConnection;
using StockManageDTO.Reports.IssuedItemSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace StockManageDAL.Reports.IssuedItemSummary
{
    public class IssuedItemSummaryDAL
    {
        DBAccess _dbObject;

        public IssuedItemSummaryDAL()
        {
            _dbObject = new DBAccess();
        }

        public  IssuedItemSummaryDataSet GetIssuedItemSummaryData(string Fdate, string Tdate, string Department, string ItemCode)
        {
            IssuedItemSummaryDataSet DS = new IssuedItemSummaryDataSet();
            string _commandtext = string.Empty;

            string _sql1 = @"SELECT iss1.`department`,iss2.`itemCode`,iss2.`itemName`,iss2.`description`,SUM(COALESCE(iss2.`qty`,0)) AS Qty,
                                    iss2.`unit`,iss2.`rate`,SUM(COALESCE(iss2.`amount`,0)) AS Amount,iss1.`date`
                             FROM `issuenotesave1` iss1
                             INNER JOIN `issuenotesave2` iss2 ON iss1.`id` = iss2.`id`
                             WHERE iss1.`date` BETWEEN @fdate AND @tdate";

            string _sql2 = " AND iss1.`department` = @department";

            string _sql3 = " AND iss2.`itemCode` = @itemcode";

            string _sql4 = " GROUP BY iss1.`date`,iss2.`rate`,iss2.`itemCode`,iss1.`department` ORDER BY iss1.`date`,iss1.`department`,iss2.`itemCode`";

            if (Department == "All" && ItemCode == "All")
            {
                _commandtext = string.Format("{0}{1}", _sql1, _sql4);
            }
            else if (Department != "All" && ItemCode == "All")
            {
                _commandtext = string.Format("{0}{1}{2}", _sql1, _sql2, _sql4);
            }
            else if (Department == "All" && ItemCode != "All")
            {
                _commandtext = string.Format("{0}{1}{2}", _sql1, _sql3, _sql4);
            }
            else
            {
                _commandtext = string.Format("{0}{1}{2}{3}", _sql1, _sql2, _sql3, _sql4);
            }

            using (var conn = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _commandtext;
                    cmd.Parameters.AddWithValue("@fdate", Fdate);
                    cmd.Parameters.AddWithValue("@tdate", Tdate);
                    cmd.Parameters.AddWithValue("@department", Department);
                    cmd.Parameters.AddWithValue("@itemcode", ItemCode);

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

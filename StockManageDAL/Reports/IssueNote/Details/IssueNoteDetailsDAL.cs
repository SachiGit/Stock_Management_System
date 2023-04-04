using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Reports.IssueNote.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageDAL.Reports.IssueNote.Details
{
    public class IssueNoteDetailsDAL
    {
         DBAccess _dbObject;

         public IssueNoteDetailsDAL()
        {
            _dbObject = new DBAccess();
        }

         public IssueNoteDetailsDataSet GetIssueNoteDetailsData(string Fdate, string Tdate, string Department)
        {
            IssueNoteDetailsDataSet DS = new IssueNoteDetailsDataSet();
            string sql = string.Empty;
            string sql1 = string.Empty;
            string sql2 = string.Empty;
            string CommandText = string.Empty;

            sql = @"SELECT iss1.`serialNo`,iss1.`date`,iss1.`title`,iss1.`vendor`,iss1.`department`,iss1.`memo`,iss1.`total`,
                           iss2.`itemCode`,iss2.`itemName`,iss2.`description`,iss2.`qty`,iss2.`rate`,iss2.`amount`,iss2.`unit`
                    FROM `issuenotesave1` iss1
                    INNER JOIN `issuenotesave2` iss2 ON iss1.`id` = iss2.`id`
                    WHERE iss1.`date` BETWEEN @fdate AND @tdate";

            sql1 = " AND iss1.`department` = @department";

            sql2 = " ORDER BY iss1.`date`,iss1.`serialNo`";

            using (var conn = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    if (Department == "All")
                    {
                        CommandText = sql + sql2;
                    }
                    else
                    {
                        CommandText = sql + sql1 + sql2;
                    }

                    cmd.CommandText = CommandText;
                    cmd.Parameters.AddWithValue("@fdate", Fdate);
                    cmd.Parameters.AddWithValue("@tdate", Tdate);
                    cmd.Parameters.AddWithValue("@department", Department);
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

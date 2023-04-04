using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Reports.IssueNoteVoteSummary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StockManageDAL.Reports.IssueNoteVoteSummary
{
    public class IssueNoteVoteSummaryDAL
    {
        DBAccess _dbObject;

        public IssueNoteVoteSummaryDAL()
        {
            _dbObject = new DBAccess();
        }

        public IssueNoteVoteSummaryDataSet GetIssueNoteVoteSummaryData(string Fdate,string Tdate, string VoteNo,string Department)
        {
            IssueNoteVoteSummaryDataSet DS = new IssueNoteVoteSummaryDataSet();

            string _sql = string.Empty;
            string _sql1 = @"SELECT iss1.`department`,iss1.`voteNo`,iss1.ginNo,SUM(iss1.`total`) AS total 
                             FROM `issuenotesave1` iss1 
                             WHERE iss1.`date` BETWEEN @fdate AND @tdate";
            string _sql2 = " AND iss1.voteNo = @voteno";
            string _sql3 = " AND iss1.department = @department";
            string _sql4 = " GROUP BY iss1.voteNo,iss1.department ORDER BY iss1.`date`,iss1.`ginNo`";

            if (VoteNo == "All" && Department == "All")
            {
                _sql = string.Format("{0}{1}", _sql1, _sql4);
            }
            else if (VoteNo != "All" && Department == "All")
            {
                _sql = string.Format("{0}{1}{2}", _sql1, _sql2, _sql4);
            }
            else if (VoteNo == "All" && Department != "All")
            {
                _sql = string.Format("{0}{1}{2}", _sql1, _sql3, _sql4);
            }
            else
            {
                _sql = string.Format("{0}{1}{2}{3}", _sql1, _sql2, _sql3, _sql4);
            }
                 
            using (var conn = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _sql;
                    cmd.Parameters.AddWithValue("@fdate", Fdate);
                    cmd.Parameters.AddWithValue("@tdate", Tdate);
                    cmd.Parameters.AddWithValue("@voteno", VoteNo);
                    cmd.Parameters.AddWithValue("@department", Department);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(DS, "DataTable1");
                }
            }

            return DS;
        }

        public DataTable GetDepartmentData()
        {
            DataTable dt = new DataTable();

            using (var con = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT `department`,`voteNo` FROM `department`";
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }
    }
}

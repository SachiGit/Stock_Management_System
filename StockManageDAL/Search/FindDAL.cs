using StockManageDAL.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace StockManageDAL.Search
{
    public class FindDAL
    {
        DBAccess _dbObject;
        private string _commandtext = string.Empty;
        private string _sql1 = string.Empty;
        private string _sql2 = string.Empty;
        private string _sql3 = string.Empty;
        private string _sql4 = string.Empty;
        private string _sql5 = string.Empty;

        public FindDAL()
        {
            _dbObject = new DBAccess();
        }

        public DataTable GetDepartmentList()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbObject.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT `department` FROM `department` ORDER BY `department` ASC";
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
            }

            return dt;
        }

        public DataTable GetVendorList()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbObject.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT `vendorname` FROM `vendor` ORDER BY `vendorname` ASC";
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
            }

            return dt;
        }

        public DataTable GetEmployeeList()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbObject.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT `vendor` FROM `issuenotesave1` ORDER BY `vendor` ASC";
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
            }

            return dt;
        }

        public DataTable GetFormData(string FormType, string Fdate, string Tdate, string Name, string SerialNo, string Department, string GINNo)
        {
            DataTable dt = new DataTable();

            try
            {
                _commandtext = CommandText(FormType, Fdate, Tdate, Name, SerialNo, Department, GINNo);

                using (var con = new MySqlConnection(_dbObject.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = _commandtext;
                        cmd.Parameters.AddWithValue("@fdate", Fdate);
                        cmd.Parameters.AddWithValue("@tdate", Tdate);
                        cmd.Parameters.AddWithValue("@name", Name);
                        cmd.Parameters.AddWithValue("@serialNo", SerialNo);
                        cmd.Parameters.AddWithValue("@department", Department);
                        cmd.Parameters.AddWithValue("@ginNo", GINNo);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
            }

            return dt;
        }

        private string CommandText(string FormType, string Fdate, string Tdate, string Name, string SerialNo, string Department, string GINNo)
        {
            string sqltext = string.Empty;

            if (FormType == "GRN")
            {
                _sql1 = @"SELECT t.`id`,
                                 t.`serialNo` AS 'Serial No',
                                 t.`date` AS 'Date',
                                 t.`vendor` AS 'Supplier Name',
                                 t.`memo` AS 'Memo',
                                 t.`total` AS 'Total'
                          FROM `grnsave1` t
                          WHERE t.`date` BETWEEN @fdate AND @tdate";
            }

            if (FormType == "IssueNote")
            {
                _sql1 = @"SELECT t.`id`,
                                 t.`serialNo` AS 'Serial No',
                                 t.`date` AS 'Date',
                                 CONCAT(t.`title`,t.`vendor`) AS 'Employee Name',
                                 t.`department` AS 'Department',
                                 t.`memo` AS 'Memo',
                                 t.`ginNo` AS 'GINNo',
                                 t.`total` AS 'Total'
                          FROM `issuenotesave1` t
                          WHERE t.`date` BETWEEN @fdate AND @tdate";
            }

            if (FormType == "GRN Return")
            {
                _sql1 = @"SELECT t.`id`,
                                 t.`serialNo` AS 'Serial No',
                                 t.`date` AS 'Date',
                                 t.`vendor` AS 'Employee Name',
                                 t.`memo` AS 'Memo',
                                 t.`total` AS 'Total'
                          FROM `grnreturnsave1` t
                          WHERE t.`date` BETWEEN @fdate AND @tdate";
            }

            if (!string.IsNullOrEmpty(Name))
            {
                _sql2 = " AND t.`vendor` = @name";
            }

            if (!string.IsNullOrEmpty(SerialNo))
            {
                _sql3 = " AND t.`serialNo` = @serialNo";
            }

            if (!string.IsNullOrEmpty(Department))
            {
                _sql4 = " AND t.`department` = @department";
            }

            if (!string.IsNullOrEmpty(GINNo))
            {
                _sql5 = " AND t.`ginNo` = @ginNo";
            }

            return sqltext = string.Format("{0}{1}{2}{3}{4}", _sql1, _sql2, _sql3, _sql4, _sql5);
        }
    }
}

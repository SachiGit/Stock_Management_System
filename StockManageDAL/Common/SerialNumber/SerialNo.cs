using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageDAL.Common.SerialNumber
{
    public class SerialNo : SerialNoInterface
    {
        DBAccess _DBConnection;

        private string _tablePreferences, _table2, _columnName;

        public SerialNo(string _formName)
        {
            _DBConnection = new DBAccess();
            SelectTables(_formName);
        }

        public DataTable GetPrefix()
        {
            DataTable dt = new DataTable();
            using (var con = new MySqlConnection(_DBConnection.Sqlconnection()))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT starting_serial_method, serial_index, vender_method AS `VendorCustomerLoadMethod` FROM {0}", MySqlHelper.EscapeString(_tablePreferences));
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void SelectTables(string _formName)
        {
            if (string.CompareOrdinal(_formName, "grn") == 0)
            {
                _tablePreferences = "preferences_grn";
                _table2 = "grnsave1";
                _columnName = "serialNo";
            }
            else if (string.CompareOrdinal(_formName, "issuenote") == 0)
            {
                _tablePreferences = "preferences_issuenote";
                _table2 = "issuenotesave1";
                _columnName = "serialNo";
            }
            else if (string.CompareOrdinal(_formName, "grnreturn") == 0)
            {
                _tablePreferences = "preferences_grnreturn";
                _table2 = "grnreturnsave1";
                _columnName = "serialNo";
            }
        }

        public int GetSimpleSerialNumber()
        {
            int _serialNumber = 0;
            DataTable _resultTable = new DataTable();

            using (var con = new MySqlConnection(_DBConnection.Sqlconnection()))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT MAX(CAST({0} AS UNSIGNED)) FROM {1} ORDER BY {0} DESC", MySqlHelper.EscapeString(_columnName), MySqlHelper.EscapeString(_table2));
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(_resultTable);
                }
            }

            if (_resultTable.Rows.Count > 0)
            {
                int.TryParse(_resultTable.Rows[0][0].ToString(), out _serialNumber);
            }

            return (_serialNumber + 1);
        }

        public DataTable GetFormWiseSerialNumber(string _prefix)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_DBConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT MAX(CAST(`serialNo` AS UNSIGNED)) FROM {0} ORDER BY `serialNo` DESC", MySqlHelper.EscapeString(_prefix));
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                throw;
            }

            return dt;
        }
    }
}

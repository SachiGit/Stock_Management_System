using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Common.SerialNumber;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageDAL.Common.SerialNumberList
{
    public class SerialNoList : SerialNoListInterface
    {
        DBAccess _dbConnection;
        string _tableName;

        public SerialNoList(string _table)
        {
            _dbConnection = new DBAccess();
            _tableName = _table;
        }

        public SerialNumberDTO GetDetails()
        {
            DataTable _dt = new DataTable();
            SerialNumberDTO _serialDTO = new SerialNumberDTO();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT `id`,`serialNo` FROM {0} ORDER BY CAST(`serialNo` AS UNSIGNED)", MySqlHelper.EscapeString(_tableName));
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(_dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                throw;
            }

            _serialDTO.IDList = new System.Collections.ArrayList();
            _serialDTO.NumberList = new System.Collections.ArrayList();

            for (int _rowCount = 0; _rowCount < _dt.Rows.Count; _rowCount++)
            {
                _serialDTO.IDList.Add(_dt.Rows[_rowCount]["id"].ToString());
                _serialDTO.NumberList.Add(_dt.Rows[_rowCount]["serialNo"].ToString());
            }

            return _serialDTO;
        }
    }
}

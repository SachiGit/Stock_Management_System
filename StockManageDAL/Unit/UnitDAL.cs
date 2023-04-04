using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Unit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageDAL.Unit
{
    public class UnitDAL
    {
         DBAccess _dbConnection;

        public UnitDAL()
        {
            _dbConnection = new DBAccess();
        }

        public bool Save(UnitDTO _unitDTOObject)
        {
            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO `unit`(`unit`,`description`) VALUES (@unit,@description)";

                        cmd.Parameters.Add(new MySqlParameter("@unit", MySqlDbType.VarChar)).Value = _unitDTOObject.Unit;
                        cmd.Parameters.Add(new MySqlParameter("@description", MySqlDbType.VarChar)).Value = _unitDTOObject.Description;
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                return false;
            }
        }

        public bool Update(UnitDTO _unitDTOObject)
        {
            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE `unit` SET `unit` = @unit,
                                                   `description` = @description
                                            WHERE `id` = @id";

                        cmd.Parameters.Add(new MySqlParameter("@unit", MySqlDbType.VarChar)).Value = _unitDTOObject.Unit;
                        cmd.Parameters.Add(new MySqlParameter("@description", MySqlDbType.VarChar)).Value = _unitDTOObject.Description;
                        cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = _unitDTOObject.ID;
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                return false;
            }
        }

        public bool Delete(int _id)
        {
            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM `unit` WHERE `id` = @id";
                        cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32)).Value = _id;
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                return false;
            }
        }

        public int GetDuplicateForSave(string _unit)
        {
            int Count = 0;

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT COUNT(`unit`) FROM `unit` WHERE `unit` = @unit";
                        cmd.Parameters.Add(new MySqlParameter("@unit", MySqlDbType.VarChar)).Value = _unit;
                        var dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            Count = dr.GetInt32(0);
                        }

                        return Count;
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
                return Count;
            }
        }

        public UnitDTO GetUnitData(int _id)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT `id`,`unit`,`description` FROM `unit` WHERE `id` = @id";
                        cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32)).Value = _id;
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} \r\nTrace Message : {1}", ex.Message, ex.StackTrace));
            }

            UnitDTO _UnitDTO = new UnitDTO()
            {
                ID = dt.Rows[0]["id"].ToString(),
                Unit = dt.Rows[0]["unit"].ToString(),
                Description = dt.Rows[0]["description"].ToString()
            };

            return _UnitDTO;
        }

        public DataTable GetUnitList()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT `id`,`unit`,`description` FROM `unit` ORDER BY `unit` ASC";
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
    }
}

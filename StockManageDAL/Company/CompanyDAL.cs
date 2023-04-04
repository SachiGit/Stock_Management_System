using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Company;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockManageDAL.Company
{
    public class CompanyDAL
    {
        DBAccess _dbObject;

        public CompanyDAL()
        {
            _dbObject = new DBAccess();
        }

        public bool CheckExist()
        {
            bool isExist = false;
            int Count = 0;

            using (var conn = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM `company`";
                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Count = dr.GetInt32(0);
                    }
                }
            }

            if (Count > 0)
            {
                isExist = true;
            }

            return isExist;
        }

        public bool Save(CompanyDTO _companyDTO)
        {
            try
            {
                using (var conn = new MySqlConnection(_dbObject.Sqlconnection()))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO `company` (`companyName`,`address`,`phone`,`fax`,`email`,`web`)
                                        VALUES (@companyName,@address,@phone,@fax,@email,@web)";

                        cmd.Parameters.Add(new MySqlParameter("@companyName", MySqlDbType.VarChar)).Value = _companyDTO.ComName;
                        cmd.Parameters.Add(new MySqlParameter("@address", MySqlDbType.VarChar)).Value = _companyDTO.Address;
                        cmd.Parameters.Add(new MySqlParameter("@phone", MySqlDbType.VarChar)).Value = _companyDTO.Telephone;
                        cmd.Parameters.Add(new MySqlParameter("@fax", MySqlDbType.VarChar)).Value = _companyDTO.Fax;
                        cmd.Parameters.Add(new MySqlParameter("@email", MySqlDbType.VarChar)).Value = _companyDTO.Email;
                        cmd.Parameters.Add(new MySqlParameter("@web", MySqlDbType.VarChar)).Value = _companyDTO.Web;

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} /r/nTrace Message : {1}", ex.Message, ex.StackTrace));
                return false;
            }
        }

        public bool Update(CompanyDTO _companyDTO)
        {
            try
            {
                using (var conn = new MySqlConnection(_dbObject.Sqlconnection()))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE `company` SET `companyName` = @companyName,
                                                                 `address` = @address,
                                                                 `phone` = @phone,
                                                                 `fax` = @fax,
                                                                 `email` = @email,
                                                                 `web` = @web
                                            WHERE `id` = @id";

                        cmd.Parameters.Add(new MySqlParameter("@companyName", MySqlDbType.VarChar)).Value = _companyDTO.ComName;
                        cmd.Parameters.Add(new MySqlParameter("@address", MySqlDbType.VarChar)).Value = _companyDTO.Address;
                        cmd.Parameters.Add(new MySqlParameter("@phone", MySqlDbType.VarChar)).Value = _companyDTO.Telephone;
                        cmd.Parameters.Add(new MySqlParameter("@fax", MySqlDbType.VarChar)).Value = _companyDTO.Fax;
                        cmd.Parameters.Add(new MySqlParameter("@email", MySqlDbType.VarChar)).Value = _companyDTO.Email;
                        cmd.Parameters.Add(new MySqlParameter("@web", MySqlDbType.VarChar)).Value = _companyDTO.Web;
                        cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32)).Value = _companyDTO.ID;

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(string.Format("Error Message : {0} /r/nTrace Message : {1}", ex.Message, ex.StackTrace));
                return false;
            }
        }

        public CompanyDTO GetCompanyData()
        {
            DataTable _dtData = new DataTable();
            CompanyDTO _companyDTO = new CompanyDTO();

            using (var con = new MySqlConnection(_dbObject.Sqlconnection()))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT `id`,`companyName`,`address`,`phone`,`fax`,`email`,`web` FROM `company`";
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(_dtData);
                }
            }

            _companyDTO.ID = (_dtData.Rows.Count > 0) ? ((_dtData.Rows[0]["id"].Equals(DBNull.Value)) ? string.Empty : _dtData.Rows[0]["id"].ToString()) : string.Empty;
            _companyDTO.ComName = (_dtData.Rows.Count > 0) ? ((_dtData.Rows[0]["companyName"].Equals(DBNull.Value)) ? string.Empty : _dtData.Rows[0]["companyName"].ToString()) : string.Empty;
            _companyDTO.Address = (_dtData.Rows.Count > 0) ? ((_dtData.Rows[0]["address"].Equals(DBNull.Value)) ? string.Empty : _dtData.Rows[0]["address"].ToString()) : string.Empty;
            _companyDTO.Telephone = (_dtData.Rows.Count > 0) ? ((_dtData.Rows[0]["phone"].Equals(DBNull.Value)) ? string.Empty : _dtData.Rows[0]["phone"].ToString()) : string.Empty;
            _companyDTO.Fax = (_dtData.Rows.Count > 0) ? ((_dtData.Rows[0]["fax"].Equals(DBNull.Value)) ? string.Empty : _dtData.Rows[0]["fax"].ToString()) : string.Empty;
            _companyDTO.Email = (_dtData.Rows.Count > 0) ? ((_dtData.Rows[0]["email"].Equals(DBNull.Value)) ? string.Empty : _dtData.Rows[0]["email"].ToString()) : string.Empty;
            _companyDTO.Web = (_dtData.Rows.Count > 0) ? ((_dtData.Rows[0]["web"].Equals(DBNull.Value)) ? string.Empty : _dtData.Rows[0]["web"].ToString()) : string.Empty;

            return _companyDTO;
        }
    }
}

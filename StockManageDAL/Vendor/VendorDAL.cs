using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Vendor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageDAL.Vendor
{
    public class VendorDAL
    {
        DBAccess _dbConnection;

        public VendorDAL()
        {
            _dbConnection = new DBAccess();
        }

        public bool Save(VendorDTO _vendorDTOObject)
        {
            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO `vendor`(`vendorname`,`address`,`phone`,`fax`,`email`,`web`,`memo`)
                                            VALUES (@vendorname,@address,@phone,@fax,@email,@web,@memo)";

                        cmd.Parameters.Add(new MySqlParameter("@vendorname", MySqlDbType.VarChar)).Value = _vendorDTOObject.VendorName;
                        cmd.Parameters.Add(new MySqlParameter("@address", MySqlDbType.VarChar)).Value = _vendorDTOObject.Address;
                        cmd.Parameters.Add(new MySqlParameter("@phone", MySqlDbType.VarChar)).Value = _vendorDTOObject.Telephone;
                        cmd.Parameters.Add(new MySqlParameter("@fax", MySqlDbType.VarChar)).Value = _vendorDTOObject.Fax;
                        cmd.Parameters.Add(new MySqlParameter("@email", MySqlDbType.VarChar)).Value = _vendorDTOObject.Email;
                        cmd.Parameters.Add(new MySqlParameter("@web", MySqlDbType.VarChar)).Value = _vendorDTOObject.Web;
                        cmd.Parameters.Add(new MySqlParameter("@memo", MySqlDbType.VarChar)).Value = _vendorDTOObject.Memo;
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

        public bool Update(VendorDTO _vendorDTOObject)
        {
            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE `vendor` SET `vendorname` = @vendorname,
		                                                        `address` = @address,
		                                                        `phone` = @phone,
		                                                        `fax` = @fax,
		                                                        `email` = @email,
		                                                        `web` = @web,
		                                                        `memo` = @memo
                                             WHERE `id` = @id";

                        cmd.Parameters.Add(new MySqlParameter("@vendorname", MySqlDbType.VarChar)).Value = _vendorDTOObject.VendorName;
                        cmd.Parameters.Add(new MySqlParameter("@address", MySqlDbType.VarChar)).Value = _vendorDTOObject.Address;
                        cmd.Parameters.Add(new MySqlParameter("@phone", MySqlDbType.VarChar)).Value = _vendorDTOObject.Telephone;
                        cmd.Parameters.Add(new MySqlParameter("@fax", MySqlDbType.VarChar)).Value = _vendorDTOObject.Fax;
                        cmd.Parameters.Add(new MySqlParameter("@email", MySqlDbType.VarChar)).Value = _vendorDTOObject.Email;
                        cmd.Parameters.Add(new MySqlParameter("@web", MySqlDbType.VarChar)).Value = _vendorDTOObject.Web;
                        cmd.Parameters.Add(new MySqlParameter("@memo", MySqlDbType.VarChar)).Value = _vendorDTOObject.Memo;
                        cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = _vendorDTOObject.ID;
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
                        cmd.CommandText = @"DELETE FROM `vendor` WHERE `id` = @id";
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

        public int GetDuplicateForSave(string _vendor)
        {
            int Count = 0;

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT COUNT(`vendorname`) FROM `vendor` WHERE `vendorname` = @vendor";
                        cmd.Parameters.Add(new MySqlParameter("@vendor", MySqlDbType.VarChar)).Value = _vendor;
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

        public VendorDTO GetVendorData(int _id)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT `id`,`vendorname`,`address`,`phone`,`fax`,`email`,`web`,`memo` FROM `vendor` WHERE `id` = @id";
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

            VendorDTO _UnitDTO = new VendorDTO()
            {
                ID = dt.Rows[0]["id"].ToString(),
                VendorName = dt.Rows[0]["vendorname"].ToString(),
                Address = dt.Rows[0]["address"].ToString(),
                Telephone = dt.Rows[0]["phone"].ToString(),
                Fax = dt.Rows[0]["fax"].ToString(),
                Email = dt.Rows[0]["email"].ToString(),
                Web = dt.Rows[0]["web"].ToString(),
                Memo = dt.Rows[0]["memo"].ToString()
            };

            return _UnitDTO;
        }

        public DataTable GetVendorList()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT `id`,`vendorname`,`address`,`phone`,`fax`,`email`,`web`,`memo` FROM `vendor` ORDER BY `vendorname` ASC";
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

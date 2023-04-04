using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageDAL.User
{
    public class UserDAL
    {
        DBAccess _dbConnection;

        public UserDAL()
        {
            _dbConnection = new DBAccess();
        }

        public bool Save(UserDTO _userDTOObject)
        {
            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO `userlogin` (`username`,`password`,`usertype`,`fullname`)
                                            VALUES (@username,@password,@usertype,@fullname)";

                        cmd.Parameters.Add(new MySqlParameter("@username", MySqlDbType.VarChar)).Value = _userDTOObject.UserName;
                        cmd.Parameters.Add(new MySqlParameter("@password", MySqlDbType.VarChar)).Value = _userDTOObject.Password;
                        cmd.Parameters.Add(new MySqlParameter("@usertype", MySqlDbType.VarChar)).Value = _userDTOObject.UserType;
                        cmd.Parameters.Add(new MySqlParameter("@fullname", MySqlDbType.VarChar)).Value = _userDTOObject.FullName;

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

        public bool Update(UserDTO _userDTOObject)
        {
            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE `userlogin` SET `username` = @username,
                                                                   `password` = @password,
                                                                   `usertype` = @usertype,
                                                                   `fullname` = @fullname
                                            WHERE `id` = @id";

                        cmd.Parameters.Add(new MySqlParameter("@username", MySqlDbType.VarChar)).Value = _userDTOObject.UserName;
                        cmd.Parameters.Add(new MySqlParameter("@password", MySqlDbType.VarChar)).Value = _userDTOObject.Password;
                        cmd.Parameters.Add(new MySqlParameter("@usertype", MySqlDbType.VarChar)).Value = _userDTOObject.UserType;
                        cmd.Parameters.Add(new MySqlParameter("@fullname", MySqlDbType.VarChar)).Value = _userDTOObject.FullName;
                        cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = _userDTOObject.ID;

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
                        cmd.CommandText = @"DELETE FROM `userlogin` WHERE `id` = @id";
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

        public int GetDuplicateForSave(string _name)
        {
            int Count = 0;

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT COUNT(`username`) FROM `userlogin` WHERE `username` = @name";
                        cmd.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar)).Value = _name;
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

        public UserDTO GetUserData(int _id)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT `id`,`username`,`password`,`usertype`,`fullname` FROM `userlogin` WHERE `id` = @id";
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

            UserDTO _userDTO = new UserDTO()
            {
                ID = dt.Rows[0]["id"].ToString(),
                UserName = dt.Rows[0]["username"].ToString(),
                Password = dt.Rows[0]["password"].ToString(),
                UserType = dt.Rows[0]["usertype"].ToString(),
                FullName = dt.Rows[0]["fullname"].ToString()
            };

            return _userDTO;
        }

        public DataTable GetUserList()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT `id`,`username`,`usertype`,`fullname` FROM `userlogin` ORDER BY `username` ASC";
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

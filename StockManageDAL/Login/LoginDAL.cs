using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageDAL.Login
{
    public class LoginDAL
    {
        DBAccess _dbObject;

        public LoginDAL()
        {
            _dbObject = new DBAccess();
        }

        public LoginDTO GetUserAuthentication(LoginDTO _loginObject)
        {
            try
            {
                using (var con = new MySqlConnection(_dbObject.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT `id`,`username`,`password`,`usertype` FROM `userlogin` WHERE `username` = @name";
                        cmd.Parameters.AddWithValue("@name", _loginObject.UserName);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        LoginDTO _login = new LoginDTO()
                        {
                            UserID = int.Parse(dt.Rows[0]["id"].ToString()),
                            UserName = dt.Rows[0]["username"].ToString(),
                            Password = dt.Rows[0]["password"].ToString(),
                            UserType = dt.Rows[0]["usertype"].ToString()
                        };

                        return _login;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}

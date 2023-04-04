using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Department;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageDAL.Department
{
    public class DepartmentDAL
    {
        DBAccess _dbConnection;

        public DepartmentDAL()
        {
            _dbConnection = new DBAccess();
        }

        public bool Save(DepartmentDTO _departmentDTOObject)
        {
            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO `department`(`department`,`description`,`voteNo`,`budget`) 
                                            VALUES (@department,@description,@voteNo,@budget)";

                        cmd.Parameters.Add(new MySqlParameter("@department", MySqlDbType.VarChar)).Value = _departmentDTOObject.Department;
                        cmd.Parameters.Add(new MySqlParameter("@description", MySqlDbType.VarChar)).Value = _departmentDTOObject.Description;
                        cmd.Parameters.Add(new MySqlParameter("@voteNo", MySqlDbType.VarChar)).Value = _departmentDTOObject.Vote;
                        cmd.Parameters.Add(new MySqlParameter("@budget", MySqlDbType.Double)).Value = _departmentDTOObject.Budget;
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

        public bool Update(DepartmentDTO _departmentDTOObject)
        {
            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE `department` SET `department` = @department,
                                                                    `description` = @description,
                                                                    `voteNo` = @voteNo,
                                                                    `budget` = @budget
                                            WHERE `id` = @id";

                        cmd.Parameters.Add(new MySqlParameter("@department", MySqlDbType.VarChar)).Value = _departmentDTOObject.Department;
                        cmd.Parameters.Add(new MySqlParameter("@description", MySqlDbType.VarChar)).Value = _departmentDTOObject.Description;
                        cmd.Parameters.Add(new MySqlParameter("@voteNo", MySqlDbType.VarChar)).Value = _departmentDTOObject.Vote;
                        cmd.Parameters.Add(new MySqlParameter("@budget", MySqlDbType.VarChar)).Value = _departmentDTOObject.Budget;
                        cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = _departmentDTOObject.ID;
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
                        cmd.CommandText = @"DELETE FROM `department` WHERE `id` = @id";
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

        public int GetDuplicateForSave(string _department)
        {
            int Count = 0;

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT COUNT(`department`) FROM `department` WHERE `department` = @department";
                        cmd.Parameters.Add(new MySqlParameter("@department", MySqlDbType.VarChar)).Value = _department;
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

        public DepartmentDTO GetDepartmentData(int _id)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT `id`,`department`,`description`,`voteNo`,`budget` FROM `department` WHERE `id` = @id";
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

            DepartmentDTO _UnitDTO = new DepartmentDTO()
            {
                ID = dt.Rows[0]["id"].ToString(),
                Department = dt.Rows[0]["department"].ToString(),
                Description = dt.Rows[0]["description"].ToString(),
                Vote = dt.Rows[0]["voteNo"].ToString(),
                Budget = Convert.ToDouble(dt.Rows[0]["budget"].ToString())
            };

            return _UnitDTO;
        }

        public DataTable GetDepartmentList()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT `id`,`department`,`description`,`voteNo`,`budget` FROM `department` ORDER BY `department` ASC";
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

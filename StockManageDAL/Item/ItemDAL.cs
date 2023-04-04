using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Item;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageDAL.Item
{
    public class ItemDAL
    {
        DBAccess _dbConnection;

        public ItemDAL()
        {
            _dbConnection = new DBAccess();
        }

        public bool Save(ItemDTO _itemDTOObject)
        {
            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO `item` (`itemName`,`itemCode`,`description`,`unit`,`price1`,`price2`,`price3`,`price4`,`price5`,`price6`,`reorderlevel`)
                                            VALUES (@itemName,@itemCode,@description,@unit,@price1,@price2,@price3,@price4,@price5,@price6,@reorderlevel)";

                        cmd.Parameters.Add(new MySqlParameter("@itemName", MySqlDbType.VarChar)).Value = _itemDTOObject.ItemName;
                        cmd.Parameters.Add(new MySqlParameter("@itemCode", MySqlDbType.VarChar)).Value = _itemDTOObject.ItemCode;
                        cmd.Parameters.Add(new MySqlParameter("@description", MySqlDbType.VarChar)).Value = _itemDTOObject.Description;
                        cmd.Parameters.Add(new MySqlParameter("@unit", MySqlDbType.VarChar)).Value = _itemDTOObject.Unit;
                        cmd.Parameters.Add(new MySqlParameter("@price1", MySqlDbType.Double)).Value = _itemDTOObject.Price1;
                        cmd.Parameters.Add(new MySqlParameter("@price2", MySqlDbType.Double)).Value = _itemDTOObject.Price2;
                        cmd.Parameters.Add(new MySqlParameter("@price3", MySqlDbType.Double)).Value = _itemDTOObject.Price3;
                        cmd.Parameters.Add(new MySqlParameter("@price4", MySqlDbType.Double)).Value = _itemDTOObject.Price4;
                        cmd.Parameters.Add(new MySqlParameter("@price5", MySqlDbType.Double)).Value = _itemDTOObject.Price5;
                        cmd.Parameters.Add(new MySqlParameter("@price6", MySqlDbType.Double)).Value = _itemDTOObject.Price6;
                        cmd.Parameters.Add(new MySqlParameter("@reorderlevel", MySqlDbType.Double)).Value = _itemDTOObject.ReOrderLevel;

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

        public bool Update(ItemDTO _itemDTOObject)
        {
            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE `item` SET  `itemName` = @itemName,
                                                               `description` = @description,
                                                               `unit` = @unit,
                                                               `price1` = @price1,
                                                               `price2` = @price2,
                                                               `price3` = @price3,
                                                               `price4` = @price4,
                                                               `price5` = @price5,
                                                               `price6` = @price6,
                                                               `reorderlevel` = @reorderlevel
                                            WHERE `id` = @id";

                        cmd.Parameters.Add(new MySqlParameter("@itemName", MySqlDbType.VarChar)).Value = _itemDTOObject.ItemName;
                        cmd.Parameters.Add(new MySqlParameter("@description", MySqlDbType.VarChar)).Value = _itemDTOObject.Description;
                        cmd.Parameters.Add(new MySqlParameter("@unit", MySqlDbType.VarChar)).Value = _itemDTOObject.Unit;
                        cmd.Parameters.Add(new MySqlParameter("@price1", MySqlDbType.Double)).Value = _itemDTOObject.Price1;
                        cmd.Parameters.Add(new MySqlParameter("@price2", MySqlDbType.Double)).Value = _itemDTOObject.Price2;
                        cmd.Parameters.Add(new MySqlParameter("@price3", MySqlDbType.Double)).Value = _itemDTOObject.Price3;
                        cmd.Parameters.Add(new MySqlParameter("@price4", MySqlDbType.Double)).Value = _itemDTOObject.Price4;
                        cmd.Parameters.Add(new MySqlParameter("@price5", MySqlDbType.Double)).Value = _itemDTOObject.Price5;
                        cmd.Parameters.Add(new MySqlParameter("@price6", MySqlDbType.Double)).Value = _itemDTOObject.Price6;
                        cmd.Parameters.Add(new MySqlParameter("@reorderlevel", MySqlDbType.Double)).Value = _itemDTOObject.ReOrderLevel;
                        cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar)).Value = _itemDTOObject.ID;

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
                        cmd.CommandText = @"DELETE FROM `item` WHERE `id` = @id";
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

        public int GetDuplicateForSave(string _code)
        {
            int Count = 0;

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT COUNT(`itemCode`) FROM `item` WHERE `itemCode` = @code";
                        cmd.Parameters.Add(new MySqlParameter("@code", MySqlDbType.VarChar)).Value = _code;
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

        public ItemDTO GetItemDetails(int _id)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT `id`,`itemName`,`itemCode`,`description`,`unit`,`price1`,`price2`,`price3`,`price4`,`price5`,`price6`,`reorderlevel` 
                                            FROM `item` 
                                            WHERE `id` = @id";
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

            ItemDTO _itemDTO = new ItemDTO()
            {
                ID = dt.Rows[0]["id"].ToString(),
                ItemName = dt.Rows[0]["itemName"].ToString(),
                ItemCode = dt.Rows[0]["itemCode"].ToString(),
                Description = dt.Rows[0]["description"].ToString(),
                Unit = dt.Rows[0]["unit"].ToString(),
                Price1 = Convert.ToDouble(dt.Rows[0]["price1"].ToString()),
                Price2 = Convert.ToDouble(dt.Rows[0]["price2"].ToString()),
                Price3 = Convert.ToDouble(dt.Rows[0]["price3"].ToString()),
                Price4 = Convert.ToDouble(dt.Rows[0]["price4"].ToString()),
                Price5 = Convert.ToDouble(dt.Rows[0]["price5"].ToString()),
                Price6 = Convert.ToDouble(dt.Rows[0]["price6"].ToString()),
                ReOrderLevel = Convert.ToDouble(dt.Rows[0]["reorderlevel"].ToString())
            };

            return _itemDTO;
        }

        public DataTable GetItemList()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT `id`,`itemCode`,`itemName`,`description`,`unit`,`price1`,`price2`,`price3`,`price4`,`price5`,`price6`,`reorderlevel` 
                                            FROM `item` 
                                            ORDER BY `itemCode`,`itemName` ASC";
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

        public DataTable GetDetailsOfItem(string _itemcode)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "CALL DetailsFoSelectedrItem(@itemCode)";
                        cmd.Parameters.AddWithValue("@itemCode", _itemcode);
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

        public DataTable GetOnhandOfItem(string _itemcode)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "CALL onhand_for_selectitem(@itemCode)";
                        cmd.Parameters.AddWithValue("@itemCode", _itemcode);
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

        public DataTable GetItemPriceList(string _itemcode) 
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "CALL ItemPriceList(@itemCode)";
                        cmd.Parameters.AddWithValue("@itemCode", _itemcode);
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

        public DataTable GetOnHandWithItemDetails()
        {
            DataTable dt = new DataTable();

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "CALL onhandWithItemDetails()";
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

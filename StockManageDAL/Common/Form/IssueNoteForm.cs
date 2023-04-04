using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Common;
using StockManageDTO.IssueNote;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageDAL.Common.Form
{
    public class IssueNoteForm : FormInterface
    {
        DBAccess _dbConnection;

        public IssueNoteForm()
        {
            _dbConnection = new DBAccess();
        }

        public bool Save(CommonDTO _issuenoteDTO)
        {
            bool _isExcecuted = false;
            MySqlTransaction _transaction = null;
            long ID;

            try
            {
                using (var conn = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    conn.Open();
                    _transaction = conn.BeginTransaction();

                    using (var cmd = conn.CreateCommand())
                    {
                        #region Fields

                        cmd.CommandText = @"INSERT INTO `issuenotesave1` (`serialNo`,`date`,`title`,`vendor`,`department`,`memo`,`total`,`voteNo`,`ginNo`)
                                            VALUES (@serialNo,@date,@title,@vendor,@department,@memo,@total,@voteNo,@ginNo)";

                        cmd.Parameters.Add(new MySqlParameter("@serialNo", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.SerialNumber;
                        cmd.Parameters.Add(new MySqlParameter("@date", MySqlDbType.Date)).Value = _issuenoteDTO.IssueNote.Fields.Date;
                        cmd.Parameters.Add(new MySqlParameter("@title", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.Title;
                        cmd.Parameters.Add(new MySqlParameter("@vendor", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.VendorName;
                        cmd.Parameters.Add(new MySqlParameter("@department", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.Department;
                        cmd.Parameters.Add(new MySqlParameter("@memo", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.Memo;
                        cmd.Parameters.Add(new MySqlParameter("@total", MySqlDbType.Double)).Value = _issuenoteDTO.IssueNote.Fields.Total;
                        cmd.Parameters.Add(new MySqlParameter("@voteNo", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.VoteNo;
                        cmd.Parameters.Add(new MySqlParameter("@ginNo", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.GinNo;
                        cmd.ExecuteNonQuery();
                        ID = cmd.LastInsertedId;

                        #endregion

                        #region Grid

                        for (int i = 0; i < _issuenoteDTO.IssueNote.Grid.Count; i++)
                        {
                            cmd.CommandText = @"INSERT INTO `issuenotesave2`(`id`,`serialNo`,`itemCode`,`itemName`,`description`,`qty`,`rate`,`amount`,`unit`)
                                                VALUES (@id,@serialNo,@itemCode,@itemName,@description,@qty,@rate,@amount,@unit)";

                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32)).Value = ID;
                            cmd.Parameters.Add(new MySqlParameter("@serialNo", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.SerialNumber;
                            cmd.Parameters.Add(new MySqlParameter("@itemCode", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Grid[i].ItemCode;
                            cmd.Parameters.Add(new MySqlParameter("@itemName", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Grid[i].ItemName;
                            cmd.Parameters.Add(new MySqlParameter("@description", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Grid[i].Description;
                            cmd.Parameters.Add(new MySqlParameter("@qty", MySqlDbType.Double)).Value = _issuenoteDTO.IssueNote.Grid[i].QTY;
                            cmd.Parameters.Add(new MySqlParameter("@rate", MySqlDbType.Double)).Value = _issuenoteDTO.IssueNote.Grid[i].Rate;
                            cmd.Parameters.Add(new MySqlParameter("@amount", MySqlDbType.Double)).Value = _issuenoteDTO.IssueNote.Grid[i].Amount;
                            cmd.Parameters.Add(new MySqlParameter("@unit", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Grid[i].Unit;
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        _transaction.Commit();
                        _isExcecuted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                try
                {
                    _transaction.Rollback();
                }
                catch (MySqlException sqlException)
                {
                    Clipboard.SetText(string.Format("Cannot rollback transaction. {0}", sqlException.Message));
                }
                throw;
            }
            return _isExcecuted;
        }

        public bool Update(CommonDTO _issuenoteDTO)
        {
            bool _isExcecuted = false;
            MySqlTransaction _transaction = null;

            try
            {
                using (var conn = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    conn.Open();
                    _transaction = conn.BeginTransaction();

                    using (var cmd = conn.CreateCommand())
                    {
                        #region Fields

                        cmd.CommandText = @"UPDATE `issuenotesave1` SET `date` = @date,
                                                                        `title` = @title,
                                                                        `vendor` = @vendor,
                                                                        `department` = @department,
                                                                        `memo` = @memo,
                                                                        `total` = @total,
                                                                        `voteNo` = @voteNo
                                             WHERE `id` = @id";

                        cmd.Parameters.Add(new MySqlParameter("@date", MySqlDbType.Date)).Value = _issuenoteDTO.IssueNote.Fields.Date;
                        cmd.Parameters.Add(new MySqlParameter("@title", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.Title;
                        cmd.Parameters.Add(new MySqlParameter("@vendor", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.VendorName;
                        cmd.Parameters.Add(new MySqlParameter("@department", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.Department;
                        cmd.Parameters.Add(new MySqlParameter("@memo", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.Memo;
                        cmd.Parameters.Add(new MySqlParameter("@total", MySqlDbType.Double)).Value = _issuenoteDTO.IssueNote.Fields.Total;
                        cmd.Parameters.Add(new MySqlParameter("@voteNo", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.VoteNo;
                        cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32)).Value = _issuenoteDTO.IssueNote.Fields.ID;
                        cmd.ExecuteNonQuery();

                        #endregion

                        #region Grid

                        for (int i = 0; i < _issuenoteDTO.IssueNote.Grid.Count; i++)
                        {
                            if (_issuenoteDTO.IssueNote.Grid[i].UpID != "")
                            {
                                #region Update
                                cmd.CommandText = @"UPDATE `issuenotesave2` SET `itemCode` = @itemCode,
                                                                          `itemName` = @itemName,
                                                                          `description` = @description,
                                                                          `qty` = @qty,
                                                                          `rate` = @rate,
                                                                          `amount` = @amount,
                                                                          `unit` = @unit
                                                    WHERE `up` = @up";

                                cmd.Parameters.Clear();
                                cmd.Parameters.Add(new MySqlParameter("@itemCode", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Grid[i].ItemCode;
                                cmd.Parameters.Add(new MySqlParameter("@itemName", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Grid[i].ItemName;
                                cmd.Parameters.Add(new MySqlParameter("@description", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Grid[i].Description;
                                cmd.Parameters.Add(new MySqlParameter("@qty", MySqlDbType.Double)).Value = _issuenoteDTO.IssueNote.Grid[i].QTY;
                                cmd.Parameters.Add(new MySqlParameter("@rate", MySqlDbType.Double)).Value = _issuenoteDTO.IssueNote.Grid[i].Rate;
                                cmd.Parameters.Add(new MySqlParameter("@amount", MySqlDbType.Double)).Value = _issuenoteDTO.IssueNote.Grid[i].Amount;
                                cmd.Parameters.Add(new MySqlParameter("@unit", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Grid[i].Unit;
                                cmd.Parameters.Add(new MySqlParameter("@up", MySqlDbType.Int32)).Value = _issuenoteDTO.IssueNote.Grid[i].UpID;
                                cmd.ExecuteNonQuery();
                                #endregion
                            }
                            else
                            {
                                #region Insert
                                cmd.CommandText = @"INSERT INTO `issuenotesave2`(`id`,`serialNo`,`itemCode`,`itemName`,`description`,`qty`,`rate`,`amount`,`unit`)
                                                VALUES (@id,@serialNo,@itemCode,@itemName,@description,@qty,@rate,@amount,@unit)";

                                cmd.Parameters.Clear();
                                cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32)).Value = _issuenoteDTO.IssueNote.Fields.ID;
                                cmd.Parameters.Add(new MySqlParameter("@serialNo", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Fields.SerialNumber;
                                cmd.Parameters.Add(new MySqlParameter("@itemCode", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Grid[i].ItemCode;
                                cmd.Parameters.Add(new MySqlParameter("@itemName", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Grid[i].ItemName;
                                cmd.Parameters.Add(new MySqlParameter("@description", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Grid[i].Description;
                                cmd.Parameters.Add(new MySqlParameter("@qty", MySqlDbType.Double)).Value = _issuenoteDTO.IssueNote.Grid[i].QTY;
                                cmd.Parameters.Add(new MySqlParameter("@rate", MySqlDbType.Double)).Value = _issuenoteDTO.IssueNote.Grid[i].Rate;
                                cmd.Parameters.Add(new MySqlParameter("@amount", MySqlDbType.Double)).Value = _issuenoteDTO.IssueNote.Grid[i].Amount;
                                cmd.Parameters.Add(new MySqlParameter("@unit", MySqlDbType.VarChar)).Value = _issuenoteDTO.IssueNote.Grid[i].Unit;
                                cmd.ExecuteNonQuery();
                                #endregion
                            }
                        }
                        #endregion

                        _transaction.Commit();
                        _isExcecuted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                try
                {
                    _transaction.Rollback();
                }
                catch (MySqlException sqlException)
                {
                    Clipboard.SetText(string.Format("Cannot rollback transaction. {0}", sqlException.Message));
                }
                throw;
            }
            return _isExcecuted;
        }

        public bool Delete(string _id)
        {
            bool _isExcecuted = false;
            MySqlTransaction _transaction = null;

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    con.Open();
                    _transaction = con.BeginTransaction();

                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM `issuenotesave2` WHERE `id` = @id";
                        cmd.Parameters.AddWithValue("@id", _id);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "DELETE FROM `issuenotesave1` WHERE `id` = @id1";
                        cmd.Parameters.AddWithValue("@id1", _id);
                        cmd.ExecuteNonQuery();

                        _transaction.Commit();
                        _isExcecuted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                try
                {
                    _transaction.Rollback();
                }
                catch (MySqlException sqlException)
                {
                    Clipboard.SetText(string.Format("Cannot rollback transaction. {0}", sqlException.Message));
                }
                throw;
            }
            return _isExcecuted;
        }

        public bool DeleteRow(string _upID)
        {
            bool _isExcecuted = false;
            MySqlTransaction _transaction = null;

            try
            {
                using (var con = new MySqlConnection(_dbConnection.Sqlconnection()))
                {
                    if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                    {
                        con.Open();
                    }
                    else
                    {
                        con.Open();
                    }
                    _transaction = con.BeginTransaction();

                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM `issuenotesave2` WHERE `up` = @up";
                        cmd.Parameters.AddWithValue("@up", _upID);
                        cmd.ExecuteNonQuery();

                        _transaction.Commit();
                        _isExcecuted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
                try
                {
                    _transaction.Rollback();
                }
                catch (MySqlException sqlException)
                {
                    Clipboard.SetText(string.Format("Cannot rollback transaction. {0}", sqlException.Message));
                }
                throw;
            }
            return _isExcecuted;
        }

        public CommonDTO NextPrevious(CommonDTO _issuenoteDTO)
        {
            DataTable dtFiled = new DataTable();
            DataTable dtGrid = new DataTable();

            using (var conn = new MySqlConnection(_dbConnection.Sqlconnection()))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM `issuenotesave1` WHERE `id` = @id";
                    cmd.Parameters.AddWithValue("@id", _issuenoteDTO.IssueNote.Fields.ID);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dtFiled);
                }

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM `issuenotesave2` WHERE `id` = @id";
                    cmd.Parameters.AddWithValue("@id", _issuenoteDTO.IssueNote.Fields.ID);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dtGrid);
                }
            }

            #region Feilds

            _issuenoteDTO.IssueNote.Fields.SerialNumber =
               (dtFiled.Rows.Count > 0) ? ((dtFiled.Rows[0]["serialNo"].Equals(DBNull.Value)) ? string.Empty : dtFiled.Rows[0]["serialNo"].ToString()) : string.Empty;

            _issuenoteDTO.IssueNote.Fields.Date =
               (dtFiled.Rows.Count > 0) ? ((dtFiled.Rows[0]["date"].Equals(DBNull.Value)) ? DateTime.Now : Convert.ToDateTime(dtFiled.Rows[0]["date"].ToString())) : DateTime.Now;

            _issuenoteDTO.IssueNote.Fields.Title =
               (dtFiled.Rows.Count > 0) ? ((dtFiled.Rows[0]["title"].Equals(DBNull.Value)) ? string.Empty : dtFiled.Rows[0]["title"].ToString()) : string.Empty;

            _issuenoteDTO.IssueNote.Fields.VendorName =
               (dtFiled.Rows.Count > 0) ? ((dtFiled.Rows[0]["vendor"].Equals(DBNull.Value)) ? string.Empty : dtFiled.Rows[0]["vendor"].ToString()) : string.Empty;

            _issuenoteDTO.IssueNote.Fields.Department =
               (dtFiled.Rows.Count > 0) ? ((dtFiled.Rows[0]["department"].Equals(DBNull.Value)) ? string.Empty : dtFiled.Rows[0]["department"].ToString()) : string.Empty;

            _issuenoteDTO.IssueNote.Fields.Memo =
               (dtFiled.Rows.Count > 0) ? ((dtFiled.Rows[0]["memo"].Equals(DBNull.Value)) ? string.Empty : dtFiled.Rows[0]["memo"].ToString()) : string.Empty;

            _issuenoteDTO.IssueNote.Fields.Total =
               (dtFiled.Rows.Count > 0) ? ((dtFiled.Rows[0]["total"].Equals(DBNull.Value)) ? 0 : Convert.ToDouble(dtFiled.Rows[0]["total"].ToString())) : 0;

            _issuenoteDTO.IssueNote.Fields.VoteNo =
               (dtFiled.Rows.Count > 0) ? ((dtFiled.Rows[0]["voteNo"].Equals(DBNull.Value)) ? string.Empty : dtFiled.Rows[0]["voteNo"].ToString()) : string.Empty;

            _issuenoteDTO.IssueNote.Fields.GinNo =
               (dtFiled.Rows.Count > 0) ? ((dtFiled.Rows[0]["ginNo"].Equals(DBNull.Value)) ? string.Empty : dtFiled.Rows[0]["ginNo"].ToString()) : string.Empty;

            #endregion

            #region Grid

            for (int i = 0; i < dtGrid.Rows.Count; i++)
            {
                IssueNoteGrid _issuenotegridDTO = new IssueNoteGrid()
                {
                    ItemCode =
                       (dtGrid.Rows.Count > 0) ? ((dtGrid.Rows[i]["itemCode"].Equals(DBNull.Value)) ? string.Empty : dtGrid.Rows[i]["itemCode"].ToString()) : string.Empty,

                    ItemName =
                       (dtGrid.Rows.Count > 0) ? ((dtGrid.Rows[i]["itemName"].Equals(DBNull.Value)) ? string.Empty : dtGrid.Rows[i]["itemName"].ToString()) : string.Empty,

                    Description =
                       (dtGrid.Rows.Count > 0) ? ((dtGrid.Rows[i]["description"].Equals(DBNull.Value)) ? string.Empty : dtGrid.Rows[i]["description"].ToString()) : string.Empty,

                    QTY =
                       (dtGrid.Rows.Count > 0) ? ((dtGrid.Rows[i]["qty"].Equals(DBNull.Value)) ? 0 : Convert.ToDouble(dtGrid.Rows[i]["qty"].ToString())) : 0,

                    Rate =
                       (dtGrid.Rows.Count > 0) ? ((dtGrid.Rows[i]["rate"].Equals(DBNull.Value)) ? 0 : Convert.ToDouble(dtGrid.Rows[i]["rate"].ToString())) : 0,

                    Amount =
                       (dtGrid.Rows.Count > 0) ? ((dtGrid.Rows[i]["amount"].Equals(DBNull.Value)) ? 0 : Convert.ToDouble(dtGrid.Rows[i]["amount"].ToString())) : 0,

                    Unit =
                       (dtGrid.Rows.Count > 0) ? ((dtGrid.Rows[i]["unit"].Equals(DBNull.Value)) ? string.Empty : dtGrid.Rows[i]["unit"].ToString()) : string.Empty,
                       
                    UpID =
                       (dtGrid.Rows.Count > 0) ? ((dtGrid.Rows[i]["up"].Equals(DBNull.Value)) ? string.Empty : dtGrid.Rows[i]["up"].ToString()) : string.Empty
                };

                _issuenoteDTO.IssueNote.Grid.Add(_issuenotegridDTO);
            }

            #endregion

            return _issuenoteDTO;
        }
    }
}

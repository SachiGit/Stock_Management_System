using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.IssueNote.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageDAL.IssueNote.Print
{
    public class IssueNotePrintDAL
    {
         DBAccess _dbConnection;

         public IssueNotePrintDAL()
         {
             _dbConnection = new DBAccess();
         }

         public IssueNoteDataSet GetIssueNoteData(string SerialNo)
         {
             IssueNoteDataSet DS = new IssueNoteDataSet();

             using (var conn = new MySqlConnection(_dbConnection.Sqlconnection()))
             {
                 conn.Open();
                 using (var cmd = conn.CreateCommand())
                 {
                     cmd.CommandText = "SELECT `serialNo`,`date`,`title`,`vendor`,`department`,`memo`,`total`,`voteNo`,`ginNo` FROM `issuenotesave1` WHERE `serialNo` = @sno";
                     cmd.Parameters.AddWithValue("@sno", SerialNo);
                     MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                     da.Fill(DS, "Fields");
                 }

                 using (var cmd1 = conn.CreateCommand())
                 {
                     cmd1.CommandText = "SELECT `itemCode`,`itemName`,`description`,`qty`,`rate`,`amount`,`unit` FROM `issuenotesave2` WHERE `serialNo` = @sno";
                     cmd1.Parameters.AddWithValue("@sno", SerialNo);
                     MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                     da1.Fill(DS, "Grid");
                 }

                 using (var cmd2 = conn.CreateCommand())
                 {
                     cmd2.CommandText = "SELECT `companyName`,`address`,`phone`,`fax`,`email`,`web` FROM `company`";
                     MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                     da2.Fill(DS, "Company");
                 }
             }

             return DS;
         }
    }
}

using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StockManageDAL.Common.GINNumber
{
    class CommonGINNumber : CommonGINNumberInterface
    {
         DBAccess _DBConnection;

         public CommonGINNumber()
         {
             _DBConnection = new DBAccess();
         }

         public string GetGINNumber()
         {
             string GINNo = string.Empty;
             DataTable dt = new DataTable();

             try
             {
                 using (var con = new MySqlConnection(_DBConnection.Sqlconnection()))
                 {
                     con.Open();
                     using (var cmd = con.CreateCommand())
                     {
                         cmd.CommandText = "SELECT `ginNo` FROM `issuenotesave1` WHERE `id` = (SELECT MAX(`id`) FROM `issuenotesave1`)";
                         MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                         da.Fill(dt);
                     }
                 }

                 if (dt.Rows.Count > 0)
                 {
                     string _ginNo = dt.Rows[0][0].ToString();
                     string[] arr = _ginNo.Split(' ');

                     int _no = arr.Last().Equals(string.Empty) ? 0 : Convert.ToInt32(arr.Last());
                     int _newno = _no + 1;
                     GINNo = string.Format("{0}{1}", "GIN ", _newno.ToString());
                 }
             }
             catch 
             {
                 GINNo = string.Empty;
             }

             return GINNo;
         }
    }
}

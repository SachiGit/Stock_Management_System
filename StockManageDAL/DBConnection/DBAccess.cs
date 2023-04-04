using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDAL.DBConnection
{
    public class DBAccess
    {
        public DBAccess()
        {
            Sqlconnection();
        }

        public string Sqlconnection()
        {
            string MyConnection = ConfigurationManager.ConnectionStrings["Key"].ConnectionString;
            return MyConnection;
        }
    }
}

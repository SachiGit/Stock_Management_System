using MySql.Data.MySqlClient;
using StockManageDAL.DBConnection;
using StockManageDTO.Login;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManageDAL.Login
{
    public class ConfigDAL
    {
        DBAccess _dbConnection;

        public ConfigDAL()
        {
            _dbConnection = new DBAccess();
        }

        public ConfigDTO GetConfigurations()
        {
            ConfigDTO _configObject = new ConfigDTO();
            string _ConnectionString;

            try
            {
                _ConnectionString = _dbConnection.Sqlconnection();

                MySqlConnectionStringBuilder _builder = new MySqlConnectionStringBuilder(_ConnectionString);

                _configObject.Server = _builder.Server;
                _configObject.Root = _builder.UserID;
                _configObject.Port = _builder.Port;
                _configObject.Password = _builder.Password;
                _configObject.DataBase = _builder.Database;
            }
            catch (Exception ex)
            {
                Clipboard.SetText(ex.Message);
            }

            return _configObject;
        }

        public bool SetConnection(ConfigDTO _configObject)
        {
            bool _canConfigure;
            try
            {
                string conString = string.Format("server={0};User Id={1};password={2};port={3};database={4};Pooling=false;Connection Lifetime=0;connection timeout=10000; default command timeout=1000;Allow User Variables=True;",
                                             _configObject.Server,
                                             _configObject.Root,
                                             _configObject.Password,
                                             _configObject.Port,
                                             _configObject.DataBase);

                const string KeyName = "Key";

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Update the setting.
                config.ConnectionStrings.ConnectionStrings[KeyName].ConnectionString = conString;

                // Save the configuration file.
                config.Save(ConfigurationSaveMode.Modified);

                // Force a reload of the changed section.
                ConfigurationManager.RefreshSection("connectionStrings");

                _canConfigure = true;
            }
            catch (Exception ex)
            {
                _canConfigure = false;
                Clipboard.SetText(ex.Message);
            }

            return _canConfigure;

        }
    }
}

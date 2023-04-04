using StockManageDAL.Login;
using StockManageDTO.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageBLL.Login
{
    public class ConfigBLL
    {
        public ConfigDTO GetConfigurations()
        {
            try
            {
                return new ConfigDAL().GetConfigurations();
            }
            catch
            {
                throw;
            }
        }

        public bool SetConfigurations(ConfigDTO _configurationDTOObject)
        {
            bool _canConfigure;

            //Save Connection String here
            ConfigDAL _configDAL = new ConfigDAL();

            if (_configDAL.SetConnection(_configurationDTOObject))
            {
                _canConfigure = true;
            }
            else
            {
                _canConfigure = false;
            }

            return _canConfigure;
        }
    }
}

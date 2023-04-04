using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDTO.Login
{
    public class ConfigDTO
    {
        public string Server { get; set; }

        public uint Port { get; set; }

        public string Root { get; set; }

        public string Password { get; set; }

        public string DataBase { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDAL.Common.SerialNumber
{
    public interface SerialNoInterface
    {
        void SelectTables(string _formName);
        DataTable GetPrefix();
        int GetSimpleSerialNumber();
        DataTable GetFormWiseSerialNumber(string _prefix);
    }
}

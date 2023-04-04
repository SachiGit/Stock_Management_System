using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDAL.Common.Items
{
    public interface CommonItemInterface
    {
        DataTable GetItems();
    }
}

using StockManageDAL.Reports.ReOrderLevel;
using StockManageDTO.Reports.ReOrderLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageBLL.Reports.ReOrderLevel
{
    public class ReOrderLevelBLL
    {
        ReOrderLevelDAL _reorderlevelDALObject;

        public ReOrderLevelBLL()
        {
            _reorderlevelDALObject = new ReOrderLevelDAL();
        }

        public ReOrderLevelDataSet GetReOrderLevelData()
        {
            return _reorderlevelDALObject.GetReOrderLevelData();
        }
    }
}

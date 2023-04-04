using StockManageDAL.Reports.ItemDetail;
using StockManageDTO.Reports.ItemDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageBLL.Reports.ItemDetail
{
    public class ItemDetailBLL
    {
        ItemDetailDAL _itemDetailDALObject;

        public ItemDetailBLL()
        {
            _itemDetailDALObject = new ItemDetailDAL();
        }

        public ItemDetailDataSet GetItemDetailData()
        {
            return _itemDetailDALObject.GetItemDetailData();
        }
    }
}

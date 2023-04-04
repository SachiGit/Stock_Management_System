using StockManageBLL.Common;
using StockManageDAL.Item;
using StockManageDTO.Item;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageBLL.Item
{
    public class ItemBLL : CommonBLL
    {
        ItemDAL _itemDALObject;

        public ItemBLL()
        {
            _itemDALObject = new ItemDAL();
        }

        public int Save(ItemDTO _itemDTOObject)
        {
            int _result = 0;
            if (_itemDALObject.GetDuplicateForSave(_itemDTOObject.ItemCode) <= 0)
            {
                if (_itemDALObject.Save(_itemDTOObject))
                {
                    _result = 1;
                }
            }
            else
            {
                _result = 2;
            }

            return _result;
        }

        public bool Update(ItemDTO _itemDTOObject)
        {
            bool _isupdate = _itemDALObject.Update(_itemDTOObject);
            return _isupdate;
        }

        public bool DeleteItemDetails(int _id)
        {
            return _itemDALObject.Delete(_id);
        }

        public ItemDTO LoadItemDetails(int _id)
        {
            return _itemDALObject.GetItemDetails(_id);
        }

        public DataTable LoadItemList()
        {
            return _itemDALObject.GetItemList();
        }

        public DataTable GetDetailsOfItem(string _itemCode)
        {
            DataTable dt = _itemDALObject.GetDetailsOfItem(_itemCode);
            return dt;
        }
        
        public DataTable GetOnhandOfItem(string _itemCode)
        {
            DataTable dt = _itemDALObject.GetOnhandOfItem(_itemCode);
            return dt;
        }

        public DataTable GetItemPriceList(string _itemCode)
        {
            DataTable dt = _itemDALObject.GetItemPriceList(_itemCode);
            return dt;
        }

        public DataTable GetOnHandWithItemDetails()
        {
            return _itemDALObject.GetOnHandWithItemDetails();
        }
    }
}

using StockManageDAL.Unit;
using StockManageDTO.Unit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageBLL.Unit
{
    public class UnitBLL
    {
         UnitDAL _unitDALObject;

        public UnitBLL()
        {
            _unitDALObject = new UnitDAL();
        }

        public int Save(UnitDTO _unitDTOObject)
        {
            int _result = 0;
            if (_unitDALObject.GetDuplicateForSave(_unitDTOObject.Unit) <= 0)
            {
                if (_unitDALObject.Save(_unitDTOObject))
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

        public bool Update(UnitDTO _unitDTOObject)
        {
            bool _isupdate = _unitDALObject.Update(_unitDTOObject);
            return _isupdate;
        }

        public bool DeleteUnitDetails(int _id)
        {
            return _unitDALObject.Delete(_id);
        }

        public UnitDTO LoadUnitDetails(int _id)
        {
            return _unitDALObject.GetUnitData(_id);
        }

        public DataTable LoadUnitList()
        {
            return _unitDALObject.GetUnitList();
        }
    }
}

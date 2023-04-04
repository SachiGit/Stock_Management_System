using StockManageDAL.Department;
using StockManageDTO.Department;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageBLL.Department
{
    public class DepartmentBLL
    {
        DepartmentDAL _unitDALObject;

        public DepartmentBLL()
        {
            _unitDALObject = new DepartmentDAL();
        }

        public int Save(DepartmentDTO _departmentDTOObject)
        {
            int _result = 0;
            if (_unitDALObject.GetDuplicateForSave(_departmentDTOObject.Department) <= 0)
            {
                if (_unitDALObject.Save(_departmentDTOObject))
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

        public bool Update(DepartmentDTO _departmentDTOObject)
        {
            bool _isupdate = _unitDALObject.Update(_departmentDTOObject);
            return _isupdate;
        }

        public bool DeleteDepartmentDetails(int _id)
        {
            return _unitDALObject.Delete(_id);
        }

        public DepartmentDTO LoadDepartmentDetails(int _id)
        {
            return _unitDALObject.GetDepartmentData(_id);
        }

        public DataTable LoadDepartmentList()
        {
            return _unitDALObject.GetDepartmentList();
        }
    }
}

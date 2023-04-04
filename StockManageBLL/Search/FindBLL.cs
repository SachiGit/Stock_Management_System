using StockManageDAL.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StockManageBLL.Search
{
    public class FindBLL
    {
        FindDAL _findDALObject;

        public FindBLL()
        {
            _findDALObject = new FindDAL();
        }

        public DataTable GetDepartmentList()
        {
            return _findDALObject.GetDepartmentList();
        }

        public DataTable GetVendorList()
        {
            return _findDALObject.GetVendorList();
        }

        public DataTable GetEmployeeList()
        {
            return _findDALObject.GetEmployeeList();
        }

        public DataTable GetFormData(string FormType, string Fdate, string Tdate, string Name, string SerialNo, string Department, string GINNo)
        {
            return _findDALObject.GetFormData(FormType, Fdate, Tdate, Name, SerialNo, Department, GINNo);
        }
    }
}

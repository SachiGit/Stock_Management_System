using StockManageDAL.Company;
using StockManageDTO.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageBLL.Company
{
    public class CompanyBLL
    {
        CompanyDAL _CompanyDALObject;

        public CompanyBLL()
        {
            _CompanyDALObject = new CompanyDAL();
        }

        public bool CheckExist()
        {
            return _CompanyDALObject.CheckExist();
        }

        public bool Save(CompanyDTO _companyDTO)
        {
            return _CompanyDALObject.Save(_companyDTO);
        }

        public bool Update(CompanyDTO _companyDTO)
        {
            return _CompanyDALObject.Update(_companyDTO);
        }

        public CompanyDTO GetCompanyData()
        {
            return _CompanyDALObject.GetCompanyData();
        }
    }
}

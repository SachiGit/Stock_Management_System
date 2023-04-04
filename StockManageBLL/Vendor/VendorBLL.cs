using StockManageDAL.Vendor;
using StockManageDTO.Vendor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageBLL.Vendor
{
    public class VendorBLL
    {
        VendorDAL _vendorDALObject;

        public VendorBLL()
        {
            _vendorDALObject = new VendorDAL();
        }

        public int Save(VendorDTO _vendorDTOObject)
        {
            int _result = 0;
            if (_vendorDALObject.GetDuplicateForSave(_vendorDTOObject.VendorName) <= 0)
            {
                if (_vendorDALObject.Save(_vendorDTOObject))
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

        public bool Update(VendorDTO _vendorDTOObject)
        {
            bool _isupdate = _vendorDALObject.Update(_vendorDTOObject);
            return _isupdate;
        }

        public bool DeleteVendorDetails(int _id)
        {
            return _vendorDALObject.Delete(_id);
        }

        public VendorDTO LoadVendorDetails(int _id)
        {
            return _vendorDALObject.GetVendorData(_id);
        }

        public DataTable LoadVendorList()
        {
            return _vendorDALObject.GetVendorList();
        }
    }
}

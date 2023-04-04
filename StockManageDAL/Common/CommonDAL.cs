using StockManageDAL.Common.Department;
using StockManageDAL.Common.Form;
using StockManageDAL.Common.GINNumber;
using StockManageDAL.Common.Items;
using StockManageDAL.Common.SerialNumber;
using StockManageDAL.Common.SerialNumberList;
using StockManageDAL.Common.Unit;
using StockManageDAL.Common.Vendor;
using StockManageDAL.DBConnection;
using StockManageDTO.Common;
using StockManageDTO.Common.SerialNumber;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDAL.Common
{
    public class CommonDAL
    {
        DBAccess _dbConnection;

        FormInterface Form;
        SerialNoInterface SerialNo;
        SerialNoListInterface SerialNoList;
        CommonItemInterface Items;
        CommonVendorInterface Vendor;
        CommonDepartmentInterface Department;
        CommonUnitInterface Unit;
        CommonGINNumberInterface GINNo;

        public enum FormType
        {
            grn,
            issuenote,
            grnreturn
        };

        public enum SerialNumberListType
        {
            Generic
        };

        public CommonDAL()
        {
            _dbConnection = new DBAccess();
        }

        public void SetSerialNumberListBehaviour(FormType _formType)
        {
            SerialNumberListType _serialType = SerialNumberListType.Generic;
            string _tableForList = string.Empty;
            string _tableColumnName = string.Empty;
            string _tableColumnUserName = string.Empty;
            string _formForSeialNo = string.Empty;

            switch (_formType)
            {
                case (FormType.grn):
                    _serialType = SerialNumberListType.Generic;
                    _tableForList = "grnsave1";
                    _tableColumnName = "serialNo";
                    _tableColumnUserName = "username";
                    _formForSeialNo = "grn";
                    break;

                case (FormType.issuenote):
                    _serialType = SerialNumberListType.Generic;
                    _tableForList = "issuenotesave1";
                    _tableColumnName = "serialNo";
                    _tableColumnUserName = "username";
                    _formForSeialNo = "issuenote";
                    break;

                case (FormType.grnreturn):
                    _serialType = SerialNumberListType.Generic;
                    _tableForList = "grnreturnsave1";
                    _tableColumnName = "serialNo";
                    _tableColumnUserName = "username";
                    _formForSeialNo = "grnreturn";
                    break;
            }

            if (_serialType.Equals(SerialNumberListType.Generic))
            {
                SerialNoList = new SerialNoList(_tableForList);
                SerialNo = new SerialNo(_formForSeialNo);
            }
        }

        public void SetFormBehaviour(FormType _type)
        {
            switch (_type)
            {
                case (FormType.grn):
                    Form = new GRNForm();
                    break;

                case (FormType.issuenote):
                    Form = new IssueNoteForm();
                    break;

                case (FormType.grnreturn):
                    Form = new GRNReturnForm();
                    break;
            }
        }

        public SerialNumberDTO GetSerialNumberList()
        {
            return SerialNoList.GetDetails();
        }

        public DataTable GetPrefix()
        {
            return SerialNo.GetPrefix();
        }

        public int GetSimpleSerialNumber()
        {
            return SerialNo.GetSimpleSerialNumber();
        }

        public DataTable GetFormWiseSerialNumber(string _prefix)
        {
            return SerialNo.GetFormWiseSerialNumber(_prefix);
        }

        public void setVendorNames()
        {
            Vendor = new CommonVendor();
        }

        public DataTable GetVendorNames()
        {
            return Vendor.GetVendor();
        }

        public void setDepartment()
        {
            Department = new CommonDepartment();
        }

        public DataTable GetDepartment()
        {
            return Department.GetDepartment();
        }

        public void setUnit()
        {
            Unit = new CommonUnit();
        }

        public DataTable GetUnit()
        {
            return Unit.GetUnit();
        }

        public void setItems()
        {
            Items = new CommonItems();
        }

        public DataTable GetItems()
        {
            return Items.GetItems();
        }

        public void setGINNo()
        {
            GINNo = new CommonGINNumber();
        }

        public string GetGINNo()
        {
            return GINNo.GetGINNumber();
        }

        public bool SaveTransaction(CommonDTO _txnDTO)
        {
            return Form.Save(_txnDTO);
        }

        public bool UpdateTransaction(CommonDTO _txnDTO)
        {
            return Form.Update(_txnDTO);
        }

        public bool DeleteTransaction(string _txnID)
        {
            return Form.Delete(_txnID);
        }

        public bool DeleteRowTransaction(string _upID)
        {
            return Form.DeleteRow(_upID);
        }

        public virtual CommonDTO NextPreviousTransaction(CommonDTO _txnDTO)
        {
            return Form.NextPrevious(_txnDTO);
        }
    }
}

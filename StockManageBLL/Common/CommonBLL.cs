using StockManageDAL.Common;
using StockManageDAL.GRN;
using StockManageDAL.GRNReturn;
using StockManageDAL.IssueNote;
using StockManageDTO.Common;
using StockManageDTO.Common.SerialNumber;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageBLL.Common
{
    public class CommonBLL
    {
        protected CommonDAL _commonDAL;
        protected SerialNumberDTO _serialNumberList = null;
        private int _currentSerialIndex = 0;

        private DataTable _vendorTable = null;
        private DataTable _departmentTable = null;
        private DataTable _itemTable = null;
        private DataTable _unitTable = null;
        private string _ginNumber = string.Empty;

        #region variables for serial number
        int _serialPaddingCount;
        private string _finalSerial, _method;
        string _vendorCustomerLoadMethod = "normal";
        string _prefixMethod = string.Empty;
        DataTable _serialDetailTable;
        #endregion

        public enum FormType
        {
            GRN,
            IssueNote,
            GRNReturn
        };

        public enum SerialNumberListType
        {
            Generic,
        };

        public void SetFormBehaviour(FormType _type)
        {
            switch (_type)
            {
                case (FormType.GRN):
                    _commonDAL = new GRNDAL()
                    {
                    };
                    break;

                case (FormType.IssueNote):
                    _commonDAL = new IssueNoteDAL()
                    {
                    };
                    break;

                case (FormType.GRNReturn):
                    _commonDAL = new GRNReturnDAL()
                    {
                    };
                    break;
            }
        }

        #region Serial / Txn Number operations

        public string[] GenerateSerialNumber(SerialNumberListType _serialType, int _paddingCount = 0)
        {
            try
            {
                if (_serialType.Equals(SerialNumberListType.Generic))
                {
                    _serialPaddingCount = _paddingCount;
                    _serialDetailTable = _commonDAL.GetPrefix();
                    _vendorCustomerLoadMethod = _serialDetailTable.Rows[0]["VendorCustomerLoadMethod"].ToString();// load customer name using method
                    _prefixMethod = _serialDetailTable.Rows[0]["starting_serial_method"].ToString();


                    if (_serialDetailTable.Rows.Count > 0)
                    {
                        if (string.CompareOrdinal("normal", _prefixMethod) == 0)
                        {
                            GetSimpleSerialNumber();
                            _method = "normal";
                        }

                        else if (string.CompareOrdinal("formvise", _prefixMethod) == 0)
                        {
                            string _formWisePrefixIndex = _serialDetailTable.Rows[0][1].ToString();
                            if (string.IsNullOrWhiteSpace(_formWisePrefixIndex))
                            {
                                GetSimpleSerialNumber();
                                _method = "formvise";
                            }
                            else
                            {
                                GetFormWiseSerialNumber(_formWisePrefixIndex);
                                _method = "formvise";
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            string[] _returnArray = new string[3];
            _returnArray[0] = _finalSerial;
            _returnArray[1] = _method;
            _returnArray[2] = _vendorCustomerLoadMethod;

            return _returnArray;
        }

        private void GetSimpleSerialNumber()
        {
            int _numaricValue = _commonDAL.GetSimpleSerialNumber();

            _finalSerial = _numaricValue.ToString().PadLeft(_serialPaddingCount, '0');//increment number and keep zeros in front
        }

        private void GetFormWiseSerialNumber(string _prefix)
        {
            string _maxNumber = "1";

            DataTable _serialTable = _commonDAL.GetFormWiseSerialNumber(_prefix);

            int[] _list = new int[_serialTable.Rows.Count];

            if (_serialTable.Rows.Count > 0)
            {
                for (int _rowCount = 0; _rowCount < _serialTable.Rows.Count; _rowCount++)
                {
                    try
                    {
                        int.TryParse(_serialTable.Rows[_rowCount]["serialNo"].ToString(), out _list[_rowCount]);
                    }
                    catch
                    {
                        _list[_rowCount] = 0;
                    }
                }

                _maxNumber = (_list.Max() + 1).ToString();
            }

            _finalSerial = string.Format("{0}/{1}", _prefix, _maxNumber.PadLeft(_serialPaddingCount, '0'));//increment number and keep zeros in front
        }

        public void PrepareSerialNumberList(FormType _formType)
        {
            switch (_formType)
            {
                case (FormType.GRN):
                    _commonDAL.SetSerialNumberListBehaviour(CommonDAL.FormType.grn);
                    break;

                case (FormType.IssueNote):
                    _commonDAL.SetSerialNumberListBehaviour(CommonDAL.FormType.issuenote);
                    break;

                case (FormType.GRNReturn):
                    _commonDAL.SetSerialNumberListBehaviour(CommonDAL.FormType.grnreturn);
                    break;
            }
        }

        public void LoadSerialNumberList()
        {
            _serialNumberList = _commonDAL.GetSerialNumberList();
            _currentSerialIndex = _serialNumberList.IDList.Count;
        }

        public int GetCurrenctSerialIndex()
        {
            return _currentSerialIndex;
        }

        public void SetCurrentSerialIndex(int _index)
        {
            _currentSerialIndex = _index;
        }

        public int GetMaximunSerialIndex()
        {
            return _serialNumberList.IDList.Count;
        }

        public int GetIDForSerialIndex(int _index)
        {
            if (_serialNumberList.IDList.Count > _index)
            {
                return Convert.ToInt32(_serialNumberList.IDList[_index]);
            }
            else
            {
                return -1;
            }

        }

        public int GetIndexOfSelectedSerial(string _serialNumber)
        {
            try
            {
                int _index = _serialNumberList.NumberList.IndexOf(_serialNumber);
                return _index;
            }
            catch
            {
                throw new Exception("Could not found transaction", new Exception("QBLValidation_CouldNotFoundTransaction"));
            }
        }

        public int GetIndexOfSelectedSerialID(int _serialID)
        {
            try
            {
                int _index = _serialNumberList.IDList.IndexOf(_serialID.ToString());
                return _index;
            }
            catch
            {
                throw new Exception("Could not found transaction", new Exception("QBLValidation_CouldNotFoundTransaction"));
            }
        }
        #endregion

        #region Vendor Data
        public void LoadVendor()
        {
            _vendorTable = _commonDAL.GetVendorNames();
        }

        public DataTable GetVendor()
        {
            DataTable _vendorTableCopy = _vendorTable.Copy();
            DataRow _row1 = _vendorTableCopy.NewRow();
            _row1["vendorname"] = "";
            _vendorTableCopy.Rows.InsertAt(_row1, 0);

            return _vendorTableCopy;
        }
        #endregion

        #region Department Data
        public void LoadDepartment()
        {
            _departmentTable = _commonDAL.GetDepartment();
        }

        public DataTable GetDepartment()
        {
            DataTable _departmentTableCopy = _departmentTable.Copy();
            DataRow _row1 = _departmentTableCopy.NewRow();
            _row1["department"] = "";
            _departmentTableCopy.Rows.InsertAt(_row1, 0);

            return _departmentTableCopy;
        }
        #endregion

        #region Item Data
        public void LoadItems()
        {
            _itemTable = _commonDAL.GetItems();
        }

        public DataTable GetItems()
        {
            DataTable _itemTableCopy = _itemTable.Copy();
            DataRow _row1 = _itemTableCopy.NewRow();
            _row1["itemCode"] = "";
            _row1["itemName"] = "";
            _itemTableCopy.Rows.InsertAt(_row1, 0);

            return _itemTableCopy;
        }
        #endregion

        #region Unit Data
        public void LoadUnit()
        {
            _unitTable = _commonDAL.GetUnit();
        }

        public DataTable GetUnit()
        {
            DataTable _unitTableCopy = _unitTable.Copy();
            DataRow _row1 = _unitTableCopy.NewRow();
            _row1["unit"] = "";
            _unitTableCopy.Rows.InsertAt(_row1, 0);

            return _unitTableCopy;
        }
        #endregion

        #region GIN Number
        public void LoadGINNo()
        {
            _ginNumber = _commonDAL.GetGINNo();
        }

        public string GetGINNo()
        {
            return _ginNumber;
        }
        #endregion
    
        public virtual bool SaveTransaction(CommonDTO _txnDTO)
        {
            return false;
        }

        public virtual bool UpdateTransaction(CommonDTO _txnDTO)
        {
            return false;
        }

        public virtual CommonDTO NextPreviousTransaction(int _index)
        {
            return new CommonDTO();
        }

        public virtual bool DeleteTransaction(string _txnID)
        {
            return _commonDAL.DeleteTransaction(_txnID);
        }

        public virtual bool DeleteRowTransaction(string _upID)
        {
            return _commonDAL.DeleteRowTransaction(_upID);
        }
    }
}

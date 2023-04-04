using StockManageBLL.Common;
using StockManageDTO.Common;
using StockManageDTO.GRNReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageBLL.GRNReturn
{
    public class GRNReturnBLL : CommonBLL
    {
        public bool Save(CommonDTO _txnDTO)
        {
            return _commonDAL.SaveTransaction(_txnDTO);
        }

        public bool Update(CommonDTO _txnDTO)
        {
            return _commonDAL.UpdateTransaction(_txnDTO);
        }

        public bool Delete(string _id)
        {
            return _commonDAL.DeleteTransaction(_id);
        }

        public bool DeleteRow(string _upid)
        {
            return _commonDAL.DeleteRowTransaction(_upid);
        }

        public override CommonDTO NextPreviousTransaction(int _index)
        {
            try
            {
                CommonDTO _txnDTO = new CommonDTO()
                {
                    GRNReturn = new GRNReturnDTO()
                    {
                        Fields = new GRNReturnField(),
                        Grid = new List<GRNReturnGrid>()
                    }
                };

                _txnDTO.GRNReturn.Fields.ID = Convert.ToInt32(_serialNumberList.IDList[_index].ToString());
                _txnDTO = _commonDAL.NextPreviousTransaction(_txnDTO);
                return _txnDTO;
            }
            catch
            {
                throw;
            }
        }
    }
}

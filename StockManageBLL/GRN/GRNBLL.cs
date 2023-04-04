using StockManageBLL.Common;
using StockManageDTO.Common;
using StockManageDTO.GRN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageBLL.GRN
{
    public class GRNBLL : CommonBLL
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
                    GRN = new GRNDTO()
                    {
                        Fields = new GRNField(),
                        Grid = new List<GRNGrid>()
                    }
                };

                _txnDTO.GRN.Fields.ID = Convert.ToInt32(_serialNumberList.IDList[_index].ToString());
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

using StockManageBLL.Common;
using StockManageDTO.Common;
using StockManageDTO.IssueNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageBLL.IssueNote
{
    public class IssueNoteBLL : CommonBLL
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
                    IssueNote = new IssueNoteDTO()
                    {
                        Fields = new IssueNoteField(),
                        Grid = new List<IssueNoteGrid>()
                    }
                };

                _txnDTO.IssueNote.Fields.ID = Convert.ToInt32(_serialNumberList.IDList[_index].ToString());
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

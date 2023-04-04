using StockManageDTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDAL.Common.Form
{
    public interface FormInterface
    {
        bool Save(CommonDTO _txnDTO);

        bool Update(CommonDTO _txnDTO);

        bool Delete(string _id);

        bool DeleteRow(string _upID);

        CommonDTO NextPrevious(CommonDTO _txnDTO);
    }
}

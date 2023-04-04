using StockManageDTO.GRN;
using StockManageDTO.GRNReturn;
using StockManageDTO.IssueNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDTO.Common
{
    public class CommonDTO
    {
        public GRNDTO GRN { get; set; }
        public IssueNoteDTO IssueNote { get; set; }
        public GRNReturnDTO GRNReturn { get; set; }
    }
}

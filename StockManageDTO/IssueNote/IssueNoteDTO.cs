using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDTO.IssueNote
{
    public class IssueNoteDTO
    {
        public IssueNoteField Fields { get; set; }

        public List<IssueNoteGrid> Grid { get; set; }
    }
}

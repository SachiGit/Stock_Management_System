using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDTO.IssueNote
{
    public class IssueNoteField
    {
        public int ID { get; set; }
        public string SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string VendorName { get; set; }
        public string Department { get; set; }
        public string Memo { get; set; }
        public double Total { get; set; }
        public string VoteNo { get; set; }
        public string GinNo { get; set; }
    }
}

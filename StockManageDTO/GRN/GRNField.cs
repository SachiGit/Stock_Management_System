using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDTO.GRN
{
    public class GRNField
    {
        public int ID { get; set; }
        public string SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string Memo { get; set; }
        public double Total { get; set; }
    }
}

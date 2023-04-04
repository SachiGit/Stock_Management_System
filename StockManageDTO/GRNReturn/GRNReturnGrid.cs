using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageDTO.GRNReturn
{
    public class GRNReturnGrid
    {
        public string UpID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public double QTY { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
    }
}

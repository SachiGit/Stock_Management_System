using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDTO.Item
{
    public class ItemDTO
    {
        public string ID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public Double Price1 { get; set; }
        public Double Price2 { get; set; }
        public Double Price3 { get; set; }
        public Double Price4 { get; set; }
        public Double Price5 { get; set; }
        public Double Price6 { get; set; }
        public Double ReOrderLevel { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManageDTO.GRNReturn
{
    public class GRNReturnDTO
    {
        public GRNReturnField Fields { get; set; }

        public List<GRNReturnGrid> Grid { get; set; }
    }
}

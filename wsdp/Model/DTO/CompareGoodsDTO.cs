using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class CompareGoodsDTO
    {
        public GoodDTO FirstGood { get; set; }
        public GoodDTO SecondGood { get; set; }

        public Dictionary<string, string> FirstProperties { get; set; }
        public Dictionary<string, string> SecondProperties { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class CompareGoodsDTO
    {
        public List<GoodDTO> Goods { get; set; }

        public List<string> Properties { get; set; }
    }
}

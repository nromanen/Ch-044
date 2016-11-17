using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class PropertyValuesDTO
    {
        public Dictionary<int, string> DictStringProperties { get; set; }
        public Dictionary<int, int> DictIntProperties { get; set; }
        public Dictionary<int, double> DictDoubleProperties { get; set; }
    }
}

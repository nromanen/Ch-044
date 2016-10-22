using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Product
{
    public class TV
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageLink { get; set; }
        public Dictionary<string, string> Characteristics { get; set; }
    }
}

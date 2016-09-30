using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_lab1
{
    class Sale
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return "producer: " + Name + "\t\t" + "quantity: " + Quantity;
        }
    }
}

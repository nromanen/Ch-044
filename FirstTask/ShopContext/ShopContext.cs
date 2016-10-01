using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    partial class ShopContext
    {
        public List<Producer> Producers = new List<Producer>();
        public List<Category> Categories = new List<Category>();
        public List<Good> Goods = new List<Good>();
    }
}

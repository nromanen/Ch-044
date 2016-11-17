using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class GoodDTO
    {
        public int Id { get; set; }

        public virtual CategoryDTO Category { get; set; }

        public int Category_Id { get; set; }

        public virtual WebShopDTO WebShop { get; set; }

        public int WebShop_Id { get; set; }

        public PropertyValuesDTO PropertyValues { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ComparingCategoriesDTO
    {
        public Dictionary<CategoryDTO,List<GoodDTO>> CategoriesGoods { get; set; }
    }
}

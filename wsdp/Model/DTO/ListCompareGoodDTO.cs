using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ListCompareGoodDTO
    {
        //int - category id, list - list of compare goods from this category
        public Dictionary<int, List<int>> CompareGoods { get; set; }

        public ListCompareGoodDTO()
        {
            CompareGoods = new Dictionary<int, List<int>>();
        }
    }
}

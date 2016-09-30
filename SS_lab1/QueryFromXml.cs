using SS_lab1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_lab1
{
    static class QueryFromXml
    {
        public static List<Good> GoodsInThisCategory(string path, Category cat)
        {
            List<Good> goods = new List<Good>();
            GoodsLinqToXmlManager stream = new GoodsLinqToXmlManager();
            stream.GoodsFromXml(path, out goods);
            return goods.Where(x => x.Category.Id == cat.Id).ToList<Good>();
        }

        public static List<Sale> Sales(string path)
        {
            List<Good> goods = new List<Good>();
            GoodsLinqToXmlManager stream = new GoodsLinqToXmlManager();
            stream.GoodsFromXml(path, out goods);          

            var res = goods.Select(x => new {
                Name = x.Producer.Name,
                Quantity = goods.Where(y => y.Producer.Id == x.Producer.Id).Count() }).
                      Distinct().Select(t => new Sale { Name = t.Name, Quantity = t.Quantity}).ToList();

            return res;
        }
    }
}

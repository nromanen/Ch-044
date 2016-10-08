using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Goods.DbModels;

namespace Goods.Managers
{
    public static class Manager_FullGoods
    {
        public static List<Good> Full_Goods(List<Producer> Producers, List<Category> Categories, List<Good> Goods)
        {

            var newGoodList = new List<Good>(Goods);
            foreach (var good in Goods)
            {

                var category = Categories.FirstOrDefault(i => i.Id == good.Category.Id);

                var producer = Producers.FirstOrDefault(i => i.Id == good.Producer.Id);

                if (category != null && producer != null)
                {
                    good.Category = category;
                    good.Producer = producer;
                }
                else
                {
                    newGoodList.Remove(good);
                }
            }
            return newGoodList;
        }
        public static void GoodsCounter(List<Good> goods)
        {
            List<string> countries = new List<string>();
            foreach (var i in goods)
            {
                var countryname = i.Producer.Country;
                countries.Add(countryname);
            }

            var CountryCount = countries.GroupBy(x => x)
        .Select(g => new { Value = g.Key, Count = g.Count() })
        .OrderByDescending(x => x.Count);

            foreach (var x in CountryCount)
            {
                Console.WriteLine("Country: " + x.Value + "\nNumber of Goods: " + x.Count);
            }

        }
    }
}

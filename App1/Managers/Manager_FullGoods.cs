using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace App1
{
    public static class Manager_FullGoods
    {
        public static List<Good> Full_Goods(List<Producer> Producers, List<Category> Categories, List<Good> Goods)
        {
            List<Good> goodList = new List<Good>();

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
        public static void Xml_Serializer(string path, List<Good> Goods)
        {
            var formatter = new XmlSerializer(typeof(List<Good>));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Goods);
            }
        }
        public static List<Good> Xml_Deserialize(string filename)
        {
            var serializer = new XmlSerializer(typeof(List<Good>));
            using (var stream = new StreamReader(filename))
            {
                return (List<Good>)serializer.Deserialize(stream);

            }
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

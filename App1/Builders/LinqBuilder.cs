using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App1
{
    public static class LinqBuilder
    {
        public static void WriteToXmlLinq(List<Good> goods, string path)
        {

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                XDocument goodXml = new XDocument(new XElement("ArrayOfGood",
                from good in goods
                select new XElement("Good",
                new XElement("id", good.Id),
                new XElement("name", good.Name),
                new XElement("price", good.Price),
                new XElement("Category",
                new XAttribute("id", good.Category.Id),
                new XAttribute("name", good.Category.Name)),
                new XElement("Producer",
                new XAttribute("id", good.Producer.Id),
                new XAttribute("name", good.Producer.Name),
                new XAttribute("country", good.Producer.Country))))
                );
                goodXml.Save(fs);
            }
        }
        public static List<Good> ReadWithLinq(string path)
        {
            var file = File.ReadAllText(path);
            var doc = XDocument.Parse(file);

            var goods = (from s in doc.Descendants("Good")
                         select new Good()
                         {
                             Id = int.Parse(s.Element("id").Value),
                             Name = s.Element("name").Value,
                             Price = decimal.Parse(s.Element("price").Value.Replace('.', ',')),

                             Category = new Category()
                             {
                                 Id = int.Parse(s.Element("Category").Attribute("id").Value),
                                 Name = s.Element("Category").Attribute("name").Value
                             },

                             Producer = new Producer()
                             {
                                 Id = int.Parse(s.Element("Producer").Attribute("id").Value),
                                 Name = s.Element("Producer").Attribute("name").Value,
                                 Country = s.Element("Producer").Attribute("country").Value
                             }

                         }).ToList();
            return goods;
        }

    }
}

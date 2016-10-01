using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FirstTask
{
    static class XmlBuilderForGoods
    {

        static public void WriteToXmlNotLinq(List<Good> list, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(list.GetType());
                x.Serialize(fs, list);
            }
        }
        static public void WriteToXmlLinq(List<Good> list, string path)
        {
            var xmlelements = from good in list
                              let category = new XElement("Category")

                              select new XElement("Good", new XElement("id", good.Id),
                                                          new XElement("name", good.Name),
                                                          new XElement("price", good.Price),
                                                          new XElement("Category", new XAttribute("id", good.Category.Id), new XAttribute("name", good.Category.Name)),
                                                          new XElement("Producer", new XAttribute("id", good.Producer.Id), new XAttribute("name", good.Producer.Name),
                                                              new XAttribute("country", good.Producer.Country)));
            XElement root = new XElement("ArrayOfGood");
            foreach (var item in xmlelements)
            {
                root.Add(item);
                //Console.WriteLine(item);
            }

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                root.Save(fs);
            }

            Console.WriteLine(root);
        }

        static public List<Good> ReadFromXmlNotLinq(string path)
        {
            XmlSerializer x = new XmlSerializer(typeof(List<Good>));
            List<Good> resultList = (List<Good>)(x.Deserialize(new FileStream(path, FileMode.Open)));
            return resultList;
        }

        static public List<Good> ReadFromXmlLinq(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string xml = sr.ReadToEnd();
                    XDocument doc = XDocument.Parse(xml);

                    var goods = from good in doc.Descendants("Good")
                                select new Good()
                                {
                                    Id = int.Parse(good.Element("id").Value),
                                    Name = good.Element("name").Value,
                                    Price = decimal.Parse(good.Element("price").Value),
                                    Category = new Category()
                                    {
                                        Id = int.Parse(good.Element("Category").Attribute("id").Value),
                                        Name = good.Element("Category").Attribute("name").Value
                                    },
                                    Producer = new Producer()
                                    {
                                        Id = int.Parse(good.Element("Producer").Attribute("id").Value),
                                        Name = good.Element("Producer").Attribute("name").Value,
                                        Country = good.Element("Producer").Attribute("country").Value
                                    }
                                };
                    return goods.ToList<Good>();
                }
            }
        }
    }
}

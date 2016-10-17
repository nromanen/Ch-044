using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using ByEntity.Model;

namespace ByEntity
{
    public class GoodsLinqToXmlManager
    {
        public void GoodsFromXml(string path, out List<Good> goods)
        {
            XDocument doc = XDocument.Parse(File.ReadAllText(path));
            var res = (from s in doc.Descendants("Good")
                         select new Good
                         {
                             Id = Int32.Parse(s.Element("id").Value),
                             Name = s.Element("name").Value,
                             Price = decimal.Parse(s.Element("price").Value.Replace(',', '.')),
                             Category = new Category
                             {
                                 Id = Int32.Parse(s.Element("Category").Attribute("id").Value),
                                 Name = s.Element("Category").Attribute("name").Value
                             },
                             Producer = new Producer
                             {
                                 Id = Int32.Parse(s.Element("Producer").Attribute("id").Value),
                                 Name = s.Element("Producer").Attribute("name").Value,
                                 Country =s.Element("Producer").Attribute("country").Value
                             }
                         }
                         ).ToList();

            goods = res;            
        }

        public void GoodsToXML(string path, List<Good> goods)
        {
            #region FirstWay
            //var t = new XmlDocument();

            //var xmlWriter = new XmlTextWriter(path, null)
            //{
            //    Formatting = Formatting.Indented                
            //};

            //xmlWriter.WriteStartDocument();
            //xmlWriter.WriteStartElement("ArrayOfGoods");
            //foreach (var item in goods)
            //{                

            //    xmlWriter.WriteStartElement("Good");

            //    xmlWriter.WriteStartElement("id");
            //    xmlWriter.WriteString(item.Id.ToString());
            //    xmlWriter.WriteEndElement();

            //    xmlWriter.WriteStartElement("name");
            //    xmlWriter.WriteString(item.Name);
            //    xmlWriter.WriteEndElement();

            //    xmlWriter.WriteStartElement("price");
            //    xmlWriter.WriteString(item.Price.ToString());
            //    xmlWriter.WriteEndElement();

            //    xmlWriter.WriteStartElement("Category");
            //    xmlWriter.WriteStartAttribute("id");
            //    xmlWriter.WriteString(item.Category.Id.ToString());
            //    xmlWriter.WriteEndAttribute();
            //    xmlWriter.WriteStartAttribute("name");
            //    xmlWriter.WriteString(item.Category.Name);
            //    xmlWriter.WriteEndAttribute();
            //    xmlWriter.WriteEndElement();

            //    xmlWriter.WriteStartElement("Producer");
            //    xmlWriter.WriteStartAttribute("id");
            //    xmlWriter.WriteString(item.Producer.Id.ToString());
            //    xmlWriter.WriteEndAttribute();
            //    xmlWriter.WriteStartAttribute("name");
            //    xmlWriter.WriteString(item.Producer.Name);
            //    xmlWriter.WriteEndAttribute();
            //    xmlWriter.WriteStartAttribute("country");
            //    xmlWriter.WriteString(item.Producer.Country);
            //    xmlWriter.WriteEndAttribute();
            //    xmlWriter.WriteEndElement();

            //    xmlWriter.WriteEndElement();


            //}
            //xmlWriter.WriteEndElement();

            //xmlWriter.Close();   
            #endregion

            XDocument xdoc = new XDocument();
            XElement rootElement = new XElement("ArrayOfGoods");

            foreach (var item in goods)
            {
                rootElement
                    .Add(new XElement(new XElement("Good",
                                new XElement("id", item.Id.ToString()),
                                new XElement("name", item.Name),
                                new XElement("price", item.Price.ToString()),
                            new XElement("Category",
                                new XAttribute("id", item.Category.Id.ToString()),
                                new XAttribute("name", item.Category.Name), ""),
                            new XElement("Producer",
                                new XAttribute("id", item.Producer.Id.ToString()),
                                new XAttribute("name", item.Producer.Name),
                                new XAttribute("country", item.Producer.Country), ""
                                ))));
            }
            rootElement.Save(path);
            

            

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Task2_XML {
	public class XmlSerializerByLINQ : IXmlManager {

		public List<Good> Deserializer(string path) {                   
			string file = File.ReadAllText(path);
			XDocument doc = XDocument.Parse(file);

			var good = doc.Descendants("Good").Select(t => new Good {
				Id = int.Parse(t.Element("Id").Value),
				Name = t.Element("Name").Value,
				Price = Decimal.Parse(t.Element("Price").Value.Replace('.', ',')),
				Category = new Category {
					Id = int.Parse(t.Element("Category").Attribute("Id").Value),
					Name = t.Element("Category").Attribute("Name").Value
				},
				Producer = new Producer {
					Id = int.Parse(t.Element("Producer").Attribute("Id").Value),
					Name = t.Element("Producer").Attribute("Name").Value,
					Country = t.Element("Producer").Attribute("Country").Value,
				}
			}).ToList();
			return good;
		}

		public void Serializer(string fileName, List<Good> goods) {                 
			using (FileStream fs = new FileStream(fileName, FileMode.Create)) {
				XDocument goodXml = new XDocument(new XElement("ArrayOfGoods",
				from good in goods
				select new XElement("Good",
				new XElement("Id", good.Id),
				new XElement("Name", good.Name),
				new XElement("Price", good.Price),
				new XElement("Category",
				new XAttribute("Id", good.Category.Id),
				new XAttribute("Name", good.Category.Name)),
				new XElement("Producer",
				new XAttribute("Id", good.Producer.Id),
				new XAttribute("Name", good.Producer.Name),
				new XAttribute("Country", good.Producer.Country))))
				);
				goodXml.Save(fs);
			}
		}
	}
}

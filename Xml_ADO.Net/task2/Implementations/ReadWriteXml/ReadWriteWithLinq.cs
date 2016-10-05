using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace task2.Implementations.LinqReadWrite
{
	public class ReadWriteWithLinq : IFileManager<List<Good>>
	{
		public List<Good> ReadFromFile(string path)
		{
			var file = File.ReadAllText(path);

			var doc = XDocument.Parse(file);

			var goods = (from s in doc.Descendants("Good")
						 select new Good()
						 {
							 Id = int.Parse(s.Element("Id").Value),
							 Name = s.Element("Name").Value,
							 Price = decimal.Parse(s.Element("Price").Value.Replace('.',',')),

							 Category = new Category()
							 {
								 Id = int.Parse(s.Element("Category").Attribute("Id").Value),
								 Name = s.Element("Category").Attribute("Name").Value
							 },

							 Producer = new Producer()
							 {
								 Id = int.Parse(s.Element("Producer").Attribute("Id").Value),
								 Name = s.Element("Producer").Attribute("Name").Value,
								 Country = s.Element("Producer").Attribute("Country").Value
							 }

						 }).ToList();

			int counter = 0;
			foreach (var good in goods)
			{
				counter++;
				Console.WriteLine("\nGood.xml {0}\n", counter);

				Console.WriteLine("\tName: {0}", good.Name);
				Console.WriteLine("\tPrice: {0}", good.Price);
				Console.WriteLine("\tCategory: {0}", good.Category.Name);
				Console.WriteLine("\tProducer: {0}", good.Producer.Name);
			}

			return goods;
		}


		public void WriteToFile(string pathLinq, List<Good> Goods)
		{
			using (FileStream fs = new FileStream(pathLinq, FileMode.Create))
			{
				XDocument goodXml = new XDocument(new XElement("ArrayOfGood",
					from good in Goods
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

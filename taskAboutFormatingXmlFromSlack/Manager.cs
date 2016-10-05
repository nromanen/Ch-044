using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using task2;

namespace taskAboutFormatingXmlFromSlack
{
	public class Manager
	{
		public List<ProducerForPrint> ReadFromFile(string path)
		{
			var file = File.ReadAllText(path);

			var doc = XDocument.Parse(file);

			var goods = (from g in doc.Descendants("Good")
						 select new Good()
						 {
							 Id = int.Parse(g.Element("id").Value),
							 Name = g.Element("name").Value,
							 Price = decimal.Parse(g.Element("price").Value),
							 

							 Category = new Category ()
							 {
							 	Id = int.Parse(g.Element("Category").Attribute("id").Value),
							 	Name = g.Element("Category").Attribute("name").Value
							 },

							 Producer = new Producer()
							 {
							    Id = int.Parse(g.Element("Producer").Attribute("id").Value),
							    Name = g.Element("Producer").Attribute("name").Value,
							    Country = g.Element("Producer").Attribute("country").Value
							 }
						 }).ToList();


			List<int> ids = goods.Select(i => i.Producer.Id).ToList();

			var disctinctIds = ids.Distinct();

			var producersWithProducts = new List<ProducerForPrint>();

			foreach(var id in disctinctIds)
			{
				var item = new ProducerForPrint() { Goods = new List<string>() };
				foreach(var g in goods)
				{
					if(g.Producer.Id == id)
					{
						item.Name = g.Producer.Name;
						item.Goods.Add(g.Name);

					}
				}
				producersWithProducts.Add(item);
			}


			return producersWithProducts;
		}
	}
}

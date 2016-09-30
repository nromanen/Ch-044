using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task2_XML {
	public static class DataWriter {
		public static void GroupByCount(List<Good> list) {
			list.GroupBy(t => t.Producer.Id).Select(t => new {
				Name = t.First().Producer.Name,
				Count = t.Count()
			}).ToList()
			.ForEach(t => Console.WriteLine("Producer: {0}, Count: {1}", t.Name, t.Count));
		}

		public static void GroupByCount(string path) {
			string file = File.ReadAllText(path);

			XDocument doc = XDocument.Parse(file);

			doc.Descendants("Good").Select(t => new Producer {


				Id = int.Parse(t.Element("Producer").Attribute("Id").Value),
				Name = t.Element("Producer").Attribute("Name").Value
			}).ToList()
			   .GroupBy(t => t.Name).Select(t => new {
				   Name = t.First().Name,
				   Count = t.Count()
			   }).ToList()
		   .ForEach(t => Console.WriteLine("Producer: {0}, Count: {1}", t.Name, t.Count));
		}

		public static void GroupByPrice(List<Good> list) {
			list.GroupBy(t => t.Name).Select(t => new {
				Name = t.First().Name,
				Price = t.First().Price
			}).ToList()
			.ForEach(t => Console.WriteLine("Good: {0}, Price: {1}", t.Name, t.Price));
		}

		public static void GroupByPrice(string path) {

			string file = File.ReadAllText(path);

			XDocument doc = XDocument.Parse(file);

			doc.Descendants("Good").Select(t => new Good {
				Name = t.Element("Name").Value,
				Price = Decimal.Parse(t.Element("Price").Value.Replace('.', ','))
			}).ToList()
			.ForEach(t => Console.WriteLine("Good: {0}, Price: {1}", t.Name, t.Price));
		}
	}
}

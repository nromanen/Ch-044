using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using task2.Implementations.LinqReadWrite;
using task2.Implementations.LinqReadWriteXml;

namespace task2
{
	public class Manager
	{
		public List<Good> GoodsBuilder(List<Good> Goods, List<Producer> Producers, List<Category> Categories)
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
		public void PrintGoods(List<Good> Goods)
		{
			int counter = 0;
			foreach (var good in Goods)
			{
				counter++;
				Console.WriteLine("\nGood {0}\n", counter);

				Console.WriteLine("\tName: {0}", good.Name);
				Console.WriteLine("\tPrice: {0}", good.Price);
				Console.WriteLine("\tCategory: {0}", good.Category.Name);
				Console.WriteLine("\tProducer: {0}", good.Producer.Name);
			}
		}
		public void WriteToFile(string path, List<Good> Goods)
		{
			var MyReader = new System.Configuration.AppSettingsReader();
			string keyvalue = MyReader.GetValue("WriteWithLinq", typeof(string)).ToString();

			if (keyvalue == "true")
			{
				ReadWriteWithLinq rw = new ReadWriteWithLinq();
				rw.WriteToFile(path, Goods);
			}
			else
			{
				ReadWriteSerializer rw = new ReadWriteSerializer();
				rw.WriteToFile(path, Goods);
			}
		}
		public List<Good> ReadFromFile(string path)
		{
			var MyReader = new System.Configuration.AppSettingsReader();
			string keyvalue = MyReader.GetValue("ReadWithLinq", typeof(string)).ToString();

			var methodLinq = new ReadWriteWithLinq();
			var methodSerialiser = new ReadWriteSerializer();

			var goods = new List<Good>();

			if (keyvalue == "true")
			{
				goods = methodLinq.ReadFromFile(path);
			}
			else
			{
				goods = methodSerialiser.ReadFromFile(path);
				
			}
			return goods;
		}
	}
}

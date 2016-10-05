using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskAboutFormatingXmlFromSlack
{
	class Program
	{
		static void Main(string[] args)
		{
			string pathOfXmlFile = @"d:\softserve\goods2.xml";

			Manager mngr = new Manager();
			var list = mngr.ReadFromFile(pathOfXmlFile);

			Console.WriteLine("Enter the producer:");
			var producerName = Console.ReadLine();

			var producer = list.Where(i => i.Name == producerName).Select(g => g.Goods).ToList();
			
			foreach(var p in producer)
			{
				foreach (var n in p)
				{
					Console.WriteLine(n);
				}
			}

			
				


			Console.ReadLine();
		}
	}
}

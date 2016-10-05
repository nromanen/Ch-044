using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using taskAboutFormatingXmlFromSlack;

namespace task2
{
	[Serializable]
	public class Good 
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public Category Category { get; set; }
		public Producer Producer { get; set; }
	}
}

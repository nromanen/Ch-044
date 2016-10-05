using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace task2
{
	[Serializable]
	public class Good
	{
		[XmlElement]
		public int Id { get; set; }
		[XmlElement]
		public string Name { get; set; }
		[XmlElement]
		public decimal Price { get; set; }
		[XmlElement]
		public Category Category { get; set; }
		[XmlElement]
		public Producer Producer { get; set; }
        public override string ToString()
        {
            return $"Id:{Id}\nName:{Name}\nPrice:{Price}\nCategory:{Category.Name}\nProducer:{Producer}";
        }
    }
}

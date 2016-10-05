using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace task2
{
	[Serializable]
	public class Producer
	{
		[XmlAttribute]
		public int Id { get; set; }
		[XmlAttribute]
		public string Name { get; set; }
		[XmlAttribute]
		public string Country { get; set; }
        public override string ToString()
        {
            return $"Id:{Id}\nName:{Name}\nCountry:{Country}";
        }
    }
}

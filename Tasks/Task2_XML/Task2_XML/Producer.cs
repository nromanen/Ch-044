using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Task2_XML {
	public class Producer {
		[XmlAttribute("Id")]
		public int Id { get; set; }
		[XmlAttribute("Name")]
		public string Name { get; set; }
		[XmlAttribute("Country")]
		public string Country { get; set; }

	}
}

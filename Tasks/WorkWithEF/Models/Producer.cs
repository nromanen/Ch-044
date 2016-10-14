using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Models {
	public class Producer {
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("country")]
		public string Country { get; set; }

		[XmlIgnore]
		public virtual ICollection<Good> Goods { get; set; }

		public override string ToString() {
			return Id.ToString() + " " + Name + " " + Country;
		}
	}
}

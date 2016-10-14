using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Models {
	[XmlRoot(ElementName = "Good")]
	public class Good {
		[XmlElement("id")]
		public int Id { get; set; }

		[XmlElement("name")]
		public string Name { get; set; }

		[XmlElement("price")]
		public decimal Price { get; set; }

		[XmlIgnore]
		public int CategoryId { get; set; }

		[XmlIgnore]
		public int ProducerId { get; set; }

		[XmlElement("Category")]
		public virtual Category Category { get; set; }

		[XmlElement("Producer")]
		public virtual Producer Producer { get; set; }

		public override string ToString() {
			return Id.ToString() + " " + Name + " " + Price.ToString() + " " +
				"\n\t Category: " + Category.ToString() +
				"\n\t Producer: " + Producer.ToString();
		}

	}
}

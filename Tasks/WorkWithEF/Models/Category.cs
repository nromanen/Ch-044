using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Models
{
	public class Category {
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlIgnore]
		public virtual ICollection<Good> Goods { get; set; }

		public override string ToString() {
			return Id.ToString() + " " + Name;
		}
	}
}

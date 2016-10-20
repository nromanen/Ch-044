using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace Model.DB {
	public class Good {

		[Key]
		public int Id { get; set; }

		[Required]
		public Category Category { get; set; }

		[Required]
		public string XmlData { get; set; }
	}
}

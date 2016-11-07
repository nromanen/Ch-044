using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DB {
	public class Parser {
		[Key]
		public int Id { get; set; }

		public string Description { get; set; }
		
		public int CategoryId  { get; set; }
		public virtual Category Category { get; set; }

		public int WebShopId { get; set; }
		public WebShop WebShop { get; set; }

		[Required]
		public string Priority { get; set; }

		[Required]
		public string Status { get; set; }

		[Required]
		public DateTime EndDate { get; set; }

		[Required]
		public string IteratorSettings { get; set; }

		[Required]
		public string GrabberSettings { get; set; }
	}
}

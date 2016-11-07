using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO {
	public class ParserDTO {
		public int Id { get; set; }
		public string Description { get; set; }

		public int CategoryId { get; set; }
		public CategoryDTO Category { get; set; }

		public int WebShopId { get; set; }
		public WebShopDTO WebShop { get; set; }

		public string Priority { get; set; }

		public string Status { get; set; }

		public DateTime EndDate { get; set; }

		public IteratorSettingsDTO IteratorSettings { get; set; }

		public GrabberSettingsDTO GrabberSettings { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoApplication {
	class Good {
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public Category Category { get; set; }

		public Producer Producer { get; set; }

		public override string ToString() {
			return $"Good ID - {Id}   /   Name - {Name}   /   Price - {Price}   /   Category - {Category.Name}   /   Producer - {Producer.Name}   /   Country - {Producer.Country}";
		}
	}
}

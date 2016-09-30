using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Tastk1_NormalVersion
{
	[DelimitedRecord(",")]
	class Good
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public Producer Producer { get; set; }

		public override string ToString() {
			var builder = new StringBuilder();
			builder.AppendLine($"{Id}, {Name}, {Price}");
			builder.AppendLine($"\t Category: {Category}");
			builder.AppendLine($"\t Producer: {Producer}");
			return builder.ToString();

		}
	}
}

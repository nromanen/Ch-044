using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Tastk1_NormalVersion
{
	[DelimitedRecord(";")]
	public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

		public override string ToString() {
			return Name;
		}

	}
}

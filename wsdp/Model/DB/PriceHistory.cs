using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DB
{
	public class PriceHistory
	{
		[Key]
		public int Id { get; set; }
		public string Url { get; set; }
		public DataType Date { get; set; }
		public decimal Price { get; set; }

	}
}

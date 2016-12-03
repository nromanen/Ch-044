using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
	public class PriceHistoryDTO
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public decimal? Price { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO {
	public class IteratorSettingsDTO {
		public string Url { get; set; }
		public string GoodsIteratorXpath { get; set; }
		public string UrlMask { get; set; }
		public int From { get; set; }
		public int To { get; set; }
	}
}

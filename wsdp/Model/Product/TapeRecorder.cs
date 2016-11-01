using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Product {
	public class TapeRecorder {
		public int Id { get; set; }
		public string Name { get; set; }
		public string ImgPath { get; set; }
		public string urlPath { get; set; }
		public string Price { get; set; }
		public Dictionary<string, string> Characteristics { get; set; }
	}
}

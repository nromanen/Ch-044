using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
	[ElasticsearchType(IdProperty = "UrlLink")]
	public class GoodDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public bool Status { get; set; }
		public decimal? Price { get; set; }
		public decimal? OldPrice { get; set; }

		public string ImgLink { get; set; }

		public string UrlLink { get; set; }

		public virtual CategoryDTO Category { get; set; }

		public int Category_Id { get; set; }

		public virtual WebShopDTO WebShop { get; set; }

		public int WebShop_Id { get; set; }
		public bool IsFollowed { get; set; }

		public PropertyValuesDTO PropertyValues { get; set; }
	}
}

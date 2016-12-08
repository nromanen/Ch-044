using Common.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DB
{
	public class Good
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public decimal? Price { get; set; }

		public string ImgLink { get; set; }

		[Required]
		public string UrlLink { get; set; }

		public decimal? OldPrice { get; set; }

		[ForeignKey("Category_Id")]
		public virtual Category Category { get; set; }

		[Required]
		public int Category_Id { get; set; }

		[ForeignKey("WebShop_Id")]
		public virtual WebShop WebShop { get; set; }

		[Required]
		public int WebShop_Id { get; set; }

		[Required]
		public string XmlData { get; set; }

		[Required]
		public bool Status { get; set; }

	}
}
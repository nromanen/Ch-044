using Common.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DB
{
	public class Good
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Category_Id")]
		public virtual Category Category { get; set; }

		[Required]
		public int Category_Id { get; set; }

		[Required]
		public string XmlData { get; set; }
	}
}
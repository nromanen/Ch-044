using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DB {
	public class Comment {
		[Key]
		public int Id { get; set; }

		[Required]
		public int UserId { get; set; }
		public virtual User User { get; set; }

		[Required]
		public int GoodId { get; set; }
		public virtual Good Good { get; set; }

		[Required]
		public string Description { get; set; }
		
		[Required]
		public DateTime Date { get; set; }

	}
}

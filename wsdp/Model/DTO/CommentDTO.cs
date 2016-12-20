using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO {
	public class CommentDTO {

		public int Id { get; set; }

		public int UserId { get; set; }
		public virtual UserDTO User { get; set; }

		public int GoodId { get; set; }
		public virtual GoodDTO Good { get; set; }

		public string Description { get; set; }

		public DateTime Date { get; set; }

		public bool CheckComment { get; set; }
	}
}

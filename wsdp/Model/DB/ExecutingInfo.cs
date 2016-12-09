using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace Model.DB {
	public class ExecutingInfo {

		[Key]
		public int Id { get; set; }

		[Required]
		public ExecuteStatus Status { get; set; }

		[Required]
		public int ParserTaskId { get; set; }

		public virtual ParserTask ParserTask { get; set; }

		[Required]
		public string GoodUrl { get; set; }

		public string ErrorMessage { get; set; }

		[Required]
		public DateTime Date { get; set; }
	}
}

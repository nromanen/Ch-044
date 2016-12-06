using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;

namespace Model.DTO {
	public class ExecutingInfoDTO {

		public int Id { get; set; }

		public ExecuteStatus Status { get; set; }

		public int ParserTaskId { get; set; }
		public virtual ParserTaskDTO ParserTask { get; set; }

		public string GoodUrl { get; set; }

		public string ErrorMessage { get; set; }

		public DateTime Date { get; set; }
	}
}

using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
	public class PriceFollowerDTO
	{
		public int id { get; set; }
		public int Good_Id { get; set; }
		public int User_Id { get; set; }
        public FollowStatus Status { get; set; }
		public decimal? Price { get; set; }
	}
}

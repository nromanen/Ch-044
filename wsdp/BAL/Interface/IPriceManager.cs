using Model.DB;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
	public interface IPriceManager
	{
		List<PriceHistoryDTO> GetAll();
		void Insert(PriceHistoryDTO price);
	}
}

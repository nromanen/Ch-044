using Model.DB;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
	public interface IFollowPriceManager
	{
		void Insert(PriceFollowerDTO model);
		List<PriceFollowerDTO> GetAll();
		void Delete(int follow_Id);
        void Update(PriceFollowerDTO model);

    }
}

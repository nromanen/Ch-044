using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
	public interface IPriceManager
	{
		void Insert(PriceHistory price);
	}
}

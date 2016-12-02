using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DB;
using DAL.Interface;

namespace BAL.Manager
{
	public class PriceManager : BaseManager,IPriceManager
	{
		public PriceManager(IUnitOfWork uOW) : base(uOW)
		{
		}

		public void Insert(PriceHistory price)
		{
			uOW.PriceRepo.Insert(price);
			uOW.Save();
		}
	}
}

using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DB;
using DAL.Interface;
using Model.DTO;
using AutoMapper;

namespace BAL.Manager
{
	public class PriceManager : BaseManager,IPriceManager
	{
		public PriceManager(IUnitOfWork uOW) : base(uOW)
		{
		}

		public List<PriceHistoryDTO> GetAll()
		{
			var prices = new List<PriceHistoryDTO>();
			foreach (var price in uOW.PriceRepo.All.ToList())
			{
				var priceq = uOW.PriceRepo.GetByID(price.Id);
				prices.Add(Mapper.Map<PriceHistoryDTO>(priceq));
			}
			return prices;
		}
		public void Insert(PriceHistoryDTO price)
		{
			var priceDb = Mapper.Map<PriceHistory>(price);
			
			var GoodPrices = uOW.PriceRepo.All.Where(x => x.Url == priceDb.Url && x.Price==price.Price).ToList();
			if(!GoodPrices.Any())
			{
				uOW.PriceRepo.Insert(priceDb);
				uOW.Save();
			}
		}
	}
}

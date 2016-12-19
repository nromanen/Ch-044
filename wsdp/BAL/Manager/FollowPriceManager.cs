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
using Common.Enum;

namespace BAL.Manager
{
	public class FollowPriceManager : BaseManager,IFollowPriceManager
	{
		public FollowPriceManager(IUnitOfWork uOW) : base(uOW)
		{
		}

		public List<PriceFollowerDTO> GetAll()
		{
			List<PriceFollowerDTO> prices = new List<PriceFollowerDTO>();
			foreach (var item in uOW.PriceFollowerRepo.All.ToList())
			{
				var price = Mapper.Map<PriceFollowerDTO>(item);
				prices.Add(price);
			}

			return prices;
		}

		public void Insert(PriceFollowerDTO model)
		{
			var item = Mapper.Map<PriceFollower>(model);
            item.Status = FollowStatus.NotSend;
			var followPrices = uOW.PriceFollowerRepo.All.Where(x => x.Good_Id == model.Good_Id && x.User_Id == model.User_Id).ToList();
			if (!followPrices.Any())
			{
				uOW.PriceFollowerRepo.Insert(item);
				uOW.Save();
			}
		}
		public void Delete(int follow_Id)
		{
			var goodDb = uOW.PriceFollowerRepo.GetByID(follow_Id);
			if (goodDb == null) return;
			uOW.PriceFollowerRepo.Delete(goodDb);
			uOW.Save();
		}
        public void Update(PriceFollowerDTO model)
        { 
            var entity = uOW.PriceFollowerRepo.GetByID(model.id);
            var item = Mapper.Map<PriceFollower>(model);

            entity.Price = item.Price;
            entity.Status = item.Status;

            uOW.Save();
        }
	}
}

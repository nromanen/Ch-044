using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using Model.DB;
using Model.DTO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BAL.Manager
{
    public class GoodManager : BaseManager, IGoodManager
    {
        public GoodManager(IUnitOfWork uOW) : base(uOW)
        {
        }
        

        public void InsertGood(GoodDTO good)
        {
            uOW.GoodRepo.Insert(Mapper.Map<Good>(good));
            uOW.Save();
        }

        public void InsertGood(Good good)
        {
            uOW.GoodRepo.Insert(good);
            uOW.Save();
        }

        public List<GoodDTO> GetAll()
        {
            var goodList = new List<GoodDTO>();
            foreach (var good in uOW.GoodRepo.All.ToList())
            {
                if (good.Status == false) continue;
                var good_temp = uOW.GoodRepo.GetByID(good.Id);
                goodList.Add(Mapper.Map<GoodDTO>(good_temp));
            }
            return goodList;
        }

        /// <summary>
        /// Insert good into database and return inserted item
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        public GoodDTO Insert(GoodDTO good)
        {
            if (good.ImgLink == null) good.ImgLink = @"http://www.kalahandi.info/wp-content/uploads/2016/05/sorry-image-not-available.png";
            var goodDb = Mapper.Map<Good>(good);
            goodDb.Status = true;
            var res = uOW.GoodRepo.Insert(goodDb);
            uOW.Save();
            return Mapper.Map<GoodDTO>(res);
        }

        /// <summary>
        /// Delete good into database
        /// </summary>
        /// <param name="good"></param>
        public void Delete(GoodDTO good)
        {
            if(good == null) return;
            var goodDb = uOW.GoodRepo.GetByID(good.Id);
            if(goodDb == null) return;
            goodDb.Status = false;
            uOW.Save();
        }
        /// <summary>
        /// Update good into database
        /// </summary>
        /// <param name="good"></param>
        public void Update(GoodDTO good)
        {
            if(good == null) return;
            var goodDb = uOW.GoodRepo.GetByID(good.Id);
            if (goodDb == null) return;

            var uGood = Mapper.Map<Good>(good);

            goodDb.Name = uGood.Name;
            goodDb.Category_Id = uGood.Category_Id;
            goodDb.WebShop_Id = uGood.WebShop_Id;
            goodDb.ImgLink = uGood.ImgLink;
            goodDb.UrlLink = uGood.UrlLink;
            goodDb.XmlData = uGood.XmlData;
            goodDb.Price = uGood.Price;
            goodDb.Status = uGood.Status;

            uOW.Save();
        }

        public GoodDTO Get(int id)
        {
            GoodDTO result = null;

            var good = uOW.GoodRepo.GetByID(id);
            if (good == null) return null;
            result = Mapper.Map<GoodDTO>(good);
			result.Category = Mapper.Map<CategoryDTO>(uOW.CategoryRepo.GetByID(good.Category_Id));
            result.WebShop = Mapper.Map<WebShopDTO>(uOW.WebShopRepo.GetByID(good.WebShop_Id));

            return result;
        }
		public GoodDTO GetAndCheckUser(int id, int? userId)
		{
			GoodDTO result = null;

			var good = uOW.GoodRepo.GetByID(id);
			if (good == null) return null;
			result = Mapper.Map<GoodDTO>(good);

			if (userId.HasValue)
			{
				var user = uOW.UserRepo.GetByID(userId.Value);
				var followed = uOW.PriceFollowerRepo.All.Any(x => x.Good_Id == id && x.User_Id == userId);
				result.IsFollowed = followed;
			}

			result.Category = Mapper.Map<CategoryDTO>(uOW.CategoryRepo.GetByID(good.Category_Id));
			result.WebShop = Mapper.Map<WebShopDTO>(uOW.WebShopRepo.GetByID(good.WebShop_Id));

			return result;
		}
	}
}
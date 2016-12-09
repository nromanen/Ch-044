using BAL.Interface;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
	public class GoodController : BaseController
	{
		private IElasticManager elasticmanager = null;
		private IGoodManager goodmanager = null;
		private ICategoryManager categoryManager = null;
		private IWebShopManager shopManager = null;

		public GoodController(IElasticManager elasticmanager,IGoodManager goodmanager,ICategoryManager categoryManager,IWebShopManager shopManager)
		{
			this.categoryManager = categoryManager;
			this.goodmanager = goodmanager;
			this.elasticmanager = elasticmanager;
			this.shopManager = shopManager;
		}
		// GET: Good
		public ActionResult ConcreteGood(int id)
		{
			GoodViewModelDTO mainmodel = new GoodViewModelDTO();

			GoodDTO good = goodmanager.Get(id);

			mainmodel.Good = good;

			List<GoodDTO> alloffers = new List<GoodDTO>();
			List<GoodDTO> similaroffers = new List<GoodDTO>();

			similaroffers = elasticmanager.Get(good.Name).ToList();

			decimal minprice = (decimal)(similaroffers.Select(c => c.Price).Min() ?? good.Price);
			decimal maxprice = (decimal)(similaroffers.Select(c => c.Price).Max() ?? good.Price);

			mainmodel.AllOffers = alloffers;
			mainmodel.SimilarOffers = similaroffers;
			mainmodel.MinPrice = minprice;
			mainmodel.MaxPrice = maxprice;

			return View(mainmodel);
		}

		public ActionResult GetCategoryGood(string c_Id)
		{
		//	var goodListCat=elasticmanager.GetByCategoryId(c_Id);
			var goodListCat = goodmanager.GetAll().Where(i => i.Category_Id == Convert.ToInt32(c_Id)).ToList();
			foreach (var item in goodListCat)
			{
				item.Category = categoryManager.Get(item.Category_Id);

				item.WebShop = shopManager.GetById(item.WebShop_Id);
			}
				return View(goodListCat);
		}
	}
}
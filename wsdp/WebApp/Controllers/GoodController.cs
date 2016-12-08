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
		private IGoodDatabasesWizard goodmanager = null;
		private IElasticManager elasticmanager = null;
		private IGoodManager goodManagerSQL = null;
		private ICategoryManager categoryManager = null;
		private IWebShopManager shopManager = null;

		public GoodController(IGoodDatabasesWizard goodmanager, IElasticManager elasticmanager,IGoodManager goodManagerSQL,ICategoryManager categoryManager,IWebShopManager shopManager)
		{
			this.categoryManager = categoryManager;
			this.goodManagerSQL = goodManagerSQL;
			this.goodmanager = goodmanager;
			this.elasticmanager = elasticmanager;
			this.shopManager = shopManager;
		}
		// GET: Good
		public ActionResult Index()
		{
			GoodViewModelDTO mainmodel = new GoodViewModelDTO();

			GoodDTO good = goodmanager.Get(609);

			mainmodel.Good = good;

			List<GoodDTO> alloffers = new List<GoodDTO>();
			List<GoodDTO> similaroffers = new List<GoodDTO>();

			alloffers = elasticmanager.GetByName("Мобільний телефон LG K5 X220 Gold").ToList();
			similaroffers = elasticmanager.Get(good.Name).ToList();

			decimal minprice = (decimal)(alloffers.Select(c => c.Price).Min() ?? good.Price);
			decimal maxprice = (decimal)(alloffers.Select(c => c.Price).Max() ?? good.Price);

			mainmodel.AllOffers = alloffers;
			mainmodel.SimilarOffers = similaroffers;
			mainmodel.MinPrice = minprice;
			mainmodel.MaxPrice = maxprice;

			return View(mainmodel);
		}
		public ActionResult GetCategoryGood(string c_Id)
		{
		//	var goodListCat=elasticmanager.GetByCategoryId(c_Id);
			var goodListCat = goodManagerSQL.GetAll().Where(i => i.Category_Id == Convert.ToInt32(c_Id)).ToList();
			foreach (var item in goodListCat)
			{
				item.Category = categoryManager.Get(item.Category_Id);

				item.WebShop = shopManager.GetById(item.WebShop_Id);
			}
				return View(goodListCat);
		}
	}
}
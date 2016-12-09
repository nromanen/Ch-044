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
		private IGoodManager goodmanager = null;
		private IElasticManager elasticmanager = null;
		private IPropertyManager propertymanager = null;
	    private ICategoryManager categoryManager = null;
	    private IWebShopManager webShopManager = null;

		public GoodController(IGoodManager goodmanager, IElasticManager elasticmanager, IPropertyManager propertymanager, ICategoryManager categoryManager, IWebShopManager webShopManager)
		{
		    this.webShopManager = webShopManager;
		    this.categoryManager = categoryManager;
			this.goodmanager = goodmanager;
			this.elasticmanager = elasticmanager;
			this.propertymanager = propertymanager;
		}
		// GET: Good
		public ActionResult ConcreteGood(int id)
		{
			GoodViewModelDTO mainmodel = new GoodViewModelDTO();

			GoodDTO good = goodmanager.Get(id);

			mainmodel.Good = good;

			List<GoodDTO> alloffers = new List<GoodDTO>();
			List<GoodDTO> similaroffers = new List<GoodDTO>();

			Dictionary<string, string> properties = new Dictionary<string, string>();

			foreach (var item in good.PropertyValues.DictStringProperties)
			{
				string propertyname = propertymanager.Get(item.Key).Name;
				properties.Add(propertyname, item.Value);
			}

			similaroffers = elasticmanager.Get(good.Name).ToList();

			decimal minprice = (decimal)(similaroffers.Select(c => c.Price).Min() ?? good.Price);
			decimal maxprice = (decimal)(similaroffers.Select(c => c.Price).Max() ?? good.Price);

			mainmodel.AllOffers = alloffers;
			mainmodel.SimilarOffers = similaroffers;
			mainmodel.MinPrice = minprice;
			mainmodel.MaxPrice = maxprice;
			mainmodel.Properties = properties;

			return View(mainmodel);
		}
		public ActionResult GetCategoryGood(int c_Id)
		{
			var goodListCat=elasticmanager.GetByCategoryId(c_Id);
            
            foreach (var item in goodListCat)
            {
                item.Category = categoryManager.Get(item.Category_Id);

                item.WebShop = webShopManager.GetById(item.WebShop_Id);
            }
            return View(goodListCat);
		}
	}
}
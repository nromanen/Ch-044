using BAL.Interface;
using Model.DB;
using Model.DTO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace WebApp.Controllers
{
	public class HomeController : BaseController
	{
		private ICategoryManager categoryManager;
		private IWebShopManager shopManager;
		private IPropertyManager propertyManager;
		private IGoodManager goodManager;

		public HomeController(ICategoryManager categoryManager, IGoodManager goodManager,IPropertyManager propertyManager,IWebShopManager shopManager)
		{	
			this.categoryManager = categoryManager;
			this.goodManager = goodManager;
			this.propertyManager = propertyManager;
			this.shopManager = shopManager;
		}

		public ActionResult Index()
		{
			var goods = goodManager.GetAll();
			var categories = categoryManager.GetAll();
			var goods_list = goodManager.GetAll();

			foreach (var item in goods_list)
			{
				item.Category = categoryManager.Get(item.Category_Id);

				item.WebShop = shopManager.GetById(item.WebShop_Id);
			}
			
			var Custom_model = new IndexViewDTO()
			{
				CategoryList = categories,
				GoodList = goods_list
			};
			ModelState.Clear();
			return View(Custom_model);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
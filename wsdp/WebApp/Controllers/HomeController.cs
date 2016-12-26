using BAL.Interface;
using Model.DB;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;
using System.Xml.Serialization;
using WebApp.Models;
using PagedList;

namespace WebApp.Controllers
{
	public class HomeController : BaseController
	{
		private ICategoryManager categoryManager;
		private IWebShopManager shopManager;
		private IPropertyManager propertyManager;
		private IGoodManager goodManager;
		private IPriceManager priceManager;
	    private IElasticManager elasticManager;
	 

		public HomeController(IElasticManager elasticManager, ICategoryManager categoryManager, IGoodManager goodManager, IPropertyManager propertyManager, IWebShopManager shopManager, IPriceManager priceManager)
		{
			this.priceManager = priceManager;
			this.categoryManager = categoryManager;
			this.goodManager = goodManager;
			this.propertyManager = propertyManager;
			this.shopManager = shopManager;
		    this.elasticManager = elasticManager;
		}

		public ActionResult Index(int? page)
		{
			int pageSize = 7;
			var goods = goodManager.GetAll();
			var categories = categoryManager.GetAll();
			var goods_list = goodManager.GetAll();

			foreach (var item in goods_list)
			{
				item.Category = categoryManager.Get(item.Category_Id);

				item.WebShop = shopManager.GetById(item.WebShop_Id);
			}
			int pageNumber = (page ?? 1);
			var Custom_model = new IndexViewDTO()
			{
				CategoryList = categories,
				GoodList = goods_list.ToPagedList(pageNumber, pageSize)
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
		public ActionResult PriceStat()
		{

			return View();
		}


		/// <summary>
		/// Get data from PriceHistory table of database.
		/// </summary>
		/// <param name="url_one"></param>
		/// <param name="year"></param>
		/// <returns></returns>
		public JsonResult getLineChartData(string url_one, string year)
		{
			var iData = new List<object>();
			var labels = new List<string>();
			var lst_dataItem_1 = new List<decimal?>();
			var labels_q = priceManager.GetAll().Where(i => i.Url == url_one && i.Date.Year.ToString() == year).Select(i => i.Date).
			OrderBy(x => x.Month).ThenBy(x=>x.Day).
			Select(m => m.Date.ToString("dd/MM/yyyy")).Distinct().ToList();	
			var price_lst = priceManager.GetAll().Where(i => i.Url == url_one && i.Date.Year.ToString() == year).Select(i => i.Price).ToList();
			if (price_lst.Count == 1 && labels_q.Count == 1)
			{
				var dd_n = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day).ToString("dd/MM/yyyy");
				var curr_p = price_lst[0];
				lst_dataItem_1.Add(curr_p);
				labels.AddRange(labels_q);
				labels.Add(dd_n);
				lst_dataItem_1.AddRange(price_lst);
			}
			else
			{
				lst_dataItem_1.AddRange(price_lst);
				labels.AddRange(labels_q);
			}
			iData.Add(labels);
			iData.Add(lst_dataItem_1);

			return Json(iData);
		}


        public JsonResult GetExactGoods(string term)
        {
            var list = elasticManager
                .GetByPrefix(term, 3)
                .Select(x=> new {value = x.Name}).Distinct();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
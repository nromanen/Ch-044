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

		private IGoodManager goodManager;

		public HomeController(ICategoryManager categoryManager, IGoodManager goodManager)
		{
			this.categoryManager = categoryManager;
			this.goodManager = goodManager;
		}

		public ActionResult Index()
		{
			var goods = goodManager.GetAll().Select(c => c).ToList();
			var categories = categoryManager.GetAll();
			var goods_TV = new List<TVDTO>();
			foreach (var good in goods)
			{
				//xRoot.ElementName = "TV";
				var good_temp = new TVDTO();
				XmlSerializer serializer = new XmlSerializer(typeof (TVDTO));
				StringReader rdr = new StringReader(good.XmlData);
				good_temp = (TVDTO) serializer.Deserialize(rdr);
				goods_TV.Add(good_temp);
			}
			var Custom_model = new IndexViewDTO()
			{
				GoodTVList = goods_TV,
				CategoryList = categories,
				GoodList = goods
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
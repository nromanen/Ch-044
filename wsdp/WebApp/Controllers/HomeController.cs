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
			var goods = goodManager.GetAll();
			var categories = categoryManager.GetAll();
			var goods_list = new List<GoodViewDTO>();
			foreach (var good in goods)
			{
				var good_temp = new GoodViewDTO();
				var xml_root = categories.Where(i => i.Id == good.Category_Id).Select(i => i.Name).FirstOrDefault();
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(GoodViewDTO), new XmlRootAttribute(xml_root));
				StringReader rdr = new StringReader(good.XmlData);
				good_temp = (GoodViewDTO)xmlSerializer.Deserialize(rdr);
				good_temp.CategoryName = xml_root;
				goods_list.Add(good_temp);
			}
			var cat_goods = goods_list.Select(i => i.CategoryName).Distinct().ToList();
			var Custom_model = new IndexViewDTO()
			{
				GoodCollection = goods_list,
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
using BAL.Interface;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace WebApp.Controllers
{
	public class GoodController : BaseController
	{
		private IGoodManager goodmanager = null;
		private IElasticManager elasticmanager = null;
		private IPropertyManager propertymanager = null;
		private ICategoryManager categoryManager = null;
		private IWebShopManager webShopManager = null;
		private IFollowPriceManager followPriceManager;
		private IUserManager userManager;

		public GoodController(IGoodManager goodmanager, IElasticManager elasticmanager, IPropertyManager propertymanager, ICategoryManager categoryManager, IWebShopManager webShopManager, IFollowPriceManager followPriceManager,IUserManager userManager)
		{
			this.userManager = userManager;
			this.webShopManager = webShopManager;
			this.categoryManager = categoryManager;
			this.goodmanager = goodmanager;
			this.elasticmanager = elasticmanager;
			this.propertymanager = propertymanager;
			this.followPriceManager = followPriceManager;
		}
		// GET: Good
		public ActionResult ConcreteGood(int id)
		{
			GoodViewModelDTO mainmodel = new GoodViewModelDTO();
			GoodDTO good = goodmanager.GetAndCheckUser(id,Convert.ToInt32(User.Identity.GetUserId()));

			mainmodel.Good = good;

			List<GoodDTO> alloffers = new List<GoodDTO>();
			List<GoodDTO> similaroffers = new List<GoodDTO>();

			Dictionary<string, string> properties = new Dictionary<string, string>();

			foreach (var item in good.PropertyValues.DictStringProperties)
			{
				string propertyname = propertymanager.Get(item.Key).Name;
				properties.Add(propertyname, item.Value);
			}
			//similaroffers = elasticmanager.GetSimilar(good.Name.Split(' ')[1]).ToList();
			similaroffers = elasticmanager.GetSimilar(good.Name).ToList();
			alloffers = elasticmanager.GetByName(good.Name).ToList();

			foreach (var simgood in similaroffers)
			{
				simgood.Category = categoryManager.Get(simgood.Category_Id);
				simgood.WebShop = webShopManager.GetById(simgood.WebShop_Id);
			}

			decimal minprice = (decimal)(similaroffers.Select(c => c.Price).Min() ?? good.Price);
			decimal maxprice = (decimal)(similaroffers.Select(c => c.Price).Max() ?? good.Price);

			mainmodel.AllOffers = alloffers;
			mainmodel.SimilarOffers = similaroffers;
			mainmodel.MinPrice = minprice;
			mainmodel.MaxPrice = maxprice;
			mainmodel.Properties = properties;
			if(User.Identity.IsAuthenticated)
			{
				mainmodel.UserId = Convert.ToInt32(User.Identity.GetUserId());
			}
			else
			{
				mainmodel.UserId = null;
			}

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
			if (!goodListCat.Any())
			{
				return View("../Error/CategoryNotFound");
			}
			else
			{
				return View(goodListCat);
			}
		}

		public ActionResult EmptyList()
		{
			return View();
		}

        [HttpPost]
		public void FollowGoodPrice(string good_Id, string user_Id,string price)
		    {
			if(Request.IsAuthenticated)
			{

				var model = new PriceFollowerDTO()
				{
					Good_Id = Convert.ToInt32(good_Id),
					User_Id = Convert.ToInt32(user_Id),
					Price = Convert.ToDecimal(price)
                };
                followPriceManager.Insert(model);
            }
            else
            {
                RedirectToAction("SignUp", "Account");
            }
			
			
		}

		[HttpPost]
		public void DeleteGoodFollow(string good_Id, string user_Id)
		{
			var del_id = followPriceManager.GetAll().
				Where(i => i.Good_Id == Convert.ToInt32(good_Id) && i.User_Id == Convert.ToInt32(user_Id)).
				Select(i => i.id).FirstOrDefault();
			followPriceManager.Delete(del_id);

		}

		public ActionResult GetGoodsByName(string name)
		{
			if (name == null) return HttpNotFound();
		  
			var goodList = elasticmanager.GetExact(name);
			if (goodList.Count == 0) return RedirectToAction("EmptyList");
			foreach (var item in goodList)
			{
				item.WebShop = webShopManager.GetById(item.WebShop_Id);
				item.Category = categoryManager.Get(item.Category_Id);
			}
			return View(goodList);
		}

        [HttpPost]
        public void SetCompareGood(int id)
        {
            if (Request.Cookies.Get("firstCompareGood") == null)
            {
                HttpCookie firstcookie = new HttpCookie("firstCompareGood");
                firstcookie.Value = id.ToString();
                Response.Cookies.Add(firstcookie);
            }
            else
            {
                HttpCookie secondcookie = new HttpCookie("secondCompareGood");
                secondcookie.Value = id.ToString();
                Response.Cookies.Add(secondcookie);
            }
        }

        public ActionResult CompareGoods()
        {
            //int firstGoodId = Convert.ToInt32(Request.Cookies.Get("firstCompareGood").Value);
            //int secondGoodId = Convert.ToInt32(Request.Cookies.Get("secondCompareGood").Value);

            int firstGoodId = 2;
            int secondGoodId = 3;
            GoodDTO firstGood = goodmanager.Get(firstGoodId);
            GoodDTO secondGood = goodmanager.Get(secondGoodId);

            Dictionary<string, string> firstProperties = new Dictionary<string, string>();
            Dictionary<string, string> secondProperties = new Dictionary<string, string>();


            foreach (var item in firstGood.PropertyValues.DictStringProperties)
            {
                string propertyname = propertymanager.Get(item.Key).Name;
                firstProperties.Add(propertyname, item.Value);
            }

            foreach (var item in secondGood.PropertyValues.DictStringProperties)
            {
                string propertyname = propertymanager.Get(item.Key).Name;
                secondProperties.Add(propertyname, item.Value);
            }

            CompareGoodsDTO info = new CompareGoodsDTO()
            {
                FirstGood = firstGood,
                SecondGood = secondGood,
                FirstProperties = firstProperties,
                SecondProperties = secondProperties
            };
            return View(info);
        }
	}
}
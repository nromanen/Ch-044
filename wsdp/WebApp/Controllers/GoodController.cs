﻿using BAL.Interface;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

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
		private ICommentManager commentManager;

		public GoodController(IGoodManager goodmanager, IElasticManager elasticmanager, IPropertyManager propertymanager, ICategoryManager categoryManager, IWebShopManager webShopManager, IFollowPriceManager followPriceManager,IUserManager userManager, ICommentManager commentManager)
		{
			this.userManager = userManager;
			this.webShopManager = webShopManager;
			this.categoryManager = categoryManager;
			this.goodmanager = goodmanager;
			this.elasticmanager = elasticmanager;
			this.propertymanager = propertymanager;
			this.followPriceManager = followPriceManager;
			this.commentManager = commentManager;
		}
		// GET: Good
		public ActionResult ConcreteGood(int id)
		{
			var comments = commentManager.GetAllCommentsByGoodId(id).ToList();
			GoodViewModelDTO mainmodel = new GoodViewModelDTO();
			GoodDTO good = goodmanager.GetAndCheckUser(id,Convert.ToInt32(User.Identity.GetUserId()));

			mainmodel.Good = good;

            ListCompareGoodDTO compgoods = Session["ComparingGoods"] as ListCompareGoodDTO;
            try
            {
                mainmodel.IsComparing = compgoods.CompareGoods[good.Category_Id].Contains(id);
            }
            catch
            {
                mainmodel.IsComparing = false;
            }

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
			
			mainmodel.Comments = commentManager.CheckCommentsDependency(Convert.ToInt32(User.Identity.GetUserId()), id).ToList();

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
            GoodDTO good = goodmanager.Get(id);
            int categoryId = good.Category_Id;

            ListCompareGoodDTO compgoods = Session["ComparingGoods"] as ListCompareGoodDTO;

            if (compgoods == null)
            {
                compgoods = new ListCompareGoodDTO();

                if (!compgoods.CompareGoods.ContainsKey(categoryId))
                {
                    compgoods.CompareGoods.Add(categoryId, new List<int>());
                }
                compgoods.CompareGoods[categoryId].Add(id);
                Session["ComparingGoods"] = compgoods;
                return;
            }
            else
            {
                if (compgoods.CompareGoods[categoryId].Find(c => c == id) != -1)
                {
                    compgoods.CompareGoods[categoryId].Add(id);
                    Session["ComparingGoods"] = compgoods;
                    return; 
                }
            }
        }

        public ActionResult ComparingCategories()
        {
            ComparingCategoriesDTO model = new ComparingCategoriesDTO();
            ListCompareGoodDTO goods = Session["ComparingGoods"] as ListCompareGoodDTO;
            model.CategoriesGoods = new Dictionary<CategoryDTO, List<GoodDTO>>();
            foreach (var key in goods.CompareGoods.Keys)
            {
                CategoryDTO category = categoryManager.Get(key);
                model.CategoriesGoods.Add(category, new List<GoodDTO>());
                foreach (var good_id in goods.CompareGoods[key])
                {
                    GoodDTO good = goodmanager.Get(good_id);
                    model.CategoriesGoods[category].Add(good);
                }
            }
            return View(model);
        }

        public ActionResult CompareGoods(int id)
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

		[Authorize(Roles = "Administrator, User")]
		[HttpPost]
		public string AddComment(string text, int goodId) {
			var comment = new CommentDTO() {
				GoodId = goodId,
				UserId = Convert.ToInt32(User.Identity.GetUserId()),
				Description = text,
				Date = DateTime.Now
			};
			return commentManager.Insert(comment) == true ? JsonConvert.SerializeObject(comment) : null;
		}

		[Authorize(Roles = "Administrator, User")]
		[HttpPost]
		public bool DeleteComment(int commentId) {
			var comment = commentManager.GetById(commentId);

			if (Convert.ToInt32(User.Identity.GetUserId()) == comment.UserId) {
				return commentManager.Delete(comment) == true ? true : false;
			} else
				return false;
		}

		[Authorize(Roles = "Administrator, User")]
		[HttpPost]
		public string UpdateComment(string text, int commentId) {
			var comment = commentManager.GetById(commentId);
			comment.Description = text;
			if (Convert.ToInt32(User.Identity.GetUserId()) == comment.UserId) {
				return commentManager.Update(comment) == true ? JsonConvert.SerializeObject(comment) : null;
			} else
				return null;
		}
	}
}
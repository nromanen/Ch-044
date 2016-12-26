using BAL.Interface;
using Common.Enum;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.Mvc;

namespace WebApp.Controllers
{
	[Authorize]
	public class AdminController : BaseController
	{
		private ICategoryManager categoryManager;
		private IPropertyManager propertyManager;
		private IUserManager userManager;
		private IRoleManager roleManager;
		private ICheckGoodManager checkergoodManager;
		private IParserTaskManager parserTaskManager;

		public AdminController(ICategoryManager categoryManager, IPropertyManager propertyManager, IUserManager userManager, IRoleManager roleManager, ICheckGoodManager checkergoodManager, IParserTaskManager parserTaskManager)
		{
			this.categoryManager = categoryManager;
			this.propertyManager = propertyManager;
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.checkergoodManager = checkergoodManager;
			this.parserTaskManager = parserTaskManager;
		}

		// GET: Admin
		public ActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Edit Category
		/// </summary>
		/// <returns></returns>
		[Authorize(Roles = "Administrator")]
		public ActionResult EditCategories()
		{
			var categories = categoryManager.GetAll().Where(c => c.ParentCategoryId == null).Select(c => c).ToList();
			ModelState.Clear();
			return View(categories);
		}
		/// <summary>
		/// Post for adding category.
		/// </summary>
		/// <param name="namecategory"></param>
		/// <param name="parentcategory"></param>
		/// <returns></returns>
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public int AddCategory(string namecategory, int? parentcategory)
		{
			return categoryManager.Add(namecategory, parentcategory ?? -1);
		}
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void UpdateCategory(string namecategory, int id)
		{
			categoryManager.Rename(id, namecategory);
		}

		/// <summary>
		/// Post for removing category.
		/// </summary>
		/// <param name="id"></param>
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void RemoveCategory(int id)
		{
			categoryManager.Delete(id);
		}
		/// <summary>
		/// Post for change a parent of category.
		/// </summary>
		/// <param name="categoryid"></param>
		/// <param name="parentid"></param>
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void ChangeParent(int categoryid, int? parentid)
		{
			categoryManager.ChangeParent(categoryid, parentid ?? -1);
		}
		/// <summary>
		/// Add Property.
		/// </summary>
		/// <param name="catid"></param>
		/// <returns></returns>
		[Authorize(Roles = "Administrator")]
		public ActionResult AddProperty(int catid)
		{
			List<CategoryDTO> categories =
				categoryManager.GetAll();
			List<string> enums = new List<string>();
			foreach (var i in Enum.GetNames(typeof(PropertyType)))
				enums.Add(i);
			PropertyViewDTO custom_model = new PropertyViewDTO() { enums = enums, categories = categories, CategoryId = catid };
			return View(custom_model);
		}
		/// <summary>
		/// Update Property.
		/// </summary>
		/// <param name="catid"></param>
		/// <param name="propid"></param>
		/// <returns></returns>
		[Authorize(Roles = "Administrator")]
		public ActionResult UpdateProperty(int catid, int propid)
		{
			var properties = propertyManager.GetAll().Select(c => c).ToList();
			var categories =
			 categoryManager.GetAll();
			var enums = new List<string>();
			foreach (var i in Enum.GetNames(typeof(PropertyType)))
				enums.Add(i);

			PropertyViewDTO custom_model = new PropertyViewDTO() { enums = enums, categories = categories, properties = properties, CategoryId = catid, PropertyId = propid };
			return View(custom_model);
		}
		/// <summary>
		/// Add Property
		/// </summary>
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void AddProperty(string Name, string Description, string Type, string Prefix, string Sufix, int Category_Id, string DefaultValue)
		{
			propertyManager.Add(Name, Description, Type, Prefix, Sufix, Category_Id, DefaultValue);
			Response.Redirect("EditCategories");
		}
		/// <summary>
		/// Post for remove property.
		/// </summary>
		/// <param name="id"></param>
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void RemoveProperty(int id)
		{
			propertyManager.Delete(id);
		}
		/// <summary>
		/// Post for update property.
		/// </summary>
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void UpdateProperty(int Property_Id, string Name, string Description, string Type, string Prefix,
			string Sufix, string DefaultValue, int Category_Id)
		{
			propertyManager.Update(Property_Id, Name, Description, Type, Prefix, Sufix, DefaultValue, Category_Id);
			Response.Redirect("EditCategories");
		}
		[Authorize(Roles = "Administrator")]
		public ActionResult EditUsers()
		{
			var Roles = roleManager.GetAll();
			var CustomView = new UserViewDTO() { Roles = Roles };
			ModelState.Clear();
			return View(CustomView);
		}

		/// <summary>
		/// Server side pagination for users table.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult LoadData()
		{
			var draw = Request.Form.GetValues("draw").FirstOrDefault();
			var start = Request.Form.GetValues("start").FirstOrDefault();
			var length = Request.Form.GetValues("length").FirstOrDefault();
			int pageSize = length != null ? Convert.ToInt32(length) : 0;
			int skip = start != null ? Convert.ToInt32(start) : 0;
			int totalRecords = 0;
			var v = userManager.GetUsersSize(skip, pageSize);
			var userList = (from us in roleManager.GetAll() select us);
			foreach (var item in v)
			{
				item.Password = new string('*', item.Password.Length);
				item.RoleName = userList.Select(i=>i).Where(u=>u.Id==item.RoleId).Select(u=>u.Name).FirstOrDefault();
			}

			totalRecords = userManager.GetUsersSize();
			return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = v }, JsonRequestBehavior.AllowGet);		
		}

		public SecureString ToSecureString(string Source)
		{
			if (string.IsNullOrWhiteSpace(Source))
				return null;
			else
			{
				var Result = new SecureString();
				foreach (char c in Source.ToCharArray())
					Result.AppendChar(c);
				return Result;
			}
		}
		/// <summary>
		/// Update user.
		/// </summary>
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void UpdateUser(int Id, string UserName, string Password, string Email, int RoleId)
		{
			userManager.UpdateUser(Id, UserName, Password, Email, RoleId);
		}
		/// <summary>
		/// Change order number.
		/// </summary>
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void ChangeOrderNo(int id, int orderno)
		{
			categoryManager.ChangeOrderNo(id, orderno);
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ActionResult CheckExistGoods()
		{
			CheckGoodDTO checkgood = new CheckGoodDTO();
			List<ParserTaskDTO> parsertasks = parserTaskManager.GetAll();
			List<CategoryDTO> categories = categoryManager.GetAll();

			checkgood.Categories = categories;
			checkgood.ParserTasks = parsertasks;

			return View(checkgood);
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ActionResult CheckGoodsAndUpdate(int category_id, int parsertask_id)
		{
			var deletedGoods = checkergoodManager.CheckGoodsFromOneCategory(category_id, parsertask_id).Select(c => new {Name = c.Name, UrlLink = c.UrlLink}).ToList();
			return Json(new { success = true, deletedgoods = deletedGoods.ToArray() },
						JsonRequestBehavior.AllowGet);
		}
	}
}
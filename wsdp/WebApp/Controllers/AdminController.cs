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

		public AdminController(ICategoryManager categoryManager, IPropertyManager propertyManager, IUserManager userManager, IRoleManager roleManager)
		{
			this.categoryManager = categoryManager;
			this.propertyManager = propertyManager;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

		// GET: Admin
		public ActionResult Index()
		{
			return View();
		}

		[Authorize(Roles = "Administrator")]
		public ActionResult EditCategories()
		{
			var categories = categoryManager.GetAll().Where(c => c.ParentCategoryId == null).Select(c => c).ToList();
			ModelState.Clear();
			return View(categories);
		}
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
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void RemoveCategory(int id)
		{
			categoryManager.Delete(id);
		}
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void ChangeParent(int categoryid, int? parentid)
		{
			categoryManager.ChangeParent(categoryid, parentid ?? -1);
		}
		[Authorize(Roles = "Administrator")]
		public ActionResult AddProperty(int catid)
		{
			List<CategoryDTO> categories =
				categoryManager.GetAll();
			List<string> enums = new List<string>();
			foreach (var i in Enum.GetNames(typeof(PropertyType)))
				enums.Add(i);
			PropertyViewDTO custom_model = new PropertyViewDTO() { enums = enums, categories = categories, CategoryId = catid };
			ModelState.Clear();
			return View(custom_model);
		}
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
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void AddProperty(string Name, string Description, string Type, string Prefix, string Sufix, int Category_Id, string DefaultValue)
		{
			propertyManager.Add(Name, Description, Type, Prefix, Sufix, Category_Id, DefaultValue);
			Response.Redirect("EditCategories");
		}
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void RemoveProperty(int id)
		{
			propertyManager.Delete(id);
		}
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
			var Users = userManager.GetAll();
			var Roles = roleManager.GetAll();
			foreach (var item in Users)
			{
				item.Password = new string('*', item.Password.Length);
			}
			var CustomView = new UserViewDTO() { Users = Users, Roles = Roles };
			ModelState.Clear();
			return View(CustomView);
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
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void UpdateUser(int Id, string UserName, string Password, string Email, int RoleId)
		{
			userManager.UpdateUser(Id, UserName, Password, Email, RoleId);
		}
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public void ChangeOrderNo(int id, int orderno)
		{
			categoryManager.ChangeOrderNo(id, orderno);
		}

	}
}
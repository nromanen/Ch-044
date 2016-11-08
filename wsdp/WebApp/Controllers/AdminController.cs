using BAL.Interface;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Enum;

namespace WebApp.Controllers
{
    public class AdminController : BaseController
    {
        ICategoryManager categoryManager;
        IPropertyManager propertyManager;
        IUserManager userManager;
        IRoleManager roleManager;

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

        public ActionResult EditCategories()
        {
            var categories = categoryManager.GetAll().Where(c => c.ParentCategoryId == null).Select(c => c).ToList();
            ModelState.Clear();
            return View(categories);
        }

        [HttpPost]
        public int AddCategory(string namecategory, int? parentcategory)
        {
            return categoryManager.Add(namecategory, parentcategory ?? -1);
        }

        [HttpPost]
        public void UpdateCategory(string namecategory, int id)
        {
            categoryManager.Rename(id, namecategory);
        }

        [HttpPost]
        public void RemoveCategory(int id)
        {
            categoryManager.Delete(id);
        }
        [HttpPost]
        public void ChangeParent(int categoryid, int? parentid)
        {
            categoryManager.ChangeParent(categoryid, parentid ?? -1);
        }
        public ActionResult AddProperty(int catid)
        {
            List<CategoryDTO> categories =
                categoryManager.GetAll().Select(c => c).ToList();
            List<string> enums = new List<string>();
            foreach (var i in Enum.GetNames(typeof(PropertyType)))
                enums.Add(i);
            PropertyViewDTO custom_model = new PropertyViewDTO() { enums = enums, categories = categories, CategoryId = catid };
            ModelState.Clear();
            return View(custom_model);
        }
        public ActionResult UpdateProperty(int catid, int propid)
        {
            var properties = propertyManager.GetAll().Select(c => c).ToList();
            var categories =
             categoryManager.GetAll().Select(c => c).ToList();
            var enums = new List<string>();
            foreach (var i in Enum.GetNames(typeof(PropertyType)))
                enums.Add(i);

            PropertyViewDTO custom_model = new PropertyViewDTO() { enums = enums, categories = categories, properties = properties, CategoryId = catid, PropertyId = propid };
            return View(custom_model);
        }

        [HttpPost]
        public void AddProperty(string Name, string Description, string Type, string Prefix, string Sufix, int Category_Id, string DefaultValue)
        {
            propertyManager.Add(Name, Description, Type, Prefix, Sufix, Category_Id, DefaultValue);
            Response.Redirect("EditCategories");
        }

        [HttpPost]
        public void RemoveProperty(int id)
        {
            propertyManager.Delete(id);
        }

        [HttpPost]
        public void UpdateProperty(int Property_Id, string Name, string Description, string Type, string Prefix,
            string Sufix, string DefaultValue, int Category_Id)
        {
            propertyManager.Update(Property_Id, Name, Description, Type, Prefix, Sufix, DefaultValue, Category_Id);
            Response.Redirect("EditCategories");
        }

        public ActionResult EditUsers()
        {
            var Users = userManager.GetAll().Select(c => c).ToList();
            var Roles = roleManager.GetAll().Select(c => c).ToList();
            var CustomView = new UserViewDTO() { Users = Users, Roles = Roles };
            ModelState.Clear();
            return View(CustomView);
        }
        [HttpPost]
        public void UpdateUser(int Id, string UserName, string Password, string Email, int RoleId)
        {
            userManager.UpdateUser(Id, UserName, Password, Email, RoleId);
        }
    }
}
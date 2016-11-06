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

        public AdminController(ICategoryManager categoryManager, IPropertyManager propertyManager, IUserManager userManager)
        {
            this.categoryManager = categoryManager;
            this.propertyManager = propertyManager;
            this.userManager = userManager;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditCategories()
        {
            var categories =
                categoryManager.GetAll().Where(c => c.ParentCategoryId == null).Select(c => c).ToList();
            var properties = propertyManager.GetAll().Select(c => c).ToList();

            UpdateCategoriesWithProperties(categories, properties);

            var CategoriesView = new PropertyViewDTO() { categories = categories, properties = properties };
            ModelState.Clear();
            return View(CategoriesView);
        }

        private void UpdateCategoriesWithProperties(ICollection<CategoryDTO> category, ICollection<PropertyDTO> properties)
        {
            category.ToList().ForEach(c =>
            {
                if (c.ChildrenCategory != null)
                {
                    UpdateCategoriesWithProperties(c.ChildrenCategory, properties);
                }
                c.PropertyList = properties.Where(prop => prop.Category_Id == c.Id).ToList();
            });
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
        public void AddProperty(string Name, string Description, string Type, string Prefix, string Sufix,
            int Characteristic_Id, int Category_Id, string DefaultValue)
        {
            propertyManager.Add(Name, Description, Type, Prefix, Sufix, Characteristic_Id, Category_Id, DefaultValue);
            Response.Redirect("EditCategories");
        }

        [HttpPost]
        public void RemoveProperty(int id)
        {
            propertyManager.Delete(id);
        }

        [HttpPost]
        public void UpdateProperty(int Property_Id, string Name, string Description, string Type, string Prefix,
            string Sufix, string DefaultValue, int Characteristic_Id, int Category_Id)
        {
            propertyManager.Update(Property_Id, Name, Description, Type, Prefix, Sufix, DefaultValue, Category_Id, Characteristic_Id);
            Response.Redirect("EditCategories");
        }

        public ActionResult EditUsers()
        {
            var Users = userManager.GetAll().Select(c => c).ToList();
            return View(Users);
        }
    }
}
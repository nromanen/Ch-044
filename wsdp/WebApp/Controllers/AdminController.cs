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

        public AdminController(ICategoryManager categoryManager, IPropertyManager propertyManager)
        {
            this.categoryManager = categoryManager;
            this.propertyManager = propertyManager;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditCategories()
        {
            List<CategoryDTO> categories =
                categoryManager.GetAll().Where(c => c.ParentCategoryId == null).Select(c => c).ToList();
            List<PropertyDTO> properties = propertyManager.GetAll().Select(c => c).ToList();
            PropertyViewDTO CategoriesView = new PropertyViewDTO() { categories = categories, properties = properties };

            return View(CategoriesView);
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
        public ActionResult AddProperty()
        {
            List<CategoryDTO> categories =
                categoryManager.GetAll().Select(c => c).ToList();
            List<string> enums = new List<string>();
            foreach (var i in Enum.GetNames(typeof(PropertyType)))
                enums.Add(i);
            PropertyViewDTO custom_model = new PropertyViewDTO() { enums = enums, categories = categories };
            return View(custom_model);
        }
        public ActionResult UpdateProperty()
        {
            List<PropertyDTO> properties = propertyManager.GetAll().Select(c => c).ToList();
            List<CategoryDTO> categories =
             categoryManager.GetAll().Select(c => c).ToList();
            List<string> enums = new List<string>();
            foreach (var i in Enum.GetNames(typeof(PropertyType)))
                enums.Add(i);

            PropertyViewDTO custom_model = new PropertyViewDTO() { enums = enums, categories = categories, properties = properties };
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
    }
}
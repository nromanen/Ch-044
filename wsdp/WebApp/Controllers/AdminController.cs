using BAL.Interface;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            List<CategoryDTO> categories = categoryManager.GetAll().Where(c => c.ParentCategoryId == null).Select(c => c).ToList();
            return View(categories);
        }

        [HttpPost]
        public void AddCategory(string namecategory, int? parentcategory)
        {
            categoryManager.Add(namecategory, parentcategory ?? -1);
            Response.Redirect("EditCategories");
        }

        [HttpPost]
        public void UpdateCategory(string namecategory, int id)
        {
            categoryManager.Rename(id, namecategory);
            Response.Redirect("EditCategories");
        }

        [HttpPost]
        public void RemoveCategory(int id)
        {
            categoryManager.Delete(id);
            Response.Redirect("EditCategories");
        }

        public ActionResult EditProperties()
        {
            List<CategoryDTO> categories = categoryManager.GetAll().Select(p => p).ToList();
            return View(categories);
        }
        [HttpPost]
        public void AddProperty(string Name, string Description, string Type, string Prefix, string Sufix, int Characteristic_Id, int Category_Id, string DefaultValue)
        {
            propertyManager.Add(Name, Description, Type, Prefix, Sufix, Characteristic_Id, Category_Id, DefaultValue);
            Response.Redirect("EditProperties");
        }

    }
}
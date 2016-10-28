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
        public AdminController(ICategoryManager categoryManager)
        {
            this.categoryManager = categoryManager;
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

        [HttpPost]
        public void ChangeParent(int categoryid, int? parentid)
        {
            categoryManager.ChangeParent(categoryid, parentid ?? -1);
        }
    }
}
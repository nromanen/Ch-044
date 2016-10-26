using BAL.Interface;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class AdminController : Controller
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
            List<CategoryDTO> categories = categoryManager.GetAll();
            return View(categories);
        }
    }
}
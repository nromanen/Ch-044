using BAL.Interface;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class UniversalParserController : BaseController
    {
        private IDownloadManager downloadManager;
        private ICategoryManager categoryManager;
        private IWebShopManager shopManager;
        public UniversalParserController(IDownloadManager downloadManager, ICategoryManager categoryManager, IWebShopManager shopManager)
        {
            this.downloadManager = downloadManager;
            this.categoryManager = categoryManager;
            this.shopManager = shopManager;
        }

        // GET: Settings
        [HttpGet]
        public ActionResult Settings()
        {
            SettingsViewDTO settingsView = new SettingsViewDTO()
            {
                Categories = categoryManager.GetAll(),
                Shops = shopManager.GetAll().ToList()
            };
            return View(settingsView);
        }

        [HttpPost]
        public ActionResult Settings(string str)
        {
            return View();
        }

        //GET:Iterator
        [HttpGet]
        public ActionResult Iterator(int id)
        {
            ViewBag.Path = TempData["Path"];
            return View();
        }

        //POST:Download/url
        [HttpPost]
        public ActionResult Iterator(string url)
        {
            string pathToSite;
            if (!String.IsNullOrWhiteSpace(url))
            {
                Guid res = downloadManager.DownloadFromPath(url);
                pathToSite = "/WebSites/" + res + ".html";
                TempData["Path"] = pathToSite;
            }
            else
            {
                ViewBag.PathIsExist = false;
                return RedirectToAction("Iterator");
            }

            return RedirectToAction("Iterator");
        }
        [HttpGet]
        public ActionResult Grabber()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Grabber(string str)
        {
            return View();
        }
    }
}
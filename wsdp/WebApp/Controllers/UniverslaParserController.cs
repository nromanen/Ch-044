using BAL.Interface;
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
        public UniversalParserController(IDownloadManager downloadManager)
        {
            this.downloadManager = downloadManager;
        }

        // GET: Settings
        [HttpGet]
        public ActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Settings(string str)
        {
            return View();
        }

        //GET:Iterator
        [HttpGet]
        public ActionResult Iterator()
        {
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
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
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
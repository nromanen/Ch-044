using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class DownloadPageController : BaseController
    {
        private IDownloadManager downloadManager;
        public DownloadPageController(IDownloadManager downloadManager)
        {
            this.downloadManager = downloadManager;
        }

        // GET: DownloadPage
        public ActionResult Index()
        {
            ViewBag.Path = TempData["path"];
            return View();
        }
        //POST:Download/url
        [HttpPost]
        public ActionResult Download(string url)
        {
            if (!String.IsNullOrWhiteSpace(url))
            {
                Guid res = downloadManager.DownloadFromPath(url);
                string pathToSite = "/WebSites/" + res + ".html";
                TempData["path"] = pathToSite;
            }
            else
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
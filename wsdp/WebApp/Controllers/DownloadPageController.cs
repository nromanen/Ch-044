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
            return View();
        }
        //POST:Download
        [HttpPost]
        public ActionResult Download(string url)
        {
            downloadManager.DownloadFromPath(url);
            return View("Index");
        }
    }
}
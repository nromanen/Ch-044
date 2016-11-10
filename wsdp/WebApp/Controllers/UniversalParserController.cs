using BAL.Interface;
using Model.DTO;
using System;
using System.Linq;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    [Authorize]
    public class UniversalParserController : BaseController
    {
        private IDownloadManager downloadManager;
        private ICategoryManager categoryManager;
        private IWebShopManager shopManager;
        private IParserTaskManager parserManager;

        public UniversalParserController(IDownloadManager downloadManager, ICategoryManager categoryManager, IWebShopManager shopManager, IParserTaskManager parserManager)
        {
            this.downloadManager = downloadManager;
            this.categoryManager = categoryManager;
            this.shopManager = shopManager;
            this.parserManager = parserManager;
        }

        // GET: Settings
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Settings()
        {
            SettingsViewDTO settingsView = new SettingsViewDTO()
            {
                Categories = categoryManager.GetAll().Where(c => c.ParentCategoryId == null).Select(c => c).ToList(),
                Shops = shopManager.GetAll().ToList()
            };
            return View(settingsView);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Settings(ParserTaskDTO parser)
        {
            parser.Status = "Not Finished";
            int newid = parserManager.Add(parser);
            //return RedirectToAction("Iterator", newid);
            return View();
        }

        //GET:Iterator
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Iterator(int id)
        {
            ViewBag.Path = TempData["Path"];
            return View();
        }

        //POST:Download/url
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Grabber()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Grabber(string str)
        {
            return View();
        }
    }
}
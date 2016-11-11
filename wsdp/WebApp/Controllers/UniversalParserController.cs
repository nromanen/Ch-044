using BAL.Interface;
using Model.DTO;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    [Authorize]
    public class UniversalParserController : BaseController
    {
        private IDownloadManager downloadManager;
        private ICategoryManager categoryManager;
        private IWebShopManager shopManager;
        private IParserTaskManager parsertaskManager;

        public UniversalParserController(IDownloadManager downloadManager, ICategoryManager categoryManager, IWebShopManager shopManager, IParserTaskManager parsertaskManager)
        {
            this.downloadManager = downloadManager;
            this.categoryManager = categoryManager;
            this.shopManager = shopManager;
            this.parsertaskManager = parsertaskManager;
        }

        // GET: Settings
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Settings(int? id)
        {
            ParserTaskDTO parsertask = null;
            if (id != null)
            {
                parsertask = parsertaskManager.Get(id ?? -1);
            }
            SettingsViewDTO settingsView = new SettingsViewDTO()
            {
                Categories = categoryManager.GetAll().Where(c => c.HasChildrenCategories == false).Select(c => c).ToList(),
                Shops = shopManager.GetAll().ToList()
            };
            if (parsertask != null)
            {
                settingsView.ParserTask = parsertask;
            }
            return View(settingsView);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Settings(ParserTaskDTO parsertask, int? parsertaskid)
        {
            int newid = -1;
            if (parsertaskid != null)
            {
                parsertask.Id = parsertaskid ?? -1;
                parsertaskManager.Update(parsertask);
            }
            else
            {
                parsertaskManager.Delete(4);
                parsertask.Status = "Not Finished";
                newid = parsertaskManager.Add(parsertask);
            }
            return RedirectToAction("Iterator", new { id = parsertaskid ?? newid });
        }

        //GET:UniverslaParser/Iterator/id?
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Iterator(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                ViewBag.Id = id;
            }

            ViewBag.Path = TempData["Path"];
            return View();
        }

        //POST:UniversalParser/url,id
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Download(string url, int? id)
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
                return RedirectToAction("Iterator", new { id = id.Value });
            }

            return RedirectToAction("Iterator", new { id = id.Value });
        }
        //POST:UniversalParser/IteratorConfigurations
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult IteratorConfigurations(int? id, string url, IteratorSettingsDTO view)
        {

            return RedirectToAction("Grabber", new { id = id.Value });
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Grabber(int? id)
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
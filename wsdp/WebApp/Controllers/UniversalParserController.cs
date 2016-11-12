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
                parsertask.Status = Common.Enum.Status.NotFinished;
                newid = parsertaskManager.Add(parsertask);
            }
            return RedirectToAction("Iterator", new { id = parsertaskid ?? newid });
        }

        //GET:UniverslaParser/Iterator/id?
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Iterator(int? id, string URL)
        {
            IteratorSettingsDTO iteratorViewModel = new IteratorSettingsDTO();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var task = parsertaskManager.Get(id.Value);

                if (task.IteratorSettings == null)
                {
                    task.IteratorSettings = new IteratorSettingsDTO();
                }
                if(iteratorViewModel.Url==null)
                {
                    iteratorViewModel.Url = URL;
                }
                iteratorViewModel = task.IteratorSettings;
                
                ViewBag.Id = id;
            }

            ViewBag.Path = TempData["Path"];

            return View(iteratorViewModel);
        }

        //POST:UniversalParser/url,id
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Download(string url, int? id)
        {
            string localPathToSite;
            if (!String.IsNullOrWhiteSpace(url))
            {
                Guid res = downloadManager.DownloadFromPath(url);
                localPathToSite = "/WebSites/" + res + ".html";
                TempData["Path"] = localPathToSite;

            }
            else
            {
                ViewBag.PathIsExist = false;
                return RedirectToAction("Iterator", new { id = id.Value });
            }

            return RedirectToAction("Iterator", new { id = id.Value, URL = url });
        }
        //POST:UniversalParser/IteratorConfigurations
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult IteratorConfigurations(int? id, IteratorSettingsDTO model)
        {
            var _task = parsertaskManager.Get(id.Value);
            _task.IteratorSettings = model;

            parsertaskManager.Update(_task);

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
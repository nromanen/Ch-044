using BAL.Interface;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
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
        public ActionResult Settings(string description, int categoryid, int shopid, string priority, DateTime datetime)
        {
            ParserTaskDTO parser = new ParserTaskDTO()
            {
                Description = description,
                CategoryId = categoryid,
                WebShopId = shopid,
                Priority = priority,
                Status = "Not Finished",
                EndDate = datetime
            };
            int newid = parserManager.Add(parser);
            int newid2 = parserManager.Add(parser);
            int newid3 = parserManager.Add(parser);
            int newid4 = parserManager.Add(parser);
            //return RedirectToAction("Iterator", newid);
            return View();
        }

        //GET:UniverslaParser/Iterator/id?
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
        [HttpPost]
        public ActionResult IteratorConfigurations(int? id, string url, IteratorSettingsDTO view)
        {

            return RedirectToAction("Grabber", new { id = id.Value });
        }

        [HttpGet]
        public ActionResult Grabber(int? id)
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
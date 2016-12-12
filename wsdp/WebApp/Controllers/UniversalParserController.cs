using BAL.Interface;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;

namespace WebApp.Controllers {
	[Authorize]
	public class UniversalParserController : BaseController {
		private IDownloadManager downloadManager;
		private ICategoryManager categoryManager;
		private IWebShopManager shopManager;
		private IParserTaskManager parserTaskManager;
        private IURLManager urlManager;
		private IPreviewManager previewManager;
        private IExecuteManager taskinfoManager;
        private IAppSettingsManager appSettingsManager;
        private IDeleteFilesManager deleteFilesManager;

        public UniversalParserController(IDownloadManager downloadManager, ICategoryManager categoryManager, IWebShopManager shopManager, IParserTaskManager parsertaskManager, IURLManager urlManager,IExecuteManager taskinfoManager, IPreviewManager previewManager, IAppSettingsManager appSettingsManager, IDeleteFilesManager deleteFilesManager) {
			this.downloadManager = downloadManager;
			this.categoryManager = categoryManager;
			this.shopManager = shopManager;
			this.parserTaskManager = parsertaskManager;
            this.urlManager = urlManager;
			this.previewManager = previewManager;
            this.taskinfoManager = taskinfoManager;
            this.appSettingsManager = appSettingsManager;
            this.deleteFilesManager = deleteFilesManager;
		}
		// GET: Index - list of all parser tasks
		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ActionResult Index() {
			List<ParserTaskDTO> parsertasks = parserTaskManager.GetAll();
			foreach (var item in parsertasks) {
				item.ExecuteInfoCount = taskinfoManager.GetAll().Where(t => t.ParserTaskId == item.Id).Count();
			}

			return View(parsertasks);
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ActionResult GetInfoByTask(int id) {
			List<ExecutingInfoDTO> executingInfos = taskinfoManager.GetAll().Where(t => t.ParserTaskId == id).ToList();
			return View(executingInfos);
		}

		// GET: Settings
		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ActionResult Settings(int? id) {
			ParserTaskDTO parsertask = null;
			SettingsViewDTO settingsView = new SettingsViewDTO() {
				Categories = categoryManager.GetAll().Where(c => c.HasChildrenCategories == false).Select(c => c).ToList(),
				Shops = shopManager.GetAll().ToList()
			};

			if (id != null) {
				parsertask = parserTaskManager.Get(id.GetValueOrDefault());
			}

			if (parsertask != null) {
				settingsView.ParserTask = parsertask;
			}
			return View(settingsView);
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public ActionResult Settings(ParserTaskDTO parsertask, int? parsertaskid) {
			int newid = -1;
			if (parsertaskid != null) {
				parsertask.Id = parsertaskid ?? -1;
				parserTaskManager.Update(parsertask);
			} else {
				parsertask.Status = Common.Enum.Status.NotFinished;
				newid = parserTaskManager.Add(parsertask);
			}
			return RedirectToAction("Iterator", new { id = parsertaskid ?? newid });
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public ActionResult Remove(int id) {
			parserTaskManager.Delete(id);
			return Redirect("Index");
		}

		//GET:UniverslaParser/Iterator/id?
		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ActionResult Iterator(int? id, string URL) {
			IteratorSettingsDTO iteratorViewModel = new IteratorSettingsDTO();
			if (id == null) {
				return HttpNotFound();
			} else {
				var task = parserTaskManager.Get(id.Value);

				if (task.IteratorSettings == null) {
					task.IteratorSettings = new IteratorSettingsDTO();
                    task.IteratorSettings.GoodsIteratorXpathes = new List<string>();
                    task.IteratorSettings.GoodsIteratorXpathes.Add("");
				}
				if (iteratorViewModel.Url == null) {
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
		public ActionResult Download(string url, int? id) {
			string localPathToSite;
			if (!String.IsNullOrWhiteSpace(url)) {
				Guid res = downloadManager.DownloadFromPath(url);
				localPathToSite = "/WebSites/" + res + ".html";
				TempData["Path"] = localPathToSite;

			} else {
				ViewBag.PathIsExist = false;
				return RedirectToAction("Iterator", new { id = id.Value });
			}

			return RedirectToAction("Iterator", new { id = id.Value, URL = url });
		}
		//POST:UniversalParser/IteratorConfigurations
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public ActionResult IteratorConfigurations(int? id, IteratorSettingsDTO model) {
			var _task = parserTaskManager.Get(id.Value);
			_task.IteratorSettings = model;
			parserTaskManager.Update(_task);

			return RedirectToAction("Grabber", new { id = id.Value });
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ActionResult Grabber(int? id) {
			if(id == null) { return HttpNotFound(); }
			var task = new ParserTaskDTO();
			var grabber = new GrabberSettingsDTO();
			var urlList = new List<string>();
			string localPathToSite;
			int index = 0;
			if (id != null) {
				task = parserTaskManager.Get(id.Value);

				if (task.GrabberSettings != null) {
					grabber = task.GrabberSettings;
                    
				} else {
					grabber.Id = id.Value;
                    task.Category = categoryManager.Get(task.CategoryId);
					grabber.PropertyItems = Mapper.Map<List<GrabberPropertyItemDTO>>(task.Category.PropertiesList);
                    
				}
				if(task.IteratorSettings != null) {
					urlList = urlManager.GetAllUrls(task.IteratorSettings);
				}
			}
			var arrayOfLinks  = urlList.ToArray();
			var jsonArray = urlList.ToArray();
			for (index = 0; index < 2; index++) {
				if (!String.IsNullOrWhiteSpace(arrayOfLinks[index])) {
					Guid result = downloadManager.DownloadFromPath(arrayOfLinks[index]);
					localPathToSite = "/WebSites/" + result + ".html";
					arrayOfLinks[index] = localPathToSite;
				}
			}
			grabber.urlJsonData = JsonConvert.SerializeObject(jsonArray);
			Session["Length"] = arrayOfLinks.Length;
			TempData["CurrentPage"] = arrayOfLinks[0];
			TempData["NextPage"] = arrayOfLinks[1];
			TempData["AllSrc"] = arrayOfLinks;
			return View(grabber);
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public string Next(int current) {
			string localPathToSite;
			var arrayOfLinks = TempData.Peek("AllSrc") as string[];
			var length = arrayOfLinks.Length;

			if (current < length-1) {
				
					if (!String.IsNullOrWhiteSpace(arrayOfLinks[current+1]) && arrayOfLinks[current+1].Contains("http")) {
						Guid result = downloadManager.DownloadFromPath(arrayOfLinks[current+1]);
						localPathToSite = "/WebSites/" + result + ".html";
						arrayOfLinks[current+1] = localPathToSite;
					}
			}
			TempData["AllSrc"] = arrayOfLinks;
			return arrayOfLinks[current];
		}
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public string Previous(int current) {
			var arrayOfLinks = TempData.Peek("AllSrc") as string[];
			return arrayOfLinks[current];
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public ActionResult Grabber(int? id, GrabberSettingsDTO grabber) {
			var _task = parserTaskManager.Get(id.Value);
			_task.GrabberSettings = grabber;

			parserTaskManager.Update(_task);
            deleteFilesManager.DeleteFiles();
			return RedirectToAction("Index", "UniversalParser");
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public string GetPreview(string url, string xpath) {
			var a = previewManager.GetPreviewForOneInput(url, xpath);
			return a;
		}

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult ExecutingTasks()
        {
            List<ExecutingInfoDTO> taskinfoes = taskinfoManager.GetAll().Where(c => c.Status == Common.Enum.ExecuteStatus.Executing).ToList();

            return View(taskinfoes);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult GetExecutingInfo()
        {
            List<ExecutingInfoDTO> taskinfoes = taskinfoManager.GetAll().Where(c => c.Status == Common.Enum.ExecuteStatus.Executing && (DateTime.Now - c.Date < TimeSpan.FromMinutes(5))).ToList();

            return Json(new { success = true, taskinfoes = taskinfoes.ToArray() },
             JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult ConfigureIntervals(int pull, int push)
        {
            var model = new AppSettingsDTO()
            {
                PullInterval = pull,
                PushInterval = push
            };
            appSettingsManager.Update(model);

            return Json(new object(), JsonRequestBehavior.AllowGet);
        }
    }
}
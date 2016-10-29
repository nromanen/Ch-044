using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL.Interface;
using Model.DTO;

namespace WebApp.Controllers
{
    public class MicrowaveController : Controller
    {
		// GET: Microwave
		private IMicrowaveManager MicrowaveManager;
		private IMicrowaveParseManager MicrowaveParseManager;
		// GET: TV
		public MicrowaveController(IMicrowaveManager MicrowaveManager, IMicrowaveParseManager MicrowaveParseManager) {
			this.MicrowaveParseManager = MicrowaveParseManager;
			this.MicrowaveManager = MicrowaveManager;
		}

		public ActionResult Index() {
			List<MicrowaveDTO> Microwaves = MicrowaveManager.GetAll();
			return View(Microwaves);
		}

		public ActionResult GetMicrowave(int id) {
			var wave = MicrowaveManager.GetById(id);
			return View(wave);
		}

		public ActionResult Load() {
				MicrowaveParseManager.GetAllWaves("http://allo.ua/ru/products/mikrovolnovki/");
			return RedirectToAction("Index");
		}
	}
}
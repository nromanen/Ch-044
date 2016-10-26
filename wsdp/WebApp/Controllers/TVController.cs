using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL.Interface;
using Model.DTO;

namespace WebApp.Controllers
{
    public class TVController : Controller
    {
        private ITVManager TVManager;
        private ITVParseManager TVParseManager;
        // GET: TV
        public TVController(ITVManager TVManager, ITVParseManager TVParseManager)
        {
            this.TVParseManager = TVParseManager;
            this.TVManager = TVManager;
        }
        public ActionResult Index()
        {
            List<TVDTO> TVs = TVManager.GetAllTVs();
            return View(TVs);
        }

        public ActionResult GetTV(int id)
        {
            var tv = TVManager.GetTVById(id);
            return View(tv);
        }

        public ActionResult Load()
        {
            TVParseManager.ParseCategory(@"https://repka.ua/products/televizori/");
            return View();
        }
    }
}
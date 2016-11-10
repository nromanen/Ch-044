using BAL.Interface;
using Model.DTO;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    [Authorize]
    public class TVController : BaseController
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
        [Authorize(Roles = "Administrator")]
        public ActionResult Load()
        {
            TVParseManager.ParseCategory(@"https://repka.ua/products/televizori/");
            return RedirectToAction("Index");
        }
    }
}
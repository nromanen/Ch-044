using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class FridgeController : BaseController
    {
        private IFridgeManager fridgeManager;
        private IFridgeParseManager fridgeParseManager;

        public FridgeController(IFridgeManager fridgeManager, IFridgeParseManager fridgeParseManager)
        {
            this.fridgeManager = fridgeManager;
            this.fridgeParseManager = fridgeParseManager;
        }
        // GET: Fridge
        public ActionResult Index()
        {
            var fridges = fridgeManager.GetAll();
             

            //return listFridges
            return View(fridges);
        }
        //GET: Fridge/Id
        public ActionResult ConcreteFridge(int id)
        {
            var fridge = fridgeManager.GetFridgeById(id);
            return View(fridge);
        }
        public ActionResult Load()
        {
            fridgeParseManager.GetConcreteGoodsFromCategory(@"http://tehnotrade.com.ua/holod/");
            return RedirectToAction("Index");
        }
    }
}
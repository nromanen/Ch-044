using BAL.Interface;
using Model.Product;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    [Authorize]
    public class LaptopController : BaseController
    {
        private ILaptopManager LaptopManager { get; }
        private ILaptopParseManager LaptopParseManager { get; }

        public LaptopController(ILaptopManager laptopManager, ILaptopParseManager laptopParseManager)
        {
            LaptopManager = laptopManager;
            LaptopParseManager = laptopParseManager;
        }

        // GET: Laptop
        public ActionResult Index()
        {
            IEnumerable<Laptop> laptops = LaptopManager.GetAll().Where(x => x.ImgPath != null);
            return View(laptops);
        }

        public ActionResult OneLaptop(int? id)
        {
            int id1 = id ?? 0;
            Laptop laptop = LaptopManager.GetById(id1) ?? new Laptop();
            return View(laptop);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult ParseOnBase()
        {
            LaptopParseManager.ParseAll("http://www.ttt.ua/shop/category/noutbuki-pk-i-orgtehnika/noutbuki");
            return View("Index");
        }
    }
}
using BAL.Interface;
using Model.Product;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class LaptopController : BaseController
    {
        private ILaptopManager _laptopManager;
        private ILaptopParseManager _laptopParseManager;

        public LaptopController(ILaptopManager laptopManager, ILaptopParseManager laptopParseManager)
        {
            _laptopManager = laptopManager;
            _laptopParseManager = laptopParseManager;
        }

        // GET: Laptop
        public ActionResult Index()
        {
            IEnumerable<Laptop> laptops = _laptopManager.GetAll().Where(x => x.ImgPath != null);
            return View(laptops);
        }

        public ActionResult OneLaptop(int? id)
        {
            int id1 = id ?? 0;
            Laptop laptop = _laptopManager.GetById(id1) ?? new Laptop();
            return View(laptop);
        }

        public ActionResult ParseOnBase()
        {
            _laptopParseManager.ParseAll("http://www.ttt.ua/shop/category/noutbuki-pk-i-orgtehnika/noutbuki");
            return View("Index");
        }
    }
}
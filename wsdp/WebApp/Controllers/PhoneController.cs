using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using BAL.Manager.ParseManagers;
using BAL.Interface;
using Model.DTO;

namespace WebApp.Controllers
{
    public class PhoneController : Controller
    {
        private IPhoneManager phoneManager;

        public PhoneController(IPhoneManager phoneManager)
        {
            this.phoneManager = phoneManager;
        }
        // GET: Phone
        public ActionResult Index()
        {
            List<PhoneSimpleDTO> phones = phoneManager.GetAllPhones();
            
             
            return View(phones);
        }

        public ActionResult Load()
        {
            phoneManager.ParseGoodsFromCategory(@"http://www.moyo.ua/telecommunication/smart/");
            return View();
        }
    }
}
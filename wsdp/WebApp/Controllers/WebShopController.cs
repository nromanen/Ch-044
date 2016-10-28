using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BAL.Interface;
using Model.DB;

namespace WebApp.Controllers
{
    public class WebShopController : BaseController
    {
        private IWebShopManager _webShopManager;
        public WebShopController(IWebShopManager webShopManager)
        {
                _webShopManager = webShopManager;
        }
            
        public ActionResult Index()
        {
            IEnumerable<WebShop> webShopsList = _webShopManager.GetAll();
            return View(webShopsList);
        }

        //public ActionResult Edit(short? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    WebShop webShop = _webShopManager.GetById((int)id);
        //    if (webShop == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(webShop);
        //}
        public ActionResult Create()
        {
            return View(new WebShop());
        }

        [HttpPost]
        public ActionResult Create(WebShop webShop, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    webShop.LogoPath = "/Content/WebShopsLogo/" + fileName;
                    upload.SaveAs(Server.MapPath(webShop.LogoPath));
                }
                //else
                //{
                //    webShop.LogoPath = "/Content/WebShopsLogo/webshop.png";
                //}
                _webShopManager.Insert(webShop);
                }
                return View(webShop);
        }

        public ActionResult Upload()
        {

            return RedirectToAction("Index");
        }


    }
}


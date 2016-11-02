using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BAL.Interface;

using Model.DTO;

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
            IEnumerable<WebShopDTO> webShopsList = _webShopManager.GetAll().Where(x=>x.Status);
            return View(webShopsList);
        }

        public ActionResult Create()
        {
            return View(new WebShopDTO());
        }
        [HttpPost]
        public ActionResult Create(WebShopDTO webShop, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string fileName = String.Format("IMG_{0}_{1}.jpg",
                        DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                        Guid.NewGuid());

                    webShop.LogoPath = fileName;
                    upload.SaveAs(Server.MapPath("/Content/WebShopsLogo/" + webShop.LogoPath));
                }
                _webShopManager.Insert(webShop);
                return RedirectToAction("Index");
            }
            return View(webShop);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return HttpNotFound();
            WebShopDTO webShop = _webShopManager.GetById((int)id);
            if (webShop != null)
                return PartialView(webShop);
            return HttpNotFound();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            WebShopDTO wShop = _webShopManager.GetById(id);
            _webShopManager.Delete(wShop);
            return RedirectToAction("Index");
        }

        //public ActionResult Edit(short? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    webShop = _webShopManager.GetById((int)id);
        //    if (webShop == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(webShop);
        //}



    }
}


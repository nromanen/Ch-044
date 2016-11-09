using BAL.Interface;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class WebShopController : BaseController
    {
        /// <summary>
        /// The variable gives the acces to DB for WebShopSTO
        /// </summary>
        private IWebShopManager _webShopManager;

        public WebShopController(IWebShopManager webShopManager)
        {
            _webShopManager = webShopManager;
        }

        /// <summary>
        /// To show a View with all WebShops in the DB
        /// </summary>
        public ActionResult Index()
        {
            IEnumerable<WebShopDTO> webShopsList = _webShopManager.GetAll();
            return View(webShopsList);
        }

        /// <summary>
        /// To show create view for WebShopDTO
        /// </summary>
        public ActionResult Create()
        {
            return View(new WebShopDTO());
        }

        /// <summary>
        /// To create new WebShop and insert into DB and redirest to all WebShopDTOs
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WebShopDTO webShop, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid) return View(webShop);

            if (upload != null)
            {
                webShop.LogoPath = CreateImgName();
                upload.SaveAs(Server.MapPath("/Content/WebShopsLogo/" + webShop.LogoPath));
            }
            _webShopManager.Insert(webShop);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// To show delete view
        /// </summary>
        public ActionResult Delete(int? id)
        {
            if (id == null) return HttpNotFound();
            WebShopDTO webShop = _webShopManager.GetById((int)id);
            if (webShop != null)
                return PartialView(webShop);
            return HttpNotFound();
        }

        /// <summary>
        /// To delete WebShop in the DB and redirect to all WebShopDTOs
        /// </summary>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            WebShopDTO wShop = _webShopManager.GetById(id);
            _webShopManager.Delete(wShop);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// To show edit view for WebShopDTO
        /// </summary>
        public ActionResult Edit(short? id)
        {
            if (id == null) return HttpNotFound();

            WebShopDTO webShop = _webShopManager.GetById((int)id);
            if (webShop == null) return HttpNotFound();

            return View(webShop);
        }

        /// <summary>
        /// To edit WebShop in the DB and redirect to all WebShopDTOs
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WebShopDTO webShop, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid) return View(webShop);
            if (upload != null)
            {
                webShop.LogoPath = CreateImgName();
                upload.SaveAs(Server.MapPath("/Content/WebShopsLogo/" + webShop.LogoPath));
            }
            _webShopManager.Update(webShop);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// To create an unique image's name
        /// </summary>
        private string CreateImgName()
        {
            return String.Format("IMG_{0}_{1}.jpg",
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Guid.NewGuid());
        }
    }
}
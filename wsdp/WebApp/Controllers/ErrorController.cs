using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace WebApp.Controllers
{
    public class ErrorController : Controller
    {

        private static readonly ILog Logger = LogManager.GetLogger("RollingLogFileAppender");
        // GET: Error

        public ActionResult NotFound()
        {
            Logger.Error(HttpContext.ToString());
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult InternalServer()
        {
            Logger.Error(HttpContext.ToString());
            Response.StatusCode = 500;
            return View();
        }
		public ActionResult CategoryNotFound()
		{
			return View();
		}
    }
}
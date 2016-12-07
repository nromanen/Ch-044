using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL.Interface;
using BAL.Manager;

namespace WebApp.Controllers
{
    public class ExecutingInfoController : BaseController
    {
        public IExecuteManager ExecuteManager { get; }

        public ExecutingInfoController(IExecuteManager executeManager)
        {
            ExecuteManager = executeManager;
        }
        // GET: ExecutingInfo
        public ActionResult Index()
        {
            return View(ExecuteManager.GetAll());
        }
    }
}
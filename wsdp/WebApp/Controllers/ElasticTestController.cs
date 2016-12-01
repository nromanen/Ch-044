using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL.Interface;
using BAL.Manager;
using Model.DB;
using Model.DTO;

namespace WebApp.Controllers
{
    public class ElasticTestController : BaseController
    {
        private IGoodDatabasesWizard Wizard { get; }

        public ElasticTestController(IGoodDatabasesWizard wizard)
        {
            Wizard = wizard;
        }

        // GET: ElasticTest
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Add()
        {
            

            return View();
        }
    }
}
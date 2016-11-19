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
            PropertyValuesDTO property = new PropertyValuesDTO()
            {
                DictDoubleProperties = new Dictionary<int, double>(),
                DictStringProperties = new Dictionary<int, string>(),
                DictIntProperties = new Dictionary<int, int>()
            };

            property.DictDoubleProperties.Add(1, 12.3);
            property.DictDoubleProperties.Add(2, 3.2);
            property.DictIntProperties.Add(3, 2);
            property.DictStringProperties.Add(2, "asdasd");

            GoodDTO good = new GoodDTO()
            {
                Category_Id = 1,
                Id = 2,
                PropertyValues = property,
                WebShop_Id = 2
            };
            Wizard.AddItem(good);

            return View();
        }
    }
}
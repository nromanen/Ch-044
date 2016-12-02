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
            var propetry = new PropertyValuesDTO()
            {
                DictDoubleProperties = new Dictionary<int, double>(),
                DictStringProperties = new Dictionary<int, string>(),
                DictIntProperties = new Dictionary<int, int>()
            };

            propetry.DictDoubleProperties.Add(2,2.333);

            var good = new GoodDTO()
            {
                WebShop_Id = 1,
                Category_Id = 1,
                ImgLink = "https://www.elastic.co",
                UrlLink = "https://www.elastic.co",
                Name = "Product",
                NewPrice = 1200,
                OldPrice = 12000,
                Price = 12,
                PropertyValues = propetry
            };

            Wizard.Insert(good);

            return View();
        }
    }
}
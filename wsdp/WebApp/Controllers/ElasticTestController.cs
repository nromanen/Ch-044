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
        private IElasticManager Elastic { get; }

        public ElasticTestController(IGoodDatabasesWizard wizard, IElasticManager elastic)
        {
            Elastic = elastic;
            Wizard = wizard;
        }

        // GET: ElasticTest
        public ActionResult Index()
        {

            var item = Elastic.GetByWebShopId(1);
            return View(item);
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

            var good1 = new GoodDTO()
            {
                WebShop_Id = 2,
                Category_Id = 1,
                UrlLink = "https://www.elastic2333.co",
                Name = "Okla",
                OldPrice = 12000,
                Price = 1,
                PropertyValues = propetry
            };
            
            Wizard.InsertOrUpdate(good1);
            

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using DAL.Elastic.Interface;
using Model.DB;
using Model.DTO;

namespace BAL.Manager
{
    public class GoodDatabasesWizard : ElasticManager, IGoodDatabasesWizard
    {
        public GoodDatabasesWizard(IElasticUnitOfWork elasticUOW) : base(elasticUOW)
        {
        }

        public void AddItem(GoodDTO good)
        {
            elasticUOW.Repository.Insert(good);
        }

    }
}

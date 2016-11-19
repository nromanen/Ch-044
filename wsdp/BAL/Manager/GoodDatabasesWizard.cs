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
    public class GoodDatabasesWizard : IGoodDatabasesWizard
    {
        private IElasticManager elasticManager;

        public GoodDatabasesWizard(IElasticManager elasticManager)
        {
            this.elasticManager = elasticManager;
        }

        public void AddItem(GoodDTO good)
        {
            elasticManager.AddItem(good);
            
        }

    }
}

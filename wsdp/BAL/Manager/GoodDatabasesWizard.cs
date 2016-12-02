using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Interface;
using DAL.Elastic.Interface;
using Model.DB;
using Model.DTO;
using ExtendedXmlSerialization; 

namespace BAL.Manager
{
    public class GoodDatabasesWizard : IGoodDatabasesWizard
    {
        private IElasticManager elasticManager;
        private IGoodManager goodManager;

        public GoodDatabasesWizard(IElasticManager elasticManager, IGoodManager goodManager)
        {
            this.elasticManager = elasticManager;
            this.goodManager = goodManager;
        }

      



    }
}

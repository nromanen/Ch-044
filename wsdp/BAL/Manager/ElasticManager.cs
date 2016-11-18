using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Elastic.Interface;

namespace BAL.Manager
{
    public class ElasticManager
    {
        protected IElasticUnitOfWork elasticUOW;

        public ElasticManager(IElasticUnitOfWork elasticUOW)
        {
            this.elasticUOW = elasticUOW;
        }
    }
}

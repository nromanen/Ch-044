using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using DAL.Elastic.Interface;

namespace BAL.Manager
{
    public class ElasticManager : IElasticManager
    {
        protected IElasticUnitOfWork elasticUOW;

        public ElasticManager(IElasticUnitOfWork elasticUOW)
        {
            this.elasticUOW = elasticUOW;
        }

        public void AddItem(object obj)
        {
            elasticUOW.Repository.Insert(obj);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Elastic.Interface;

namespace DAL.Elastic
{
    public class ElasticUnitOfWork : IElasticUnitOfWork
    {
        private ElasticContext context;

        private IElasticGoodRepository repository;

        public ElasticUnitOfWork()
        {
            context = new ElasticContext();
            repository = new ElasticGoodRepository(context);

        }

        public IElasticGoodRepository Repository
        {
            get { return repository ?? (repository = new ElasticGoodRepository(context)); }
        }
       
        public int Save()
        {
            return context.Save();
        }
    }
}

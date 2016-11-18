using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Elastic.Interface;
using DAL.Interface;

namespace DAL.Elastic
{
    public class ElasticUnitOfWork : IElasticUnitOfWork
    {
        private ElasticContext context;

        private IElasticRepository repository;

        public ElasticUnitOfWork()
        {
            context = new ElasticContext();
            repository = new ElasticRepository(context);

        }

        public IElasticRepository Repository
        {
            get { return repository ?? (repository = new ElasticRepository(context)); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Elastic
{
    class ElasticUnitOfWork
    {
        private ElasticContext context;

        public ElasticUnitOfWork()
        {
            context = new ElasticContext();
            
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Elastic.Interface;
using DAL.Interface;

namespace DAL.Elastic
{
    public class ElasticRepository : IElasticRepository
    {
        internal ElasticContext context;
      

        public ElasticRepository(ElasticContext context)
        {
            this.context = context;
        }

        //Add item into elastic database 
        public void Insert(object item)
        {
            context.Client.Index(item);
        }
    }
}

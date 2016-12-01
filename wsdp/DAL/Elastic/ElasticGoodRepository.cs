using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Elastic.Interface;
using Model.DTO;

namespace DAL.Elastic
{
    public class ElasticGoodRepository : IElasticGoodRepository
    {
        internal ElasticContext context;

        public ElasticGoodRepository(ElasticContext context)
        {
            this.context = context;
        }
        
        public void Insert(GoodDTO item)
        {
            context.AddOperation(item, CRUD.Insert);
        }

        public void Delete(GoodDTO item)
        {
            context.AddOperation(item,CRUD.Remove);
        }

        public void Update(GoodDTO item)
        {
            context.AddOperation(item,CRUD.Update);
        }

        public IList<GoodDTO> GetByUrlId(GoodDTO item)
        {
            return context.GetByIdUrl(item.UrlLink);
        } 

    }
}

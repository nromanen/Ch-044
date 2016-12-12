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
            context.AddOperation(item, CRUD.Remove);
        }

        public void Update(GoodDTO item)
        {
            context.AddOperation(item, CRUD.Update);
        }
       
        public GoodDTO GetByUrlId(string url)
        {
            return context
                .GetByIdUrl(url);

        }

        public IList<GoodDTO> GetAll()
        {
            return context.GetAll();
        }

        public IList<GoodDTO> GetByName(string name)
        {
            return context.GetByNameHard(name);
        }

        public IList<GoodDTO> GetExact(string value)
        {
            return context.GetExact(value);
        }
        public IList<GoodDTO> GetSimilar(string value)
        {
            return context.GetSimilar(value);
        }

        public IList<GoodDTO> GetByCategoryId(int id)
        {
            return context.GetByCategoryId(id);
        }

        public IList<GoodDTO> GetByWebShopId(int id)
        {
            return context.GetByWebShopId(id);
        }
    }
}

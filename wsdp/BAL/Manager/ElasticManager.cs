using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using DAL.Elastic.Interface;
using Model.DTO;

namespace BAL.Manager
{
    public class ElasticManager : IElasticManager
    {
        protected IElasticUnitOfWork elasticUOW;

        public ElasticManager(IElasticUnitOfWork elasticUOW)
        {
            this.elasticUOW = elasticUOW;
        }

        public void Insert(GoodDTO good)
        {
            elasticUOW.Repository.Insert(good);
            elasticUOW.Save();
        }

        public void Delete(GoodDTO good)
        {
            elasticUOW.Repository.Delete(good);
            elasticUOW.Save();
        }

        public void Update(GoodDTO good)
        {
            elasticUOW.Repository.Update(good);
            elasticUOW.Save();
        }

        public GoodDTO GetByUrl(string url)
        {
            return elasticUOW.Repository.GetByUrlId(url);
        }

        public IList<GoodDTO> GetAll()
        {
            return elasticUOW.Repository.GetAll();
        }
    }
}

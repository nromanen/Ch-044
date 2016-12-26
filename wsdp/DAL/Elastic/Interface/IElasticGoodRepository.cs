using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;

namespace DAL.Elastic.Interface
{
    public interface IElasticGoodRepository
    {
        GoodDTO GetByUrlId(string url);
        IList<GoodDTO> GetAll(); 
        void Update(GoodDTO item);
        void Delete(GoodDTO item);
        void Insert(GoodDTO item);
        IList<GoodDTO> GetByName(string name);
        IList<GoodDTO> GetExact(string value, int size = 500);
        IList<GoodDTO> GetSimilar(string value);
        IList<GoodDTO> GetByCategoryId(int id);
        IList<GoodDTO> GetByWebShopId(int id);
        IList<GoodDTO> GetByPrefix(string prefix, int size = 10);
    }
}

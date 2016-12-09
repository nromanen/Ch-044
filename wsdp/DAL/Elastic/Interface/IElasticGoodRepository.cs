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
        IList<GoodDTO> Get(string value);
        IList<GoodDTO> GetByCategoryId(string category);
    }
}

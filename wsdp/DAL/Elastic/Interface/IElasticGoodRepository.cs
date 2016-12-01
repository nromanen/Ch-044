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
        IList<GoodDTO> GetByUrlId(GoodDTO item);
        void Update(GoodDTO item);
        void Delete(GoodDTO item);
        void Insert(GoodDTO item);
    }
}

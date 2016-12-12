using System.Collections.Generic;
using Model.DTO;

namespace BAL.Interface
{
    public interface IElasticManager
    {
        IList<GoodDTO> GetAll();
        GoodDTO GetByUrl(string url);
        void Update(GoodDTO good);
        void Delete(GoodDTO good);
        void Insert(GoodDTO good);
        IList<GoodDTO> GetByName(string name);
        IList<GoodDTO> GetExact(string value);
        IList<GoodDTO> GetSimilar(string value);
        IList<GoodDTO> GetByCategoryId(int id);
        IList<GoodDTO> GetByWebShopId(int id);
    }
}

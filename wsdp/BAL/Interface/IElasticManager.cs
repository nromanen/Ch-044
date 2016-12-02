using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}

using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface ICheckGoodManager
    {
        List<GoodDTO> CheckGoodsFromOneCategory(int categoryid, int parsertaskid);
    }
}

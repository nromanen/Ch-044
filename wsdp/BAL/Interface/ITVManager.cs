using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;
using Model.Product;

namespace BAL.Interface
{
    public interface ITVManager
    {
        List<TVDTO> GetAllTVs();
        TV GetTVById(int id);
    }
}

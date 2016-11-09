using Model.DTO;
using Model.Product;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface ITVManager
    {
        List<TVDTO> GetAllTVs();

        TV GetTVById(int id);
    }
}
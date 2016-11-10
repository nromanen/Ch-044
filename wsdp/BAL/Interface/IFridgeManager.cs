using Model.DTO;
using Model.Product;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface IFridgeManager
    {
        Fridge GetFridgeById(int id);

        List<FridgeDTO> GetAll();
    }
}
using Model.DTO;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface IWebShopManager
    {
        void Insert(WebShopDTO webShop);

        void Update(WebShopDTO webShop);

        IEnumerable<WebShopDTO> GetAll();

        WebShopDTO GetById(int id);

        void Delete(WebShopDTO webShop);
    }
}
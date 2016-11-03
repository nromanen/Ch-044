using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DB;
using Model.DTO;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using DAL.Interface;
using Model.DB;

namespace BAL.Manager.ParseManagers
{
    public class WebShopManager : BaseManager, IWebShopManager
    {
        public WebShopManager(IUnitOfWork uOW) : base(uOW)
        {
        }

        public IEnumerable<WebShop> GetAll()
        {
            return uOW.WebShopRepo.All.ToList();
        }

        public WebShop GetById(int id)
        {
            return uOW.WebShopRepo.GetByID(id);
        }

        public void Insert(WebShop shop)
        {
            if (shop != null)
            {
                uOW.WebShopRepo.Insert(shop);
                uOW.Save();
            }

        }
    }
}

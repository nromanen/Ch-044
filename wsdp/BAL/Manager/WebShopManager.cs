using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using Model.DB;
using Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Manager.ParseManagers
{
    public class WebShopManager : BaseManager, IWebShopManager
    {
        public WebShopManager(IUnitOfWork uOW) : base(uOW)
        {
        }

        /// <summary>
        ///To get all WebShops from DB
        /// </summary>
        public IEnumerable<WebShopDTO> GetAll()
        {
            List<WebShopDTO> webShopDto = new List<WebShopDTO>();
            foreach (var webShop in uOW.WebShopRepo.All.Where(x => x.Status))
            {
                webShopDto.Add(Mapper.Map<WebShopDTO>(webShop));
            }
            return webShopDto;
        }

        /// <summary>
        ///To get one WebShop from DB
        /// </summary>
        public WebShopDTO GetById(int id)
        {
            WebShop webShop = uOW.WebShopRepo.GetByID(id);
            return webShop != null ? Mapper.Map<WebShopDTO>(webShop) : null;
        }

        /// <summary>
        ///To insert WebShop into the DB
        /// </summary>
        public void Insert(WebShopDTO webShop)
        {
            if (webShop == null) return;
            WebShop wShop = Mapper.Map<WebShop>(webShop);
            wShop.Status = true;
            uOW.WebShopRepo.Insert(wShop);
            uOW.Save();
        }

        /// <summary>
        /// To update one WebShop in the DB
        /// </summary>
        public void Update(WebShopDTO webShop)
        {
            if (webShop == null) return;
            WebShop wShop = uOW.WebShopRepo.GetByID(webShop.Id);
            if (wShop == null) return;

            wShop.Name = webShop.Name;
            //if LogoPath null it may be lead to data loss
            wShop.LogoPath = webShop.LogoPath ?? wShop.LogoPath;
            wShop.Path = webShop.Path;
            uOW.Save();
        }

        /// <summary>
        /// To delete one WebShop in the DB
        /// </summary>
        public void Delete(WebShopDTO webShop)
        {
            if (webShop == null) return;
            WebShop wShop = uOW.WebShopRepo.GetByID(webShop.Id);
            if (wShop == null) return;
            wShop.Status = false;
            uOW.Save();
        }
    }
}
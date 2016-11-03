using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using Model.DB;
using Model.DTO;

namespace BAL.Manager.ParseManagers
{
    public class WebShopManager : BaseManager, IWebShopManager
    {
        public WebShopManager(IUnitOfWork uOW) : base(uOW)
        {
        }

        public IEnumerable<WebShopDTO> GetAll()
        {
            List<WebShopDTO> webShopDto = new List<WebShopDTO>();
            foreach (var webShop in uOW.WebShopRepo.All.Where(x => x.Status))
            {
                webShopDto.Add(Mapper.Map<WebShopDTO>(webShop));
            }
            return webShopDto;
        }

        public WebShopDTO GetById(int id)
        {
            WebShop webShop = uOW.WebShopRepo.GetByID(id);
            return webShop != null ? Mapper.Map<WebShopDTO>(webShop) : null;
        }

        public void Insert(WebShopDTO webShop)
        {
            if (webShop == null) return;
            WebShop wShop = Mapper.Map<WebShop>(webShop);
            wShop.Status = true;
            uOW.WebShopRepo.Insert(wShop);
            uOW.Save();
        }

        public void Update(WebShopDTO webShop)
        {
            if (webShop == null) return;
            WebShop wShop = uOW.WebShopRepo.GetByID(webShop.Id);
            if (wShop == null) return;

            wShop.Name = webShop.Name;
            wShop.LogoPath = webShop.LogoPath;
            wShop.Path = webShop.Path;
            uOW.Save();
        }

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


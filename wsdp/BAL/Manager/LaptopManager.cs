using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using Common.Enum;
using DAL.Interface;
using ExtendedXmlSerialization;
using Model.DB;
using Model.Product;

namespace BAL.Manager
{
    public class LaptopManager : BaseManager, ILaptopManager
    {
        public LaptopManager(IUnitOfWork uOw) : base(uOw)
        {

        }

        public IEnumerable<Laptop> GetAll()
        {
            var laptopList = new List<Laptop>();
            ExtendedXmlSerializer serialiser = new ExtendedXmlSerializer();
            var goodList = uOW.GoodRepo.All.Where(x => x.Category == GoodCategory.Laptop);
            foreach (var good in goodList)
            {
                laptopList.Add(serialiser.Deserialize(good.XmlData, typeof(Laptop)) as Laptop);
                laptopList.Last().Id = good.Id;
            }

            return laptopList;
        }

        public Laptop GetById(int id)
        {
            Good good = null;
            ExtendedXmlSerializer ser = new ExtendedXmlSerializer();

            good = uOW.GoodRepo.GetByID(id);
            if (good == null) return null;

            var laptop = ser.Deserialize(good.XmlData, typeof(Laptop)) as Laptop;
            laptop.Id = id;
            //laptop.
            return laptop;
        }
    }
}

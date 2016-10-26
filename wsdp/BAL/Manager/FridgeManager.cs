using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;
using Model.Product;
using DAL.Interface;
using log4net;
using ExtendedXmlSerialization;
using AutoMapper;
using Model.DB;

namespace BAL.Manager
{
    public class FridgeManager : BaseManager, IFridgeManager
    {
        static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");

        public FridgeManager(IUnitOfWork uOW) : base(uOW)
        {

        }
        public List<FridgeDTO> GetAll()
        {
            List<FridgeDTO> fridges = new List<FridgeDTO>();
            ExtendedXmlSerializer ser = new ExtendedXmlSerializer();
            foreach (var fridgeDb in uOW.GoodRepo.All.Where(g => g.Category == Common.Enum.GoodCategory.Fridge))
            {
                var fridge = ser.Deserialize(fridgeDb.XmlData, typeof(Fridge)) as Fridge;
                fridge.Id = fridgeDb.Id;
                fridges.Add(Mapper.Map<FridgeDTO>(fridge));
            }
            return fridges;
        }

        public Fridge GetFridgeById(int id)
        {
            Good good = null;
            ExtendedXmlSerializer ser = new ExtendedXmlSerializer();
            try
            {
                var goods = uOW.GoodRepo.All.ToList();
                good = uOW.GoodRepo.All.Where(g => g.Id == id).First();
                if (good == null)
                    throw new Exception("Not Found");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            var fridge = ser.Deserialize(good.XmlData, typeof(Fridge)) as Fridge;
            fridge.Id = id;
            
            return fridge;
        }
    }
}

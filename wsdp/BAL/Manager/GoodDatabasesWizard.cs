using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Ajax;
using AutoMapper;
using BAL.Interface;
using DAL.Elastic.Interface;
using DAL.Interface;
using Model.DB;
using Model.DTO;
using ExtendedXmlSerialization;
using log4net;

namespace BAL.Manager
{
    public class GoodDatabasesWizard : IGoodDatabasesWizard
    {
        private static readonly ILog Logger = LogManager.GetLogger("RollingLogFileAppender");
        private IElasticUnitOfWork elasticUnitOfWork;
        private IUnitOfWork sqlUnitOfWork;

        public GoodDatabasesWizard(IElasticUnitOfWork elasticUnitOfWork, IUnitOfWork sqlUnitOfWork)
        {
            this.elasticUnitOfWork = elasticUnitOfWork;
            this.sqlUnitOfWork = sqlUnitOfWork;
        }
        public void Insert(GoodDTO good)
        {
            //configure good for sql database
            var goodDb = Mapper.Map<Good>(good);
            goodDb.Status = true;
            var res = sqlUnitOfWork.GoodRepo.Insert(goodDb);

            //elastic manipulation
            elasticUnitOfWork.Repository.Insert(good);

            try
            {
                if (sqlUnitOfWork.Save() < 1) throw new Exception("Item isn't added into MS SQL Server");
                elasticUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public bool Delete(GoodDTO good)
        {
            if (good == null) return false;
            var goodDb = sqlUnitOfWork.GoodRepo.GetByID(good.Id);
            if (goodDb == null) return false;
            goodDb.Status = false;

            elasticUnitOfWork.Repository.Delete(good);

            try
            {
                if (sqlUnitOfWork.Save() < 1) throw new Exception("Item isn't added into MS SQL Server");
                elasticUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
            return true;
        }

        public void Update(GoodDTO good)
        {
            if (good == null) return;
            var goodDb = sqlUnitOfWork.GoodRepo.GetByID(good.Id);
            if (goodDb == null) return;
            var uGood = Mapper.Map<Good>(good);
            goodDb.Name = uGood.Name;
            goodDb.Category_Id = uGood.Category_Id;
            goodDb.WebShop_Id = uGood.WebShop_Id;
            goodDb.ImgLink = uGood.ImgLink;
            goodDb.UrlLink = uGood.UrlLink;
            goodDb.XmlData = uGood.XmlData;
            goodDb.Price = uGood.Price;
            goodDb.Status = uGood.Status;

            elasticUnitOfWork.Repository.Update(good);

            try
            {
                if (sqlUnitOfWork.Save() < 1) throw new Exception("Item isn't added into MS SQL Server");
                elasticUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}

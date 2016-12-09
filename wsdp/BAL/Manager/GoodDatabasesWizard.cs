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

        public void InsertOrUpdate(GoodDTO good)
        {
            if (good.ImgLink == null) good.ImgLink = @"http://www.kalahandi.info/wp-content/uploads/2016/05/sorry-image-not-available.png";
            var goodDb = Mapper.Map<Good>(good);
            goodDb.Status = true;
            var request = sqlUnitOfWork.GoodRepo.All.FirstOrDefault(x => x.UrlLink == goodDb.UrlLink);
            if (request != null)
            {
                good.Id = request.Id;
                Update(good);
            }
            else
            {
                var res = sqlUnitOfWork.GoodRepo.Insert(goodDb);

                try
                {
                    if (sqlUnitOfWork.Save() < 1) throw new Exception("Item isn't added into MS SQL Server");
                    var elasticGood = Mapper.Map<GoodDTO>(res);
                    //elastic manipulation
                    elasticUnitOfWork.Repository.Insert(elasticGood);
                    elasticUnitOfWork.Save();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                }
            }
        }

        public bool Delete(GoodDTO good)
        {
            if (good == null) return false;
            var goodDb = sqlUnitOfWork.GoodRepo.GetByID(good.Id);
            if (goodDb == null) return false;
            goodDb.Status = false;

            try
            {
                var elasticGood = Mapper.Map<GoodDTO>(goodDb);
                elasticUnitOfWork.Repository.Delete(elasticGood);
           
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
            try
            {
                var elasticGood = Mapper.Map<GoodDTO>(goodDb);
                elasticUnitOfWork.Repository.Update(elasticGood);

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

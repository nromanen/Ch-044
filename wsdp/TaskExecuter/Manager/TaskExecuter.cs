using BAL;
using BAL.Interface;
using BAL.Manager;
using DAL;
using Common;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskExecuting.Interface;
using Common.Enum;
using SiteProcessor;

namespace TaskExecuting.Manager
{
    public class TaskExecuter : ITaskExecuter
    {
        private UnitOfWork uOw = null;
        private ParserTaskManager parsermanager = null;
        private PropertyManager propmanager = null;


        /// <summary>
        /// Initializating managers and uOw
        /// </summary>
        public TaskExecuter()
        {
            UnitOfWork uOw = new UnitOfWork();
            parsermanager = new ParserTaskManager(uOw);
            propmanager = new PropertyManager(uOw);

            AutoMapperConfig.Configure();
        }

        /// <summary>
        /// Parses input url by configuration from parser task
        /// </summary>
        /// <param name="parsertaskid">id of parser task</param>
        /// <param name="url">url to parse</param>
        /// <returns>New parsed GoodDTO</returns>
        public GoodDTO ExecuteTask(int parsertaskid, string url)
        {
            //downloading page source using tor+phantomjs
            SiteDownloader sw = new SiteDownloader();
            string pageSource = sw.GetPageSouce(url);

            //gets configuration from parsertask id
            ParserTaskDTO parsertask = parsermanager.Get(parsertaskid);
            GrabberSettingsDTO grabbersettings = parsertask.GrabberSettings;

            GoodDTO resultGood = new GoodDTO();

            resultGood.WebShop_Id = parsertask.WebShopId;
            resultGood.Category_Id = parsertask.CategoryId;

            PropertyValuesDTO propertyValues = new PropertyValuesDTO();

            foreach (var propitem in grabbersettings.PropertyItems)
            {
                PropertyDTO property = propmanager.Get(propitem.Id);

                var value = 

                switch (property.Type)
                {
                    case PropertyType.Integer:
                        
                  
                    default:
                        break;
                }
            }

            return null;
        }
    }
}

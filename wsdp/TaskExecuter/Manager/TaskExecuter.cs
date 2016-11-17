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
using HtmlAgilityPack;
using log4net;
using System.IO;

namespace TaskExecuting.Manager
{
    public class TaskExecuter : ITaskExecuter
    {
        private UnitOfWork uOw = null;
        private ParserTaskManager parsermanager = null;
        private PropertyManager propmanager = null;
        protected static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");

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

            HtmlDocument doc = null;
            string pageSource = "";
            try
            {
                SiteDownloader sw = new SiteDownloader();
                pageSource = sw.GetPageSouce(url);

                doc = new HtmlDocument();
                doc.LoadHtml(pageSource);
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }


            //gets configuration from parsertask id
            ParserTaskDTO parsertask = parsermanager.Get(parsertaskid);
            GrabberSettingsDTO grabbersettings = parsertask.GrabberSettings;

            GoodDTO resultGood = new GoodDTO();

            resultGood.WebShop_Id = parsertask.WebShopId;
            resultGood.Category_Id = parsertask.CategoryId;

            PropertyValuesDTO propertyValues = new PropertyValuesDTO();
            propertyValues.DictDoubleProperties = new Dictionary<int, double>();
            propertyValues.DictIntProperties = new Dictionary<int, int>();
            propertyValues.DictStringProperties = new Dictionary<int, string>();

            foreach (var propitem in grabbersettings.PropertyItems)
            {
                HtmlNode value = null;
                PropertyDTO property = propmanager.Get(propitem.Id);
                try
                {
                    value = doc.DocumentNode.SelectSingleNode(propitem.Value);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return null;
                }

                switch (property.Type)
                {
                    case PropertyType.Integer:
                        propertyValues.DictIntProperties.Add(propitem.Id, Convert.ToInt32(value.InnerHtml));
                        break;
                    case PropertyType.Double:
                        propertyValues.DictDoubleProperties.Add(propitem.Id, Convert.ToDouble(value.InnerHtml));
                        break;
                    case PropertyType.String:
                        propertyValues.DictStringProperties.Add(propitem.Id, value.InnerHtml);
                        break;
                    default:
                        break;
                }
            }
            //using (FileStream fs = new FileStream("page.txt", FileMode.Create))
            //{
            //    using (StreamWriter sw = new StreamWriter(fs))
            //    {
            //        sw.Write("213" + doc.DocumentNode.OuterHtml);
            //    }
            //}

            //var b = doc.DocumentNode.SelectSingleNode(@"//div[@class='product_descr']");

            //var a = doc.DocumentNode.SelectNodes(@"//div[@class='product_descr']");
            
            resultGood.PropertyValues = propertyValues;

            return resultGood;
        }
    }
}

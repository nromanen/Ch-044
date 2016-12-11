using BAL.Interface;
using DAL.Interface;
using HtmlAgilityPack;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
    public class CheckGoodManager : ICheckGoodManager
    {
        private IGoodManager goodManager;
        private IParserTaskManager parserTaskManager;
        private IGoodDatabasesWizard wizardManager;
        private IURLManager urlManager;
        
        public CheckGoodManager(IGoodManager goodManager, IParserTaskManager parserTaskManager, IGoodDatabasesWizard wizardManager, IURLManager urlManager)
        {
            this.goodManager = goodManager;
            this.parserTaskManager = parserTaskManager;
            this.wizardManager = wizardManager;
            this.urlManager = urlManager;
        }

        /// <summary>
        /// Checks goods from db with goods in same category in web shop. 
        /// </summary>
        /// <param name="categoryid">category to check</param>
        /// <param name="parsertaskid">parsertask with configuration</param>
        public List<GoodDTO> CheckGoodsFromOneCategory(int categoryid, int parsertaskid)
        {
            var resultList = new List<GoodDTO>();
            var goods = goodManager.GetAll().Where(c => c.Category_Id == categoryid).Select(c => c).ToList();
            var parserTask = parserTaskManager.Get(parsertaskid);
            var goodsFromShop = this.GetAllNamesOfGoods(parserTask);



            foreach (var good in goods)
            {
                good.Name = good.Name.Trim();
                good.Name = good.Name.Replace(Environment.NewLine, " ");
                good.Name = good.Name.Replace("\"", "&quot;");
                if (goodsFromShop.Find(s => s == good.UrlLink) == null)
                {
                    good.Status = false;
                    wizardManager.Update(good);
                    resultList.Add(good);
                }
            }

            return resultList;
        }

        /// <summary>
        /// Get all names of goods from parsertask
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<string> GetAllNamesOfGoods(ParserTaskDTO model)
        {

            var links = urlManager.GetAllUrls(model.IteratorSettings);

            return links;
        }

        /// <summary>
        /// Gets name of good from one page
        /// </summary>
        /// <param name="url"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        private string GetNameFromOnePage(string url, List<string> xpathes)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            string name = "";
            foreach (var xpath in xpathes)
            {
                var node = doc.DocumentNode.SelectSingleNode(xpath);
                if (node !=null)
                {
                    name = node.InnerHtml.Trim();
                    break;
                }
                
            }
            return name;
        }
    }
}

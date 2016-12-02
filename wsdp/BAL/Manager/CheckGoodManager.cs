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
    class CheckGoodManager : ICheckGoodManager
    {
        private IGoodManager goodManager;
        private IParserTaskManager parserTaskManager;
        private IGoodDatabasesWizard wizardManager;
        
        public CheckGoodManager(IGoodManager goodManager, IParserTaskManager parserTaskManager, IGoodDatabasesWizard wizardManager)
        {
            this.goodManager = goodManager;
            this.parserTaskManager = parserTaskManager;
        }

        /// <summary>
        /// Checks goods from db with goods in same category in web shop. 
        /// </summary>
        /// <param name="categoryid">category to check</param>
        /// <param name="parsertaskid">parsertask with configuration</param>
        public void CheckGoodsFromOneCategory(int categoryid, int parsertaskid)
        {
            var goods = goodManager.GetAll().Where(c => c.Category_Id == categoryid);
            var parserTask = parserTaskManager.Get(parsertaskid);
            var goodsFromShop = this.GetAllNamesOfGoods(parserTask.IteratorSettings);

            foreach (var good in goods)
            {
                if (goodsFromShop.IndexOf(good.Name) == -1)
                {
                    good.Status = false;
                    wizardManager.Update(good);
                }
            }
        }

        /// <summary>
        /// Get all names of goods from parsertask
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<string> GetAllNamesOfGoods(IteratorSettingsDTO model)
        {
            int from = model.From;
            int to = model.To;

            //string xpath = model.GoodsIteratorXpathes;
            string url = model.UrlMask;

            var allNames = new List<string>();
            for (int i = from; i <= to; i++)
            {
               // allNames.AddRange(GetNamesFromOnePage(url.Replace("{n}", i.ToString()), xpath));
            }
            return allNames;
        }

        /// <summary>
        /// Gets names of goods from one page
        /// </summary>
        /// <param name="url"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        private List<string> GetNamesFromOnePage(string url, string xpath)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            var names = doc.DocumentNode.SelectNodes(xpath).Select(s => s.InnerHtml).ToList();

            return names;
        }
    }
}

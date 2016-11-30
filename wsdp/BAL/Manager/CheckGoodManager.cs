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
        public CheckGoodManager(IGoodManager goodManager, IParserTaskManager parserTaskManager)
        {
            this.goodManager = goodManager;
            this.parserTaskManager = parserTaskManager;
        }
        public void CheckGoodsFromOneCategory(int categoryid, int parsertaskid)
        {
            var goods = goodManager.GetAll().Where(c => c.Category_Id == categoryid).Select(s => s.Name);
            var parserTask = parserTaskManager.Get(parsertaskid);
            var goodsFromShop = this.GetAllNamesOfGoods(parserTask.IteratorSettings);

            foreach (var goodName in goods)
            {
                if (goodsFromShop.IndexOf(goodName) == -1)
                {
                    //good wizard update status - TO DO
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

            string xpath = model.GoodsIteratorXpath;
            string url = model.UrlMask;

            var allNames = new List<string>();
            for (int i = from; i <= to; i++)
            {
                allNames.AddRange(GetNamesFromOnePage(url.Replace("{n}", i.ToString()), xpath));
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

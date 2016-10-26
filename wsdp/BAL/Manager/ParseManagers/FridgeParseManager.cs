using BAL.Interface;
using Common.Enum;
using DAL.Interface;
using ExtendedXmlSerialization;
using HtmlAgilityPack;
using log4net;
using Model.DB;
using Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager.ParseManagers
{
    public class FridgeParseManager : BaseManager, IFridgeParseManager
    {
        public FridgeParseManager(IUnitOfWork uOW) : base(uOW)
        {

        }
        static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");

        public void GetConcreteGoodsFromCategory(string url)
        {
            try
            {
                GoodManager gm = new GoodManager(uOW);

                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);

                int countOfPages = this.GetCountOfPages(url);
                var fridges = new List<Fridge>();

                for (int i = 6; i < 7; i++)
                {
                    foreach (var good in GetFridgesFromPage(url + i.ToString()))
                    {
                        var goodDB = new Good();
                        goodDB.Category = GoodCategory.Fridge;
                        ExtendedXmlSerializer ser = new ExtendedXmlSerializer();

                        var xmlGood = ser.Serialize(good);
                        goodDB.XmlData = xmlGood;
                        if (good.Price != null)
                        {
                            gm.InsertGood(goodDB);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public int GetCountOfPages(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            var findDiv = doc.DocumentNode.Descendants("span").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("last")).FirstOrDefault();

            var link = findDiv.ChildNodes.First(a => a.Name == "a");
            string href = link.Attributes["href"].Value;

            var countOfPages = new string(href.Where(c => Char.IsDigit(c)).ToArray());

            return Convert.ToInt32(countOfPages);
        }

        public List<Fridge> GetFridgesFromPage(string url)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                var listOfFridges = new List<Fridge>();

                var findDivs = doc.DocumentNode.Descendants("div").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == "model_info").ToList();
                var pathList = new List<string>();

                foreach (var div in findDivs)
                {

                    var link = div.ChildNodes.Where(d => d.Name == "p").FirstOrDefault().ChildNodes.Where(h => h.Name == "a").FirstOrDefault();

                    string pathForGood = @"http://tehnotrade.com.ua/" + link.Attributes["href"].Value;

                    pathList.Add(pathForGood);
                }
                foreach (var path in pathList)
                {
                    listOfFridges.Add(this.ParseFridge(path));
                }
                return listOfFridges;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }

        }
        public Fridge ParseFridge(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            var fridge = new Fridge();
            try
            {
                try
                {
                    //find name
                    var name = doc.DocumentNode.SelectNodes("//*[@id='sideLeft']/ul/li/span").FirstOrDefault().InnerHtml;
                    fridge.Name = name;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }

                try
                {
                    //find img src
                    var findDiv = doc.DocumentNode.Descendants("div").Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("model-photo")).FirstOrDefault();
                    var link = findDiv.ChildNodes.FirstOrDefault(n => n.Name == "a");
                    var href = @"http://tehnotrade.com.ua" + link.Attributes["href"].Value;

                    fridge.ImagePath = href;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }

                try
                {
                    //find price
                    var findDivForPrice = doc.DocumentNode.Descendants("span").Where(d => d.Attributes.Contains("itemprop") && d.Attributes["itemprop"].Value.Contains("price")).FirstOrDefault();
                    var price = findDivForPrice.InnerHtml;

                    fridge.Price = price;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }

                try
                {
                    //find charasteristics
                    var elementsTrs = doc.DocumentNode.SelectNodes("//*[@id='second']/table/tr").ToList();
                    if (elementsTrs != null)
                    {
                        foreach (var el in elementsTrs)
                        {
                            var a = el.ChildNodes[1].InnerText.ToString();
                            var b = el.ChildNodes[3].InnerText;
                            if (!fridge.CharacteristicsDictionary.ContainsKey(a))
                            {
                                fridge.CharacteristicsDictionary.Add(a, b);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
                return fridge;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }

        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using DAL;
using DAL.Interface;
using ExtendedXmlSerialization;
using HtmlAgilityPack;
using log4net;
using Model.DB;
using Model.Product;

namespace BAL.Manager.ParseManagers
{
    public class TVParseManager : BaseManager, ITVParseManager
    {
        public TVParseManager(IUnitOfWork uOW) : base(uOW)
        {
        }

        static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");

        public int GetCountOfPages(string urlpath)

        {
            HtmlWeb web = new HtmlWeb();
            Encoding enc = Encoding.GetEncoding(1251);
            web.OverrideEncoding = enc;
            HtmlDocument doc = web.Load(urlpath);


            var num =
                doc.DocumentNode.Descendants("div")
                    .FirstOrDefault(
                        d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("pages"))
                    .ChildNodes.Last(el => el.Name == "a")
                    .InnerText;

            return Convert.ToInt32(num);
        }

        public void ParseCategory(string urlpath)
        {
            try
            {
                GoodManager gm = new GoodManager(uOW);
                HtmlWeb web = new HtmlWeb();
                Encoding enc = Encoding.GetEncoding(1251);
                web.OverrideEncoding = enc;
                HtmlDocument doc = web.Load(urlpath);
                int countOfPages = GetCountOfPages(urlpath);


                for (int i = 1; i <= countOfPages; i++)
                {
                    foreach (var good in GetGoodsFromPage(urlpath + "?PAGEN_1=" + i))
                    {
                        Good goodDb = new Good();
                        goodDb.Category = Common.Enum.GoodCategory.TV;
                        ExtendedXmlSerializer ser = new ExtendedXmlSerializer();

                        goodDb.XmlData = ser.Serialize(good);

                        gm.InsertGood(goodDb);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public List<TV> GetGoodsFromPage(string urlpath)
        {
            try
            {
                List<TV> resultList = new List<TV>();

                HtmlWeb web = new HtmlWeb();
                Encoding enc = Encoding.GetEncoding(1251);
                web.OverrideEncoding = enc;
                HtmlDocument doc = web.Load(urlpath);
                var finddivs = doc.DocumentNode.Descendants("div").Where(d =>
                            d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("cat-list-item"))
                    .ToList();
                foreach (var i in finddivs)
                {
                    var pathForGood =
                        i.Descendants("div")
                            .FirstOrDefault(
                                d =>
                                    d.Attributes.Contains("class") &&
                                    d.Attributes["class"].Value.Contains("cat-list-col item-col-2"))
                            .ChildNodes.First(d => d.Name == "div")
                            .ChildNodes.First(d => d.Name == "p")
                            .ChildNodes.First(d => d.Name == "a")
                            .Attributes["href"].Value;

                    resultList.Add(GetGood(pathForGood));
                }
                return resultList;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }

        public TV GetGood(string urlpath)
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                string name = "";
                string srcimg = "";
                string Price = "";


                HtmlWeb web = new HtmlWeb();
                Encoding enc = Encoding.GetEncoding(1251);
                web.OverrideEncoding = enc;
                HtmlDocument doc = web.Load("https://repka.ua" + urlpath);


                name = doc.DocumentNode
                    .Descendants("span")
                    .FirstOrDefault(
                        d => d.Attributes.Contains("itemprop") && d.Attributes["itemprop"].Value.Contains("name"))
                    .InnerText.Replace("&nbsp;", " ");
                srcimg = doc.GetElementbyId("main_photo").Attributes["src"].Value;

                Price = doc.DocumentNode
                    .Descendants("span")
                    .FirstOrDefault(
                        d => d.Attributes.Contains("itemprop") && d.Attributes["itemprop"].Value.Contains("price"))
                    .Attributes["content"].Value;

                var nameofchar =
                    doc.DocumentNode.Descendants("td")
                        .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == "first")
                        .ToList();
                var value = doc.DocumentNode.Descendants("span")
                    .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == "td-separator")
                    .ToList();
                for (int i = 0; i < nameofchar.Count; i++)
                {
                    var key = nameofchar[i].InnerText;
                    var val = value[i].InnerText;
                    if (!dict.ContainsKey(key))
                    {
                        dict.Add(nameofchar[i].InnerText, value[i].InnerText);
                    }
                }

                var TV = new TV
                {
                    Id = 1,
                    Name = name,
                    Price = Price,
                    ImageLink = "https://repka.ua/" + srcimg,
                    Characteristics = dict
                };

                return TV;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }
    }
}

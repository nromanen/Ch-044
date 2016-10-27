using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using Common.Enum;
using DAL.Interface;
using ExtendedXmlSerialization;
using HtmlAgilityPack;
using log4net;
using Model.DB;
using Model.Product;

namespace BAL.Manager.ParseManagers
{
    public class LaptopParseManager : BaseManager, ILaptopParseManager
    {
        static readonly ILog Logger = LogManager.GetLogger("RollingLogFileAppender");


        private readonly HtmlWeb _webClient;
        public LaptopParseManager(IUnitOfWork uOW) : base(uOW)
        {
            _webClient = new HtmlWeb()
            {
                OverrideEncoding = Encoding.UTF8
            };
        }

        public IEnumerable<Good> ParseAll(string path)
        {
            List<Good> resultList = new List<Good>();
            HtmlDocument page = null;
            try
            {
                page = _webClient.Load(path);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }


            for (int i = 0, pager = 0; i <= GetPageQuantity(page); i++, pager += GetGoodsQuantity(page))
            {
                resultList.AddRange(ParseOnePage(path + "?per_page=" + i));
            }

            return resultList;
        }

        public IEnumerable<Good> ParseOnePage(string path)
        {
            HtmlDocument page = _webClient.Load(path);
            int goodsQuantity = GetGoodsQuantity(page);
            List<Good> productList = new List<Good>();
            for (int i = 1; i <= goodsQuantity; i++)
            {
                try
                {
                    string productPath = page
                        .DocumentNode
                        .SelectSingleNode(String.Format("//*[@id='items-catalog-main']/div[{0}]/a", i))
                        .GetAttributeValue("href", "undefined");
                    productList.Add(ParseOne(productPath));
                }
                catch
                {
                    Logger.Error("Uncorrect page");
                    continue;
                }
            }

            return productList;
        }

        public Good ParseOne(string path)
        {
            HtmlDocument page;
            try
            {
                page = _webClient.Load(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var resultGood = new Laptop();

            resultGood.Name = page.DocumentNode.SelectSingleNode("//h1").InnerText ?? "undefined";
            resultGood.ImgPath = @"http://www.ttt.ua" + page
                .DocumentNode
                .SelectSingleNode(@"//a[@id='photoProduct']/span[contains(@class, 'photo-block')]/img[contains(@class, 'vImgPr')]")
                .GetAttributeValue("src", "undefined") ?? "undefined";

            resultGood.ImgPath = (@"http://www.ttt.ua" + page
                .DocumentNode
                .SelectSingleNode(@"//a[@id='photoProduct']/span[contains(@class, 'photo-block')]/img[contains(@class, 'vImgPr')]")
                .GetAttributeValue("src", "undefined")) ?? "undefined";

            var descrList = page.DocumentNode
                .SelectNodes("//div[contains(@class, 'product-descr patch-product-view')]/div[contains(@class, 'text')]/p");
            if (descrList != null)
            {
                foreach (var p in descrList)
                {
                    resultGood.Description += p.InnerText + "\n";
                }
            }

            var characteristicList = (from table in page.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic')]//table//tbody").Cast<HtmlNode>()
                                      from row in table.SelectNodes("tr").Cast<HtmlNode>()
                                      from key in row.SelectNodes("th").Cast<HtmlNode>()
                                      from value in row.SelectNodes("td").Cast<HtmlNode>()
                                      select new { Key = key.InnerText, Value = value.InnerText }).ToList();

            foreach (var cell in characteristicList)
            {
                resultGood.Characteristic.Add(cell.Key, cell.Value);
            }


            var producer =
                page.DocumentNode.SelectNodes("//li[contains(@class, 'btn-crumb')]/a/span[contains(@class, 'text-el')]")
                    .Last().InnerText ?? "undefined";
            resultGood.Characteristic.Add("Producer", producer);


            var price =
                page.DocumentNode.SelectSingleNode(
                    "//div[contains(@class, 'frame-prices-buy f-s_0')]/div[contains(@class, 'frame-prices f-s_0')]/span[contains(@class, 'current-prices f-s_0')]//span[contains(@class, 'price-new')]/span/span[contains(@class, 'price priceVariant')]")
                    .InnerText ?? "0";
            resultGood.Characteristic.Add("Price", price);

            Good result = new Good();
            ExtendedXmlSerializer serializer = new ExtendedXmlSerializer();

            result.Category = GoodCategory.Laptop;
            result.XmlData = serializer.Serialize(resultGood);

            GoodManager goodManager = new GoodManager(uOW);

            goodManager.InsertGood(result);

            return result;
        }

        private int GetGoodsQuantity(HtmlDocument page)
        {
            return page.DocumentNode.SelectNodes("//*[@id='items-catalog-main']/div").Count;
        }

        private int GetPageQuantity(HtmlDocument page)
        {
            int position = page.DocumentNode
                .SelectNodes("//div[@class='pagination']//ul[contains(@class, 'f-s_0')]/li").Count;
            string xpath = $"//div[@class='pagination']//ul[contains(@class, 'f-s_0')]/li[{position}]";

            string value = page.DocumentNode.SelectSingleNode(xpath).InnerText;
            return int.Parse(value);
        }

    }
}

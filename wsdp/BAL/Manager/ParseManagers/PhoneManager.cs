using HtmlAgilityPack;
using Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using log4net;
using Model.DB;
using System.Xml.Serialization;
using ExtendedXmlSerialization;

namespace BAL.Manager.ParseManagers {
    //parser for MOYO.UA
	public class PhoneManager : BaseManager
    {
        static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");
        public PhoneManager(IUnitOfWork uOW) : base(uOW)
        {

        }

        private int GetCountOfPages(string urlpath)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(urlpath);


            var finddiv = doc.DocumentNode.Descendants("div").Where(d =>
            d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("pagination")).FirstOrDefault();

            var pagelink = finddiv.ChildNodes.Where(el => el.Name == "a").Last();

            return Convert.ToInt32(pagelink.InnerHtml);
        }
        public void ParseGoodsFromCategory(string urlpath)
        {
            GoodManager gm = new GoodManager(uOW);
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(urlpath);
            int countOfPages = this.GetCountOfPages(urlpath);

            for (int i = 2; i <= countOfPages; i++)
            {
                foreach (var good in GetConcreteGoodsFromOnePage(urlpath + "?page=" + i.ToString()))
                {
                    Good goodDb = new Good();
                    goodDb.Category = Common.Enum.Category.Phone;
                    goodDb.Id = 1;

                    ExtendedXmlSerializer ser = new ExtendedXmlSerializer();

                    goodDb.XmlData = ser.Serialize(good);
                    
                    gm.InsertGood(goodDb);
                } 
            }

        }

        private List<ConcreteGood> GetConcreteGoodsFromOnePage(string urlpath)
        {
            List<ConcreteGood> resultListOfConcreteGoods = new List<ConcreteGood>();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(urlpath);



            var finddivs = doc.DocumentNode.Descendants("div").Where(d =>
            d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("goods_item"));

            foreach (var itemdiv in finddivs)
            {
                var innerItemDiv = itemdiv.Descendants("div").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("goodsitem_inner")).FirstOrDefault();

                var link = innerItemDiv.ChildNodes.Where(d => d.Name == "a").Last();

                string pathForGood = link.Attributes["href"].Value;

                resultListOfConcreteGoods.Add(this.GetConcreteGood(@"http://moyo.ua" + pathForGood));

            }
            return resultListOfConcreteGoods;
        }

        private ConcreteGood GetConcreteGood(string urlpath)
        {
            Shop shop = new Shop()
            {
                Id = 1,
                Name = "MOYO"
            };

            string namePhone = "";
            decimal pricePhone = 0;
            Producer producer = null;

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(urlpath);

            try
            {
                namePhone = doc.DocumentNode.Descendants("meta")
                              .Where(d => d.Attributes.Contains("name") && d.Attributes["name"].Value.Contains("keywords"))
                              .FirstOrDefault().Attributes["Content"].Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                pricePhone = Convert.ToDecimal(doc.GetElementbyId("quickOrder_productPrice").Attributes["value"].Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                string nameProducer = doc.DocumentNode.Descendants("div").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == "breadcrumbs clear_after").FirstOrDefault().Descendants("div")
                                     .Where(d => d.Attributes.Contains("itemtype")).Last().ChildNodes[0].ChildNodes[0].InnerHtml;
                //string nameProducer = "123";
                producer = new Producer()
                {
                    Id = 1,
                    Name = nameProducer
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Characteristics cr = new Characteristics();

            try
            {
                var crUl = doc.DocumentNode.Descendants("div").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == "full_content").FirstOrDefault()
                    .Descendants("li").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == "clear_after").ToList();

                foreach (var li in crUl)
                {
                    string a = li.ChildNodes[1].InnerHtml;
                    string b = li.ChildNodes[3].InnerHtml;
                    cr.Dict.Add(li.ChildNodes[1].InnerHtml, li.ChildNodes[3].InnerHtml);
                    //Console.WriteLine(cr.Dict.Last().Key);
                    //Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ClearGood good = new ClearGood()
            {
                Id = 1,
                Name = namePhone,
                Description = "",
                Characteristics = cr
            };

            ConcreteGood concreteGood = new ConcreteGood()
            {
                Good = good,
                Producer = producer,
                Id = 1,
                Price = pricePhone,
                Shop = shop
            };
            Console.WriteLine(concreteGood);
            Console.ReadLine();
            return null;
        }


    
}


}

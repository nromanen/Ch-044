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
using BAL.Interface;
using Model.DTO;
using AutoMapper;

namespace BAL.Manager.ParseManagers {
    //parser for MOYO.UA

    
	public class PhoneManager : BaseManager , IPhoneManager
    {
        static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");
        public PhoneManager(IUnitOfWork uOW) : base(uOW)
        {

        }

        public List<PhoneSimpleDTO> GetAllPhones()
        {
            List<PhoneSimpleDTO> phones = new List<PhoneSimpleDTO>();
            ExtendedXmlSerializer ser = new ExtendedXmlSerializer();
            foreach (var phoneDb in uOW.GoodRepo.All)
            {
                var phone = ser.Deserialize(phoneDb.XmlData, typeof(ConcreteGood));

                phones.Add(Mapper.Map<PhoneSimpleDTO>(phone));
            }

            return phones;
        }
        private int GetCountOfPages(string urlpath)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(urlpath);
            int count = 1;

            try
            {
                var finddiv = doc.DocumentNode.Descendants("div")
                 .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("pagination"))
                 .FirstOrDefault();

                var pagelink = finddiv.ChildNodes
                                      .Where(el => el.Name == "a")
                                      .Last();
                count = Convert.ToInt32(pagelink.InnerHtml);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return count;
        }
        public void ParseGoodsFromCategory(string urlpath)
        {
            GoodManager gm = new GoodManager(uOW);
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(urlpath);
            int countOfPages = this.GetCountOfPages(urlpath);
            /*Category category = new Category()
            {
                Id = 0,
                З
            }*/


            for (int i = 1; i <= countOfPages; i++)
            {
                foreach (var good in GetConcreteGoodsFromOnePage(urlpath + "?page=" + i.ToString()))
                {
                    Good goodDb = new Good();
                    goodDb.Category = Common.Enum.GoodCategory.Phone;
                    goodDb.Id = 1;

                    ExtendedXmlSerializer ser = new ExtendedXmlSerializer();

                    goodDb.XmlData = ser.Serialize(good);

                    if (good.Price != 0)
                        gm.InsertGood(goodDb);
                } 
            }

        }

        private List<ConcreteGood> GetConcreteGoodsFromOnePage(string urlpath)
        {
            List<ConcreteGood> resultListOfConcreteGoods = new List<ConcreteGood>();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(urlpath);



            var finddivs = doc.DocumentNode.Descendants("div")
                              .Where(d => d.Attributes.Contains("class") && d.Attributes["class"]
                              .Value
                              .Contains("goods_item"));

            foreach (var itemdiv in finddivs)
            {
                var innerItemDiv = itemdiv.Descendants("div")
                                          .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("goodsitem_inner"))
                                          .FirstOrDefault();

                var link = innerItemDiv.ChildNodes
                                       .Where(d => d.Name == "a")
                                       .Last();

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
            string ulrimg = "";
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
                logger.Error(ex.Message);
            }

            try
            {
                pricePhone = Convert.ToDecimal(doc.GetElementbyId("quickOrder_productPrice")
                                                  .Attributes["value"].Value);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            try
            {
                ulrimg = doc.DocumentNode.Descendants("div")
                            .Where(a => a.Attributes.Contains("class") && a.Attributes["class"].Value.Contains("tovarnew-mainimagecontainer"))
                            .FirstOrDefault()
                            .ChildNodes[1]
                            .Attributes["href"]
                            .Value;

                //ulrimg = @"http://img1.moyo.ua/img/products/2796/64_4000x_1475233394.png";
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            try
            {
                string nameProducer = doc.DocumentNode.Descendants("div")
                                         .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == "breadcrumbs clear_after")
                                         .FirstOrDefault()
                                         .Descendants("div")
                                         .Where(d => d.Attributes.Contains("itemtype"))
                                         .Last()
                                         .ChildNodes[0]
                                         .ChildNodes[0]
                                         .InnerHtml;
                //string nameProducer = "123";
                producer = new Producer()
                {
                    Id = 1,
                    Name = nameProducer
                };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            Characteristics cr = new Characteristics();

            try
            {
                var crUl = doc.DocumentNode.Descendants("div")
                              .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == "full_content")
                              .FirstOrDefault()
                              .Descendants("li")
                              .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == "clear_after")
                              .ToList();

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
                logger.Error(ex.Message);
            }

            ClearGood good = new ClearGood()
            {
                Id = 1,
                Name = namePhone,
                Description = "",
                Characteristics = cr,
                ImgUrl = ulrimg
            };

            ConcreteGood concreteGood = new ConcreteGood()
            {
                Good = good,
                Producer = producer,
                Id = 1,
                Price = pricePhone,
                Shop = shop,
                Link = urlpath
            };

            //Console.WriteLine(ulrimg);
            //Console.ReadLine();
            return concreteGood;
        }


    
}


}

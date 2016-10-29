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
using Model.DB;
using Model.Product;

namespace BAL.Manager.ParseManagers {
	public class MicrowaveParseManager : BaseManager, IMicrowaveParseManager {
		public MicrowaveParseManager(IUnitOfWork uOW) : base(uOW) {}

		public int GetCountOfPages(string url) {
			HtmlWeb web = new HtmlWeb();
			HtmlDocument doc = web.Load(url);

			var node = doc.DocumentNode.SelectSingleNode("//a[@class='last']").InnerHtml;
			return Convert.ToInt32(node);
		}
		public List<string> GetAllUrls(string url) {
			HtmlWeb web = new HtmlWeb();
			int countOfPages = GetCountOfPages(url);
			List<string> urlItems = new List<string>();
			for (int i = 1; i <= countOfPages - 29; i++) {
				HtmlDocument doc = web.Load($"http://allo.ua/ru/products/mikrovolnovki/p-{i}/");
				var hrefs = doc.DocumentNode.SelectNodes("//div[@class='category-products']/ul[@class='products-grid']/li[@class='item']//div[@class='product-container-all']/p[@class='product-name-container']/a/@href").Select(t => t.Attributes.SingleOrDefault(d => d.Name == "href").Value);
				urlItems.AddRange(hrefs);
			}
			return urlItems;
		}
		public void GetAllWaves(string url) {
			List<string> urls = GetAllUrls(url);
			ExtendedXmlSerializer sererializer = new ExtendedXmlSerializer();
			GoodManager goodManager = new GoodManager(uOW);
			foreach (var item in urls) {
				HtmlWeb web = new HtmlWeb();
				HtmlDocument doc = web.Load(item);
				Microwave wave = new Microwave();
				wave.Name = doc.DocumentNode.SelectNodes("//span[@itemprop='title']").Last().LastChild.InnerHtml;
				wave.ImgPath = doc.DocumentNode.SelectSingleNode("//img[@id='image']/@src").Attributes.SingleOrDefault(t => t.Name == "src").Value;
				wave.urlPath = item;
				try {
					wave.Price = doc.DocumentNode.SelectSingleNode("//span[@class='price price-sym-6']/span[@class='new_sum']").InnerHtml;
				} catch {
					wave.Price = doc.DocumentNode.SelectSingleNode("//span[@class='price price-sym-6']/span[@class='sum']").InnerHtml;
				}
				wave.Characteristics = new Dictionary<string, string>();
				var trs = doc.DocumentNode.SelectNodes("//table[@id='specsTable']//tr");
				foreach (var tr in trs) {
					wave.Characteristics.Add(tr.ChildNodes[1].InnerHtml.Trim(), tr.ChildNodes[3].InnerHtml.Trim());
				}
				Good good = new Good() {
					Category = GoodCategory.Microwave,
					XmlData = sererializer.Serialize(wave)
				};
				goodManager.InsertGood(good);
			}
		}

		//public List<Good> MakeListOfGood(string url) {
		//	List<Microwave> waves = GetAllWaves(url);
		//	List<Good> goods = new List<Good>();
		//	ExtendedXmlSerializer sererializer = new ExtendedXmlSerializer();
		//	foreach (Microwave wave in waves) {
		//		Good good = new Good() {
		//			Category = GoodCategory.Microwave,
		//			XmlData = sererializer.Serialize(wave)
		//		};
		//		goods.Add(good);
		//	}
		//	return goods;
		//}
	}
}

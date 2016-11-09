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
	public class TapeRecorderParseManager : BaseManager, ITapeRecorderParseManager {
		public TapeRecorderParseManager(IUnitOfWork uOW) : base(uOW) {}

		public int GetCountOfPages(string url) {
			HtmlWeb web = new HtmlWeb();
			HtmlDocument doc = web.Load(url);

			var node = doc.DocumentNode.SelectSingleNode("//div[@class='pagination']/a[13]").InnerHtml;
			return Convert.ToInt32(node);
		}
		public List<string> GetAllUrls(string url) {
			HtmlWeb web = new HtmlWeb();
			int countOfPages = GetCountOfPages(url);
			List<string> urlItems = new List<string>();
			for (int i = 1; i <= countOfPages - 27; i++) {
                //HtmlDocument doc = web.Load($"http://pulsepad.com.ua/catalog/g4619581-avtomagnitoly?page={i}");
                //var hrefs = doc.DocumentNode.SelectNodes("//ul[@class='browsed_products tiny_products']/li[@class='productli']/div[@class='alldivhover']/h3/a/@href").Select(t => t.Attributes.SingleOrDefault(d => d.Name == "href").Value);
                //urlItems.AddRange(hrefs);
			}
			for(int i = 0; i < urlItems.Count; i++) {
				urlItems[i] = "http://pulsepad.com.ua/" + urlItems[i];
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
				TapeRecorder wave = new TapeRecorder();
				wave.Name = doc.DocumentNode.SelectSingleNode("//div[@class='content_dimensions']/h1").InnerHtml;
				wave.ImgPath = doc.DocumentNode.SelectSingleNode("//a[@class='zoom']/img/@src").Attributes.SingleOrDefault(t => t.Name == "src").Value;
				wave.urlPath = item;
				wave.Price = doc.DocumentNode.SelectSingleNode("//span[@id='priceConv']").InnerHtml;
				wave.Characteristics = new Dictionary<string, string>();
				var trs = doc.DocumentNode.SelectNodes("//div[@id='cha']/table//tr");
				foreach (var tr in trs) {
					wave.Characteristics.Add(tr.ChildNodes[1].ChildNodes[1].InnerHtml.Trim(), tr.ChildNodes[3].ChildNodes[1].InnerHtml.Trim());
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

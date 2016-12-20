using BAL;
using BAL.Manager;
using DAL.Interface;
using Model.DB;
using Model.DTO;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSDP.Tests.BAL
{
	[TestFixture]
	public class URLManager_tests
	{
		[Test]
		public void GetUrlsFromOnePage()
		{
			AutoMapperConfig.Configure();
			// mock Repo logic
			var uof = new Mock<IUnitOfWork>();
			var mngr = new URLManager(uof.Object);
			var testurl = "http://rozetka.com.ua/notebooks/c80004/filter/page=1;preset=dlya-biznesa/";
			var testxpath = "xpath-//div[@id='block_with_goods']/div/div/div/div/div/div/div/a";
			var result=mngr.GetUrlsFromOnePage(testurl,testxpath);
			var resultList = new List<string>() {
			"http://rozetka.com.ua/apple_a1708_macbook_pro_13_mll42ua_a/p12318700/",
			"http://rozetka.com.ua/dell_monet14skl1703_016_ubu/p11041615/",
			"http://rozetka.com.ua/lenovo_20evs03n00/p9311138/",
			"http://rozetka.com.ua/lenovo_20evs03m00/p9310900/",
			"http://rozetka.com.ua/hp_t6p10ea/p10126687/",
			"http://rozetka.com.ua/hp_p5r72ea/p10113779/",
			"http://rozetka.com.ua/dell_van15skl1701_017_ubu/p8914053/",
			"http://rozetka.com.ua/hp_p5s45ea/p10101893/",
			"http://rozetka.com.ua/hp_p4p07ea/p10151852/",
			"http://rozetka.com.ua/dell_van15skl1703_009_ubu/p11042350/",
			"http://rozetka.com.ua/hp_probook_450_w4p15ea/p11107282/",
			"http://rozetka.com.ua/hp_probook_440_g3_p5s55ea/p8425390/",
			"http://rozetka.com.ua/hp_probook_450_w4p13ea/p11099869/",
			"http://rozetka.com.ua/hp_probook_440_w4p04ea/p11095368/",
			"http://rozetka.com.ua/hp_probook_430_x0p48es/p11092932/",
			"http://rozetka.com.ua/dell_monet14skl1703_008/p11041370/",
			"http://rozetka.com.ua/hp_probook_470_w4p87ea/p11107660/",
			"http://rozetka.com.ua/dell_n002l347014emea_ubu/p11080178/",
			"http://rozetka.com.ua/hp_probook_450_w4p68ea/p11107338/",
			"http://rozetka.com.ua/asus_pro_p2520la_xo0131r/p12154843/",
			"http://rozetka.com.ua/dell_van15skl1703_008_ubu/p10988933/",
			"http://rozetka.com.ua/hp_probook_440_v5e85av/p12011434/",
			"http://rozetka.com.ua/apple_macbook_pro_retina_15_mjlq2uaa/p3252779/",
			"http://rozetka.com.ua/dell_van15skl1703_006/p11042287/",
			"http://rozetka.com.ua/asus_pu301la-ro173h/p1839562/",
			"http://rozetka.com.ua/hp_probook_430_w4n80ea/p11090097/",
			"http://rozetka.com.ua/dell_vostro_5459_monet14skl1605_007glw/p8913871/",
			"http://rozetka.com.ua/lenovo_20ets03100/p9303305/",
			"http://rozetka.com.ua/hp_probook_470_w4p93ea/p11107520/",
			"http://rozetka.com.ua/hp_probook_450_w4p51ea/p11107009/",
			"http://rozetka.com.ua/hp_probook_450_p4p32ea/p6592119/",
			"http://rozetka.com.ua/dell_monet14skl1703_011/p11041902/",
			};
			Assert.IsNotNull(result, "Null property returned");
			Assert.AreEqual(resultList, result);

		}
	}
}

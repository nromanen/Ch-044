using BAL.Interface;
using Model.DB;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;
using System.Xml.Serialization;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class HomeController : BaseController
	{
		private ICategoryManager categoryManager;
		private IWebShopManager shopManager;
		private IPropertyManager propertyManager;
		private IGoodManager goodManager;
		private IPriceManager priceManager;

		public HomeController(ICategoryManager categoryManager, IGoodManager goodManager,IPropertyManager propertyManager,IWebShopManager shopManager,IPriceManager priceManager)
		{
			this.priceManager = priceManager;
			this.categoryManager = categoryManager;
			this.goodManager = goodManager;
			this.propertyManager = propertyManager;
			this.shopManager = shopManager;
		}

		public ActionResult Index()
		{
			var goods = goodManager.GetAll();
			var categories = categoryManager.GetAll();
			var goods_list = goodManager.GetAll();

			foreach (var item in goods_list)
			{
				item.Category = categoryManager.Get(item.Category_Id);

				item.WebShop = shopManager.GetById(item.WebShop_Id);
			}
			
			var Custom_model = new IndexViewDTO()
			{
				CategoryList = categories,
				GoodList = goods_list
			};
			ModelState.Clear();
			return View(Custom_model);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
		public ActionResult PriceStat()
		{

			return View();
		}

		[WebMethod]
		public List<object> getLineChartData(string url_one, string url_two, string year)
		{
			List<object> iData = new List<object>();
			List<string> labels = new List<string>();
			var priceList = priceManager.GetAll();
			string query1 = "Select distinct( DateName( month , DateAdd( month , DATEPART(MONTH,Date) , -1 ) )) as month_name, ";
			query1 += " DATEPART(MONTH,Date) as month_number from PriceHistories  where DATEPART(YEAR,Date)='" + year + "'  ";
			query1 += " order by month_number;";

			DataTable dtLabels = commonFuntionGetData(query1);
			foreach (DataRow drow in dtLabels.Rows)
			{
				labels.Add(drow["month_name"].ToString());
			}

			iData.Add(labels);
			string query_DataSet_1 = " select DATENAME(MONTH,DATEADD(MONTH,month(Date),-1 )) as month_name, month(Date) as month_number ,";
			query_DataSet_1 += " Price as price_history  from PriceHistories ";
			query_DataSet_1 += " where YEAR(Date)='" + year + "' and  Url='" + url_one + "'  ";
			query_DataSet_1 += " group by   month(Date),Price ";
			query_DataSet_1 += " order by  month_number,Price  ";

			DataTable dtDataItemsSets_1 = commonFuntionGetData(query_DataSet_1);
			var lst_dataItem_1 = new List<decimal>();
			foreach (DataRow dr in dtDataItemsSets_1.Rows)
			{
				lst_dataItem_1.Add(Convert.ToDecimal(dr["price_history"].ToString()));
			}
			iData.Add(lst_dataItem_1);

			string query_DataSet_2 = " select DATENAME(MONTH,DATEADD(MONTH,month(Date),-1 )) as month_name, month(Date) as month_number ,";
			query_DataSet_2 += " Price as price_history  from PriceHistories ";
			query_DataSet_2 += " where YEAR(Date)='" + year + "' and  Url='" + url_two + "'  ";
			query_DataSet_2 += " group by   month(Date),Price ";
			query_DataSet_2 += " order by  month_number,Price  ";

			DataTable dtDataItemsSets_2 = commonFuntionGetData(query_DataSet_2);
			var lst_dataItem_2 = new List<decimal>();
			foreach (DataRow dr in dtDataItemsSets_2.Rows)
			{
				lst_dataItem_2.Add(Convert.ToDecimal(dr["price_history"].ToString()));
			}
			iData.Add(lst_dataItem_2);

			return iData;
		}

		public DataTable commonFuntionGetData(string strQuery)
		{
			var cn = new SqlConnection(@"Data Source=СЛАВА-ПК\SQLEXPRESS_2014;Initial Catalog=MyShop;user id=admin;password=123");
			SqlDataAdapter dap = new SqlDataAdapter(strQuery,cn);
			DataSet ds = new DataSet();
			dap.Fill(ds);
			return ds.Tables[0];
		}
	}
}
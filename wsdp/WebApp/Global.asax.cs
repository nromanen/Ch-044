using BAL;
using BAL.Interface;
using BAL.Manager;
using BAL.Manager.ParseManagers;
using DAL;
using DAL.Interface;
using log4net;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DAL.Elastic;
using DAL.Elastic.Interface;


namespace WebApp
{
	public class MvcApplication : HttpApplication
	{
		private static readonly ILog Logger = LogManager.GetLogger("RollingLogFileAppender");

		protected void Application_Start()
		{
			log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
			InjectorContainer();
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AutoMapperConfig.Configure();
		}

		/// <summary>
		/// To create injector container and register dependencies
		/// </summary>
		private void InjectorContainer()
		{
			try
			{
				var container = new Container();
				container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
				container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
				container.Register<IElasticUnitOfWork, ElasticUnitOfWork>(Lifestyle.Scoped);


                container.Register<IElasticManager, ElasticManager>(Lifestyle.Scoped);
                container.Register<IGoodDatabasesWizard, GoodDatabasesWizard>();
                container.Register<IUserManager, UserManager>();
				container.Register<ICategoryManager, CategoryManager>();
				container.Register<IPropertyManager, PropertyManager>();
				container.Register<IWebShopManager, WebShopManager>();
				container.Register<IDownloadManager, DownloadManager>();
				container.Register<IRoleManager, RoleManager>();
				container.Register<IParserTaskManager, ParserTaskManager>();
				container.Register<IGoodManager, GoodManager>();
				container.Register<IURLManager, URLManager>();
				container.Register<IHtmlValidator, HtmlValidator>();
				container.Register<IPriceManager, PriceManager>();
				container.Register<IExecuteManager, ExecuteManager>();
				container.Register<IPreviewManager, PreviewManager>();
                container.Register<IAppSettingsManager, AppSettingsManager>();
                container.Register<ICheckGoodManager, CheckGoodManager>();
                container.Register<IDeleteFilesManager, DeleteFilesManager>();
				container.Register<IFollowPriceManager, FollowPriceManager>();
				container.Register<ICommentManager, CommentManager>();
				container.Verify();
				DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
			if (cookie != null && cookie.Value != null)
			{
				Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie.Value);
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value);
			}
			else
			{
				Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
				Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
			}
		}
	}
}
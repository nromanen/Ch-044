using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BAL;
using BAL.Interface;
using BAL.Manager;
using DAL;
using DAL.Interface;
using log4net;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System.Globalization;
using System.Threading;
using BAL.Manager.ParseManagers;

namespace WebApp
{
    public class MvcApplication : HttpApplication
    {
        static readonly ILog Logger = LogManager.GetLogger("RollingLogFileAppender");
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
                container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Singleton);
                container.Register<IUserManager, UserManager>();
                container.Register<ICategoryManager, CategoryManager>();
                container.Register<IPhoneManager, PhoneManager>();
                container.Register<IPhoneParseManager, PhoneParseManager>();
                container.Register<IFridgeManager, FridgeManager>();
                container.Register<IFridgeParseManager, FridgeParseManager>();
                container.Register<ITVManager, TVManager>();
                container.Register<ITVParseManager, TVParseManager>();
                container.Register<ILaptopParseManager, LaptopParseManager>();
                container.Register<ILaptopManager, LaptopManager>();
                container.Register<ITapeRecorderParseManager, TapeRecorderParseManager>();
                container.Register<ITapeRecorderManager, TapeRecorderManager>();
                container.Register<IPropertyManager, PropertyManager>();
                container.Register<IWebShopManager, WebShopManager>();
                container.Register<IDownloadManager, DownloadManager>();
                container.Register<IRoleManager, RoleManager>();
                container.Register<IParserTaskManager, ParserTaskManager>();

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

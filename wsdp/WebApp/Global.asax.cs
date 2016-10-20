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

        private void  InjectorContainer()
        {
            try
            {
                var container = new Container();
                container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Singleton);
                container.Register<IUserManager, UserManager>();
                container.Verify();
                DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}

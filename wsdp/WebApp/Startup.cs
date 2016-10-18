using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(WebApp.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }

    }
}
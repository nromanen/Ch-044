using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tor;
using Tor.Config;


namespace SiteProcessor
{
    public class SiteDownloader
    {
        private const int CONTROL_PORT = 9051;
        private const string PATH = @"D:\TORNET\Example\Tor\Tor\tor.exe";

        protected static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");
        static Client client = null;

        public SiteDownloader()
        {
            if (client == null)
            {
                ClientCreateParams createParameters = new ClientCreateParams();
                createParameters.ConfigurationFile = "";
                createParameters.ControlPassword = "";
                createParameters.ControlPort = CONTROL_PORT;
                createParameters.DefaultConfigurationFile = "";
                createParameters.Path = PATH;

                createParameters.SetConfig(ConfigurationNames.AvoidDiskWrites, true);
                createParameters.SetConfig(ConfigurationNames.GeoIPFile, Path.Combine(Environment.CurrentDirectory, @"Tor\Data\Tor\geoip"));
                createParameters.SetConfig(ConfigurationNames.GeoIPv6File, Path.Combine(Environment.CurrentDirectory, @"Tor\Data\Tor\geoip6"));

                client = Client.Create(createParameters);

            }
        }


        public bool DownloadSite(string url, string path)
        {
            //Process[] previous = Process.GetProcessesByName("tor");


            //if (previous != null && previous.Length > 0)
            //{

            //    foreach (Process process in previous)
            //        process.Kill();
            //}

            PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService();
            service.AddArguments(new string[] {
                "--proxy-type=socks5","--proxy=127.0.0.1:9050"
            });
            using (IWebDriver driver = new PhantomJSDriver(service))
            {
                try
                {
                    driver.Navigate().GoToUrl(url);

                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.Write(driver.PageSource);
                        }
                    }

                    return true;

                }
                catch(Exception ex)
                {
                    logger.Error(ex.Message);
                    return false;
                }

            }
        }

        public string GetPageSouce(string url)
        {
            PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService();
            service.AddArguments(new string[] {
                "--proxy-type=socks5","--proxy=127.0.0.1:9050"
            });
            using (IWebDriver driver = new PhantomJSDriver(service))
            {
                try
                {
                    driver.Navigate().GoToUrl(@"http://2ip.ru");
                    return driver.PageSource;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return null;
                }

            }
        }


    }
}

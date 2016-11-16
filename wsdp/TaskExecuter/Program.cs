using SiteProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskExecuter
{
    class Program
    {
        static void Main(string[] args)
        {
            SiteDownloader downloader = new SiteDownloader();

            Console.WriteLine(downloader.GetPageSouce(@"http://2ip.ru"));

            Console.ReadLine();
        }
    }
}

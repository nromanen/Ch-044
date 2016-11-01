using BAL.Interface;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
    public class DownloadManager : IDownloadManager
    {
        public void DownloadFromPath(string url)
        {
            string htmlSource;
            var encoding = Encoding.GetEncoding(1252);
            using (var client = new WebClient())
            {
                client.Encoding = encoding;
                htmlSource = client.DownloadString(url);
            }
            this.ReplaceHrefs(htmlSource, url);

            //return replaced html source
        }
        public void WriteToFile(string source, string url)
        {
            //string path = @"D:\" + linkName;
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}

            //return "true" html source
            var path = @"D:\softserve\page1.html";
            var encoding = Encoding.GetEncoding(1251);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.Default))
                {
                    w.WriteLine(source);
                }
            }
        }
        public string ReplaceHrefs(string source, string url)
        {
            var doc = new HtmlDocument();
            
            doc.LoadHtml(source);

            var hrefs = doc.DocumentNode.Descendants("link").Where(d => d.Attributes.Contains("href")).ToList();

            foreach (var href in hrefs)
            {
                var link = href.Attributes["href"].Value;
                if (!link.Contains("http"))
                {
                    string[] vals = url.Split('/');
                    var siteName = vals[2];
                    var fullLink = siteName + link;

                    source = source.Replace(link, fullLink);
                }
            }
            this.WriteToFile(source, url);

            return source;
        }
    }
}

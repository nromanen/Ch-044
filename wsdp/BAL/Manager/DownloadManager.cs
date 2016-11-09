using BAL.Interface;
using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BAL.Manager
{
    public class DownloadManager : IDownloadManager
    {
        public Guid guid = Guid.NewGuid();

        public Guid DownloadFromPath(string url)
        {
            string htmlSource;
            var encoding = Encoding.UTF8;
            using (var client = new WebClient())
            {
                client.Encoding = encoding;
                htmlSource = client.DownloadString(url);
            }
            this.ReplaceHrefs(htmlSource, url);

            return guid;
        }

        public void WriteToFile(string source)
        {
            string file = guid + ".html";
            string path = AppDomain.CurrentDomain.BaseDirectory + "WebSites\\" + file;

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs))
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
                    var siteName = "http://" + vals[2];
                    var fullLink = siteName + link;

                    source = source.Replace(link, fullLink);
                }
            }
            this.WriteToFile(source);
            return source;
        }
    }
}
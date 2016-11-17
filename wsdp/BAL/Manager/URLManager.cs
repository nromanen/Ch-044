using BAL.Interface;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
    public class URLManager : IURLManager
    {
        public List<string> GetUrls(string url, string xpath)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            var links = doc.DocumentNode.SelectNodes(xpath + "/@href").Select(t => t.Attributes.SingleOrDefault(d => d.Name == "href").Value).ToList();
            var newLinkList = new List<string>();

            foreach (var link in links)
            {
                if (!link.Equals("#"))
                {
                    if (!link.Contains("http"))
                    {
                        string[] vals = url.Split('/');
                        var siteName = "http://" + vals[2];

                        var newLink = siteName + link;

                        newLinkList.Add(newLink);
                    }
                    else
                    {
                        newLinkList.Add(link);
                    }
                }
            }

            return newLinkList;
        }
    }
}

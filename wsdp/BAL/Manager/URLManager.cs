using BAL.Interface;
using DAL.Interface;
using HtmlAgilityPack;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
    public class URLManager : BaseManager, IURLManager
    {
        public URLManager(IUnitOfWork uOw)
            : base(uOw)
        {
        }
        /// <summary>
        /// get all urls from page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<string> GetAllUrls(IteratorSettingsDTO model)
        {
            var allUrls = new List<string>();
            try
            {
                int from = model.From;
                int to = model.To;


                var xpathList = model.GoodsIteratorXpathes;

                string url = model.UrlMask;

                foreach (var xpath in xpathList)
                {
                    List<string> urlFromOnePage = new List<string>();
                    for (int i = from; i <= to; i++)
                    {
                        urlFromOnePage = GetUrlsFromOnePage(url.Replace("{n}", i.ToString()), xpath);
                        if (urlFromOnePage.Count != 0)
                        {
                            allUrls.AddRange(urlFromOnePage);
                            break;
                        }
                        break;
                    }
                    if (urlFromOnePage.Count != 0)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return allUrls;
        }
        /// <summary>
        /// get urls from one page
        /// </summary>
        /// <param name="url"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public List<string> GetUrlsFromOnePage(string url, string xpath)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            var newLinkList = new List<string>();

            try
            {
                var links = doc.DocumentNode.SelectNodes(xpath + "/@href").Select(t => t.Attributes.SingleOrDefault(d => d.Name == "href").Value).ToList();
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
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return newLinkList;
            }

            return newLinkList;
        }
    }
}

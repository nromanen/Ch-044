using BAL.Interface;
using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DAL.Interface;

namespace BAL.Manager
{
	public class DownloadManager : BaseManager, IDownloadManager
	{
		public IHtmlValidator htmlValidator;

		public Guid guid = Guid.NewGuid();

		public DownloadManager(IUnitOfWork uOW,IHtmlValidator htmlValidator) : base(uOW)
		{
			this.htmlValidator = htmlValidator;
		}

		/// <summary>
		/// Download page from url
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public Guid DownloadFromPath(string url)
		{
			try
			{
				string htmlSource;
				var encoding = Encoding.UTF8;
				using (var client = new WebClient())
				{
					client.Encoding = encoding;
					htmlSource = client.DownloadString(url);
				}
				guid = Guid.NewGuid();
				this.ReplaceHrefs(htmlSource, url);
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
			}


			return guid;
		}
		/// <summary>
		/// write to file html source
		/// </summary>
		/// <param name="source"></param>
		public void WriteToFile(string source)
		{
			try
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
			catch (Exception ex)
			{
				logger.Error(ex.Message);
			}

		}
		/// <summary>
		/// method for replacingnot full links
		/// </summary>
		/// <param name="source"></param>
		/// <param name="url"></param>
		/// <returns></returns>
		public string ReplaceHrefs(string source, string url)
		{
			try
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
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
			}
			return source;
		}
	}
}
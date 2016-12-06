using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Interface;
using HtmlAgilityPack;

namespace BAL.Manager {
	public class PreviewManager : IPreviewManager {

		public string GetPreviewForOneInput(string url, string xpath) {
			HtmlWeb web = new HtmlWeb();
			HtmlDocument doc = web.Load(url);
			return doc.DocumentNode.SelectSingleNode(xpath).InnerHtml;
		}
	}
}

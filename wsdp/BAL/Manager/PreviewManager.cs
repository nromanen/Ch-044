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
			if (xpath != "" && xpath != null) {
				var node = doc.DocumentNode.SelectSingleNode(xpath);

				if (node != null) {

					if (node.Attributes["src"] != null) {
						return node.Attributes.SingleOrDefault(t => t.Name == "src").Value;
					}

					if (node.InnerHtml == "") {
						return "Can`t get Value";
					}
					return node.InnerHtml.Trim();

				} else
					return "Can`t get Value";

			} else { return "Input Xpath"; }
		}
	}
}

using BAL.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TidyManaged;

namespace BAL.Manager
{
	public class HtmlValidator : IHtmlValidator
	{
		public string CheckHtml(string html)
		{
			var htmlstream = new MemoryStream(Encoding.UTF8.GetBytes(html));
			using (var doc = Document.FromStream(htmlstream))
			{
				doc.InputCharacterEncoding = EncodingType.Utf8;
				doc.OutputCharacterEncoding = EncodingType.Utf8;
				doc.CharacterEncoding = EncodingType.Utf8;
				doc.OutputBodyOnly = AutoBool.No;
				doc.DropEmptyElements = false;
				doc.DropEmptyParagraphs = false;
				doc.DropFontTags = false;
				doc.DropProprietaryAttributes = false;
				doc.ForceOutput = true;
				doc.DropEmptyElements = false;
				doc.DropEmptyParagraphs = false;
				doc.DropFontTags = false;
				doc.DropProprietaryAttributes = false;
				doc.Quiet = true;
				doc.CleanAndRepair();
				return doc.Save();
			}
			
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XmlSearcher {
	class Program {
		static void Main(string[] args) {
			string path = @"D:\Folder\";
			XmlParser xml = new XmlParser();
			xml.GetXmlSize(path);
			Console.ReadKey();
		}
	}
}

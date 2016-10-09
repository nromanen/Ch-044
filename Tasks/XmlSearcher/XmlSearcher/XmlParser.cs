using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XmlSearcher {
	class  XmlParser {
		public void GetXmlSize(params string[] pathes) {
			foreach(string path in pathes) {
				Thread thread = new Thread(GetXml);
				Console.WriteLine("New thread and it HashCode - " + thread.GetHashCode());
				thread.Start(path);
				thread.Join();
			}
		}
		public void GetXml(object str) {
			string path = (string)str;
			DirectoryInfo mainFolder = new DirectoryInfo(path);
			DirectoryInfo[] folders = mainFolder.GetDirectories();
			FileInfo[] files = mainFolder.GetFiles("*.xml");
			foreach (FileInfo fl in files) {
				Console.WriteLine(path);
				Console.WriteLine(fl.Name + "-" + fl.Length + " bites");
			}
			foreach(var directoryFiles in folders) {
				GetXmlSize(directoryFiles.FullName);
			}
			
		}
	}
}

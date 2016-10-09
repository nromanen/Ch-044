using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SearcherWithThreads {
	class Program {

		static void Main(string[] args) {
			SearcherWithThreads Swt = new SearcherWithThreads();
			string path = @"D:\Programming\SoftServe\Repository\Tasks\Tastk1_NormalVersion";
			var countList = Swt.Searcher(path);
			foreach (var item in countList) {
				Console.WriteLine(item);
			}
			Console.ReadKey();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SearcherWithThreads {
	class Program {

		static void Main(string[] args) {
			SearcherWithThreads Swt = new SearcherWithThreads();
			string path = @"D:\Taxi", format = "*.cs", text = "!=";
			int countOfThreads = 10;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var countList = Swt.Searcher(path, format, text, countOfThreads);
			int count = 0;
			foreach (var item in countList) {
				Console.WriteLine(item);
				count += item.countOfSigns;
			}
			Console.WriteLine(count);
			sw.Stop();
			Console.WriteLine("Miliseconds: " + sw.ElapsedMilliseconds);
			Console.ReadKey();
		}
	}
}

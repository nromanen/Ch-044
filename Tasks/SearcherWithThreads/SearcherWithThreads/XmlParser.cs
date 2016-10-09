using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SearcherWithThreads {

	class SearcherWithThreads {
		public List<string> GetPathes(string folderPath) {
			List<string> pathes = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories).ToList();
			return pathes;
		}

		public List<Counter> Searcher(string path) {
			var maxThreads = 5;
			List<Counter> counters = new List<Counter>();
			var files = GetPathes(path);
			Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = maxThreads }, (filePath) => {
				var text = File.ReadAllText(filePath);
				Counter c = new Counter() {
					countofConsolas = Regex.Matches(text, "Console").Count,
					countofAssignment = Regex.Matches(text, "=").Count,
					ThreadId = Thread.CurrentThread.ManagedThreadId,
					filePath = filePath
				};
				counters.Add(c);
			});
			return counters;
		}
	}

}


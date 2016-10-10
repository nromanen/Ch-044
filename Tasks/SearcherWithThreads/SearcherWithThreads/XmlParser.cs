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
		public List<string> GetPathes(string folderPath, string fileType) {
			List<string> pathes = Directory.GetFiles(folderPath, fileType, SearchOption.AllDirectories).ToList();
			return pathes;
		}

		public List<Counter> Searcher(string path, string fileType, string sign, int countThreads = 5) {
			List<Counter> counters = new List<Counter>();
			var files = GetPathes(path, fileType);
			Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = countThreads }, (filePath) => {
				var text = File.ReadAllText(filePath);
				Counter c = new Counter() {
					countOfSigns = Regex.Matches(text, sign).Count,
					ThreadId = Thread.CurrentThread.ManagedThreadId,
					filePath = filePath
				};
				counters.Add(c);
			});
			return counters;
		}
	}

}


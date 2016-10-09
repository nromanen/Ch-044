using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherWithThreads {
	class Counter {
		public int countofConsolas;
		public int countofAssignment;
		public int ThreadId;
		public string filePath;

		public override string ToString() {
			var builder = new StringBuilder();
			builder.AppendLine($"Consolas - {countofConsolas}");
			builder.AppendLine($"Assignment - {countofAssignment}");
			builder.AppendLine($"ThreadId - {ThreadId}");
			builder.AppendLine($"FilePath - {filePath}");
			return builder.ToString();
		}
	}
}

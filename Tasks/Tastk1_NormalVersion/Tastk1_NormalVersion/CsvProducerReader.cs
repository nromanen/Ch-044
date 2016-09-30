using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using FileHelpers;

namespace Tastk1_NormalVersion {
	class CsvProducerReader {
		public static IEnumerable<Producer> ReadFromFile(string fileName, string pattern) {
			using (var reader = new StreamReader(File.OpenRead(fileName))) {
				var letterRegex = new Regex(pattern, RegexOptions.Compiled);
				List<Producer> producers = new List<Producer>();
				int counter = 0;
				reader.ReadLine();
				while (!reader.EndOfStream) {
					counter++;
					bool isValid = true;
					string line = reader.ReadLine();
					string[] splitedLine = line.Split(';');
					Producer producer = new Producer();

					if (splitedLine.Length != 3) {
						Console.WriteLine("Does not match the number of segments on line number {0} in file Producers", counter);
						continue;
					}

					int Id;
					if (Int32.TryParse(splitedLine[0], out Id)) {
						producer.Id = Id;
					} else {
						Console.WriteLine(@"Error is in the colomn 'ID' in file Producer, on the line {0}", counter);
						isValid = false;
					}

					if (letterRegex.IsMatch(splitedLine[1])) {
						producer.Name = splitedLine[1];
					} else {
						Console.WriteLine(@"Error is in the colomn 'Name' in file Producer, on the line {0}", counter);
						isValid = false;
					}

					if (letterRegex.IsMatch(splitedLine[2]))
						producer.Country = splitedLine[2];
					else {
						Console.WriteLine(@"Error is in the colomn 'Country' in file Producer, on the line {0}", counter);
						isValid = false;
					}

					if (isValid) {
						producers.Add(producer);
					}
				}
				return producers;
			}
		}

		public static void WriteInFile(string fileName, IEnumerable<Producer> producers) {
			FileHelperEngine engine = new FileHelperEngine(typeof(Producer));
			engine.WriteFile(fileName, producers);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using FileHelpers;

namespace Tastk1_NormalVersion
{
    class CsvCategoryReader
    {
		public static IEnumerable<Category> ReadFromFile(string fileName, string pattern) {
			using (var reader = new StreamReader(File.OpenRead(fileName))) {
				var letterRegex = new Regex(pattern, RegexOptions.Compiled);
				List<Category> categories = new List<Category>();
				int counter = 0;
				reader.ReadLine();
				while (!reader.EndOfStream) {
					counter++;
					bool isValid = true;
					string line = reader.ReadLine();
					string[] splitedLine = line.Split(';');
					Category category = new Category();

					if (splitedLine.Length != 2) {
						Console.WriteLine("Does not match the number of segments on line number {0} in file Category", counter);
						continue;
					}

					int Id;
					if (Int32.TryParse(splitedLine[0], out Id)) {
						category.Id = Id;
					} else {
						Console.WriteLine(@"Error is in the colomn 'ID' in file Category, on the line {0}", counter);
						isValid = false;
					}

					if (letterRegex.IsMatch(splitedLine[1])) {
						category.Name = splitedLine[1];
					} else {
						Console.WriteLine(@"Error is in the colomn 'Name' in file Category, on the line {0}", counter);
						isValid = false;
					}

					if (isValid) {
						categories.Add(category);
					}
				}
				return categories;
			}
		}

		public static void WriteInFile(string fileName, IEnumerable<Category> categories) {
			FileHelperEngine engine = new FileHelperEngine(typeof(Category));
			engine.WriteFile(fileName, categories);
		}
	}
}

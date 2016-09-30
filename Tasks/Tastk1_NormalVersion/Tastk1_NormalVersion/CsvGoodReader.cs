using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FileHelpers;

namespace Tastk1_NormalVersion
{
    class CsvGoodReader
    {
		public static IEnumerable<Good> ReadFromFile(string fileName, string pattern, IEnumerable<Category> categories, IEnumerable<Producer> producers) {
			using (var reader = new StreamReader(File.OpenRead(fileName))) {
				var letterRegex = new Regex(pattern, RegexOptions.Compiled);
				List<Good> goods = new List<Good>();
				int counter = 0;
				reader.ReadLine();
				while (!reader.EndOfStream) {
					counter++;
					bool isValid = true;
					string line = reader.ReadLine();
					string[] splitedLine = line.Split(';');
					Good good = new Good();

					if (splitedLine.Length != 5) {
						Console.WriteLine("Does not match the number of segments on line number {0} in file Goods", counter);
						isValid = false;
						continue;
					}

					int Id;
					if (Int32.TryParse(splitedLine[0], out Id)) {
						good.Id = Id;
					} else {
						Console.WriteLine(@"Error is in the colomn 'ID' in file Goods, on the line {0}", counter);
						isValid = false;
					}

					if (letterRegex.IsMatch(splitedLine[1])) {
						good.Name = splitedLine[1];
					} else {
						Console.WriteLine(@"Error is in the colomn 'Name' in file Goods, on the line {0}", counter);
						isValid = false;
					}

					decimal money;
					if (Decimal.TryParse(splitedLine[2], out money)) {
						good.Price = money;
					} else {
						Console.WriteLine(@"Error is in the colomn 'Price' in file Goods, on the line {0}", counter);
						isValid = false;
					}

					int categoryId;
					if (Int32.TryParse(splitedLine[3], out categoryId)) {
						good.Category = categories.SingleOrDefault(p => p.Id == Convert.ToInt32(splitedLine[3]));
					} else {
						Console.WriteLine(@"Error is in the colomn 'CategoryId' in file Goods, on the line {0}", counter);
						isValid = false;
					}

					int producerId;
					if (Int32.TryParse(splitedLine[3], out producerId)) {
						good.Producer = producers.SingleOrDefault(p => p.Id == Convert.ToInt32(splitedLine[4]));

					} else {
						Console.WriteLine(@"Error is in the colomn 'ProducerId' in file Goods, on the line {0}", counter);
						isValid = false;
					}

					if (isValid) {
						goods.Add(good);
					}
				}
				return goods;
			}
		}
		public static void WriteInFile(string fileName, IEnumerable<Good> goods) {
			StreamWriter file = new StreamWriter(fileName);
			StringBuilder res = new StringBuilder();
			
			foreach (var item in goods) {
				
				res.Append(item.Id);
				res.Append(";");
				res.Append(item.Name);
				res.Append(";");
				res.Append(item.Price);
				res.Append(";");
				res.Append(item.Category.Id);
				res.Append(";");
				res.Append(item.Producer.Id);
				file.WriteLine(res.ToString());
				res.Clear();
			}
			file.Close();
		}
	}
	}


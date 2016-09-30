using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tastk1_NormalVersion {
	class Program {
		static void Main(string[] args) {
			string pattern = @"^[A-Z][a-zA-Z]+$";
			#region paths for read from file
			string readProdPath = "Producer.csv";
			string readCategoryPath = "Category.csv";
			string readGoodPath = "Goods.csv";
			#endregion
			#region paths for write in file
			string writeProdPath = "WritedProducer.csv";
			string writeCategoryPath = "WritedCategory.csv";
			string writeGoodPath = "WritedGoods.csv";
			#endregion
			#region Read from file and write in console
			var a = CsvProducerReader.ReadFromFile(readProdPath, pattern);
			var b = CsvCategoryReader.ReadFromFile(readCategoryPath, pattern);
			var c = CsvGoodReader.ReadFromFile(readGoodPath, pattern, b, a);
			foreach (var item in c) {
				if (item.Category != null && item.Producer != null)
					Console.WriteLine(item);
			}
			#endregion
			#region Write in new files
			try {
				CsvCategoryReader.WriteInFile(writeCategoryPath, b);
				CsvProducerReader.WriteInFile(writeProdPath, a);
				CsvGoodReader.WriteInFile(writeGoodPath, c);
			}
			catch(Exception ex) {
				Console.WriteLine("Input correct values to write in files");
			}
			#endregion
			Console.ReadKey();
		}
	}
}

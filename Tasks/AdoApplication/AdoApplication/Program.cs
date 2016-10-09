using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoApplication {
	class Program {
		static void Main(string[] args) {
			CategoryManager categoryManager = new CategoryManager();
			ProducerManager prodManager = new ProducerManager();
			GoodManager goodManager = new GoodManager();
			
			var good = goodManager.Get(1);
			good.Category = categoryManager.Get(3);
			good.Producer = prodManager.Get(3);
			goodManager.Update(good);
			Console.ReadKey();
		}
	}
}


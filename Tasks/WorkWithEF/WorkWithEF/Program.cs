using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.UnitOfWork;
using Models;
using WorkWithEF.Serialization;

namespace WorkWithEF {
	class Program {
		static void Main(string[] args) {
			UnitOfWork unit = new UnitOfWork();
			Producer p = new Producer { Id = 1, Name = "BMW", Country = "Germany" };
			Category c = new Category { Id = 1, Name = "Car" };
			Good g = new Good { Id = 1, Name = "M6", Price = 30000m, Producer = p, Category = c };
			XmlSerializer serializer = new XmlSerializer();

			var goods = serializer.Deserializer("Goods.xml");
			unit.Goods.InsertList(goods);
			unit.Save();
		}
	}
}

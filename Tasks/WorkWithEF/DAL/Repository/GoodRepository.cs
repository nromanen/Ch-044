using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using Models;

namespace DAL.Repository {
	public class GoodRepository : IRepository<Good> {
		private BaseDbContext db;

		public GoodRepository(BaseDbContext context) {
			this.db = context;
		}
		public bool Create(Good item) {
			if (!db.Goods.Any(t => t.Name == item.Name && t.Price == item.Price)) {
				db.Goods.Add(item);
				Console.WriteLine("Element was successfuly added in the table - Goods!");
				return true;
			}
			Console.WriteLine("Element wasn`t added in the table - Goods!");
			return false;
		}
		public void AddOrUpdate(Good item) {
			if (!Create(item)) {
				db.Entry<Good>(item).State = EntityState.Modified;
			}
		}
		public bool Delete(int id) {
			Good good = db.Goods.Find(id);
			if (good != null) {
				db.Goods.Remove(good);
				Console.WriteLine($"The element with Id - {id}, was seccessfully deleted from the table - Good! ");
				return true;
			} else { return false; }
		}

		public Good Get(int id) {
			return db.Goods.Find(id);
		}

		public IEnumerable<Good> GetAll() {
			return db.Goods.ToList();
		}

		public void Update(Good item) {
			db.Goods.Attach(item);
			db.Entry(item).State = EntityState.Modified;
			Console.WriteLine($"The element was seccessfully updated in the table - Good! ");
		}

		public void InsertList(List<Good> goods) {
			List<string> catsNames = db.Categories.Select(i => i.Name).ToList();
			List<decimal> goodsPrices = db.Goods.Select(i => i.Price).ToList();
			List<string> prodsNames = db.Producers.Select(i => i.Name).ToList();
			List<string> prodsCountries = db.Producers.Select(i => i.Country).ToList();
			List<string> goodsNames = db.Goods.Select(i => i.Name).ToList();
			var maxg = db.Goods.Select(i => i.Id).DefaultIfEmpty().Max();
			var maxp = db.Producers.Select(i => i.Id).DefaultIfEmpty().Max();
			var maxc = db.Categories.Select(i => i.Id).DefaultIfEmpty().Max();
			foreach (var item in goods) {


				if (!goodsNames.Contains(item.Name)) {
					if (!goodsPrices.Contains(item.Price)) {
						if (maxg != 0)
							item.Id = ++maxg;
						else {
							maxg = 0;
						}

						if (catsNames.Contains(item.Category.Name)) {
							item.CategoryId = item.Category.Id;
							item.Category = null;
						} else {

							if (maxc != 0)

								item.Category.Id = ++maxc;
							else {
								maxc = 0;
							}
							item.CategoryId = item.Category.Id;
						}

						if (prodsNames.Contains(item.Producer.Name)) {

							if (prodsCountries.Contains(item.Producer.Country)) {
								item.ProducerId = item.Producer.Id;
								item.Producer = null;
							}
						} else {
							if (maxp != 0)

								item.Producer.Id = ++maxp;
							else {
								maxp = 0;
							}
							item.ProducerId = item.Producer.Id;

						}
						Create(item);
					}
				}
			}
		}
	}
}

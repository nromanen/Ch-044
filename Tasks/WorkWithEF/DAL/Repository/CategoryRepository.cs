using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using Models;

namespace DAL.Repository {
	public class CategoryRepository : IRepository<Category> {
		private BaseDbContext db;

		public CategoryRepository(BaseDbContext context) {
			this.db = context;
		}
		public bool Create(Category item) {
			if (!db.Categories.Any(t => t.Id == item.Id)) {
				db.Categories.Add(item);
				Console.WriteLine("Element was successfully added in the table - Category!");
				return true;
			}
			return false;
		}
		public void AddOrUpdate(Category item) {
			if (!Create(item)) {
				db.Entry<Category>(item).State = EntityState.Modified;
				Console.WriteLine("Element wasn`t added in the table - Category!");
			}
		}
		public bool Delete(int id) {
			Category category = db.Categories.Find(id);
			if (category != null) {
				db.Categories.Remove(category);
				Console.WriteLine($"The element with Id - {id}, was seccessfully deleted from the table - Category! ");
				return true;
			} else { return false; }
		}

		public Category Get(int id) {
			return db.Categories.Find(id);

		}

		public IEnumerable<Category> GetAll() {
			return db.Categories.ToList();
		}

		public void Update(Category item) {
			db.Entry<Category>(item).State = EntityState.Modified;
			Console.WriteLine($"The element was seccessfully updated in the table - Category! ");
		}
	}
}

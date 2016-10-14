using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using Models;

namespace DAL.Repository {
	public class ProducerRepository : IRepository<Producer> {
		private BaseDbContext db;

		public ProducerRepository(BaseDbContext context) {
			this.db = context;
		}
		public bool Create(Producer item) {
			if (!db.Producers.Any(t => t.Id == item.Id)) {
				db.Producers.Add(item);
				Console.WriteLine("Element was successfuly added in the table - Producer!");
				return true;
			}
			return false;
		}
		public void AddOrUpdate(Producer item) {
			if (!Create(item)) {
				db.Entry<Producer>(item).State = EntityState.Modified;
				Console.WriteLine("Element wasn`t added in the table - Producer!");
			}
		}
		public bool Delete(int id) {
			Producer producer = db.Producers.Find(id);
			if (producer != null) {
				db.Producers.Remove(producer);
				Console.WriteLine($"The element with Id - {id}, was seccessfully deleted from the table - Producer! ");
				return true;
			} else { return false; }
		}

		public Producer Get(int id) {
			return db.Producers.Find(id);
		}

		public IEnumerable<Producer> GetAll() {
			return db.Producers.ToList();
		}

		public void Update(Producer item) {
			db.Entry<Producer>(item).State = EntityState.Modified;
			Console.WriteLine($"The element was seccessfully updated in the table - Producer! ");
		}
	}
}

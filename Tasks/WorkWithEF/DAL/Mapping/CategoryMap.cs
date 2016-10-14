using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL.Mapping {
	public class CategoryMap : EntityTypeConfiguration<Category> {
		public CategoryMap() {
			this.ToTable("Category");

			this.HasKey(t => t.Id);
			this.Property(t => t.Name).IsRequired().HasMaxLength(10);

			this.HasMany(t => t.Goods).WithRequired().HasForeignKey(t => t.CategoryId);
		}
	}
}

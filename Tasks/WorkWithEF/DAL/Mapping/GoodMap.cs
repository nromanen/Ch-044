using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL.Mapping {
	public class GoodMap : EntityTypeConfiguration<Good> {
		public GoodMap() {
			this.ToTable("Good");

			this.HasKey(t => t.Id);
			this.Property(t => t.Name).IsRequired().HasMaxLength(10);
			this.Property(t => t.Price).IsRequired();
			
			this.HasRequired(t => t.Producer).WithMany().HasForeignKey(t => t.ProducerId);
			this.HasRequired(t => t.Category).WithMany().HasForeignKey(t => t.CategoryId);
		}
	}
}

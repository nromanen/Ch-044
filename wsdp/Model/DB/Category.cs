using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DB
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public ICollection<Category> ChildrenCategory { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}

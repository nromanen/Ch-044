using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public List<Property> Properties { get; set; }
        public int OrderNo { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public ICollection<CategoryDTO> ChildrenCategory { get; set; }
        public ICollection<PropertyDTO> PropertyList { get; set; }
    }
}

using System.Collections.Generic;

namespace Model.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public ICollection<CategoryDTO> ChildrenCategory { get; set; }
        public ICollection<PropertyDTO> PropertiesList { get; set; }
        public int OrderNo { get; set; }

        /// <summary>
        /// Answers if there are children categories for current one
        /// </summary>
        public bool HasChildrenCategories
        {
            get { return ChildrenCategory != null && ChildrenCategory.Count > 0; }
        }

        /// <summary>
        /// Answers if there are properties for current category
        /// </summary>
        public bool HasProperties
        {
            get { return PropertiesList != null && PropertiesList.Count > 0; }
        }
    }
}
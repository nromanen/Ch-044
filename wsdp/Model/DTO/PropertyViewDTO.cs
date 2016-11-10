using System.Collections.Generic;

namespace Model.DTO
{
    public class PropertyViewDTO
    {
        public List<PropertyDTO> properties { get; set; }
        public List<CategoryDTO> categories { get; set; }
        public List<string> enums { get; set; }
        public int CategoryId { get; set; }
        public int PropertyId { get; set; }
    }
}
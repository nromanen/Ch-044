using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class PropertyViewDTO
    {
        public List<PropertyDTO> properties { get; set; }
        public List<CategoryDTO> categories { get; set; }
        public List<string> enums { get; set; }
        public int CategoryId { get; set; }
    }
}

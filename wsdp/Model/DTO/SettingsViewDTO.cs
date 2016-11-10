using System.Collections.Generic;

namespace Model.DTO
{
    public class SettingsViewDTO
    {
        public List<CategoryDTO> Categories { get; set; }
        public List<WebShopDTO> Shops { get; set; }
        public ParserTaskDTO ParserTask { get; set; }
    }
}
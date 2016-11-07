using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class SettingsViewDTO
    {
        public List<CategoryDTO> Categories { get; set; }
        public List<WebShopDTO> Shops { get; set; }
    }
}

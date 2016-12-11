using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class CheckGoodDTO
    {
        public List<CategoryDTO> Categories { get; set; }
        public List<ParserTaskDTO> ParserTasks { get; set; }
    }
}

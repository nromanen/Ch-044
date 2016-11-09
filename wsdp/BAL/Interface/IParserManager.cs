using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IParserTaskManager
    {
        ParserTaskDTO Get(int id);
        int Add(ParserTaskDTO parser);
        bool Delete(int id);
        ParserTaskDTO Update(ParserTaskDTO parser);
        List<ParserTaskDTO> GetAll();
    }
}

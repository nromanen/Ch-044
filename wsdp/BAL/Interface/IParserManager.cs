using Model.DTO;
using System.Collections.Generic;

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
using Model.DB;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface ILaptopParseManager
    {
        IEnumerable<Good> ParseAll(string path);
    }
}
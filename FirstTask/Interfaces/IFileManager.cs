using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    interface IFileManager<T> where T: IEnumerable
    {
        T ReadFromCsv(string path);
        void WriteToCsv(T list, string path);

        void WriteToXml(T list, string path);
        
        T ReadFromXml(string path);

    }
}

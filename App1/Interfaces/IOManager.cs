using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public interface IOManager<T> where T : IEnumerable
    {
        T ReadFromFile(string path);
        void WriteToFile(string path, T list);

    }
}

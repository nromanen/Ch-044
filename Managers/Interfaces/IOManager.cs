using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goods.DbModels;

namespace Goods.Managers
{
    public interface IOManager<T> where T : IEnumerable
    {
        T ReadFromFile(string path);
        void WriteToFile(string path, T list);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    interface IManager<T> where T : class
    {
        void Add(T item);
        void Delete(T item);
        void Update(T item);
        T GetById(int id);
    }
}

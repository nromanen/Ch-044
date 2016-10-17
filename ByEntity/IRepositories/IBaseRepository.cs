using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByEntity.IRepositories
{
    interface IBaseRepository<T> where T:class
    {
        ICollection<T> GetAll();
        T Get(object id);       
        bool Delete(T item);
        void Add(T item);
        bool Update(T item, object key);     


    }
}

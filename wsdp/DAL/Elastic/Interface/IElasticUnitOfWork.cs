using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Elastic.Interface
{
    public interface IElasticUnitOfWork
    {
        IElasticGoodRepository Repository { get; }
        int Save();
    }
}

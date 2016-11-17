using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskExecuting.Interface
{
    public interface ITaskExecuter
    {
        void ExecuteTask( int parsertaskid, string url);
    }
}

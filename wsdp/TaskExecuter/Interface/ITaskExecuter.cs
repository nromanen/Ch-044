using Model.DTO;

namespace TaskExecuting.Interface
{
    public interface ITaskExecuter
    {
        GoodDTO ExecuteTask( int parsertaskid, string url);
    }
}

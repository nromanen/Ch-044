using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskExecuting.LogStorage
{
    public class TaskInformation
    {
        static List<TaskInfoDTO> ExecutingTasks;

        public List<TaskInfoDTO> GetTasks()
        {
            if (ExecutingTasks == null)
            {
                ExecutingTasks = new List<TaskInfoDTO>();
            }
            return ExecutingTasks;
        }

        public void SetList(List<TaskInfoDTO> list)
        {
            ExecutingTasks = list;
        }

        internal void AddExecutingTask(TaskInfoDTO taskinfo)
        {
            if (ExecutingTasks == null)
            {
                ExecutingTasks = new List<TaskInfoDTO>();
            }

            ExecutingTasks.Add(taskinfo);
        }

        internal void DeleteExecutingTaskByUrl(string url)
        {
            ExecutingTasks.Remove(ExecutingTasks.Where(c => c.Url == url).FirstOrDefault());
        }
    }
}

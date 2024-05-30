using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model.Models;

namespace WebApplication1.Repository.Repositories
{
    public interface ITasksInterface
    {
        TaskModel AddTask(TaskModel taskModel);

        TaskModel GetTask(int id);

        TaskModel EditTask(TaskModel taskModel);

        List<TaskModel> GetTasksList(int id);

        List<TasksAssignedModel> GetDetails(int id);

        int TasksCreated(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model.DBContext;
using WebApplication1.Model.Models;

namespace WebApplication1.Helper.Helpers
{
    public class TasksHelper
    {
        public static tasks CustomToDB(TaskModel taskModel)
        {
            try
            {
                tasks task = new tasks();
                if (taskModel != null)
                {
                    task.taskId = taskModel.taskId;
                    task.taskName = taskModel.taskName;
                    task.description = taskModel.taskDescription;
                    task.deadline = taskModel.deadline;
                    task.creatorId = taskModel.creatorId;
                }
                return task;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static TaskModel DBToCustom(tasks task)
        {
            try
            {
                TaskModel taskModel = new TaskModel();
                if (task != null)
                {
                    taskModel.taskId = task.taskId;
                    taskModel.taskName = task.taskName;
                    taskModel.taskDescription = task.description;
                    taskModel.deadline = (DateTime)task.deadline;
                    taskModel.creatorId = (int)task.creatorId;
                }
                return taskModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TaskModel> TaskList(List<tasks> taskList)
        {
            try
            {
                List<TaskModel> taskModels = new List<TaskModel>();
                if (taskList != null)
                {
                    foreach (tasks t in taskList)
                    {
                        TaskModel taskModel = new TaskModel();
                        taskModel.taskId = t.taskId;
                        taskModel.taskName = t.taskName;
                        taskModel.taskDescription = t.description;
                        taskModel.deadline = (DateTime)t.deadline;
                        taskModel.creatorId = (int)t.creatorId;

                        taskModels.Add(taskModel);
                    }
                }
                return taskModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Helper.Helpers;
using WebApplication1.Model.DBContext;
using WebApplication1.Model.Models;
using WebApplication1.Repository.Repositories;

namespace WebApplication1.Repository.Services
{
    public class TasksService : ITasksInterface
    {
        private TaskManagementEntities DBContext = new TaskManagementEntities();

        public TaskModel AddTask(TaskModel taskModel)
        {
            try
            {
                tasks task = TasksHelper.CustomToDB(taskModel);
                DBContext.tasks.Add(task);
                DBContext.SaveChanges();
                return taskModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TaskModel GetTask(int id)
        {
            tasks task = DBContext.tasks.Find(id);
            TaskModel taskModel = TasksHelper.DBToCustom(task);
            return taskModel;
        }

        public List<TaskModel> GetTasksList(int id)
        {
            try
            {
                List<tasks> taskList = DBContext.tasks.Where(t => t.creatorId == id).ToList();
                return TasksHelper.TaskList(taskList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TaskModel EditTask(TaskModel taskModel)
        {
            tasks task = TasksHelper.CustomToDB(taskModel);
            DBContext.Entry(task).State = System.Data.Entity.EntityState.Modified;
            DBContext.SaveChanges();
            return taskModel;
        }

        public List<TasksAssignedModel> GetDetails(int id)
        {
            try
            {
                tasks task = DBContext.tasks.Find(id);
                List<TasksAssignedModel> taskAssignedList = new List<TasksAssignedModel>();
                List<assignment> assignments = DBContext.assignment.ToList();

                foreach (assignment a in assignments)
                {
                    if (id == a.taskId)
                    {
                        TasksAssignedModel m = new TasksAssignedModel();
                        DateTime lastDate = (DateTime)task.deadline;
                        students student = DBContext.students.FirstOrDefault(s => s.studentId == a.studentId);
                        m.studentName = student.username;
                        if (a.status == 1)
                        {
                            m.status = "Submitted";
                        }
                        else if (a.status == 0 && lastDate < DateTime.Now)
                        {
                            m.status = "Delayed";
                        }
                        else
                        {
                            m.status = "Pending";
                        }

                        taskAssignedList.Add(m);
                    }
                }

                return taskAssignedList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TasksCreated(int id)
        {
            int created = 0;
            List<tasks> taskList = DBContext.tasks.Where(t => t.creatorId == id).ToList();
            created = taskList.Count();
            return created;
        }
    }
}

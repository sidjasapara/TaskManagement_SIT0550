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
    public class StudentsService : IStudentsInterface
    {
        private TaskManagementEntities DBContext = new TaskManagementEntities();

        public List<students> GetStudents(int taskId)
        {
            List<students> studentsList = DBContext.students.ToList();
            List<students> list = DBContext.students.ToList();
            List<assignment> assignments = DBContext.assignment.Where(a => a.taskId == taskId).ToList();

            foreach(assignment a in assignments)
            {
                foreach(students s in studentsList)
                {
                    if(a.studentId == s.studentId)
                    {
                        list.Remove(s);
                    }
                }
            }

            return list;
        }

        public List<string> GetTaskName(int taskId)
        {
            List<string> taskDetails = new List<string>();
            try
            {
                if (taskId > 0)
                {
                    tasks task = DBContext.tasks.FirstOrDefault(t => t.taskId == taskId);
                    string name = task.taskName;
                    string description = task.description;
                    DateTime deadline = (DateTime)task.deadline;
                    string deadlineString = deadline.ToString("yyyy-MM-dd");

                    teachers teacher = DBContext.teachers.FirstOrDefault(t => t.teacherId == task.creatorId);
                    string teacherName = teacher.username;

                    taskDetails.Add(name);
                    taskDetails.Add(description);
                    taskDetails.Add(deadlineString);
                    taskDetails.Add(teacherName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return taskDetails;
        }
    }
}

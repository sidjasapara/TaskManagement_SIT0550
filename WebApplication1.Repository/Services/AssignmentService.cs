using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model.DBContext;
using WebApplication1.Model.Models;
using WebApplication1.Repository.Repositories;

namespace WebApplication1.Repository.Services
{
    public class AssignmentService : IAssignmentInterface
    {
        private TaskManagementEntities DBContext = new TaskManagementEntities();

        public bool NewAssignment(int taskId, List<int> studentId)
        {
            bool assignmentAdded = false;
            try
            {
                foreach (int i in studentId)
                {
                    assignment assign = new assignment();
                    assign.studentId = i;
                    assign.taskId = taskId;

                    DBContext.assignment.Add(assign);
                    DBContext.SaveChanges();

                    assignmentAdded = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return assignmentAdded;
        }

        public List<AssignmentModel> MyAssignments(int id)
        {
            List<AssignmentModel> a = new List<AssignmentModel>();
            try
            {
                if (id > 0)
                {
                    List<assignment> assign = DBContext.assignment.Where(i => i.studentId == id).ToList();

                    foreach(assignment i in assign)
                    {
                        AssignmentModel amodel = new AssignmentModel();
                        amodel.assignmentId = i.assignmentId;
                        amodel.taskId = (int)i.taskId;
                        amodel.studentId = (int)i.studentId;
                        amodel.status = (int)i.status;

                        a.Add(amodel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return a;
        }

        public bool UpdateAssignmentStatus(int assignmentId)
        {
            bool updated = false;
            assignment assigned = (assignment)DBContext.assignment.FirstOrDefault(a => a.assignmentId == assignmentId);
            assigned.status = 1;
            DBContext.SaveChanges();
            return updated;
        }

        public int TotalAssignments(int id)
        {
            int total = 0;
            List<assignment> assign = DBContext.assignment.Where(i => i.studentId == id).ToList();
            total = assign.Count();
            return total;
        }

        public int PendingAssignments(int id)
        {
            int pending = 0;
            List<tasks> ls = DBContext.tasks.ToList();
            List<assignment> assign = DBContext.assignment.Where(i => i.studentId == id && i.status == 0).ToList();
            foreach (assignment a in assign)
            {
                foreach(tasks t in ls)
                {
                    if(a.taskId == t.taskId && t.deadline > DateTime.Now)
                    {
                        pending += 1;
                    }
                }
            }
            return pending;
        }

        public int SubmittedAssignments(int id)
        {
            int submitted = 0;
            List<assignment> assign = DBContext.assignment.Where(i => i.studentId == id && i.status == 1).ToList();
            submitted = assign.Count();
            return submitted;
        }

        public int TasksAssigned(int id)
        {
            int n = 0;
            List<tasks> taskList = DBContext.tasks.Where(t => t.creatorId == id).ToList();

            foreach (tasks t in taskList)
            {
                List<assignment> ls = DBContext.assignment.Where(a => a.taskId == t.taskId).ToList();
                if(ls.Count() != 0)
                {
                    n += 1;
                }
            }
            return n;
        }

        public int CountAssignedBy(int id)
        {
            int assigned = 0;
            List<tasks> taskList = DBContext.tasks.Where(t => t.creatorId == id).ToList();

            foreach (tasks t in taskList)
            {
                assigned += DBContext.assignment.Count(a => a.taskId == t.taskId);
            }
            return assigned;
        }

        public int PendingAssigned(int id)
        {
            int pending = 0;
            List<tasks> taskList = DBContext.tasks.Where(t => t.creatorId == id).ToList();

            foreach (tasks t in taskList)
            {
                pending += DBContext.assignment.Count(a => a.taskId == t.taskId && a.status == 0);
            }
            return pending;
        }

        public int CompletedAssigned(int id)
        {
            int completed = 0;
            List<tasks> taskList = DBContext.tasks.Where(t => t.creatorId == id).ToList();

            foreach (tasks t in taskList)
            {
                completed += DBContext.assignment.Count(a => a.taskId == t.taskId && a.status == 1);
            }
            return completed;
        }
    }
}

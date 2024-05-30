using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model.Models;

namespace WebApplication1.Repository.Repositories
{
    public interface IAssignmentInterface
    {
        bool NewAssignment(int taskId, List<int> studentId);

        List<AssignmentModel> MyAssignments(int id);

        bool UpdateAssignmentStatus(int assignmentId);

        int TotalAssignments(int id);

        int PendingAssignments(int id);

        int SubmittedAssignments(int id);

        int TasksAssigned(int id);

        int CountAssignedBy(int id);

        int PendingAssigned(int id);

        int CompletedAssigned(int id);
    }
}

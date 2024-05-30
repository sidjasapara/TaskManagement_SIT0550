using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Model.Models
{
    public class PaginationModel
    {
        public int currentIndex { get; set; }

        public List<TaskModel> taskModels { get; set; }

        public List<AssignmentModel> assignementModels { get; set; }

        public int total { get; set; }

        public List<List<string>> assignedTasks { get; set; }
    }
}

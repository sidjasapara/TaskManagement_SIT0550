using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Model.Models
{
    public class TasksAssignedModel
    {
        [Display(Name = "Student")]
        public string studentName { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }
    }
}

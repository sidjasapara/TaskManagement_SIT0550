using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Model.Models
{
    public class AssignmentModel
    {
        public int assignmentId { get; set; }

        [Required]
        [Display(Name = "Task")]
        public int taskId { get; set; }

        [Required]
        [Display(Name = "Select Student")]
        public int studentId { get; set; }

        public int status { get; set; }
    }
}

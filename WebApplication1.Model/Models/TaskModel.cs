using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Model.Models
{
    public class TaskModel
    {
        [Required]
        [Display(Name = "Task Name")]
        public string taskName { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Task Description")]
        public string taskDescription { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Deadline")]
        public DateTime deadline { get; set; }
        [Display(Name = "Creator ID")]
        public int creatorId { get; set; }

        public int taskId { get; set; }
    }
}

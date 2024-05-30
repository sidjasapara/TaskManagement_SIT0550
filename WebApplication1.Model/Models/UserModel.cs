using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Model.Models
{
    public class UserModel
    {
        [Required]
        public string role { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [Compare("password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string confirmpassword { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string address { get; set; }
        [Required]
        [RegularExpression("^([0-9]{10})$")]
        public string contact { get; set; }
        [Required]
        [Display(Name = "State")]
        public int stateId { get; set; }
        [Required]
        [Display(Name = "City")]
        public int cityId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.ViewModels
{
    public class EditUserDetailsViewModel
    {
        public int UserId { get; set; }
        [Required]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string Name { get;set; }
        [Required]
        public string MobileNumber { get; set; }
    }
}

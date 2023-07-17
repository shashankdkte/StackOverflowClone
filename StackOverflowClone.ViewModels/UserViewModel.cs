using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.ViewModels
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
        public string Name { get; set; }
        public string MobileNumber { get; set; }    
    public string PhoneNumber { get; set; }
        public bool isAdmin { get; set; }
    }
}

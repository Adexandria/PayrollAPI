using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Authentication
{
    public class Signup
    {
        [Required(ErrorMessage = "Enter Firstname"), StringLength(20)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter LastName"), StringLength(20)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Enter Address")]
        public string HomeAddress { get; set; }
       
    }
}

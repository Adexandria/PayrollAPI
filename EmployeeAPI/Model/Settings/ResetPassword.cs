using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.Settings
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "Enter Password")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Enter Password"), StringLength(6), RegularExpression("/^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).\\S{8,15}$/gm")]
        public string NewPassword { get; set; }
    }
}

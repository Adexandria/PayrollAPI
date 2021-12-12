using System.ComponentModel.DataAnnotations;


namespace EmployeeAPI.Model.Authentication
{
    public class Login
    {
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
    }
}

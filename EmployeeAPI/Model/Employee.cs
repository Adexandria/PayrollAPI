using System;

namespace EmployeeAPI.Model
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //Assigned a default password
        public string Password { get; set; } = "Employee";
        //default role for the employee
        public string Role { get; set; } = "User";

    }
}

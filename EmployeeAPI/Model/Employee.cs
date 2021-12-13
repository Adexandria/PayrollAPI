using System;

namespace EmployeeAPI.Model
{
    public class Employee
    {
        public Guid EmployeeID { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Email { get; set; }

        //Assigned a default password
        public string Password { get; set; } = "Employee";
        
        //default role for the employee
        public string Role { get; set; } = "User";

        public string Home_Address { get; set; }

        public string Modified_By { get; set; }
        

    }
}

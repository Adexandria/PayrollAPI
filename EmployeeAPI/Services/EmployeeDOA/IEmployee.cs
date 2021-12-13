using EmployeeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAPI.Services.EmployeeDOA
{
    public interface IEmployee
    {
        public Task AddEmployee(Employee employee);

        public Task<Employee> RetrieveEmployeeById(string id);
        public Task<Employee> RetrieveEmployeeByEmail(string email);
        public Task<Result<Employee>> RetrieveAllEmployees(int pageNo, int pageSize, Employee queryParams);

        public Task UpdateEmployee(Employee employee);
    }
}

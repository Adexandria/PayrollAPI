using EmployeeAPI.Model;
using System;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
    public interface IEmployee
    {
        Employee GetEmployee(string email);
        Employee GetEmployeeById(Guid id);
        Task AddEmployee(Employee employee);
        Task UpdatePassword(Guid id,string password);
    }
}

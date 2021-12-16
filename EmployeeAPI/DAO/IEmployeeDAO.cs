using EmployeeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAPI.DAO
{
    public interface IEmployeeDAO
    {
        public Task<Result<Employee>> AddEmployee(Employee employee);

        public Task<Result<Employee>> RetrieveEmployeeById(Guid id);

        public Task<Result<Employee>> RetrieveEmployeeByEmail(string email);

        public Task<Result<Employee>> RetrieveEmployeeByAddress(string address);

        public Task<Result<Employee>> RetrieveEmployeeByBankName(string bank);

        public Task<Result<Employee>> RetrieveAllEmployees(int pageNo, int pageSize, Employee queryParams);

        public Task<Result<Employee>> UpdateEmployee(Employee employee);

        public Task<Result<Employee>> UpdatePassword(Guid id, string password);
    }
}

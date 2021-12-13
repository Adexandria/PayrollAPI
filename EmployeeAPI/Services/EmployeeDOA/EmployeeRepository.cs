using Dapper;
using EmployeeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAPI.Services.EmployeeDOA
{
    public class EmployeeRepository : DBConfigService, IEmployee
    {
        IConfiguration configuration;
        ILogger<EmployeeRepository> logger;

        public EmployeeRepository (IConfiguration configuration, ILogger<EmployeeRepository> logger) : base(configuration)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task AddEmployee(Employee employee)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("Employee ID", employee.EmployeeID);
                    parameter.Add("First Name", employee.First_Name);
                    parameter.Add("Last Name", employee.Last_Name);
                    parameter.Add("Email Address", employee.Email);
                    parameter.Add("Password", employee.Password);
                    parameter.Add("Id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                    parameter.Add("Return_Val", System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue, size: 1);
                    int dbResult = await conn.ExecuteAsync("dbo.Sp_Add_Employee", parameter, commandType: System.Data.CommandType.StoredProcedure);
                    int x = dbResult;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ":" + ex.StackTrace, ex);
                
            }
        }

        public async Task<Employee> RetrieveEmployeeById(string id)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("Employee_ID", id);
                    parameter.Add("Return_Val", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
                    Employee employee = await conn.QuerySingleAsync<Employee>("[Sp_Retrieve_EmployeeById]", parameter, commandType: System.Data.CommandType.StoredProcedure);
                    return employee;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ":" + ex.StackTrace, ex);
                return null;
            }
        }

        public async Task<Result<Employee>> RetrieveAllEmployees(int pageNo, int pageSize, Employee queryParams)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("Page_Number", pageNo);
                    parameter.Add("Page_Size", pageSize);
                    parameter.Add("Employee ID", queryParams.EmployeeID);
                    parameter.Add("First Name", queryParams.First_Name);
                    parameter.Add("Last Name", queryParams.Last_Name);
                    parameter.Add("Email", queryParams.Email);
                    parameter.Add("No_Of_Records", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                    parameter.Add("Return_Val", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
                    IEnumerable<Employee> Accountes = await conn.QueryAsync<Employee>("[Sp_Retrieve_Employees]", parameter, commandType: System.Data.CommandType.StoredProcedure);

                    Result<Employee> result = new Result<Employee>();
                    result.List = Accountes.AsList();
                    result.PageNumber = pageNo;
                    result.PageSize = pageSize;
                    result.NoOfRecords = parameter.Get<Int32>("No_Of_Records");
                    result.ReturnValue = parameter.Get<Int32>("Return_Val");

                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ":" + ex.StackTrace, ex);
                return null;
            }
        }

        public async Task UpdateEmployee(Employee employee)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("Employee ID", employee.EmployeeID);
                    parameter.Add("First Name", employee.First_Name);
                    parameter.Add("Last Name", employee.Last_Name);
                    parameter.Add("Email", employee.Email);
                    parameter.Add("Home Address", employee.Home_Address);
                    parameter.Add("ModifiedBy", employee.Modified_By);
                    parameter.Add("Password", employee.Password);
                    parameter.Add("Return_Val", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);

                    await conn.ExecuteAsync("Sp_Update_Employee", parameter, commandType: System.Data.CommandType.StoredProcedure);
                }                
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ":" + ex.StackTrace, ex);
               
            }            
        }

        public async Task<Employee> RetrieveEmployeeByEmail(string email)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("Email", email);
                    parameter.Add("Return_Val", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
                    Employee employee = await conn.QuerySingleAsync<Employee>("[Sp_Retrieve_EmployeeByEmail]", parameter, commandType: System.Data.CommandType.StoredProcedure);
                    return employee;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ":" + ex.StackTrace, ex);
                return null;
            }
        }

       
    }
}

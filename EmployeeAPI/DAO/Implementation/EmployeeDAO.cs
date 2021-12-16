using Dapper;
using EmployeeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.DAO.Implementation
{
    public class EmployeeDAO : DBConfigService, IEmployeeDAO
    {
        IConfiguration configuration;
        ILogger<EmployeeDAO> logger;

        public EmployeeDAO (IConfiguration configuration, ILogger<EmployeeDAO> logger) : base(configuration)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<Result<Employee>> AddEmployee(Employee employee)
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
                    var dbResult = await conn.ExecuteAsync("dbo.Sp_Add_Employee", parameter, commandType: System.Data.CommandType.StoredProcedure);

                    Result<Employee> result = new Result<Employee>
                    {
                        IdentityValue = parameter.Get<Int32>("Id"),
                        ReturnValue = parameter.Get<Int32>("Return_Val")
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ":" + ex.StackTrace, ex);
                return null;
            }
        }

        public async Task<Result<Employee>> RetrieveEmployeeById(Guid id)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("Employee_ID", id);
                    parameter.Add("Return_Val", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
                    IEnumerable<Employee> Accountes = await conn.QueryAsync<Employee>("[Sp_Retrieve_EmployeeById]", parameter, commandType: System.Data.CommandType.StoredProcedure);

                    Result<Employee> result = new Result<Employee>();
                    result.Data = Accountes.AsList()[0];
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

        public async Task<Result<Employee>> RetrieveEmployeeByBankName(string bank)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("Bank Name", bank);
                    parameter.Add("Return_Val", direction: System.Data.ParameterDirection.ReturnValue);
                    var dbData = await conn.QueryAsync<Employee>("[Sp_Retrieve_Employee_By_BankName]", parameter, commandType: System.Data.CommandType.StoredProcedure);
                    
                    Result<Employee> result = new Result<Employee>
                    {
                        List = dbData.ToList(),
                        ReturnValue = parameter.Get<Int32>("Return_Val")
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ":" + ex.StackTrace, ex);
                return null;
            }
        }

        public async Task<Result<Employee>> RetrieveEmployeeByEmail(string email)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("Email", email);
                    parameter.Add("Return_Val", direction: System.Data.ParameterDirection.ReturnValue);
                    var dbData = await conn.QueryAsync<Employee>("[Sp_Retrieve_Employee_By_Email]", parameter, commandType: System.Data.CommandType.StoredProcedure);
                    
                    var result = new Result<Employee>
                    {
                        List = dbData.ToList(),
                        ReturnValue = parameter.Get<Int32>("Return_Val")
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ":" + ex.StackTrace, ex);
                return null;
            }
        }

        public async Task<Result<Employee>> RetrieveEmployeeByAddress(string address)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("Address", address);
                    parameter.Add("Return_Val", direction: System.Data.ParameterDirection.ReturnValue);
                    var dbData = await conn.QueryAsync<Employee>("[Sp_Retrieve_Employee_By_Address]", parameter, commandType: System.Data.CommandType.StoredProcedure);

                    var result = new Result<Employee>
                    {
                        List = dbData.ToList(),
                        ReturnValue = parameter.Get<Int32>("Return_Val")
                    };
                    return result;
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

        public Task<Result<Employee>> UpdatePassword(Guid id, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Employee>> UpdateEmployee(Employee employee)
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
                    parameter.Add("Return_Val", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);

                    int dbData = await conn.ExecuteAsync("Sp_Update_Employee", parameter, commandType: System.Data.CommandType.StoredProcedure);
                    Result<Employee> result = new Result<Employee>
                    {
                        IdentityValue = dbData,
                        ReturnValue = parameter.Get<Int32>("Return_Val")
                    };
                    return result;
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

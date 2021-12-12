using EmployeeAPI.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
    public class EmployeeRepository : IEmployee
    {
        readonly SqlService sqlService;

        public EmployeeRepository(SqlService sqlService)
        {
            this.sqlService = sqlService;
        }
        public async Task AddEmployee(Employee employee)
        {
            try
            {
                var sqlConnection = sqlService.CreateConnection();
                // Enter procedureName
                var sqlCommand = new SqlCommand("{procedureName}", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //generate random id
                employee.EmployeeId = Guid.NewGuid();

                sqlCommand.Parameters.AddWithValue("EmployeeId", employee.EmployeeId);
                sqlCommand.Parameters.AddWithValue("FirstName", employee.FirstName);
                sqlCommand.Parameters.AddWithValue("LastName", employee.LastName);
                sqlCommand.Parameters.AddWithValue("Password", employee.Password);
                sqlCommand.Parameters.AddWithValue("Email", employee.Email);
                sqlCommand.Parameters.AddWithValue("Role", employee.Role);
                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();
                sqlConnection.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Employee GetEmployee(string email)
        {
            try
            {
                var employee = new Employee();

                var sqlConnection = sqlService.CreateConnection();
                //Enter procedure name
                var sqlCommand = new SqlCommand("{procedure name}", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("Email", email);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

                using (var sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        employee.EmployeeId = Guid.Parse(sqlReader["EmployeeId"].ToString());
                        employee.Email = sqlReader["Email"].ToString();
                        employee.FirstName= sqlReader["FirstName"].ToString();
                        employee.LastName = sqlReader["LastName"].ToString();
                        employee.Password = sqlReader["Password"].ToString();
                        employee.Role = sqlReader["Role"].ToString();
                    }
                }

                sqlConnection.Close();
                return employee;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Employee GetEmployeeById(Guid id)
        {
            try
            {
                var employee = new Employee();

                var sqlConnection = sqlService.CreateConnection();
                //Enter procedure name
                var sqlCommand = new SqlCommand("{procedure name}", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("EmployeeId", id);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

                using (var sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        employee.EmployeeId = Guid.Parse(sqlReader["EmployeeId"].ToString());
                        employee.Email = sqlReader["Email"].ToString();
                        employee.FirstName = sqlReader["FirstName"].ToString();
                        employee.LastName = sqlReader["LastName"].ToString();
                        employee.Password = sqlReader["Password"].ToString();
                    }
                }

                sqlConnection.Close();
                return employee;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task UpdatePassword(Guid id, string password)
        {
            try
            {
                var sqlConnection = sqlService.CreateConnection();
                var sqlCommand = new SqlCommand("{ProcedureName}", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("EmployeeId", id);
                sqlCommand.Parameters.AddWithValue("Password", password);

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();
                sqlConnection.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}

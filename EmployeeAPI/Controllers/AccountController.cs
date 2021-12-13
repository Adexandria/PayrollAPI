using AutoMapper;
using EmployeeAPI.Model;
using EmployeeAPI.Model.Authentication;
using EmployeeAPI.Model.Settings;
using EmployeeAPI.Services;
using EmployeeAPI.Services.EmployeeDOA;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IPasswordHasher<Employee> passwordHasher;
        private readonly Credentials _credentials;
        private readonly IMapper mapper;
        private readonly IEmployee _employee;

        public AccountController( IPasswordHasher<Employee> passwordHasher, Credentials _credentials,IEmployee _employee, IMapper mapper)
        {
            this.passwordHasher = passwordHasher;
            this._credentials = _credentials;
            this._employee = _employee;
            this.mapper = mapper;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(Signup newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   var newEmployee = mapper.Map<Employee>(newUser);
                    //Confirm if it will work
                    //Stores hashed password
                    newEmployee.Password = passwordHasher.HashPassword(newEmployee, newEmployee.Password);
                    await _employee.AddEmployee(newEmployee);
                   return this.StatusCode(StatusCodes.Status201Created, "Created");
                }
                else
                {
                   return BadRequest("Enter the required Details");
                }
            
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(Login user)
        {
            try
            {
                var currentEmployee = await _employee.RetrieveEmployeeByEmail(user.Email);
                if (currentEmployee == null)
                {
                    return NotFound("User doesn't exist");
                }

                //This verfies the user password by using IPasswordHasher interface
                var passwordVerifyResult = passwordHasher.VerifyHashedPassword(currentEmployee, currentEmployee.Password, user.Password);
                if (passwordVerifyResult.ToString() == "Success")
                {
                    var signingCredentials = _credentials.GetSigningCredentials();
                    var tokenOptions = _credentials.GenerateTokenOptions(signingCredentials, currentEmployee);
                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(token); 
                }

                return BadRequest("password is not correct");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
        [HttpGet("{id}/password/reset")]
        public async Task<ActionResult> ResetPassword(string id,ResetPassword reset)
        {
            try
            {
                var currentEmployee = await _employee.RetrieveEmployeeById(id);
                if (currentEmployee == null) return NotFound("username doesn't exist");
                var passwordVerifyResult = passwordHasher.VerifyHashedPassword(currentEmployee, currentEmployee.Password, reset.OldPassword);
                if (passwordVerifyResult.ToString() != "Success")
                {
                    currentEmployee.Password = passwordHasher.HashPassword(currentEmployee, reset.NewPassword);
                    await _employee.UpdateEmployee(currentEmployee);
                    return Ok("Success");
                }
                return BadRequest("Old Password can't be new password");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("{id}/signout")]
        public ActionResult Signout(string id)
        {
            try
            {
                var currentUser = _employee.RetrieveEmployeeById(id);
                if (currentUser == null) return NotFound("username doesn't exist");
                if(this.User.Identity.IsAuthenticated)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{id}/delete")]
        public ActionResult DeleteAccount(string id)
        {
            try
            {
                var currentUser = _employee.RetrieveEmployeeById(id);
                if (currentUser == null) return NotFound("username doesn't exist");
                _employee.DeleteEmployee(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

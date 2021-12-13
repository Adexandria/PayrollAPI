using AutoMapper;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Services.AttendanceDOA;
using EmployeeAPI.Services.EmployeeDOA;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/{employeeId}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAttendance _attendance;
        private readonly IEmployee _employee;

        public AttendanceController(IMapper mapper, IEmployee _employee, IAttendance _attendance)
        {
            this.mapper = mapper;
            this._attendance = _attendance;
            this._employee = _employee;
        }
        [HttpGet]
        public async Task<ActionResult> GetUserAttendance(string employeeId,string attendanceId)
        {
            try
            {
                int hours = 0;
                var currentEmployee = await _employee.RetrieveEmployeeById(employeeId);
                if (currentEmployee == null)
                {
                    return NotFound("This user doesn't exist");
                }
                var currentAttendance = await _attendance.GetAttendance(employeeId, attendanceId);
                if (currentAttendance == null)
                {
                    return NotFound("Not found");
                }
                if (currentAttendance.OverTime)
                {
                    hours = currentAttendance.HoursWorked - 6;
                }
                var mappedAttendance = mapper.Map<AttendanceDTO>(currentAttendance);
                mappedAttendance.OverTimeHours = hours;
                return Ok(mappedAttendance);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> StartTimer(string employeeId)
        {
            var currentEmployee = await _employee.RetrieveEmployeeById(employeeId);
            if(currentEmployee == null)
            {
                return NotFound("This user doesn't exist");
            }
            await _attendance.AddAttendance(employeeId);
            return Ok();
        }

        [HttpPut("{attendanceId}")]
        public async  Task<ActionResult> EndTimer(string employeeId,string attendanceId)
        {
            try
            {
                var currentEmployee = await _employee.RetrieveEmployeeById(employeeId);
                if (currentEmployee == null)
                {
                    return NotFound("This user doesn't exist");
                }
                var currentAttendance = await _attendance.GetAttendance(employeeId, attendanceId);
                if(currentAttendance == null)
                {
                    return NotFound("Not found");
                }
                var hours = DateTime.Now.Hour - currentAttendance.Date.Hour;
                if(hours > 6)
                {
                    currentAttendance.OverTime = true;
                }
                currentAttendance.HoursWorked = hours;
                await _attendance.UpdateAttendance(currentAttendance);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}

using Dapper;
using EmployeeAPI.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Services.AttendanceDOA
{
    public class AttendanceRepository : DBConfigService,IAttendance
    {
        IConfiguration configuration;
        ILogger<AttendanceRepository> logger;

        public AttendanceRepository(IConfiguration configuration, ILogger<AttendanceRepository> logger) : base(configuration)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        public async Task AddAttendance(string id)
        {
            try
            {
                Attendance attendance = new Attendance()
                {
                    EmployeeID = id
                };
                using (var conn = Connection)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("EmployeeID", attendance.EmployeeID);
                    parameter.Add("AttendanceID", attendance.AttendanceID);
                    parameter.Add("Date", attendance.Date);
                    parameter.Add("HoursWorked", attendance.HoursWorked);
                    parameter.Add("OverTime", attendance.OverTime);
                    int dbResult = await conn.ExecuteAsync("{stored procedure}", parameter, commandType: System.Data.CommandType.StoredProcedure);
                    
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ":" + ex.StackTrace, ex);

            }
        }

        public async Task<Attendance> GetAttendance(string employeeId, string attendanceId)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("EmployeeID", employeeId);
                    parameter.Add("AttendanceID", attendanceId);
                    Attendance attendance = await conn.QuerySingleAsync<Attendance>("{stored procedure}", parameter, commandType: System.Data.CommandType.StoredProcedure);
                    return attendance;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ":" + ex.StackTrace, ex);
                return null;

            }
        }

        public async Task UpdateAttendance(Attendance attendance)
        {
            try
            {
                using (var conn = Connection)
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("EmployeeID", attendance.EmployeeID);
                    parameter.Add("AttendanceID", attendance.AttendanceID);
                    parameter.Add("Last Name", attendance.Date);
                    parameter.Add("Email Address", attendance.HoursWorked);
                    parameter.Add("Password", attendance.OverTime);
                    await conn.ExecuteAsync("{stored procedure}", parameter, commandType: System.Data.CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ":" + ex.StackTrace, ex);

            }
        }
    }
}

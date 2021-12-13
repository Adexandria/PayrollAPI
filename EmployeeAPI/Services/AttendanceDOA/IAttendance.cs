using EmployeeAPI.Model;
using System.Threading.Tasks;

namespace EmployeeAPI.Services.AttendanceDOA
{
    public interface IAttendance
    {
        public Task AddAttendance(string id);
        public Task<Attendance> GetAttendance(string employeeId, string attendanceId);
        public Task UpdateAttendance(Attendance attendance);
    }
}

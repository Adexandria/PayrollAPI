using System;

namespace EmployeeAPI.Model
{
    public class Attendance
    {
        public string AttendanceID { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int HoursWorked { get; set; } = 0;
        public bool OverTime { get; set; } = false;
        public string EmployeeID { get; set; }
    }
}

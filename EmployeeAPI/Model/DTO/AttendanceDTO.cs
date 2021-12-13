using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Model.DTO
{
    public class AttendanceDTO
    {
        public DateTime Date { get; set; } 
        public int HoursWorked { get; set; }
        public int OverTimeHours { get; set; } = 0;
    }
}

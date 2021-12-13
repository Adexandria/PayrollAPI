using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Model
{
    public class Bank
    {
        public Guid BankID { get; set; }
        public string Bank_Name { get; set; }
        public string Account_Name { get; set; }
        public string Account_Number { get; set; }
        public Guid EmployeeID { get; set; }
    }
}

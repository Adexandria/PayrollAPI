using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Model
{
    public class Bank
    {
        public Guid BankId { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public Guid EmployeeId { get; set; }
    }
}

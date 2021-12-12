using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Model
{
    public class Address
    {
        //required
        public Guid AddressId { get; set; }
        //required
        public string AddressBox1 { get; set; }
        //optional
        public string AddressBox2 { get; set; }
        //foreign key
        public Guid EmployeeId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Model
{
    public class Address
    {
        //required
        public Guid AddressID { get; set; }
        //required
        public string Address_Box1 { get; set; }
        //optional
        public string Address_Box2 { get; set; }
        //foreign key
        public Guid EmployeeID { get; set; }
    }
}

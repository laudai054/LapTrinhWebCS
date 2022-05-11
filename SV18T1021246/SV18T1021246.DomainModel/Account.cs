using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021246.DomainModel
{
    public class Account
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public int EmployeeID { get; set; }
        public string Message { get; set; }
    }
}

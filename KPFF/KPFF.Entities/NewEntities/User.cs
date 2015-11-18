using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPFF.Entities
{
    public class User : EntityBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserLevel { get; set; }
        public int EmployeeId { get; set; }
    }
}

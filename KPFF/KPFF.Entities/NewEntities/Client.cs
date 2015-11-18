using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPFF.Entities
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public int ClientTypeId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Fax { get; set; }
        public string Comments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPFF.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public string LastModifiedBy { get; set; }
    }
}

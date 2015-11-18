using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using KPFF.PMP.Entities;

namespace KPFF.PMP.Service.Contracts
{
    [DataContract]
    public class EngineerResponse
    {
        [DataMember]
        public Engineer Engineer { get; set; }
    }

    [DataContract]
    public class EngineerRequest
    {
        [DataMember]
        public int Id { get; set; }
    }

    [DataContract]
    public class DateRequest
    {
        [DataMember]
        public string Date { get; set; }
    }

    [DataContract]
    public class DateResponse
    {
        [DataMember]
        public string Date { get; set; }
    }

}
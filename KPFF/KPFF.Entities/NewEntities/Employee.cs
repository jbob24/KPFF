using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPFF.Entities
{
    public class Employee : EntityBase
    {
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public int EmployeeTypeId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int YearsOfExperience { get; set; }
        public string Education { get; set; }
        public string Licenses { get; set; }
        public string ProfessionalMemberships { get; set; }
        public string ProfessionalCommittees { get; set; }
        public decimal HoursPerWeek { get; set; }
        public string Comments { get; set; }
        public bool IsActive { get; set; }
        public int PhoneExtension { get; set; }
        public string EmailAddress { get; set; }
        public bool HasPMFiscalSummaryAddress { get; set; }
        public bool HasPICFiscalSummaryAddress { get; set; }

        public List<Project> Projects { get; set; }
    }
}

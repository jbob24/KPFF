using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace KPFF.Entities
{
    public class Project : EntityBase
    {
        public decimal ProjectNumber { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public int StatusId { get; set; }
        public string Location { get; set; }
        public string ConstructionType { get; set; }
        public string ProjectType { get; set; }
        public int PhaseId { get; set; }
        public DateTime EstimatedStartDate { get; set; }
        public DateTime EstimatedCompletionDate { get; set; }
        public decimal FeeAmount { get; set; }
        public string FeeStructure { get; set; }
        public int ContractTypeId { get; set; }
        public decimal ContractFee { get; set; }
        public int PICId { get; set; }
        public string PICCode { get; set; }
        public int PM1Id { get; set; }
        public string PM1Code { get; set; }
        public int PM2Id { get; set; }
        public string PM2Code { get; set; }
        public string Comments { get; set; }
        public bool IsActive { get; set; }

        public string ProjectDescription
        {
            get { return string.Format("{0} - {1}", Name, ProjectNumber); }
        }

        public ScheduleItemList Schedule { get; set; }
    }

    public class ProjectList : List<Project> { }
}

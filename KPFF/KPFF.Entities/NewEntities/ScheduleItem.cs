using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPFF.Entities
{
    public class ScheduleItem : EntityBase
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public int WeekId { get; set; }
        public decimal Hours { get; set; }

        public ScheduleItem(int id, int projectId, int employeeId, int weekId, decimal hours)
        {
            this.Id = id;
            this.ProjectId = projectId;
            this.EmployeeId = employeeId;
            this.WeekId = weekId;
            this.Hours = hours;
        }
    }

    public class ScheduleItemList : List<ScheduleItem> { }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KPFF.Data;

namespace KPFF.Business
{
    public class Project
    {
        public static KPFF.Entities.ScheduleItemList GetProjectSchedules(KPFF.Entities.ProjectList projects, DateTime startDate, DateTime endDate)
        {
            return ScheduleData.GetProjectSchedules(projects, startDate, endDate);
        }

        public static KPFF.Entities.Project GetById(int projectId)
        {
            return ProjectData.GetById(projectId);
        }

        public static List<KPFF.Entities.Employee> GetProjectEngineers(int projectId)
        {
            return EmployeeData.GetEngineersByProjectId(projectId);
        }
    }
}

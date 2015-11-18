using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KPFF.Data;

namespace KPFF.Business
{
    public class Employee
    {
        public static KPFF.Entities.Employee GetById(int employeeId)
        {
            return EmployeeData.GetById(employeeId);
        }

        public static KPFF.Entities.ProjectList GetEmployeeProjects(int employeeId)
        {
            return ProjectData.GetProjectsByEmployeeID(employeeId);
        }
    }
}

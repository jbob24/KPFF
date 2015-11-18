using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KPFF.Entities;
using System.Data.SqlClient;

namespace KPFF.Data
{
    public class EmployeeData
    {
        public static Employee GetById(int employeeId)
        {
            Employee employee = null;

            var parms = new Dictionary<string, string>();
            parms.Add("EmployeeID", employeeId.ToString());

            using (var db = new DataBase("sp_GetEmployeeByID", parms))
            {
                var reader = db.ExecuteReader();

                if (reader.HasRows)
                {
                    employee = ConvertReaderDataToProjectList(reader)[0];
                }
            }

            return employee;
        }

        public static List<Employee> GetEngineersByProjectId(int projectId)
        {
            List<Employee> employees = null;

            var parms = new Dictionary<string, string>();
            parms.Add("@ProjectId", projectId.ToString());

            using (var db = new DataBase("sp_GetEngineersByProjectID_NEW", parms))
            {
                var reader = db.ExecuteReader();

                if (reader.HasRows)
                {
                    employees = ConvertReaderDataToProjectList(reader);
                }
            }

            return employees;
        }

        private static List<Employee> ConvertReaderDataToProjectList(SqlDataReader reader)
        {
            List<Employee> employees = null;

            if (reader.HasRows)
            {
                employees = new List<Employee>();
                while (reader.Read())
                {
                    var employee = new Employee();

                    employee.Id = reader.GetValueOrDefault<int>("EmployeeID");
                    employee.FirstName = reader.GetValueOrDefault<string>("EmployeeFirst");
                    employee.LastName = reader.GetValueOrDefault<string>("EmployeeLast");
                    employee.Name = reader.GetValueOrDefault<string>("EmployeeName");
                    employee.Title = reader.GetValueOrDefault<string>("Title");
                    employee.StartDate = reader.GetValueOrDefault<DateTime>("EmploymentStartDate");

                    int yearsOfExperience = 0;
                    int.TryParse(reader.GetValueOrDefault<string>("YearsOfExperience"), out yearsOfExperience);
                    employee.YearsOfExperience = yearsOfExperience;

                    employee.Education = reader.GetValueOrDefault<string>("Education");
                    employee.Licenses = reader.GetValueOrDefault<string>("Licenses");
                    employee.ProfessionalMemberships = reader.GetValueOrDefault<string>("ProfessionalMemberships");
                    employee.ProfessionalCommittees = reader.GetValueOrDefault<string>("ProfessionalCommittees");
                    employee.Comments = reader.GetValueOrDefault<string>("Comments");
                    employee.HoursPerWeek = reader.GetValueOrDefault<decimal>("HoursPerWeek");
                    employee.PhoneExtension = reader.GetValueOrDefault<int>("PhoneExtension");
                    employee.EmailAddress = reader.GetValueOrDefault<string>("EmailAddress");
                    employee.HasPMFiscalSummaryAddress = reader.GetValueOrDefault<bool>("PMFiscalSummary");
                    employee.HasPICFiscalSummaryAddress = reader.GetValueOrDefault<bool>("PICFiscalSummary");
                    employee.LastModifiedDate = reader.GetValueOrDefault<DateTime>("LastModified");
                    employee.LastModifiedBy = reader.GetValueOrDefault<string>("LastModifiedBy");

                    employees.Add(employee);
                }
            }

            return employees;
        }
    }
}

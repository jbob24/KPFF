using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using KPFF.Entities;

namespace KPFF.Data
{
    public class ProjectData
    {
        public static Project GetById(int projectId)
        {
            Project project = null;

            var parms = new Dictionary<string, string>();
            parms.Add("@ProjectId", projectId.ToString());

            using (var db = new DataBase("sp_GetProjectById_NEW", parms))
            {
                var reader = db.ExecuteReader();

                if (reader.HasRows)
                {
                    project = ConvertReaderDataToProjectList(reader)[0];
                }
            }

            return project;
        }

        public static ProjectList GetProjectsByEmployeeID(int employeeID)
        {
            ProjectList projects = null;

            var parms = new Dictionary<string, string>();
            parms.Add("@EmployeeID", employeeID.ToString());

            using (var db = new DataBase("sp_GetProjectsByEmployeeID_NEW", parms))
            {
                var reader = db.ExecuteReader();

                if (reader.HasRows)
                {
                    projects = ConvertReaderDataToProjectList(reader);
                }
            }

            return projects;
        }

        public static ProjectList GetAllActive()
        {
            ProjectList projects = null;

            using (var db = new DataBase("sp_GetActiveProjects_NEW", null))
            {
                var reader = db.ExecuteReader();

                if (reader.HasRows)
                {
                    projects = ConvertReaderDataToProjectList(reader);
                }
            }

            return projects;
        }

        public static ProjectList GetAllInactive()
        {
            ProjectList projects = null;

            using (var db = new DataBase("sp_GetInActiveProjects_NEW", null))
            {
                var reader = db.ExecuteReader();

                if (reader.HasRows)
                {
                    projects = ConvertReaderDataToProjectList(reader);
                }
            }

            return projects;
        }

        public static bool Insert(Project newProject, int createdByEmployeeId)
        {
            var parms = new Dictionary<string, string>();
            parms.Add("@ProjectNo", newProject.ProjectNumber.ToString());
            parms.Add("@ProjectName", newProject.Name);
            parms.Add("@ClientID", newProject.ClientId.ToString());
            parms.Add("@ProjectLocation", newProject.Location);
            parms.Add("@ConstructionType", newProject.ConstructionType);
            parms.Add("@ProjectType", newProject.ProjectType);
            parms.Add("@PhaseID", newProject.PhaseId.ToString());
            parms.Add("@EstimatedStartDate", newProject.EstimatedStartDate != DateTime.MinValue ? newProject.EstimatedStartDate.ToShortDateString() : DBNull.Value.ToString());
            parms.Add("@EstimatedCompletionDate", newProject.EstimatedCompletionDate != DateTime.MinValue ? newProject.EstimatedCompletionDate.ToShortDateString() : DBNull.Value.ToString());
            parms.Add("@FeeAmount", newProject.FeeAmount.ToString());
            parms.Add("@FeeStructure", newProject.FeeStructure);
            parms.Add("@Comments", newProject.Comments);
            parms.Add("@PIC", newProject.PICId.ToString());
            parms.Add("@PICCode", newProject.PICCode);
            parms.Add("@PM1", newProject.PM1Id.ToString());
            parms.Add("@PM1Code", newProject.PM1Code);
            parms.Add("@EmployeeID", createdByEmployeeId.ToString());
            
            using (var db = new DataBase("sp_Project_Insert_NEW", parms))
            {
                return db.ExecuteProcedure();
            }
        }

        public static void Update(Project project)
        {
            throw new NotImplementedException();
        }

        private static ProjectList ConvertReaderDataToProjectList(SqlDataReader reader)
        {
            ProjectList projects = null;

            if (reader.HasRows)
            {
                projects = new ProjectList();
                while (reader.Read())
                {
                    var project = new Project();

                    project.Id = reader.GetValueOrDefault<int>("ID");
                    project.ProjectNumber = reader.GetValueOrDefault<decimal>("ProjectNo");
                    project.Name = reader.GetValueOrDefault<string>("ProjectName");
                    project.ClientId = reader.GetValueOrDefault<int>("ClientID");
                    project.StatusId = reader.GetValueOrDefault<int>("ProjectStatus");
                    project.Location = reader.GetValueOrDefault<string>("ProjectLocation");
                    project.ConstructionType = reader.GetValueOrDefault<string>("ConstructionType");
                    project.ProjectType = reader.GetValueOrDefault<string>("ProjectType");
                    project.PhaseId = reader.GetValueOrDefault<int>("PhaseID");
                    project.EstimatedStartDate = reader.GetValueOrDefault<DateTime>("EstimatedStartDate");
                    project.EstimatedCompletionDate = reader.GetValueOrDefault<DateTime>("EstimatedCompletionDate");
                    project.FeeAmount = reader.GetValueOrDefault<decimal>("FeeAmount");
                    project.FeeStructure = reader.GetValueOrDefault<string>("FeeStructure");
                    project.ContractTypeId = reader.GetValueOrDefault<int>("ContractType");
                    project.PICId = reader.GetValueOrDefault<int>("PIC");
                    project.PM1Id = reader.GetValueOrDefault<int>("PM1");
                    project.PM2Id = reader.GetValueOrDefault<int>("PM2");
                    project.LastModifiedByUserId = reader.GetValueOrDefault<int>("LastModifiedByUserID");
                    project.PICCode = reader.GetValueOrDefault<string>("PICCode");
                    project.PM1Code = reader.GetValueOrDefault<string>("PM1Code");
                    project.PM2Code = reader.GetValueOrDefault<string>("PM2Code");
                    project.Comments = reader.GetValueOrDefault<string>("Comments");
                    project.IsActive = reader.GetValueOrDefault<bool>("Active");
                    project.LastModifiedDate = reader.GetValueOrDefault<DateTime>("LastModifiedDate");

                    projects.Add(project);
                }
            }

            return projects;

        }
    }
}

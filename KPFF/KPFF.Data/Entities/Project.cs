using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace KPFF.Data.Entities
{
    public class ProjectData
    {
        public int ID { get; set; }
        public decimal ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription 
        {
            get { return string.Format("{0} - {1}", ProjectName, ProjectNumber); }
        }

        public ProjectData() { }
        
        public ProjectData(int id, decimal projectNumber, string projectName)
        {
            this.ID = id;
            this.ProjectNumber = projectNumber;
            this.ProjectName = projectName;
        }
    }

    public class ProjectDataList : List<ProjectData> { }

    public class Project : DABase
    {
        public Project()
        { }

        public Project(WeekDate weekDate) :
            base(null, weekDate)
        {
        }

        public ProjectDataList GetActiveProjectsList(SqlConnection con)
        {
            ProjectDataList result = new ProjectDataList();
            SqlDataReader dr = GetActiveProjects(con);
            while (dr.Read())
            {
                result.Add(new ProjectData(dr.GetValueOrDefault<int>("ID"), dr.GetValueOrDefault<decimal>("ProjectNo"), dr.GetValueOrDefault<string>("Project")));
            }
            dr.Close();
            dr = null;
            return result;
        }

        public ProjectDataList GetActiveProjectsSearchList(string searchVal, SqlConnection con)
        {
            ProjectDataList result = new ProjectDataList();
            SqlDataReader dr = GetActiveProjectsBySearchValue(searchVal, con);
            while (dr.Read())
            {
                result.Add(new ProjectData(dr.GetValueOrDefault<int>("ID"), dr.GetValueOrDefault<decimal>("ProjectNo"), dr.GetValueOrDefault<string>("Project")));
            }
            dr.Close();
            dr = null;
            return result;
        }


        public SqlDataReader GetActiveProjects(SqlConnection con)
        {
            var parms = new Dictionary<string, string>();
            return GetDataReaderByStoredProcedure("sp_GetActiveProjects", parms, con);
        }

        public SqlDataReader GetActiveProjectsBySearchValue(string searchVal, SqlConnection con)
        {
            var parms = new Dictionary<string, string>();
            if ((searchVal.Substring(0, 1) != "%"))
            {
                searchVal = ("%" + searchVal);
            }
            if ((searchVal.Substring((searchVal.Length - 1), 1) != "%"))
            {
                searchVal = (searchVal + "%");
            }
            parms.Add("@SearchVal", searchVal);
            return GetDataReaderByStoredProcedure("sp_GetActiveProjectsSearch", parms, con);
        }

        public DataSet GetScheduleByEmployeeID(int employeeID)
        {
            var dsProjects = ScheduleDataSource();
            var parms = new Dictionary<string, string>();
            parms.Add("@EmployeeID", employeeID.ToString());
            parms.Add("@StartDate", WeekDate.WYFirst.MondayDate.ToShortDateString());
            parms.Add("@EndDate", WeekDate.WYLast.MondayDate.ToShortDateString());
            parms.Add("@FirstWeekNum", WeekDate.Weeks[0].ToString());
            return GetDataSetByStoredProcedure(dsProjects, "sp_GetProjectListByEmployeeID", parms);
        }

        public DataSet GetActiveScheduleByEmployeeID(int employeeID)
        {
            var dsProjects = ScheduleDataSource();
            var parms = new Dictionary<string, string>();
            parms.Add("@EmployeeID", employeeID.ToString());
            parms.Add("@StartDate", WeekDate.WYFirst.MondayDate.ToShortDateString());
            parms.Add("@EndDate", WeekDate.WYLast.MondayDate.ToShortDateString());
            parms.Add("@FirstWeekNum", WeekDate.Weeks[0].ToString());
            return GetDataSetByStoredProcedure(dsProjects, "sp_GetActiveProjectListByEmployeeID", parms);
        }

        public DataSet GeMyProjects(int employeeID)
        {
            //  ByVal startDate As DateTime, ByVal endDate As DateTime, ByVal firstWeekID As Integer) As DataSet
            var dsProjects = ProjectDataSource();
            var parms = new Dictionary<string, string>();
            parms.Add("@EmployeeID", employeeID.ToString());
            parms.Add("@StartDate", WeekDate.WYFirst.MondayDate.ToShortDateString());
            parms.Add("@EndDate", WeekDate.WYLast.MondayDate.ToShortDateString());
            parms.Add("@FirstWeekNum", WeekDate.Weeks[0].ToString());
            return GetDataSetByStoredProcedure(dsProjects, "sp_GetProjectsByEmployeeID", parms);
        }

        public DataSet GetAllProjects()
        {
            //  ByVal startDate As DateTime, ByVal endDate As DateTime, ByVal firstWeekID As Integer) As DataSet
            var dsProjects = ProjectDataSource();
            var parms = new Dictionary<string, string>();
            parms.Add("@StartDate", WeekDate.WYFirst.MondayDate.ToShortDateString());
            parms.Add("@EndDate", WeekDate.WYLast.MondayDate.ToShortDateString());
            parms.Add("@FirstWeekNum", WeekDate.Weeks[0].ToString());
            return GetDataSetByStoredProcedure(dsProjects, "sp_GetAllProjects", parms);
        }

        public DataSet GetInactiveProjects()
        {
            //  ByVal startDate As DateTime, ByVal endDate As DateTime, ByVal firstWeekID As Integer) As DataSet
            var dsProjects = ProjectDataSource();
            var parms = new Dictionary<string, string>();
            parms.Add("@StartDate", WeekDate.WYFirst.MondayDate.ToShortDateString());
            parms.Add("@EndDate", WeekDate.WYLast.MondayDate.ToShortDateString());
            parms.Add("@FirstWeekNum", WeekDate.Weeks[0].ToString());
            return GetDataSetByStoredProcedure(dsProjects, "sp_GetInactiveProjects", parms);
        }

        public bool ArchiveProject(int modifiedByEmployeeID, int projectID)
        {
            var parms = new Dictionary<string, string>();
            parms.Add("@ModifiedByUserID", modifiedByEmployeeID.ToString());
            parms.Add("@ProjectID", projectID.ToString());
            return ExecuteProcedure("sp_ArchiveProject", parms);
        }

        public bool UnArchiveProject(int modifiedByEmployeeID, int projectID)
        {
            var parms = new Dictionary<string, string>();
            parms.Add("@ModifiedByUserID", modifiedByEmployeeID.ToString());
            parms.Add("@ProjectID", projectID.ToString());
            return ExecuteProcedure("sp_UnArchiveProject", parms);
        }

        public bool CreateNewProject(
                    Decimal projectNo,
                    string projectName,
                    int clientID,
                    string projectLocation,
                    string constructionType,
                    string projectType,
                    int phaseID,
                    DateTime estimatedStartDate,
                    DateTime estimatedCompletionDate,
                    Decimal feeAmount,
                    string feeStructure,
                    string comments,
                    int pIC,
                    string pICCode,
                    int pM1,
                    string pM1Code,
                    int employeeID)
        {
            var parms = new Dictionary<string, string>();
            parms.Add("@ProjectNo", projectNo.ToString());
            parms.Add("@ProjectName", projectName);
            parms.Add("@ClientID", clientID.ToString());
            parms.Add("@ProjectLocation", projectLocation);
            parms.Add("@ConstructionType", constructionType);
            parms.Add("@ProjectType", projectType);
            parms.Add("@PhaseID", phaseID.ToString());
            if (!(estimatedStartDate == DateTime.MinValue))
            {
                parms.Add("@EstimatedStartDate", estimatedStartDate.ToShortDateString());
            }
            else
            {
                parms.Add("@EstimatedStartDate", DBNull.Value.ToString());
            }
            if (!(estimatedCompletionDate == DateTime.MinValue))
            {
                parms.Add("@EstimatedCompletionDate", estimatedCompletionDate.ToShortDateString());
            }
            else
            {
                parms.Add("@EstimatedCompletionDate", DBNull.Value.ToString());
            }
            parms.Add("@FeeAmount", feeAmount.ToString());
            parms.Add("@FeeStructure", feeStructure);
            parms.Add("@Comments", comments);
            parms.Add("@PIC", pIC.ToString());
            parms.Add("@PICCode", pICCode);
            parms.Add("@PM1", pM1.ToString());
            parms.Add("@PM1Code", pM1Code);
            parms.Add("@EmployeeID", employeeID.ToString());
            return ExecuteProcedure("sp_CreateProject", parms);
        }

        public bool UpdateProject(
                    int projectID,
                    Decimal projectNo,
                    string projectName,
                    int clientID,
                    string projectLocation,
                    string constructionType,
                    string projectType,
                    int phaseID,
                    DateTime estimatedStartDate,
                    DateTime estimatedCompletionDate,
                    Decimal feeAmount,
                    string feeStructure,
                    string comments,
                    int pIC,
                    string pICCode,
                    int pM1,
                    string pM1Code,
                    int employeeID)
        {
            var parms = new Dictionary<string, string>();
            parms.Add("@ProjectID", projectID.ToString());
            parms.Add("@ProjectNo", projectNo.ToString());
            parms.Add("@ProjectName", projectName);
            parms.Add("@ClientID", clientID.ToString());
            parms.Add("@ProjectLocation", projectLocation);
            parms.Add("@ConstructionType", constructionType);
            parms.Add("@ProjectType", projectType);
            parms.Add("@PhaseID", phaseID.ToString());
            if (!(estimatedStartDate == DateTime.MinValue))
            {
                parms.Add("@EstimatedStartDate", estimatedStartDate.ToShortDateString());
            }
            else
            {
                parms.Add("@EstimatedStartDate", DBNull.Value.ToString());
            }
            if (!(estimatedCompletionDate == DateTime.MinValue))
            {
                parms.Add("@EstimatedCompletionDate", estimatedCompletionDate.ToShortDateString());
            }
            else
            {
                parms.Add("@EstimatedCompletionDate", DBNull.Value.ToString());
            }
            parms.Add("@FeeAmount", feeAmount.ToString());
            parms.Add("@FeeStructure", feeStructure);
            parms.Add("@Comments", comments);
            parms.Add("@PIC", pIC.ToString());
            parms.Add("@PICCode", pICCode);
            parms.Add("@PM1", pM1.ToString());
            parms.Add("@PM1Code", pM1Code);
            parms.Add("@EmployeeID", employeeID.ToString());
            return ExecuteProcedure("sp_UpdateProject", parms);
        }

        public DataSet GetProjectAssignments(int projectID)
        {
            var dsProjects = AssignmentDetailsDataSource();
            var parms = new Dictionary<string, string>();
            parms.Add("@ProjectID", projectID.ToString());
            parms.Add("@StartDate", WeekDate.WYFirst.MondayDate.ToShortDateString());
            parms.Add("@EndDate", WeekDate.WYLast.MondayDate.ToShortDateString());
            parms.Add("@FirstWeekNum", WeekDate.Weeks[0].ToString());
            return GetDataSetByStoredProcedure(dsProjects, "sp_GetProjectAssignments", parms);
        }

        private DataSet ScheduleDataSource()
        {
            var dsProjects = new DataSet();
            DataTable dtSchedule;
            // 
            DataColumn dtCol;
            //  Schedule Table
            dtSchedule = new DataTable("Schedule");
            // 
            dtCol = new DataColumn("ID");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("ProjectID");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("ProjectNo");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("ProjectName");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week1");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week2");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week3");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week4");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week5");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week6");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week7");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week8");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week9");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week10");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week11");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week12");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week13");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week14");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week15");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week16");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week17");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week18");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week19");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week20");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("IDField");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            //  Add Tables to dataset
            dsProjects.Tables.Add(dtSchedule);
            return dsProjects;
        }

        private DataSet ProjectDataSource()
        {
            DataSet dsProjects;
            DataTable dtProjects;
            //  Initialize dataset object
            dsProjects = new System.Data.DataSet();
            // 
            DataColumn dtCol;
            //  Projects Table
            dtProjects = new DataTable("Projects");
            // 
            dtCol = new DataColumn("ProjectID");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("ProjectNo");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("ProjectName");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("PICCode");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("PM1Code");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week1");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week2");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week3");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week4");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week5");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week6");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week7");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week8");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week9");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week10");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week11");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week12");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week13");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week14");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week15");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week16");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week17");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week18");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week19");
            // With...
            dtProjects.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week20");
            // With...
            dtProjects.Columns.Add(dtCol);
            // ' '' '' Add primary Key
            //  '' ''Dim keys(1) As DataColumn
            //  '' ''keys(0) = dtProjects.Columns("ProjectID")
            //  '' ''dtProjects.PrimaryKey = keys
            // 
            //  Add Tables to dataset
            dsProjects.Tables.Add(dtProjects);
            // 
            return dsProjects;
        }

        private DataSet AssignmentDetailsDataSource()
        {
            var dsProjects = new DataSet();
            DataTable dtSchedule;
            // 
            DataColumn dtCol;
            //  Schedule Table
            dtSchedule = new DataTable("Schedule");
            // 
            dtCol = new DataColumn("ID");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("ProjectID");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("EmployeeID");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("EmployeeName");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Role");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week1");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week2");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week3");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week4");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week5");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week6");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week7");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week8");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week9");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week10");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week11");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week12");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week13");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week14");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week15");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week16");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week17");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week18");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week19");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("Week20");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            dtCol = new DataColumn("IDField");
            // With...
            dtSchedule.Columns.Add(dtCol);
            // 
            //  Add Tables to dataset
            dsProjects.Tables.Add(dtSchedule);
            return dsProjects;
        }
    }
}

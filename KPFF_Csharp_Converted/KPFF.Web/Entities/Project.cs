
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using KPFF.PMP.Entities;
using System.Linq;


namespace KPFF.PMP.Entities
{
    public class ProjectData : DABase
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private decimal _projectNumber;
        public decimal ProjectNumber
        {
            get { return _projectNumber; }
            set { _projectNumber = value; }
        }

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }


        private string _projectDescription;
        public string ProjectDescription
        {
            get { return _projectDescription; }
            set { _projectDescription = value; }
        }


        //Public Shared Function ToJSONOrderedByProjectName(ByVal projects As ProjectDataList) As String
        //    'Dim result As New StringBuilder()
        //    Dim orderedProjects = From p In projects Order By p.ProjectName Take 100 Select p

        //    'For Each item As ProjectData In orderedProjects
        //    '    result.Append(String.Format("{0} - {1}#{2}{3}", item.ProjectName, item.ProjectNumber, item.ID, "|"))
        //    'Next item

        //    'Dim projectList = GetActiveProjectsList(con)
        //    Dim result As String
        //    Dim jsSerializer As New JavaScriptSerializer()

        //    result = jsSerializer.Serialize(orderedProjects)

        //    Return result
        //End Function

        //Public Shared Function ToJSONOrderedByProjectNumber(ByVal projects As ProjectDataList) As String
        //    'Dim result As New StringBuilder()
        //    Dim orderedProjects = From p In projects Order By p.ProjectNumber Take 100 Select p

        //    'For Each item As ProjectData In orderedProjects
        //    '    result.Append(String.Format("{0} - {1}#{2}", item.ProjectName, item.ProjectNumber, item.ID))
        //    'Next item

        //    'Return result.ToString()

        //    Dim result As String
        //    Dim jsSerializer As New JavaScriptSerializer()

        //    result = jsSerializer.Serialize(orderedProjects)

        //    Return result
        //End Function
    }

    public class ProjectDataList : List<ProjectData>
    {
    }


    public class Project : DABase
    {

        public Project()
        {
        }

        public Project(WeekDate weekDate)
            : base(null, weekDate)
        {
        }

        public ProjectDataList GetActiveProjectsList(SqlConnection con)
        {
            ProjectDataList result = new ProjectDataList();
            SqlDataReader dr = GetActiveProjects(con);

            while ((dr.Read()))
            {
                ProjectData item = new ProjectData();
                item.ID = dr.GetValueOrDefault<int>(0);
                item.ProjectNumber = dr.GetValueOrDefault<decimal>(1);
                item.ProjectName = dr.GetValueOrDefault<string>(2);
                item.ProjectDescription = string.Format("{0} - {1}", item.ProjectName, item.ProjectNumber);

                result.Add(item);
            }

            dr.Close();
            dr = null;

            return result;
        }

        public ProjectDataList GetActiveProjectsSearchList(string searchVal, SqlConnection con)
        {
            ProjectDataList result = new ProjectDataList();
            SqlDataReader dr = GetActiveProjectsBySearchValue(searchVal, con);

            while ((dr.Read()))
            {
                ProjectData item = new ProjectData();
                item.ID = dr.GetValueOrDefault<int>(0);
                item.ProjectNumber = dr.GetValueOrDefault<decimal>(1);
                item.ProjectName = dr.GetValueOrDefault<string>(2);
                item.ProjectDescription = string.Format("{0} - {1}", item.ProjectName, item.ProjectNumber);

                result.Add(item);
            }

            dr.Close();
            dr = null;

            return result;
        }

        //Public Function GetActiveProjectsJSON(ByVal con As SqlConnection) As String
        //    Dim projectList = GetActiveProjectsList(con)
        //    Dim result As String
        //    Dim jsSerializer As New JavaScriptSerializer()

        //    result = jsSerializer.Serialize(projectList)

        //    Return result
        //End Function

        public SqlDataReader GetActiveProjects(SqlConnection con)
        {
            Dictionary<string, string> @params = new Dictionary<string, string>();
            return GetDataReaderByStoredProcedure("sp_GetActiveProjects", @params, con);
        }

        public SqlDataReader GetActiveProjectsBySearchValue(string searchVal, SqlConnection con)
        {
            Dictionary<string, string> @params = new Dictionary<string, string>();

            if (searchVal.Substring(0, 1) != "%")
            {
                searchVal = "%" + searchVal;
            }

            if (searchVal.Substring(searchVal.Length - 1, 1) != "%")
            {
                searchVal = searchVal + "%";
            }

            @params.Add("@SearchVal", searchVal);
            return GetDataReaderByStoredProcedure("sp_GetActiveProjectsSearch", @params, con);
        }

        public DataSet GetScheduleByEmployeeID(int employeeID)
        {
            var dsProjects = ScheduleDataSource();
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@EmployeeID", employeeID.ToString());
            @params.Add("@StartDate", weekDate.WYFirst.MondayDate);
            @params.Add("@EndDate", weekDate.WYLast.MondayDate);
            @params.Add("@WeekNums", string.Join(",", weekDate.Weeks.Select(w => w.ToString()).ToArray()));  // split array andpass to sproc

            return GetDataSetByStoredProcedure(dsProjects, "sp_GetProjectListByEmployeeID", @params);
        }

        public DataSet GetActiveScheduleByEmployeeID(int employeeID)
        {
            var dsProjects = ScheduleDataSource();
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@EmployeeID", employeeID.ToString());
            @params.Add("@StartDate", weekDate.WYFirst.MondayDate);
            @params.Add("@EndDate", weekDate.WYLast.MondayDate);
            @params.Add("@WeekNums", string.Join(",", weekDate.Weeks.Select(w => w.ToString()).ToArray()));  // split array andpass to sproc

            return GetDataSetByStoredProcedure(dsProjects, "sp_GetActiveProjectListByEmployeeID", @params);
        }

        public DataSet GeMyProjects(int employeeID)
        {
            // ByVal startDate As DateTime, ByVal endDate As DateTime, ByVal firstWeekID As Integer) As DataSet
            var dsProjects = ProjectDataSource();
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@EmployeeID", employeeID.ToString());
            @params.Add("@StartDate", weekDate.WYFirst.MondayDate);
            @params.Add("@EndDate", weekDate.WYLast.MondayDate);
            @params.Add("@WeekNums", string.Join(",", weekDate.Weeks.Select(w=>w.ToString()).ToArray()));  // split array andpass to sproc

            return GetDataSetByStoredProcedure(dsProjects, "sp_GetProjectsByEmployeeID", @params);
        }

        public DataSet GetAllProjects()
        {
            // ByVal startDate As DateTime, ByVal endDate As DateTime, ByVal firstWeekID As Integer) As DataSet
            var dsProjects = ProjectDataSource();
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@StartDate", weekDate.WYFirst.MondayDate);
            @params.Add("@EndDate", weekDate.WYLast.MondayDate);
            @params.Add("@WeekNums", string.Join(",", weekDate.Weeks.Select(w => w.ToString()).ToArray()));  // split array andpass to sproc

            return GetDataSetByStoredProcedure(dsProjects, "sp_GetAllProjects", @params);
        }

        public DataSet GetInactiveProjects()
        {
            // ByVal startDate As DateTime, ByVal endDate As DateTime, ByVal firstWeekID As Integer) As DataSet
            var dsProjects = ProjectDataSource();
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@StartDate", weekDate.WYFirst.MondayDate);
            @params.Add("@EndDate", weekDate.WYLast.MondayDate);
            @params.Add("@WeekNums", string.Join(",", weekDate.Weeks.Select(w => w.ToString()).ToArray()));  // split array andpass to sproc

            return GetDataSetByStoredProcedure(dsProjects, "sp_GetInactiveProjects", @params);
        }

        public bool ArchiveProject(int modifiedByEmployeeID, int projectID)
        {
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@ModifiedByUserID", modifiedByEmployeeID.ToString());
            @params.Add("@ProjectID", projectID.ToString());

            return ExecuteProcedure("sp_ArchiveProject", @params);
        }

        public bool UnArchiveProject(int modifiedByEmployeeID, int projectID)
        {
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@ModifiedByUserID", modifiedByEmployeeID.ToString());
            @params.Add("@ProjectID", projectID.ToString());

            return ExecuteProcedure("sp_UnArchiveProject", @params);
        }

        public bool CreateNewProject(decimal projectNo, string projectName, int clientID, string projectLocation, string constructionType, string projectType, int phaseID, DateTime estimatedStartDate, DateTime estimatedCompletionDate, decimal feeAmount,
        string feeStructure, string comments, int pIC, string pICCode, int pM1, string pM1Code, int employeeID)
        {

            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@ProjectNo", projectNo.ToString());
            @params.Add("@ProjectName", projectName);
            @params.Add("@ClientID", clientID.ToString());
            @params.Add("@ProjectLocation", projectLocation);
            @params.Add("@ConstructionType", constructionType);
            @params.Add("@ProjectType", projectType);
            @params.Add("@PhaseID", phaseID.ToString());

            if (!(estimatedStartDate == DateTime.MinValue))
            {
                @params.Add("@EstimatedStartDate", estimatedStartDate.ToString());
            }
            else
            {
                @params.Add("@EstimatedStartDate", DBNull.Value.ToString());
            }

            if (!(estimatedCompletionDate == DateTime.MinValue))
            {
                @params.Add("@EstimatedCompletionDate", estimatedCompletionDate.ToString());
            }
            else
            {
                @params.Add("@EstimatedCompletionDate", DBNull.Value.ToString());
            }

            @params.Add("@FeeAmount", feeAmount.ToString());
            @params.Add("@FeeStructure", feeStructure);
            @params.Add("@Comments", comments);
            @params.Add("@PIC", pIC.ToString());
            @params.Add("@PICCode", pICCode);
            @params.Add("@PM1", pM1.ToString());
            @params.Add("@PM1Code", pM1Code);
            @params.Add("@EmployeeID", employeeID.ToString());

            return ExecuteProcedure("sp_CreateProject", @params);
        }

        public bool UpdateProject(int projectID, decimal projectNo, string projectName, int clientID, string projectLocation, string constructionType, string projectType, int phaseID, DateTime estimatedStartDate, DateTime estimatedCompletionDate,
        decimal feeAmount, string feeStructure, string comments, int pIC, string pICCode, int pM1, string pM1Code, int employeeID)
        {

            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@ProjectID", projectID.ToString());
            @params.Add("@ProjectNo", projectNo.ToString());
            @params.Add("@ProjectName", projectName);
            @params.Add("@ClientID", clientID.ToString());
            @params.Add("@ProjectLocation", projectLocation);
            @params.Add("@ConstructionType", constructionType);
            @params.Add("@ProjectType", projectType);
            @params.Add("@PhaseID", phaseID.ToString());

            if (!(estimatedStartDate == DateTime.MinValue))
            {
                @params.Add("@EstimatedStartDate", estimatedStartDate.ToString());
            }
            else
            {
                @params.Add("@EstimatedStartDate", DBNull.Value.ToString());
            }

            if (!(estimatedCompletionDate == DateTime.MinValue))
            {
                @params.Add("@EstimatedCompletionDate", estimatedCompletionDate.ToString());
            }
            else
            {
                @params.Add("@EstimatedCompletionDate", DBNull.Value.ToString());
            }

            @params.Add("@FeeAmount", feeAmount.ToString());
            @params.Add("@FeeStructure", feeStructure);
            @params.Add("@Comments", comments);
            @params.Add("@PIC", pIC.ToString());
            @params.Add("@PICCode", pICCode);
            @params.Add("@PM1", pM1.ToString());
            @params.Add("@PM1Code", pM1Code);
            @params.Add("@EmployeeID", employeeID.ToString());

            return ExecuteProcedure("sp_UpdateProject", @params);
        }

        public DataSet GetProjectAssignments(int projectID)
        {
            var dsProjects = AssignmentDetailsDataSource();
            Dictionary<string, string> @params = new Dictionary<string, string>();
            @params.Add("@ProjectID", projectID.ToString());
            @params.Add("@StartDate", weekDate.WYFirst.MondayDate);
            @params.Add("@EndDate", weekDate.WYLast.MondayDate);
            @params.Add("@WeekNums", string.Join(",", weekDate.Weeks.Select(w => w.ToString()).ToArray()));  // split array andpass to sproc

            return GetDataSetByStoredProcedure(dsProjects, "sp_GetProjectAssignments", @params);
        }


        private DataSet ScheduleDataSource()
        {
            var dsProjects = new DataSet();
            DataTable dtSchedule = default(DataTable);

            //
            DataColumn dtCol = default(DataColumn);
            // Schedule Table
            dtSchedule = new DataTable("Schedule");
            //
            dtCol = new DataColumn("ID");
            {
                dtCol.DataType = typeof(int);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("ProjectID");
            {
                dtCol.DataType = typeof(int);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("ProjectNo");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("ProjectName");
            {
                dtCol.DataType = typeof(string);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week1");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week2");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week3");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week4");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week5");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week6");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week7");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week8");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week9");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week10");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week11");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week12");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week13");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week14");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week15");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);

            //
            dtCol = new DataColumn("Week16");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week17");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week18");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week19");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week20");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);


            //
            dtCol = new DataColumn("IDField");
            {
                dtCol.DataType = typeof(int);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            // Add Tables to dataset
            dsProjects.Tables.Add(dtSchedule);

            return dsProjects;
        }

        private DataSet ProjectDataSource()
        {
            DataSet dsProjects = default(DataSet);
            DataTable dtProjects = default(DataTable);

            // Initialize dataset object
            dsProjects = new System.Data.DataSet();
            //
            DataColumn dtCol = default(DataColumn);
            // Projects Table
            dtProjects = new DataTable("Projects");
            //
            dtCol = new DataColumn("ProjectID");
            {
                dtCol.DataType = typeof(int);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("ProjectNo");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("ProjectName");
            {
                dtCol.DataType = typeof(string);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("PICCode");
            {
                dtCol.DataType = typeof(string);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("PM1Code");
            {
                dtCol.DataType = typeof(string);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week1");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week2");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week3");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week4");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week5");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week6");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week7");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week8");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week9");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week10");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week11");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week12");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week13");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week14");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week15");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);

            //
            dtCol = new DataColumn("Week16");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week17");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week18");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week19");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week20");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtProjects.Columns.Add(dtCol);


            //' '' '' Add primary Key
            // '' ''Dim keys(1) As DataColumn
            // '' ''keys[0] = dtProjects.Columns("ProjectID")
            // '' ''dtProjects.PrimaryKey = keys
            //
            // Add Tables to dataset
            dsProjects.Tables.Add(dtProjects);
            //

            return dsProjects;
        }


        private DataSet AssignmentDetailsDataSource()
        {
            var dsProjects = new DataSet();
            DataTable dtSchedule = default(DataTable);

            //
            DataColumn dtCol = default(DataColumn);
            // Schedule Table
            dtSchedule = new DataTable("Schedule");
            //
            dtCol = new DataColumn("ID");
            {
                dtCol.DataType = typeof(int);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("ProjectID");
            {
                dtCol.DataType = typeof(int);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("EmployeeID");
            {
                dtCol.DataType = typeof(int);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("EmployeeName");
            {
                dtCol.DataType = typeof(string);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Role");
            {
                dtCol.DataType = typeof(string);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week1");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week2");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week3");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week4");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week5");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week6");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week7");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week8");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week9");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week10");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week11");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week12");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week13");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week14");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week15");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);

            //
            dtCol = new DataColumn("Week16");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week17");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week18");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week19");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week20");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);

            //
            dtCol = new DataColumn("IDField");
            {
                dtCol.DataType = typeof(int);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            // Add Tables to dataset
            dsProjects.Tables.Add(dtSchedule);

            return dsProjects;
        }
    }
}
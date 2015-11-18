using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;
using KPFF.Data;
using KPFF.Data.Entities;

public class Engineer : DABase
{
    public int EmployeeID { get; set; }
    public decimal HoursPerWeek { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeEmail { get; set; }

    public Engineer(ClsSchedule schedule, WeekDate weekDate, int empId) :
        base(schedule, weekDate)
    {
        this.EmployeeID = empId;
        SetEmployeeDetails();
    }

    public Engineer()
    {
    }

    private void SetEmployeeDetails()
    {
        if ((EmployeeID > 0))
        {
            SqlConnection con = new SqlConnection(Configuration.ConnectionString);
            con.Open();
            SqlDataReader empReader = GetByEmployeeID(this.EmployeeID, con);
            if (empReader.HasRows)
            {
                empReader.Read();
                EmployeeName = Utilities.Nz(empReader["EmployeeName"], "").ToString();
                EmployeeEmail = Utilities.Nz(empReader["EmailAddress"], "").ToString();
                HoursPerWeek = decimal.Parse(Utilities.Nz(empReader["HoursPerWeek"], "").ToString());
            }
            con.Close();
            empReader.Close();
            con = null;
            empReader = null;
        }
    }

    public DataSet GetSchedulesAllEngineers(string strFilterField, string strFilterText) {
        var dsEngineers = InitializeDataSource();
        var parms = new Dictionary<string, string>();
        parms.Add("@StartDate", WeekDate.WYFirst.MondayDate.ToShortDateString());
        parms.Add("@EndDate", WeekDate.WYLast.MondayDate.ToShortDateString());
        parms.Add("@FirstWeekNum", WeekDate.Weeks[0].ToString());
        return GetDataSetByStoredProcedure(dsEngineers, "sp_GetScheduleAllEngineers", parms);
    }

    public DataSet GetSchedulesAllEngineers(string fromDate, string toDate, string availHours) {
        var dsEngineers = InitializeDataSource();
        var parms = new Dictionary<string, string>();

        parms.Add("@StartDate", WeekDate.WYFirst.MondayDate.ToShortDateString());
        parms.Add("@EndDate", WeekDate.WYLast.MondayDate.ToShortDateString());
        parms.Add("@FirstWeekNum", WeekDate.Weeks[0].ToString());
        parms.Add("@AvailFromDate", fromDate);
        parms.Add("@AvailToDate", toDate);
        parms.Add("@AvailHours", availHours);
        return GetDataSetByStoredProcedure(dsEngineers, "sp_GetFilteredScheduleAllEngineers", parms);
    }

    public SqlDataReader GetAllEngineers(SqlConnection con) {
        var parms = new Dictionary<string, string>();
        return GetDataReaderByStoredProcedure("sp_GetAllEmployees", parms, con);
    }

    public SqlDataReader GetByEmployeeID(int employeeID, SqlConnection con) {
        var parms = new Dictionary<string, string>();
        parms.Add("@EmployeeID", employeeID.ToString());
        return GetDataReaderByStoredProcedure("sp_GetEmployeeByID", parms, con);
    }

    public bool DeleteSchedule(int employeeID, int projectID, int fromDateID, int toDateID) {
        var parms = new Dictionary<string, string>();
        parms.Add("@EmployeeID", employeeID.ToString());
        parms.Add("@ProjectID", projectID.ToString());
        parms.Add("@FromDateID", fromDateID.ToString());
        parms.Add("@ToDateID", toDateID.ToString());
        return ExecuteProcedure("sp_DeleteSchedule", parms);
    }

    public Decimal GetEmployeeWeekTotalHours(int employeeID, int weekID) {
        var parms = new Dictionary<string, string>();
        parms.Add("@EmployeeID", employeeID.ToString());
        parms.Add("@WeekID", weekID.ToString());
        Decimal value = 0;
        Decimal.TryParse(ExecuteScalar("sp_GetEmployeeWeekTotalHours", parms).ToString(), out value);
        return value;
    }

    public bool InsertSchedule(int employeeID, int projectID, int weekID, Decimal hours, int modifiedByUserID) {
        var parms = new Dictionary<string, string>();
        parms.Add("@EmployeeID", employeeID.ToString());
        parms.Add("@ProjectID", projectID.ToString());
        parms.Add("@WeekID", weekID.ToString());
        parms.Add("@Hours", hours.ToString());
        parms.Add("@ModifiedByUserID", modifiedByUserID.ToString());
        return ExecuteProcedure("sp_InsertSchedule", parms);
    }

    public bool UpdateSchedule(int employeeID, int projectID, int weekID, Decimal hours, int modifiedByUserID) {
        var parms = new Dictionary<string, string>();
        parms.Add("@EmployeeID", employeeID.ToString());
        parms.Add("@ProjectID", projectID.ToString());
        parms.Add("@WeekID", weekID.ToString());
        parms.Add("@Hours", hours.ToString());
        parms.Add("@ModifiedByUserID", modifiedByUserID.ToString());
        return ExecuteProcedure("sp_UpdateSchedule", parms);
    }

    public bool AddProject(int employeeID, int projectID, int weekID, int assignedByUserID) {
        var parms = new Dictionary<string, string>();
        parms.Add("@EmployeeID", employeeID.ToString());
        parms.Add("@ProjectID", projectID.ToString());
        parms.Add("@WeekID", weekID.ToString());
        parms.Add("@AssignedByUserID", assignedByUserID.ToString());
        return ExecuteProcedure("sp_AddProject", parms);
    }

    public bool UnAssignProject(int employeeID, int projectID, int unAssignedByUserID) {
        var fparms = new Dictionary<string, string>();
        fparms.Add("@EmployeeID", employeeID.ToString());
        fparms.Add("@ProjectID", projectID.ToString());
        fparms.Add("@StartDate", DateTime.Now.ToShortDateString());
        SqlDataReader futureSchedReader;
        futureSchedReader = GetDataReaderByStoredProcedure("sp_GetFutureScheduleByEmployeeID", fparms, new SqlConnection(conString));
        while (futureSchedReader.Read()) {
            object totalHours = futureSchedReader["total_hours"];
            if (totalHours != null && totalHours != DBNull.Value) {
                Decimal hours;
                Decimal.TryParse(totalHours.ToString(), out hours);
                if ((hours > 0)) {
                    return false;
                }
            }
        }

        var parms = new Dictionary<string, string>();
        parms.Add("@EmployeeID", employeeID.ToString());
        parms.Add("@ProjectID", projectID.ToString());
        parms.Add("@UnAssignedByUserID", unAssignedByUserID.ToString());
        return ExecuteProcedure("sp_UnassignProject", parms);
    }

    public bool AssignProject(int employeeID, int projectID, int assignedByUserID) {
        var parms = new Dictionary<string, string>();
        parms.Add("@EmployeeID", employeeID.ToString());
        parms.Add("@ProjectID", projectID.ToString());
        parms.Add("@AssignedByUserID", assignedByUserID.ToString());
        return ExecuteProcedure("sp_AssignProject", parms);
    }

    public bool ReassignProjectAllWeeks(int employeeID, int projectID, int newEmployeeID, int reAssignedByUserID) {
        if (((employeeID <= 0) 
                    || ((projectID <= 0) 
                    || (newEmployeeID <= 0)))) {
            return false;
        }
        var parms = new Dictionary<string, string>();
        parms.Add("@EmployeeID", employeeID.ToString());
        parms.Add("@ProjectID", projectID.ToString());
        parms.Add("@NewEmployeeID", newEmployeeID.ToString());
        ExecuteProcedure("sp_ReassignProjectAllWeeks", parms);
        AssignProject(newEmployeeID, projectID, reAssignedByUserID);
        UnAssignProject(employeeID, projectID, reAssignedByUserID);
        return true;
    }

    public bool ReassignProjectByDateRange(int employeeID, int projectID, int newEmployeeID, DateTime fromDate, DateTime toDate, int reAssignedByUserID) {
        if (((employeeID <= 0) 
                    || ((projectID <= 0) 
                    || (newEmployeeID <= 0)))) {
            return false;
        }
        var parms = new Dictionary<string, string>();
        parms.Add("@EmployeeID", employeeID.ToString());
        parms.Add("@ProjectID", projectID.ToString());
        parms.Add("@NewEmployeeID", newEmployeeID.ToString());
        parms.Add("@FromDate", fromDate.Date.ToShortDateString());
        parms.Add("@ToDate", toDate.Date.ToShortDateString());
        ExecuteProcedure("sp_ReassignProjectByDateRange", parms);
        AssignProject(newEmployeeID, projectID, reAssignedByUserID);
        UnAssignProject(employeeID, projectID, reAssignedByUserID);
        return true;
    }

    private DataSet InitializeDataSource()
    {
        //  Initialize dataset object
        var dsEngineers = new DataSet();
        DataTable dtEngineers;
        // 
        DataColumn dtCol;
        //  Projects Table
        dtEngineers = new DataTable("Engineers");
        // 
        dtCol = new DataColumn("EmployeeID");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("EmployeeName");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("EmployeeType");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week1");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week2");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week3");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week4");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week5");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week6");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week7");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week8");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week9");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week10");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week11");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week12");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week13");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week14");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week15");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week16");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week17");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week18");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week19");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("Week20");
        // With...
        dtEngineers.Columns.Add(dtCol);
        // 
        dtCol = new DataColumn("IDField");
        // With...
        dtEngineers.Columns.Add(dtCol);
        //  Add primary Key
        var keys = new DataColumn[1];
        keys[0] = dtEngineers.Columns["EmployeeID"];
        dtEngineers.PrimaryKey = keys;
        // 
        //  Add Tables to dataset
        dsEngineers.Tables.Add(dtEngineers);
        return dsEngineers;
    }
}
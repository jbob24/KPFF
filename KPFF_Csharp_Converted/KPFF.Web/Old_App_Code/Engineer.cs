
//using Microsoft.VisualBasic;
//using System.Data.SqlClient;
//using System.Data;
//using System.Collections.Generic;
//using System;
//using KPFF.PMP.Entities;

//namespace KPFF.PMP
//{
//    public class Engineer : DABase
//    {


//        private int _employeeID;
//        public int EmployeeID
//        {
//            get { return _employeeID; }
//            set { _employeeID = value; }
//        }

//        private decimal _hoursPerWeek;
//        public decimal HoursPerWeek
//        {
//            get { return _hoursPerWeek; }
//            set { _hoursPerWeek = value; }
//        }

//        private string _empName;
//        public string EmployeeName
//        {
//            get { return _empName; }
//            set { _empName = value; }
//        }

//        private string _empEmail;
//        public string EmployeeEmail
//        {
//            get { return _empEmail; }
//            set { _empEmail = value; }
//        }

//        public Engineer(clsSchedule schedule, WeekDate weekDate, int empId)
//            : base(schedule, weekDate)
//        {
//            this.EmployeeID = empId;
//            SetEmployeeDetails();
//        }


//        public Engineer()
//        {
//        }

//        private void SetEmployeeDetails()
//        {
//            if ((EmployeeID > 0))
//            {
//                SqlConnection con = new SqlConnection(Configuration.ConnectionString);
//                con.Open();
//                SqlDataReader empReader = GetByEmployeeID(this.EmployeeID, con);


//                if (empReader.HasRows)
//                {
//                    empReader.Read();

//                    EmployeeName = empReader.GetValueOrDefault<string>("EmployeeName");
//                    EmployeeEmail = empReader.GetValueOrDefault<string>("EmailAddress");
//                    HoursPerWeek = empReader.GetValueOrDefault<decimal>("HoursPerWeek");
//                }

//                con.Close();
//                empReader.Close();
//                con = null;
//                empReader = null;
//            }

//        }

//        public DataSet GetSchedulesAllEngineers(string strFilterField, string strFilterText)
//        {
//            var dsEngineers = InitializeDataSource();
//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            @params.Add("@StartDate", weekDate.WYFirst.MondayDate);
//            @params.Add("@EndDate", weekDate.WYLast.MondayDate);
//            @params.Add("@FirstWeekNum", weekDate.Weeks[0].ToString());

//            return GetDataSetByStoredProcedure(dsEngineers, "sp_GetScheduleAllEngineers", @params);
//        }

//        public DataSet GetSchedulesAllEngineers(string fromDate, string toDate, string availHours)
//        {
//            var dsEngineers = InitializeDataSource();
//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            @params.Add("@StartDate", weekDate.WYFirst.MondayDate);
//            @params.Add("@EndDate", weekDate.WYLast.MondayDate);
//            @params.Add("@FirstWeekNum", weekDate.Weeks[0].ToString());
//            @params.Add("@AvailFromDate", fromDate);
//            @params.Add("@AvailToDate", toDate);
//            @params.Add("@AvailHours", availHours);

//            return GetDataSetByStoredProcedure(dsEngineers, "sp_GetFilteredScheduleAllEngineers", @params);
//        }



//        public SqlDataReader GetAllEngineers(SqlConnection con)
//        {
//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            return GetDataReaderByStoredProcedure("sp_GetAllEmployees", @params, con);
//        }

//        public SqlDataReader GetByEmployeeID(int employeeID, SqlConnection con)
//        {
//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            @params.Add("@EmployeeID", employeeID.ToString());

//            return GetDataReaderByStoredProcedure("sp_GetEmployeeByID", @params, con);
//        }

//        public bool DeleteSchedule(int employeeID, int projectID, int fromDateID, int toDateID)
//        {
//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            @params.Add("@EmployeeID", employeeID.ToString());
//            @params.Add("@ProjectID", projectID.ToString());
//            @params.Add("@FromDateID", fromDateID.ToString());
//            @params.Add("@ToDateID", toDateID.ToString());

//            return ExecuteProcedure("sp_DeleteSchedule", @params);
//        }

//        public decimal GetEmployeeWeekTotalHours(int employeeID, int weekID)
//        {
//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            @params.Add("@EmployeeID", employeeID.ToString());
//            @params.Add("@WeekID", weekID.ToString());

//            decimal value = 0;
//            decimal.TryParse(ExecuteScalar("sp_GetEmployeeWeekTotalHours", @params).ToString(), out value);

//            return value;
//        }

//        public bool InsertSchedule(int employeeID, int projectID, int weekID, decimal hours, int modifiedByUserID)
//        {
//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            @params.Add("@EmployeeID", employeeID.ToString());
//            @params.Add("@ProjectID", projectID.ToString());
//            @params.Add("@WeekID", weekID.ToString());
//            @params.Add("@Hours", hours.ToString());
//            @params.Add("@ModifiedByUserID", modifiedByUserID.ToString());

//            return ExecuteProcedure("sp_InsertSchedule", @params);
//        }

//        public bool UpdateSchedule(int employeeID, int projectID, int weekID, decimal hours, int modifiedByUserID)
//        {
//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            @params.Add("@EmployeeID", employeeID.ToString());
//            @params.Add("@ProjectID", projectID.ToString());
//            @params.Add("@WeekID", weekID.ToString());
//            @params.Add("@Hours", hours.ToString());
//            @params.Add("@ModifiedByUserID", modifiedByUserID.ToString());

//            return ExecuteProcedure("sp_UpdateSchedule", @params);
//        }

//        public bool AddProject(int employeeID, int projectID, int weekID, int assignedByUserID)
//        {
//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            @params.Add("@EmployeeID", employeeID.ToString());
//            @params.Add("@ProjectID", projectID.ToString());
//            @params.Add("@WeekID", weekID.ToString());
//            @params.Add("@AssignedByUserID", assignedByUserID.ToString());

//            return ExecuteProcedure("sp_AddProject", @params);
//        }

//        public bool UnAssignProject(int employeeID, int projectID, int unAssignedByUserID)
//        {

//            Dictionary<string, string> fparams = new Dictionary<string, string>();
//            fparams.Add("@EmployeeID", employeeID.ToString());
//            fparams.Add("@ProjectID", projectID.ToString());
//            fparams.Add("@StartDate", DateTime.Now.ToShortDateString());

//            SqlDataReader futureSchedReader = default(SqlDataReader);
//            futureSchedReader = GetDataReaderByStoredProcedure("sp_GetFutureScheduleByEmployeeID", fparams, new SqlConnection(conString));

//            while ((futureSchedReader.Read()))
//            {
//                var totalHours = futureSchedReader.GetValueOrDefault<decimal>("total_hours");
//                if (totalHours > 0)
//                {
//                    return false;
//                }

//                //object totalHours = futureSchedReader("total_hours");
//                //if (!Information.IsDBNull(totalHours))
//                //{
//                //    decimal hours = default(decimal);
//                //    decimal.TryParse(totalHours.ToString(), hours);
//                //    if (hours > 0)
//                //    {
//                //        return false;
//                //    }
//                //}
//            }

//            //If futureSchedReader.Read() Then
//            //    Return False
//            //Else
//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            @params.Add("@EmployeeID", employeeID.ToString());
//            @params.Add("@ProjectID", projectID.ToString());
//            @params.Add("@UnAssignedByUserID", unAssignedByUserID.ToString());

//            return ExecuteProcedure("sp_UnassignProject", @params);
//            //End If
//        }

//        public bool AssignProject(int employeeID, int projectID, int assignedByUserID)
//        {
//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            @params.Add("@EmployeeID", employeeID.ToString());
//            @params.Add("@ProjectID", projectID.ToString());
//            @params.Add("@AssignedByUserID", assignedByUserID.ToString());

//            return ExecuteProcedure("sp_AssignProject", @params);
//        }

//        public bool ReassignProjectAllWeeks(int employeeID, int projectID, int newEmployeeID, int reAssignedByUserID)
//        {
//            if (employeeID <= 0 | projectID <= 0 | newEmployeeID <= 0)
//            {
//                return false;
//            }

//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            @params.Add("@EmployeeID", employeeID.ToString());
//            @params.Add("@ProjectID", projectID.ToString());
//            @params.Add("@NewEmployeeID", newEmployeeID.ToString());

//            ExecuteProcedure("sp_ReassignProjectAllWeeks", @params);

//            AssignProject(newEmployeeID, projectID, reAssignedByUserID);
//            UnAssignProject(employeeID, projectID, reAssignedByUserID);

//            return true;
//        }

//        public bool ReassignProjectByDateRange(int employeeID, int projectID, int newEmployeeID, DateTime fromDate, DateTime toDate, int reAssignedByUserID)
//        {
//            if (employeeID <= 0 | projectID <= 0 | newEmployeeID <= 0)
//            {
//                return false;
//            }

//            Dictionary<string, string> @params = new Dictionary<string, string>();
//            @params.Add("@EmployeeID", employeeID.ToString());
//            @params.Add("@ProjectID", projectID.ToString());
//            @params.Add("@NewEmployeeID", newEmployeeID.ToString());
//            @params.Add("@FromDate", fromDate.Date.ToString());
//            @params.Add("@ToDate", toDate.Date.ToString());

//            ExecuteProcedure("sp_ReassignProjectByDateRange", @params);

//            AssignProject(newEmployeeID, projectID, reAssignedByUserID);
//            UnAssignProject(employeeID, projectID, reAssignedByUserID);

//            return true;
//        }


//        private DataSet InitializeDataSource()
//        {
//            // Initialize dataset object
//            var dsEngineers = new DataSet();
//            DataTable dtEngineers = default(DataTable);
//            //
//            DataColumn dtCol = default(DataColumn);
//            // Projects Table
//            dtEngineers = new DataTable("Engineers");
//            //
//            dtCol = new DataColumn("EmployeeID");
//            {
//                dtCol.DataType = typeof(int);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("EmployeeName");
//            {
//                dtCol.DataType = typeof(string);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("EmployeeType");
//            {
//                dtCol.DataType = typeof(string);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week1");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week2");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week3");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week4");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week5");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week6");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week7");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week8");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week9");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week10");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week11");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week12");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week13");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week14");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week15");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week16");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week17");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week18");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week19");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            //
//            dtCol = new DataColumn("Week20");
//            {
//                dtCol.DataType = typeof(decimal);
//            }
//            dtEngineers.Columns.Add(dtCol);

//            //
//            dtCol = new DataColumn("IDField");
//            {
//                dtCol.DataType = typeof(int);
//            }
//            dtEngineers.Columns.Add(dtCol);
//            // Add primary Key
//            DataColumn[] keys = new DataColumn[2];
//            keys[0] = dtEngineers.Columns["EmployeeID"];
//            dtEngineers.PrimaryKey = keys;
//            //
//            // Add Tables to dataset
//            dsEngineers.Tables.Add(dtEngineers);

//            return dsEngineers;
//        }
//    }
//}
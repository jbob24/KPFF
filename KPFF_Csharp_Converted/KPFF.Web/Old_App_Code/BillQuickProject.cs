
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP
{
    public class BillQuickProject
    {
        #region "Properties"
        private string _projectID;
        public string ProjectID
        {
            get { return _projectID; }
            set { _projectID = value; }
        }


        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }


        private string _clientID;
        public string ClientID
        {
            get { return _clientID; }
            set { _clientID = value; }
        }


        private string _clientName;
        public string ClientName
        {
            get { return _clientName; }
            set { _clientName = value; }
        }


        private string _pm;
        public string PM
        {
            get { return _pm; }
            set { _pm = value; }
        }

        private string _pic;
        public string PIC
        {
            get { return _pic; }
            set { _pic = value; }
        }


        private decimal _cost;
        public decimal Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        public string CostString
        {
            get { return Math.Ceiling(_cost).ToString("C0"); }
        }


        private decimal _fee;
        public decimal Fee
        {
            get { return _fee; }
            set { _fee = value; }
        }

        public string FeeString
        {
            get { return Math.Ceiling(_fee).ToString("C0"); }
        }

        private decimal _billing;
        public decimal Billing
        {
            get { return _billing; }
            set { _billing = value; }
        }

        public string BillingString
        {
            get { return Math.Ceiling(_billing).ToString("C0"); }
        }

        public decimal Profit
        {
            get { return Billing - Cost; }
        }

        public string ProfitString
        {
            get { return Math.Ceiling(Profit).ToString("C0"); }
        }

        private decimal _ar;
        public decimal AR
        {
            get { return _ar; }

            set { _ar = value; }
        }

        public string ARString
        {
            get { return Math.Ceiling(_ar).ToString("C0"); }
        }
        #endregion

        private static DateTime _currentFiscalYearStart;
        private static DateTime _currentFiscalYearEnd;
        private static DateTime _previousMonthStart;

        private static DateTime _previousMonthEnd;
        private static DateTime CurrentFiscalYearStart
        {
            get
            {
                if (_currentFiscalYearStart == DateTime.MinValue)
                {
                    SetCurrentFiscalYear();
                }
                return _currentFiscalYearStart;
            }
            set { _currentFiscalYearStart = value; }
        }

        private static DateTime CurrentFiscalYearEnd
        {
            get
            {
                if (_currentFiscalYearEnd == DateTime.MinValue)
                {
                    SetCurrentFiscalYear();
                }
                return _currentFiscalYearEnd;
            }
            set { _currentFiscalYearEnd = value; }
        }

        private static DateTime PreviousMonthStart
        {
            get
            {
                if (_previousMonthStart == DateTime.MinValue)
                {
                    SetPreviousMonth();
                }
                return _previousMonthStart;
            }
            set { _previousMonthStart = value; }
        }

        private static DateTime PreviousMonthEnd
        {
            get
            {
                if (_previousMonthEnd == DateTime.MinValue)
                {
                    SetPreviousMonth();
                }
                return _previousMonthEnd;
            }
            set { _previousMonthEnd = value; }
        }

        private static void SetCurrentFiscalYear()
        {
            DateTime today = DateTime.Now;

            if ((today.Month >= 6))
            {
                _currentFiscalYearStart = new DateTime(today.Year, 6, 1);
                _currentFiscalYearEnd = new DateTime(today.AddYears(1).Year, 5, 31);
            }
            else
            {
                _currentFiscalYearStart = new DateTime(today.AddYears(-1).Year, 6, 1);
                _currentFiscalYearEnd = new DateTime(today.Year, 5, 31);
            }
        }

        private static void SetPreviousMonth()
        {
            DateTime previousMonth = DateTime.Now.AddMonths(-1);

            _previousMonthStart = new DateTime(previousMonth.Year, previousMonth.Month, 1);
            _previousMonthEnd = new DateTime(previousMonth.Year, previousMonth.Month, DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month));
        }

        public static List<BillQuickProject> GetProjectsByPM(int employeeID, BQTimeFrame timeFrame)
        {
            List<BillQuickProject> projects = new List<BillQuickProject>();

            try
            {
                SqlConnection con = new SqlConnection(Configuration.ConnectionString);
                con.Open();

                Dictionary<string, string> @params = new Dictionary<string, string>();
                @params.Add("@PM", employeeID.ToString());

                SqlDataReader reader = default(SqlDataReader);

                switch (timeFrame)
                {
                    case BQTimeFrame.PreviousMonth:
                        //Dim prevMonth = DateTime.Now.AddMonths(-1)
                        @params.Add("@FromDate", PreviousMonthStart.ToShortDateString());
                        @params.Add("@ToDate", PreviousMonthEnd.ToShortDateString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQProjectsByPMAndTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.CurrentFiscalYear:
                        //Dim startDate = New DateTime(DateTime.Now.AddYears(-1).Year, 6, 1)
                        //Dim endDate = New DateTime(DateTime.Now.Year, 5, 31)
                        @params.Add("@FromDate", CurrentFiscalYearStart.ToShortDateString());
                        @params.Add("@ToDate", CurrentFiscalYearEnd.ToShortDateString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQProjectsByPMAndTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.AllOpen:
                        reader = GetDataReaderByStoredProcedure("sp_GetBQProjectsByPM", @params, con);
                        break;
                }

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BillQuickProject project = new BillQuickProject();
                        project.ProjectID = reader.GetValueOrDefault<string>("ProjectID");
                        project.ProjectName = reader.GetValueOrDefault<string>("ProjectName");
                        project.ClientID = reader.GetValueOrDefault<string>("ClientID");
                        project.ClientName = reader.GetValueOrDefault<string>("ClientCompany");
                        project.PM = reader.GetValueOrDefault<string>("PM");
                        project.PIC = reader.GetValueOrDefault<string>("PIC");
                        project.Cost = reader.GetValueOrDefault<decimal>("TotalCost");
                        project.Fee = reader.GetValueOrDefault<decimal>("Fee");
                        project.Billing = reader.GetValueOrDefault<decimal>("Billing");
                        project.AR = reader.GetValueOrDefault<decimal>("AR");

                        projects.Add(project);
                    }
                }

                con.Close();
                con = null;

            }
            catch (Exception ex)
            {
            }
            return projects;
        }

        public static List<BillQuickProject> GetProjectsByPIC(int employeeID, BQTimeFrame timeFrame)
        {
            List<BillQuickProject> projects = new List<BillQuickProject>();

            try
            {
                SqlConnection con = new SqlConnection(Configuration.ConnectionString);
                con.Open();

                Dictionary<string, string> @params = new Dictionary<string, string>();
                @params.Add("@PIC", employeeID.ToString());

                SqlDataReader reader = default(SqlDataReader);

                switch (timeFrame)
                {
                    case BQTimeFrame.PreviousMonth:
                        //Dim prevMonth = DateTime.Now.AddMonths(-1)
                        @params.Add("@FromDate", PreviousMonthStart.ToString());
                        @params.Add("@ToDate", PreviousMonthEnd.ToString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQProjectsByPICAndTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.CurrentFiscalYear:
                        //Dim startDate = New DateTime(DateTime.Now.AddYears(-1).Year, 6, 1)
                        //Dim endDate = New DateTime(DateTime.Now.Year, 5, 31)
                        @params.Add("@FromDate", CurrentFiscalYearStart.ToString());
                        @params.Add("@ToDate", CurrentFiscalYearEnd.ToString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQProjectsByPICAndTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.AllOpen:
                        reader = GetDataReaderByStoredProcedure("sp_GetBQProjectsByPIC", @params, con);
                        break;
                }

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BillQuickProject project = new BillQuickProject();
                        project.ProjectID = reader.GetValueOrDefault<string>("ProjectID");
                        project.ProjectName = reader.GetValueOrDefault<string>("ProjectName");
                        project.ClientID = reader.GetValueOrDefault<string>("ClientID");
                        project.ClientName = reader.GetValueOrDefault<string>("ClientCompany");
                        project.PM = reader.GetValueOrDefault<string>("PM");
                        project.PIC = reader.GetValueOrDefault<string>("PIC");
                        project.Cost = reader.GetValueOrDefault<decimal>("TotalCost");
                        project.Fee = reader.GetValueOrDefault<decimal>("Fee");
                        project.Billing = reader.GetValueOrDefault<decimal>("Billing");
                        project.AR = reader.GetValueOrDefault<decimal>("AR");

                        projects.Add(project);
                    }
                }

                con.Close();
                con = null;

            }
            catch (Exception ex)
            {
            }
            return projects;
        }

        public static List<BillQuickProject> GetProjects(BQTimeFrame timeFrame)
        {
            List<BillQuickProject> projects = new List<BillQuickProject>();

            try
            {
                SqlConnection con = new SqlConnection(Configuration.ConnectionString);
                con.Open();

                Dictionary<string, string> @params = new Dictionary<string, string>();
                SqlDataReader reader = default(SqlDataReader);

                switch (timeFrame)
                {
                    case BQTimeFrame.PreviousMonth:
                        //Dim prevMonth = DateTime.Now.AddMonths(-1)
                        @params.Add("@FromDate", PreviousMonthStart.ToString());
                        @params.Add("@ToDate", PreviousMonthEnd.ToString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQProjectsByTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.CurrentFiscalYear:
                        //Dim startDate = New DateTime(DateTime.Now.AddYears(-1).Year, 6, 1)
                        //Dim endDate = New DateTime(DateTime.Now.Year, 5, 31)
                        @params.Add("@FromDate", CurrentFiscalYearStart.ToString());
                        @params.Add("@ToDate", CurrentFiscalYearEnd.ToString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQProjectsByTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.AllOpen:
                        reader = GetDataReaderByStoredProcedure("sp_GetBQProjects", @params, con);
                        break;
                }

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BillQuickProject project = new BillQuickProject();
                        project.ProjectID = reader.GetValueOrDefault<string>("ProjectID");
                        project.ProjectName = reader.GetValueOrDefault<string>("ProjectName");
                        project.ClientID = reader.GetValueOrDefault<string>("ClientID");
                        project.ClientName = reader.GetValueOrDefault<string>("ClientCompany");
                        project.PM = reader.GetValueOrDefault<string>("PM");
                        project.PIC = reader.GetValueOrDefault<string>("PIC");
                        project.Cost = reader.GetValueOrDefault<decimal>("TotalCost");
                        project.Fee = reader.GetValueOrDefault<decimal>("Fee");
                        project.Billing = reader.GetValueOrDefault<decimal>("Billing");
                        project.AR = reader.GetValueOrDefault<decimal>("AR");

                        projects.Add(project);
                    }
                }

                con.Close();
                con = null;

            }
            catch (Exception ex)
            {
            }
            return projects;
        }

        public static List<BillQuickProject> GetClientsByPM(int employeeID, BQTimeFrame timeFrame)
        {
            List<BillQuickProject> projects = new List<BillQuickProject>();

            try
            {
                SqlConnection con = new SqlConnection(Configuration.ConnectionString);
                con.Open();

                Dictionary<string, string> @params = new Dictionary<string, string>();
                @params.Add("@PM", employeeID.ToString());

                SqlDataReader reader = default(SqlDataReader);

                switch (timeFrame)
                {
                    case BQTimeFrame.PreviousMonth:
                        //Dim prevMonth = DateTime.Now.AddMonths(-1)
                        @params.Add("@FromDate", PreviousMonthStart.ToString());
                        @params.Add("@ToDate", PreviousMonthEnd.ToString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQClientsByPMAndTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.CurrentFiscalYear:
                        //Dim startDate = New DateTime(DateTime.Now.AddYears(-1).Year, 6, 1)
                        //Dim endDate = New DateTime(DateTime.Now.Year, 5, 31)
                        @params.Add("@FromDate", CurrentFiscalYearStart.ToString());
                        @params.Add("@ToDate", CurrentFiscalYearEnd.ToString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQClientsByPMAndTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.AllOpen:
                        reader = GetDataReaderByStoredProcedure("sp_GetBQClientsByPM", @params, con);
                        break;
                }

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BillQuickProject project = new BillQuickProject();
                        project.ClientName = reader.GetValueOrDefault<string>("ClientCompany");
                        project.Cost = reader.GetValueOrDefault<decimal>("TotalCost");
                        project.Fee = reader.GetValueOrDefault<decimal>("Fee");
                        project.Billing = reader.GetValueOrDefault<decimal>("Billing");
                        project.AR = reader.GetValueOrDefault<decimal>("AR");

                        projects.Add(project);
                    }
                }

                con.Close();
                con = null;

            }
            catch (Exception ex)
            {
            }
            return projects;
        }

        public static List<BillQuickProject> GetClientsByPIC(int employeeID, BQTimeFrame timeFrame)
        {
            List<BillQuickProject> projects = new List<BillQuickProject>();

            try
            {
                SqlConnection con = new SqlConnection(Configuration.ConnectionString);
                con.Open();

                Dictionary<string, string> @params = new Dictionary<string, string>();
                @params.Add("@PIC", employeeID.ToString());

                SqlDataReader reader = default(SqlDataReader);

                switch (timeFrame)
                {
                    case BQTimeFrame.PreviousMonth:
                        //Dim prevMonth = DateTime.Now.AddMonths(-1)
                        @params.Add("@FromDate", PreviousMonthStart.ToString());
                        @params.Add("@ToDate", PreviousMonthEnd.ToString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQClientsByPICAndTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.CurrentFiscalYear:
                        //Dim startDate = New DateTime(DateTime.Now.AddYears(-1).Year, 6, 1)
                        //Dim endDate = New DateTime(DateTime.Now.Year, 5, 31)
                        @params.Add("@FromDate", CurrentFiscalYearStart.ToString());
                        @params.Add("@ToDate", CurrentFiscalYearEnd.ToString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQClientsByPICAndTimeFrame", @params, con);
                        break;
                    //reader = GetDataReaderByStoredProcedure("sp_GetBQClientsByPICCurrentFiscalYear", params, con)
                    case BQTimeFrame.AllOpen:
                        reader = GetDataReaderByStoredProcedure("sp_GetBQClientsByPIC", @params, con);
                        break;
                }

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BillQuickProject project = new BillQuickProject();
                        project.ClientName = reader.GetValueOrDefault<string>("ClientCompany");
                        project.Cost = reader.GetValueOrDefault<decimal>("TotalCost");
                        project.Fee = reader.GetValueOrDefault<decimal>("Fee");
                        project.Billing = reader.GetValueOrDefault<decimal>("Billing");
                        project.AR = reader.GetValueOrDefault<decimal>("AR");

                        projects.Add(project);
                    }
                }

                con.Close();
                con = null;

            }
            catch (Exception ex)
            {
            }
            return projects;
        }

        public static List<BillQuickProject> GetClients(BQTimeFrame timeFrame)
        {
            List<BillQuickProject> projects = new List<BillQuickProject>();

            try
            {
                SqlConnection con = new SqlConnection(Configuration.ConnectionString);
                con.Open();

                Dictionary<string, string> @params = new Dictionary<string, string>();
                SqlDataReader reader = default(SqlDataReader);

                switch (timeFrame)
                {
                    case BQTimeFrame.PreviousMonth:
                        //Dim prevMonth = DateTime.Now.AddMonths(-1)
                        @params.Add("@FromDate", PreviousMonthStart.ToString());
                        @params.Add("@ToDate", PreviousMonthEnd.ToString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQClientsByTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.CurrentFiscalYear:
                        //Dim startDate = New DateTime(DateTime.Now.AddYears(-1).Year, 6, 1)
                        //Dim endDate = New DateTime(DateTime.Now.Year, 5, 31)
                        @params.Add("@FromDate", CurrentFiscalYearStart.ToString());
                        @params.Add("@ToDate", CurrentFiscalYearEnd.ToString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQClientsByTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.AllOpen:
                        reader = GetDataReaderByStoredProcedure("sp_GetBQClients", @params, con);
                        break;
                }

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BillQuickProject project = new BillQuickProject();
                        project.ClientName = reader.GetValueOrDefault<string>("ClientCompany");
                        project.Cost = reader.GetValueOrDefault<decimal>("TotalCost");
                        project.Fee = reader.GetValueOrDefault<decimal>("Fee");
                        project.Billing = reader.GetValueOrDefault<decimal>("Billing");
                        project.AR = reader.GetValueOrDefault<decimal>("AR");

                        projects.Add(project);
                    }
                }

                con.Close();
                con = null;

            }
            catch (Exception ex)
            {
            }
            return projects;
        }

        public static List<BillQuickProject> GetPMsByPM(int employeeID, BQTimeFrame timeFrame)
        {
            List<BillQuickProject> projects = new List<BillQuickProject>();

            try
            {
                SqlConnection con = new SqlConnection(Configuration.ConnectionString);
                con.Open();

                Dictionary<string, string> @params = new Dictionary<string, string>();
                @params.Add("@PM", employeeID.ToString());

                var sproc = string.Empty;
                

                switch (timeFrame)
                {
                    case BQTimeFrame.PreviousMonth:
                        //Dim prevMonth = DateTime.Now.AddMonths(-1)
                        @params.Add("@FromDate", PreviousMonthStart.ToString());
                        @params.Add("@ToDate", PreviousMonthEnd.ToString());
                        sproc = "sp_GetBQPMsByPMAndTimeFrame";
                        break;
                    case BQTimeFrame.CurrentFiscalYear:
                        //Dim startDate = New DateTime(DateTime.Now.AddYears(-1).Year, 6, 1)
                        //Dim endDate = New DateTime(DateTime.Now.Year, 5, 31)
                        @params.Add("@FromDate", CurrentFiscalYearStart.ToString());
                        @params.Add("@ToDate", CurrentFiscalYearEnd.ToString());
                        sproc = "sp_GetBQPMsByPMAndTimeFrame";
                        break;
                    case BQTimeFrame.AllOpen:
                        sproc = "sp_GetBQPMsByPM";
                        break;
                }

                if (!string.IsNullOrEmpty(sproc))
                {
                    var reader = GetDataReaderByStoredProcedure(sproc, @params, con);

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            BillQuickProject project = new BillQuickProject();
                            project.PM = reader.GetValueOrDefault<string>("PM");
                            project.Cost = reader.GetValueOrDefault<decimal>("TotalCost");
                            project.Fee = reader.GetValueOrDefault<decimal>("Fee");
                            project.Billing = reader.GetValueOrDefault<decimal>("Billing");
                            project.AR = reader.GetValueOrDefault<decimal>("AR");

                            projects.Add(project);
                        }
                    }
                }
                con.Close();
                con = null;

            }
            catch (Exception ex)
            {
            }
            return projects;
        }

        public static List<BillQuickProject> GetPMsByPIC(int employeeID, BQTimeFrame timeFrame)
        {
            List<BillQuickProject> projects = new List<BillQuickProject>();

            try
            {
                SqlConnection con = new SqlConnection(Configuration.ConnectionString);
                con.Open();

                Dictionary<string, string> @params = new Dictionary<string, string>();
                @params.Add("@PIC", employeeID.ToString());

                var sproc = string.Empty;

                switch (timeFrame)
                {
                    case BQTimeFrame.PreviousMonth:
                        //Dim prevMonth = DateTime.Now.AddMonths(-1)
                        @params.Add("@FromDate", PreviousMonthStart.ToString());
                        @params.Add("@ToDate", PreviousMonthEnd.ToString());
                        sproc = "sp_GetBQPMsByPICAndTimeFrame";
                        break;
                    case BQTimeFrame.CurrentFiscalYear:
                        //Dim startDate = New DateTime(DateTime.Now.AddYears(-1).Year, 6, 1)
                        //Dim endDate = New DateTime(DateTime.Now.Year, 5, 31)
                        @params.Add("@FromDate", CurrentFiscalYearStart.ToString());
                        @params.Add("@ToDate", CurrentFiscalYearEnd.ToString());
                        sproc = "sp_GetBQPMsByPICAndTimeFrame";
                        break;
                    case BQTimeFrame.AllOpen:
                        sproc = "sp_GetBQPMsByPIC";
                        break;
                }

                if (!string.IsNullOrEmpty(sproc))
                {
                    var reader = GetDataReaderByStoredProcedure(sproc, @params, con);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            BillQuickProject project = new BillQuickProject();
                            project.PM = reader.GetValueOrDefault<string>("PM");
                            project.Cost = reader.GetValueOrDefault<decimal>("TotalCost");
                            project.Fee = reader.GetValueOrDefault<decimal>("Fee");
                            project.Billing = reader.GetValueOrDefault<decimal>("Billing");
                            project.AR = reader.GetValueOrDefault<decimal>("AR");

                            projects.Add(project);
                        }
                    }

                }
                con.Close();
                con = null;

            }
            catch (Exception ex)
            {
            }
            return projects;
        }

        public static List<BillQuickProject> GetPICs(BQTimeFrame timeFrame)
        {
            List<BillQuickProject> projects = new List<BillQuickProject>();

            try
            {
                SqlConnection con = new SqlConnection(Configuration.ConnectionString);
                con.Open();

                Dictionary<string, string> @params = new Dictionary<string, string>();
                SqlDataReader reader = default(SqlDataReader);

                switch (timeFrame)
                {
                    case BQTimeFrame.PreviousMonth:
                        //Dim prevMonth = DateTime.Now.AddMonths(-1)
                        @params.Add("@FromDate", PreviousMonthStart.ToString());
                        @params.Add("@ToDate", PreviousMonthEnd.ToString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQPICsByTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.CurrentFiscalYear:
                        //Dim startDate = New DateTime(DateTime.Now.AddYears(-1).Year, 6, 1)
                        //Dim endDate = New DateTime(DateTime.Now.Year, 5, 31)
                        @params.Add("@FromDate", CurrentFiscalYearStart.ToString());
                        @params.Add("@ToDate", CurrentFiscalYearEnd.ToString());
                        reader = GetDataReaderByStoredProcedure("sp_GetBQPICsByTimeFrame", @params, con);
                        break;
                    case BQTimeFrame.AllOpen:
                        reader = GetDataReaderByStoredProcedure("sp_GetBQPICs", @params, con);
                        break;
                }

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BillQuickProject project = new BillQuickProject();
                        project.PIC = reader.GetValueOrDefault<string>("PIC");
                        project.Cost = reader.GetValueOrDefault<decimal>("TotalCost");
                        project.Fee = reader.GetValueOrDefault<decimal>("Fee");
                        project.Billing = reader.GetValueOrDefault<decimal>("Billing");
                        project.AR = reader.GetValueOrDefault<decimal>("AR");

                        projects.Add(project);
                    }
                }

                con.Close();
                con = null;

            }
            catch (Exception ex)
            {
            }
            return projects;
        }

        protected static SqlDataReader GetDataReaderByStoredProcedure(string procedureName, Dictionary<string, string> @params, SqlConnection con)
        {
            SqlDataReader rdr = default(SqlDataReader);

            try
            {
                if (!(con.State == ConnectionState.Open))
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand(procedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;

                AddParamaters(cmd, @params);

                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured in GetDataReaderByStoredProcedure.", ex);
            }

            return rdr;
        }

        private static void AddParamaters(SqlCommand cmd, Dictionary<string, string> @params)
        {
            foreach (KeyValuePair<string, string> pair in @params)
            {
                if (pair.Value != DBNull.Value.ToString())
                {
                    cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(pair.Key, DBNull.Value);
                }

            }
        }

        public static BQTimeFrame GetTimeFrame(string value)
        {
            switch (value)
            {
                case "CurYear":
                    return BQTimeFrame.CurrentFiscalYear;
                case "AllOpen":
                    return BQTimeFrame.AllOpen;
                default:
                    return BQTimeFrame.PreviousMonth;
            }
        }
    }



    public enum BQTimeFrame
    {
        PreviousMonth = 1,
        CurrentFiscalYear = 2,
        AllOpen = 3
    }

}
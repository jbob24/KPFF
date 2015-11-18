
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Configuration;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP
{
    public class WeekYear
    {
        public int Week = 0;
        public int Year = 0;
        public string MondayDate = "";
    }

    public class SearchFilter
    {
        public string Field = "";
        public string Text = "";
    }

    public class clsSchedule
    {
        clsGeneral general = new clsGeneral();
        // Constants 
        public const int cMoveWeeks = 4;
        public const int cWeekSpan = 19;
        public const decimal cWeekHours = 40;
        //
        public DateTime GetFirstWeekStartDate(System.Object intYear)
        {
            System.DateTime dtm = Convert.ToDateTime("1/1/" + intYear);
            //int intWeekDay = (int)dtm.DayOfWeek; // DatePart(DateInterval.Weekday, dtm);
            //while (intWeekDay != 1)
            while (dtm.DayOfWeek != DayOfWeek.Monday)
            {
                dtm = dtm.AddDays(1); //dtm = DateAdd(DateInterval.Day, 1, dtm);
                //intWeekDay = (int)dtm.DayOfWeek; // DatePart(DateInterval.Weekday, dtm);
            }
            return dtm; // Convert.ToString(dtm);
        }

        public virtual DateTime GetWeekMondayDate(System.DateTime dtmDate)
        {
            //string strWeekMondayDate = null;
            System.DateTime dtmWeekMonday = default(System.DateTime);
            int intWeekDay = 0;
            //
            //dtmWeekMonday = dtmDate
            intWeekDay = (int)dtmDate.DayOfWeek; // DatePart(DateInterval.Weekday, dtmDate);
            //While intWeekDay <> 2
            //    dtmWeekMonday = DateAdd(DateInterval.Weekday, -1, dtmWeekMonday)
            //End While
            dtmWeekMonday = dtmDate.AddDays(-((int)dtmDate.DayOfWeek) + 1);
            //strWeekMondayDate = Convert.ToString(DateValue(dtmWeekMonday));
            return dtmWeekMonday;
        }

        public virtual string GetWeekMondayDate(int intWeek, int intYear)
        {
            System.DateTime dtm = GetFirstWeekStartDate(intYear);
            int intI = 0;
            //
            for (intI = 2; intI <= intWeek; intI++)
            {
                dtm = dtm.AddDays(7); // DateAdd(DateInterval.Day, 7, dtm);
            }
            //
            return Convert.ToString(dtm);
        }

        public WeekYear GetWeekYear(System.DateTime dtmDate)
        {
            WeekYear wyCurrent = new WeekYear();
            string strWeekMondayDate = GetWeekMondayDate(dtmDate).ToShortDateString();
            // Get Week No and Year from table
            int intCurrWeek = 0;
            int intCurrYear = 0;
            string strSQL = null;
            strSQL = "SELECT WeekNo, WeekNoYear ";
            strSQL += "FROM tblWeeks ";
            strSQL += "WHERE WeekNoStartDate = '" + Convert.ToDateTime(strWeekMondayDate) + "'";
            DataSet ds = general.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                intCurrWeek = Convert.ToInt32(dt.Rows[0]["WeekNo"]);
                intCurrYear = Convert.ToInt32(dt.Rows[0]["WeekNoYear"]);
            }
            dt.Dispose();
            ds.Dispose();
            dt = null;
            ds = null;
            //
            wyCurrent.Week = intCurrWeek;
            wyCurrent.Year = intCurrYear;
            wyCurrent.MondayDate = strWeekMondayDate;
            //
            return wyCurrent;
        }
        ////
        //public WeekYear GetWeekYear_Old(System.DateTime dtmDate)
        //{
        //    WeekYear wyCurrent = new WeekYear();
        //    int intCurrWeek = DatePart(DateInterval.WeekOfYear, dtmDate);
        //    int intCurrYear = DatePart(DateInterval.Year, dtmDate);
        //    string strWeekMondayDate = GetWeekMondayDate(intCurrWeek, intCurrYear);
        //    //
        //    wyCurrent.Week = intCurrWeek;
        //    wyCurrent.Year = intCurrYear;
        //    wyCurrent.MondayDate = strWeekMondayDate;
        //    //
        //    return wyCurrent;
        //}

        public string GetCurrentWeekLabel()
        {
            WeekYear wyCurrent = GetWeekYear(System.DateTime.Now);
            System.DateTime dtm = Convert.ToDateTime(wyCurrent.MondayDate);
            string strWeekLabel = string.Format("{0}/{1}", dtm.Month.ToString(), dtm.Day.ToString("00")); // DatePart(DateInterval.Month, dtm) + "/" + Strings.Format(DatePart(DateInterval.Day, dtm), "00");
            return strWeekLabel;
        }

        public int GetNoOfWeeks(int intYear)
        {
            int intNoOfWeeks = 0;
            string strSQL = "";
            strSQL = "SELECT COUNT(*) AS NoOfWeeks ";
            strSQL += "FROM tblWeeks ";
            strSQL += "WHERE WeekNoYear = " + intYear;
            DataSet ds = general.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                intNoOfWeeks = Convert.ToInt32(dt.Rows[0]["NoOfWeeks"]);
            }
            dt.Dispose();
            ds.Dispose();
            dt = null;
            ds = null;
            //
            return intNoOfWeeks;
        }

        public WeekYear GetWeekYearFirst(WeekYear wyCurrent, bool blnNext)
        {
            int intWeek = wyCurrent.Week;
            int intYear = wyCurrent.Year;
            int intWeekFirst = 0;
            int intYearFirst = 0;
            WeekYear wyFirst = new WeekYear();
            int intNoOfWeeks = GetNoOfWeeks(intYear);
            //
            // Check how many weeks in the year

            // Forward
            if (blnNext)
            {
                intWeekFirst = intWeek + cMoveWeeks;
                intYearFirst = intYear;
                //If intWeekFirst > 52 Then
                //    intWeekFirst = intWeekFirst - 52
                //    intYearFirst = intYear + 1
                //End If
                if (intWeekFirst > intNoOfWeeks)
                {
                    intWeekFirst = intWeekFirst - intNoOfWeeks;
                    intYearFirst = intYear + 1;
                }
                // Back
            }
            else
            {
                intWeekFirst = intWeek - cMoveWeeks;
                intYearFirst = intYear;
                //If intWeekFirst <= 0 Then
                //    intWeekFirst = 52 + intWeekFirst
                //    intYearFirst = intYear - 1
                //End If
                if (intWeekFirst <= 0)
                {
                    intWeekFirst = intNoOfWeeks + intWeekFirst;
                    intYearFirst = intYear - 1;
                }
            }
            //
            wyFirst.Week = intWeekFirst;
            wyFirst.Year = intYearFirst;
            wyFirst.MondayDate = GetWeekMondayDate(intWeekFirst, intYearFirst);
            //
            return wyFirst;
        }

        public WeekYear GetWeekYearLast(WeekYear wyFirst)
        {
            int intWeek = wyFirst.Week;
            int intYear = wyFirst.Year;
            int intWeekLast = 0;
            int intYearLast = 0;
            WeekYear wyLast = new WeekYear();
            int intNoOfWeeks = GetNoOfWeeks(intYear);
            //
            intWeekLast = intWeek + cWeekSpan;
            intYearLast = intYear;
            //If intWeekLast > 52 Then
            //    intWeekLast = intWeekLast - 52
            //    intYearLast = intYear + 1
            //End If
            if (intWeekLast > intNoOfWeeks)
            {
                intWeekLast = intWeekLast - intNoOfWeeks;
                intYearLast = intYear + 1;
            }
            //
            wyLast.Week = intWeekLast;
            wyLast.Year = intYearLast;
            wyLast.MondayDate = GetWeekMondayDate(intWeekLast, intYearLast);
            //
            return wyLast;
        }

        public int GetGridHeight(string strHeight)
        {
            int intHeight = 0;
            if (Convert.ToInt32(strHeight) > 0)
            {
                intHeight = Convert.ToInt32(strHeight);
                intHeight = intHeight - 390;
            }
            else
            {
                intHeight = 493;
            }
            return intHeight;
        }

        public string GetEmployeeCode(int intEmployeeID)
        {
            string strConn = ConfigurationManager.AppSettings["ConnectionString"];
            string strSQL = null;
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = default(DataSet);
            DataTable dt = default(DataTable);
            string strEmployeeCode = "";
            //
            strSQL = "SELECT EmployeeCode ";
            strSQL += "FROM tblEmployees ";
            strSQL += "WHERE EmployeeID = " + intEmployeeID;
            //
            da = new SqlDataAdapter(strSQL, strConn);
            ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                strEmployeeCode = dt.Rows[0]["EmployeeCode"].GetValueOrDefault<string>();
            }
            //
            da.Dispose();
            ds.Dispose();
            dt.Dispose();
            da = null;
            ds = null;
            dt = null;

            return strEmployeeCode;
        }

        public decimal GetEmployeeWeekTotal(int intEmpID, int intWeekID)
        {
            string strConn = ConfigurationManager.AppSettings["ConnectionString"];
            string strSQL = null;
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = default(DataSet);
            DataTable dt = default(DataTable);
            decimal decWeekHours = 0;
            //
            strSQL = "SELECT SUM(tblSchedule.Hours) AS WeekHours ";
            strSQL += "FROM tblSchedule INNER JOIN ";
            strSQL += "tblProjects ON ";
            strSQL += "tblSchedule.ProjectID = tblProjects.ID ";
            strSQL += "WHERE (tblSchedule.EmployeeID = " + intEmpID + ") ";
            strSQL += "AND (tblSchedule.WeekID = " + intWeekID + ") ";
            strSQL += "AND (tblProjects.Active = 1)";
            //
            da = new SqlDataAdapter(strSQL, strConn);
            ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                decWeekHours = Convert.ToDecimal(dt.Rows[0]["WeekHours"]);
            }
            //
            da.Dispose();
            ds.Dispose();
            dt.Dispose();
            da = null;
            ds = null;
            dt = null;
            //
            return decWeekHours;
        }

        public string GetWeekHoursStyle(int val)
        {
            string strStyle = null;
            if (val < 40)
            {
                strStyle = "CellUnderSyle";
            }
            else if (val >= 40 & val <= 43)
            {
                strStyle = "CellScheduledStyle";
            }
            else if (val >= 44 & val <= 50)
            {
                strStyle = "Cell1025Style";
            }
            else if (val > 50)
            {
                strStyle = "CellOver25Style";
            }
            //
            return strStyle;
        }

        public string GetProjectTitle(int intProjectID)
        {
            string strProjNo = "";
            string strProjName = "";
            string strProj = "Project";
            string strSQL = null;
            string strConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(strConn);
            strSQL = "SELECT ProjectNo, ProjectName ";
            strSQL += "FROM tblProjects ";
            strSQL += "WHERE ID = " + intProjectID;
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            {
                strProjNo = dr.GetValueOrDefault<string>("ProjectNo");
                strProjName = dr.GetValueOrDefault<string>("ProjectName");
            }
            dr.Close();
            conn.Dispose();
            cmd.Dispose();
            conn = null;
            cmd = null;
            dr = null;
            //
            if (!string.IsNullOrEmpty(strProjNo) & !string.IsNullOrEmpty(strProjName))
            {
                strProj = string.Format("{0}-{1}", strProjName, strProjNo).ToUpper(); // Strings.UCase(strProjName + "-" + strProjNo);
            }
            //
            return strProj;
        }

        public int ActiveProject(int intProjectID)
        {
            bool blnActive = false;
            int intActive = 0;
            string strSQL = null;
            string strConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(strConn);
            strSQL = "SELECT ID, Active ";
            strSQL += "FROM tblProjects ";
            strSQL += "WHERE ID = " + intProjectID;
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            conn.Open();
            //
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            {
                blnActive = dr.GetValueOrDefault<bool>("Active");
            }
            //
            dr.Close();
            conn.Dispose();
            cmd.Dispose();
            conn = null;
            cmd = null;
            dr = null;
            //
            intActive = Convert.ToInt32(blnActive);
            intActive = System.Math.Abs(intActive);
            //
            return intActive;
        }
    }
}
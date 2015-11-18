using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace KPFF.Data.Entities
{
    public class ClsSchedule : DABase
    {
        clsGeneral general = new clsGeneral();
        
        public const int cMoveWeeks = 4;
        public const int cWeekSpan = 19;
        public const Decimal cWeekHours = 40;

        public ClsSchedule()
        {
        }

        public string GetFirstWeekStartDate(int year)
        {
            DateTime dtm = Convert.ToDateTime("1/1/" + year);
            int intWeekDay = DateAndTime.DatePart(DateInterval.Weekday, dtm);
            while (intWeekDay != 2)
            {
                dtm = DateAndTime.DateAdd(DateInterval.Day, 1, dtm);
                intWeekDay = DateAndTime.DatePart(DateInterval.Weekday, dtm);
            }
            return dtm.ToShortDateString();
        }

        public virtual string GetWeekMondayDate(DateTime dtmDate)
        {
            DateTime dtmWeekMonday;
            int intWeekDay;

            intWeekDay = DateAndTime.DatePart(DateInterval.Weekday, dtmDate);
            dtmWeekMonday = dtmDate.AddDays(((int)dtmDate.DayOfWeek * -1) + 1);
            return dtmWeekMonday.ToShortDateString();
        }

        public virtual string GetWeekMondayDate(int intWeek, int intYear)
        {
            DateTime dtm = DateTime.Parse(GetFirstWeekStartDate(intYear));
            int intI;
            // 
            for (intI = 2; (intI <= intWeek); intI++)
            {
                dtm = DateAndTime.DateAdd(DateInterval.Day, 7, dtm);
            }
            // 
            return dtm.ToString();
        }

        public WeekYear GetWeekYear(DateTime dtmDate)
        {
            WeekYear wyCurrent = new WeekYear();
            string strWeekMondayDate = GetWeekMondayDate(dtmDate);
            //  Get Week No and Year from table
            int intCurrWeek = 0;
            int intCurrYear = 0;
            string strSQL;
            strSQL = "SELECT WeekNo, WeekNoYear FROM tblWeeks WHERE WeekNoStartDate = \'" + (DateTime.Parse(strWeekMondayDate) + "\'");
            DataSet ds = general.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];
            if ((dt.Rows.Count > 0))
            {
                intCurrWeek = Convert.ToInt32(Utilities.Nz(dt.Rows[0]["WeekNo"], 0));
                intCurrYear = Convert.ToInt32(Utilities.Nz(dt.Rows[0]["WeekNoYear"], 0));
            }
            dt.Dispose();
            ds.Dispose();
            dt = null;
            ds = null;
            wyCurrent.Week = intCurrWeek;
            wyCurrent.Year = intCurrYear;
            wyCurrent.MondayDate = DateTime.Parse(strWeekMondayDate);
            // 
            return wyCurrent;
        }

        // 
        public WeekYear GetWeekYear_Old(DateTime dtmDate) {
        WeekYear wyCurrent = new WeekYear();
        int intCurrWeek = DateAndTime.DatePart(DateInterval.WeekOfYear, dtmDate);
        int intCurrYear = DateAndTime.DatePart(DateInterval.Year, dtmDate);
        string strWeekMondayDate = GetWeekMondayDate(intCurrWeek, intCurrYear);
        // 
        wyCurrent.Week = intCurrWeek;
        wyCurrent.Year = intCurrYear;
        wyCurrent.MondayDate = DateTime.Parse(strWeekMondayDate);
        // 
        return wyCurrent;
    }

        public string GetCurrentWeekLabel()
        {
            WeekYear wyCurrent = GetWeekYear(DateTime.Now);
            DateTime dtm = Convert.ToDateTime(wyCurrent.MondayDate);
            //string strWeekLabel = (DateAndTime.DatePart(DateInterval.Month, dtm) + ("/" + DateAndTime.DatePart(DateInterval.Day, dtm).ToString().PadLeft('0')));
            return string.Format("{0}/{1}", dtm.Month.ToString(), dtm.Day.ToString().PadLeft(2, '0')); ;
        }

        public int GetNoOfWeeks(int intYear) {
        int intNoOfWeeks = 0;
        string strSQL = "";
        strSQL = "SELECT COUNT(*) AS NoOfWeeks FROM tblWeeks WHERE WeekNoYear = " + intYear.ToString();
        DataSet ds = general.FillDataset(strSQL);
        DataTable dt = ds.Tables[0];
        if ((dt.Rows.Count > 0)) {
            intNoOfWeeks = Convert.ToInt32(Utilities.Nz(dt.Rows[0]["NoOfWeeks"], 0));
        }
        dt.Dispose();
        ds.Dispose();
        dt = null;
        ds = null;
        return intNoOfWeeks;
    }

        public WeekYear GetWeekYearFirst(WeekYear wyCurrent, bool blnNext)
        {
            int intWeek = wyCurrent.Week;
            int intYear = wyCurrent.Year;
            int intWeekFirst;
            int intYearFirst;
            WeekYear wyFirst = new WeekYear();
            int intNoOfWeeks = GetNoOfWeeks(intYear);
            // 
            //  Check how many weeks in the year
            if (blnNext)
            {
                //  Forward
                intWeekFirst = (intWeek + cMoveWeeks);
                intYearFirst = intYear;
                // If intWeekFirst > 52 Then
                //     intWeekFirst = intWeekFirst - 52
                //     intYearFirst = intYear + 1
                // End If
                if ((intWeekFirst > intNoOfWeeks))
                {
                    intWeekFirst = (intWeekFirst - intNoOfWeeks);
                    intYearFirst = (intYear + 1);
                }
            }
            else
            {
                //  Back
                intWeekFirst = (intWeek - cMoveWeeks);
                intYearFirst = intYear;
                // If intWeekFirst <= 0 Then
                //     intWeekFirst = 52 + intWeekFirst
                //     intYearFirst = intYear - 1
                // End If
                if ((intWeekFirst <= 0))
                {
                    intWeekFirst = (intNoOfWeeks + intWeekFirst);
                    intYearFirst = (intYear - 1);
                }
            }
            // 
            wyFirst.Week = intWeekFirst;
            wyFirst.Year = intYearFirst;
            wyFirst.MondayDate = DateTime.Parse(GetWeekMondayDate(intWeekFirst, intYearFirst));
            // 
            return wyFirst;
        }

        public WeekYear GetWeekYearLast(WeekYear wyFirst)
        {
            int intWeek = wyFirst.Week;
            int intYear = wyFirst.Year;
            int intWeekLast;
            int intYearLast;
            WeekYear wyLast = new WeekYear();
            int intNoOfWeeks = GetNoOfWeeks(intYear);
            // 
            intWeekLast = (intWeek + cWeekSpan);
            intYearLast = intYear;
            // If intWeekLast > 52 Then
            //     intWeekLast = intWeekLast - 52
            //     intYearLast = intYear + 1
            // End If
            if ((intWeekLast > intNoOfWeeks))
            {
                intWeekLast = (intWeekLast - intNoOfWeeks);
                intYearLast = (intYear + 1);
            }
            // 
            wyLast.Week = intWeekLast;
            wyLast.Year = intYearLast;
            wyLast.MondayDate = DateTime.Parse(GetWeekMondayDate(intWeekLast, intYearLast));
            // 
            return wyLast;
        }

        public int GetGridHeight(string strHeight)
        {
            int intHeight;
            if ((int.Parse(strHeight) > 0))
            {
                intHeight = int.Parse(strHeight);
                intHeight = (intHeight - 390);
            }
            else
            {
                intHeight = 493;
            }
            return intHeight;
        }

        public string GetEmployeeCode(int employeeID) 
        {
            string empCode = string.Empty;
            var conStr = KPFF.Data.Entities.Configuration.ConnectionString;
            var con = new SqlConnection(conStr);

            var parms = new Dictionary<string, string>();
            parms.Add("@EmployeeID", employeeID.ToString());
            var dr = GetDataReaderByStoredProcedure("sp_GetEmployeeCode", parms, con);

            if (dr.HasRows)
            {
                dr.Read();
                empCode = dr.GetValueOrDefault<string>("EmployeeCode");
                dr.Close();
            }

            return empCode;
    }

        public Decimal GetEmployeeWeekTotal(int employeeID, int weekId)
        {
            decimal weekHours = 0;
            var conStr = KPFF.Data.Entities.Configuration.ConnectionString;
            var con = new SqlConnection(conStr);

            var parms = new Dictionary<string, string>();
            parms.Add("@EmployeeID", employeeID.ToString());
            parms.Add("WWeekId", weekId.ToString());
            var dr = GetDataReaderByStoredProcedure("sp_GetEmployeeWeekTotal", parms, con);

            if (dr.HasRows)
            {
                dr.Read();
                weekHours = dr.GetValueOrDefault<decimal>("WeekHours");
                dr.Close();
            }

            return weekHours;
        }

        public string GetWeekHoursStyle(int val)
        {
            string strStyle = string.Empty;
            if (val < 40)
            {
                strStyle = "CellUnderSyle";
            }
            else if (val >= 40 && val <= 43)
            {
                strStyle = "CellScheduledStyle";
            }
            else if (val >= 44 && val <= 50)
            {
                strStyle = "Cell1025Style";
            }
            else if (val > 50)
            {
                strStyle = "CellOver25Style";
            }

            return strStyle;
        }

        public string GetProjectTitle(int projectId)
        {
            string projTitle = string.Empty;
            var conStr = KPFF.Data.Entities.Configuration.ConnectionString;
            var con = new SqlConnection(conStr);

            var parms = new Dictionary<string, string>();
            parms.Add("@ProjectId", projectId.ToString());
            var dr = GetDataReaderByStoredProcedure("sp_GetProjectTitle", parms, con);

            if (dr.HasRows)
            {
                dr.Read();
                projTitle = string.Format("{0}-{1}", dr.GetValueOrDefault<string>("ProjectName"), dr.GetValueOrDefault<decimal>("ProjectNo").ToString());
                dr.Close();
            }

            return projTitle;
        }
    }
}


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAdmin
{
    partial class OfficeSummary : System.Web.UI.Page
    {

        #region " Custom Routines"
        public string strConn = ConfigurationManager.AppSettings["ConnectionString"];
        public int[] intWeekIDs = new int[20];
        public int[] intWeeks = new int[20];
        public int[] intYears = new int[20];
        public string[] strMondayDates = new string[20];
        public string[] strWeekLabel = new string[20];
        public decimal[] decTotalHours = new decimal[20];

        public clsSchedule schedule = new clsSchedule();
        public void SetWeekDateArrays(WeekYear wyFirst, WeekYear wyLast)
        {
            SqlConnection conn = new SqlConnection(strConn);
            SqlDataReader dr = default(SqlDataReader);
            string strSQL = null;
            int intI = 0;
            int intWeekFirst = wyFirst.Week;
            int intYearFirst = wyFirst.Year;
            string strDateFirst = wyFirst.MondayDate;
            int intWeekLast = wyLast.Week;
            int intYearLast = wyLast.Year;
            string strDateLast = wyLast.MondayDate;
            //
            strSQL = "SELECT * FROM tblWeeks ";
            strSQL += "WHERE WeekNoStartDate ";
            strSQL += "BETWEEN '" + strDateFirst + "' ";
            strSQL += "AND '" + strDateLast + "' ";
            strSQL = strSQL + "ORDER BY WeekNoStartDate";
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            conn.Open();
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                intWeekIDs[intI] = dr.GetValueOrDefault<int>("ID");
                intWeeks[intI] = dr.GetValueOrDefault<int>("WeekNo");
                intYears[intI] = dr.GetValueOrDefault<int>("WeekNoYear");
                strMondayDates[intI] = dr.GetValueOrDefault<DateTime>("WeekNoStartDate").ToShortDateString();
                strWeekLabel[intI] = dr.GetValueOrDefault<int>("WeekNoMonth").ToString() + "/" + dr.GetValueOrDefault<int>("WeekNoDay").ToString("00");
                intI += 1;
            }
            dr.Close();
            dr = null;
            conn = null;
        }

        private DataSet GetOverallWorkLoadByHours_Needed(WeekYear wyFirst, WeekYear wyLast)
        {
            string strSQL = null;
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = default(DataSet);
            //
            int intWeekFirst = wyFirst.Week;
            int intYearFirst = wyFirst.Year;
            string strDateFirst = wyFirst.MondayDate;
            int intWeekLast = wyLast.Week;
            int intYearLast = wyLast.Year;
            string strDateLast = wyLast.MondayDate;
            int intK = 0;
            // Set week labels
            SetWeekDateArrays(wyFirst, wyLast);
            //
            strSQL = "SELECT ID";
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                strSQL += ", (SELECT SUM([Hours]) ";
                strSQL += "FROM v_EmployeeScheduleByWeek_ActiveProjects s ";
                strSQL += "WHERE (s.WeekNoStartDate ";
                strSQL += "BETWEEN '" + strDateFirst + "' ";
                strSQL += "AND '" + strDateLast + "') ";
                strSQL += "AND (s.WeekNo = " + intWeeks[intK] + ")) AS [" + strWeekLabel[intK] + "] ";
            }
            strSQL += "FROM tblTemp";
            //
            da = new SqlDataAdapter(strSQL, strConn);
            ds = new DataSet();
            da.Fill(ds);
            //
            return ds;
        }

        private decimal GetOverallWorkLoadByHours_Available(System.DateTime dtmStartDate, System.DateTime dtmEndDate)
        {
            string strSQL = null;
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = default(DataSet);
            DataTable dt = default(DataTable);
            int intI = 0;
            int intEmpID = 0;
            //
            decimal decHoursAvailable = 0;
            //
            strSQL = "SELECT EmployeeID, EmploymentEndDate, ([HoursPerWeek]) AS HoursAvailable ";
            strSQL += "FROM tblEmployees ";
            strSQL += "WHERE ([HoursPerWeek] > 0) ";
            //strSQL &= "AND (Active = 1) "
            strSQL += "AND (EmploymentStartDate <= '" + dtmStartDate + "')";
            da = new SqlDataAdapter(strSQL, strConn);
            ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (intI = 0; intI <= dt.Rows.Count - 1; intI++)
                {
                    intEmpID = dt.Rows[intI]["EmployeeID"].GetValueOrDefault<int>();
                    if (dt.Rows[intI]["EmploymentEndDate"].GetValueOrDefault<DateTime>() == DateTime.MinValue)
                    {
                        decHoursAvailable += dt.Rows[intI]["HoursAvailable"].GetValueOrDefault<decimal>();
                    }
                    else
                    {
                        if (dt.Rows[intI]["EmploymentEndDate"].GetValueOrDefault<DateTime>() >= dtmEndDate)
                        {
                            decHoursAvailable += dt.Rows[intI]["HoursAvailable"].GetValueOrDefault<decimal>();
                        }
                    }
                }
            }
            //
            dt.Dispose();
            ds.Dispose();
            da.Dispose();
            dt = null;
            ds = null;
            da = null;
            //
            return decHoursAvailable;
        }

        private DataSet GetOverallWorkLoadByHours_Available_OLD(WeekYear wyFirst, WeekYear wyLast)
        {
            string strSQL = null;
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = default(DataSet);
            //
            int intWeekFirst = wyFirst.Week;
            int intYearFirst = wyFirst.Year;
            string strDateFirst = wyFirst.MondayDate;
            int intWeekLast = wyLast.Week;
            int intYearLast = wyLast.Year;
            string strDateLast = wyLast.MondayDate;
            int intK = 0;
            // Set week labels
            SetWeekDateArrays(wyFirst, wyLast);
            //
            strSQL = "SELECT ID";
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                strSQL += ", (SELECT COUNT(DISTINCT EmployeeID) ";
                strSQL += "FROM v_EmployeeScheduleByWeek s ";
                strSQL += "WHERE (EmployeeName NOT LIKE '-%') ";
                strSQL += "AND (s.WeekNoStartDate ";
                strSQL += "BETWEEN '" + strDateFirst + "' ";
                strSQL += "AND '" + strDateLast + "') ";
                strSQL += "AND (s.WeekNo = " + intWeeks[intK] + ")) AS [" + strWeekLabel[intK] + "] ";
            }
            strSQL += "FROM tblTemp";
            //
            da = new SqlDataAdapter(strSQL, strConn);
            ds = new DataSet();
            da.Fill(ds);
            //
            return ds;
        }

        private List<int> GetEmployeeWorkLoadByPeople_Under(WeekYear wyFirst, WeekYear wyLast)
        {
            string strSQL = null;
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = default(DataSet);
            var al = new List<int>();
            //
            int intWeekFirst = wyFirst.Week;
            int intYearFirst = wyFirst.Year;
            string strDateFirst = wyFirst.MondayDate;
            int intWeekLast = wyLast.Week;
            int intYearLast = wyLast.Year;
            string strDateLast = wyLast.MondayDate;
            int intK = 0;
            // Set week labels
            SetWeekDateArrays(wyFirst, wyLast);
            //
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                //
                strSQL = "SELECT DISTINCT EmployeeID ";
                strSQL += "FROM v_EmployeeScheduleByWeek_ActiveProjects s ";
                strSQL += "WHERE (EmployeeName NOT LIKE '-%') ";
                strSQL += "AND (s.WeekNoStartDate ";
                strSQL += "BETWEEN '" + strDateFirst + "' ";
                strSQL += "AND '" + strDateLast + "') ";
                strSQL += "AND (s.WeekNo = " + intWeeks[intK] + ") ";
                strSQL += "GROUP BY EmployeeID ";
                strSQL += "HAVING (SUM(Hours) < 40)";
                //
                da = new SqlDataAdapter(strSQL, strConn);
                ds = new DataSet();
                da.Fill(ds);
                //
                al.Add(ds.Tables[0].Rows.Count);
                //
                da.Dispose();
                ds.Dispose();
            }
            //
            return al;
        }

        private List<int> GetEmployeeWorkLoadByPeople_Scheduled(WeekYear wyFirst, WeekYear wyLast)
        {
            string strSQL = null;
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = default(DataSet);
            var al = new List<int>();  //ArrayList al = new ArrayList();
            //
            int intWeekFirst = wyFirst.Week;
            int intYearFirst = wyFirst.Year;
            string strDateFirst = wyFirst.MondayDate;
            int intWeekLast = wyLast.Week;
            int intYearLast = wyLast.Year;
            string strDateLast = wyLast.MondayDate;
            int intK = 0;
            // Set week labels
            SetWeekDateArrays(wyFirst, wyLast);
            //
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                //
                strSQL = "SELECT DISTINCT EmployeeID ";
                strSQL += "FROM v_EmployeeScheduleByWeek_ActiveProjects s ";
                strSQL += "WHERE (EmployeeName NOT LIKE '-%') ";
                strSQL += "AND (s.WeekNoStartDate ";
                strSQL += "BETWEEN '" + strDateFirst + "' ";
                strSQL += "AND '" + strDateLast + "') ";
                strSQL += "AND (s.WeekNo = " + intWeeks[intK] + ") ";
                strSQL += "GROUP BY EmployeeID ";
                strSQL += "HAVING (SUM(Hours) BETWEEN 40 AND 43)";
                //
                da = new SqlDataAdapter(strSQL, strConn);
                ds = new DataSet();
                da.Fill(ds);
                //
                al.Add(ds.Tables[0].Rows.Count);
                //
                da.Dispose();
                ds.Dispose();
            }
            //
            return al;
        }

        private List<int> GetEmployeeWorkLoadByPeople_10To25(WeekYear wyFirst, WeekYear wyLast)
        {
            string strSQL = null;
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = default(DataSet);
            var al = new List<int>(); // ArrayList al = new ArrayList();
            //
            int intWeekFirst = wyFirst.Week;
            int intYearFirst = wyFirst.Year;
            string strDateFirst = wyFirst.MondayDate;
            int intWeekLast = wyLast.Week;
            int intYearLast = wyLast.Year;
            string strDateLast = wyLast.MondayDate;
            int intK = 0;
            // Set week labels
            SetWeekDateArrays(wyFirst, wyLast);
            //
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                //
                strSQL = "SELECT DISTINCT EmployeeID ";
                strSQL += "FROM v_EmployeeScheduleByWeek_ActiveProjects s ";
                strSQL += "WHERE (EmployeeName NOT LIKE '-%') ";
                strSQL += "AND (s.WeekNoStartDate ";
                strSQL += "BETWEEN '" + strDateFirst + "' ";
                strSQL += "AND '" + strDateLast + "') ";
                strSQL += "AND (s.WeekNo = " + intWeeks[intK] + ") ";
                strSQL += "GROUP BY EmployeeID ";
                strSQL += "HAVING (SUM(Hours) BETWEEN 44 AND 50)";
                //
                da = new SqlDataAdapter(strSQL, strConn);
                ds = new DataSet();
                da.Fill(ds);
                //
                al.Add(ds.Tables[0].Rows.Count);
                //
                da.Dispose();
                ds.Dispose();
            }
            //
            return al;
        }

        private List<int> GetEmployeeWorkLoadByPeople_Over25(WeekYear wyFirst, WeekYear wyLast)
        {
            string strSQL = null;
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = default(DataSet);
            var al = new List<int>(); // ArrayList al = new ArrayList();
            //
            int intWeekFirst = wyFirst.Week;
            int intYearFirst = wyFirst.Year;
            string strDateFirst = wyFirst.MondayDate;
            int intWeekLast = wyLast.Week;
            int intYearLast = wyLast.Year;
            string strDateLast = wyLast.MondayDate;
            int intK = 0;
            // Set week labels
            SetWeekDateArrays(wyFirst, wyLast);
            //
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                //
                strSQL = "SELECT DISTINCT EmployeeID ";
                strSQL += "FROM v_EmployeeScheduleByWeek_ActiveProjects s ";
                strSQL += "WHERE (EmployeeName NOT LIKE '-%') ";
                strSQL += "AND (s.WeekNoStartDate ";
                strSQL += "BETWEEN '" + strDateFirst + "' ";
                strSQL += "AND '" + strDateLast + "') ";
                strSQL += "AND (s.WeekNo = " + intWeeks[intK] + ") ";
                strSQL += "GROUP BY EmployeeID ";
                strSQL += "HAVING (SUM(Hours) > 50)";
                //
                da = new SqlDataAdapter(strSQL, strConn);
                ds = new DataSet();
                da.Fill(ds);
                //
                al.Add(ds.Tables[0].Rows.Count);
                //
                da.Dispose();
                ds.Dispose();
            }
            //
            return al;
            //
        }

        private DataSet GetUnAssignedEngineer_ByHours(WeekYear wyFirst, WeekYear wyLast)
        {
            string strSQL = null;
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = default(DataSet);
            //
            int intWeekFirst = wyFirst.Week;
            int intYearFirst = wyFirst.Year;
            string strDateFirst = wyFirst.MondayDate;
            int intWeekLast = wyLast.Week;
            int intYearLast = wyLast.Year;
            string strDateLast = wyLast.MondayDate;
            int intK = 0;
            // Set week labels
            SetWeekDateArrays(wyFirst, wyLast);
            //
            strSQL = "SELECT ID";
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                strSQL += ", (SELECT SUM(Hours) ";
                strSQL += "FROM v_EmployeeScheduleByWeek s ";
                strSQL += "WHERE (EmployeeName LIKE '- Unassigned%') ";
                strSQL += "AND (s.WeekNoStartDate ";
                strSQL += "BETWEEN '" + strDateFirst + "' ";
                strSQL += "AND '" + strDateLast + "') ";
                strSQL += "AND (s.WeekNo = " + intWeeks[intK] + ")) AS [" + strWeekLabel[intK] + "] ";
            }
            strSQL += "FROM tblTemp";
            //
            da = new SqlDataAdapter(strSQL, strConn);
            ds = new DataSet();
            da.Fill(ds);
            //
            return ds;
        }


        private void ConstructTableByHours(WeekYear wyFirst, WeekYear wyLast)
        {
            DataSet ds = default(DataSet);
            DataTable dt = default(DataTable);
            //
            TableRow rEng = default(TableRow);
            TableCell cEng = default(TableCell);
            //
            Table t = default(Table);
            TableRow r = default(TableRow);
            TableCell c = default(TableCell);
            //
            var alNeeded = new List<int>();// ArrayList alNeeded = new ArrayList();
            var alAvailable = new List<int>();// ArrayList alAvailable = new ArrayList();
            var alSurplusShortage = new List<int>();// ArrayList alSurplusShortage = new ArrayList();
            var alIndex = new List<int>();// ArrayList alIndex = new ArrayList();
            var alUnder = new List<int>();// ArrayList alUnder = new ArrayList();
            var alScheduled = new List<int>();// ArrayList alScheduled = new ArrayList();
            var al10To25 = new List<int>();// ArrayList al10To25 = new ArrayList();
            var alOver25 = new List<int>();// ArrayList alOver25 = new ArrayList();
            var alUnAssignedEng = new List<int>();// ArrayList alUnAssignedEng = new ArrayList();
            int intK = 0;
            System.DateTime dtmStart = DateTime.Parse(wyFirst.MondayDate);
            System.DateTime dtmEnd = default(System.DateTime);
            //
            decimal decAvailable = 0;
            //
            string strHeaderStyle = null;
            string strStyle = null;
            //
            decimal decHours = default(decimal);
            string strHours = null;
            object val = null;
            //
            string strCurWeekLabel = schedule.GetCurrentWeekLabel();
            bool blnCurWeek = false;
            // Set week labels
            SetWeekDateArrays(wyFirst, wyLast);
            // Reset all array lists
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                alNeeded.Add(0);
                alAvailable.Add(0);
                alSurplusShortage.Add(0);
                alUnAssignedEng.Add(0);
            }
            // Overall WorkLoad Needed
            ds = GetOverallWorkLoadByHours_Needed(wyFirst, wyLast);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                {
                    alNeeded[intK] = (dt.Rows[0][intK + 1].GetValueOrDefault<int>());
                }
            }
            // Overall WorkLoad Available
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                dtmEnd = dtmStart.AddDays(6); // DateAdd(DateInterval.Day, 6, dtmStart);
                alAvailable[intK] = (int)GetOverallWorkLoadByHours_Available(dtmStart, dtmEnd);
                dtmStart = dtmStart.AddDays(7); // DateAdd(DateInterval.Day, 7, dtmStart);
            }
            // Surplus/Shortage Hours
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                alSurplusShortage[intK] = (alNeeded[intK] - alAvailable[intK]);
            }
            // Surplus/Shortage Index
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                alIndex.Add(alNeeded[intK] / alAvailable[intK]);
            }
            // Employee WorkLoad Under
            alUnder = GetEmployeeWorkLoadByPeople_Under(wyFirst, wyLast);
            // Employee WorkLoad Scheduled
            alScheduled = GetEmployeeWorkLoadByPeople_Scheduled(wyFirst, wyLast);
            // Employee WorkLoad 10 To 25 %
            al10To25 = GetEmployeeWorkLoadByPeople_10To25(wyFirst, wyLast);
            // Employee WorkLoad Over 25 %
            alOver25 = GetEmployeeWorkLoadByPeople_Over25(wyFirst, wyLast);
            //
            // Unassigned Engineer By Hours
            ds = GetUnAssignedEngineer_ByHours(wyFirst, wyLast);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                {
                    alUnAssignedEng[intK] = (dt.Rows[0][intK + 1].GetValueOrDefault<int>());
                }
            }
            // Set table width
            this.tblEngineers.Width = Unit.Pixel(855);
            // Add row to container table
            rEng = new TableRow();
            cEng = new TableCell();
            //
            // New Table Inside
            t = new Table();
            t.CellSpacing = 0;
            t.CellPadding = 0;
            t.BorderWidth = Unit.Pixel(0);
            //t.Width = Unit.Pixel(850)
            // Header Row
            r = new TableRow();
            r.Height = Unit.Pixel(13);
            //
            // Blank Column
            c = new TableCell();
            c.Width = Unit.Pixel(200);
            c.HorizontalAlign = HorizontalAlign.Center;
            c.CssClass = "ChildHeaderStyle";
            c.Text = "&nbsp;";
            r.Cells.Add(c);
            // Blank Column
            c = new TableCell();
            c.Width = Unit.Pixel(155);
            c.HorizontalAlign = HorizontalAlign.Center;
            c.CssClass = "ChildHeaderStyle";
            c.Text = "&nbsp;";
            r.Cells.Add(c);
            // Week Hours Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                c.Width = Unit.Pixel(50);
                c.HorizontalAlign = HorizontalAlign.Center;
                c.CssClass = "ChildHeaderStyle";
                if (strWeekLabel[intK] == strCurWeekLabel)
                {
                    c.CssClass = "ChildCurWeekHeaderStyle";
                }
                c.Text = strWeekLabel[intK];
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // OVERALL WORKLOAD Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "GrayRowStyle";
            c.Text = "OVERALL WORKLOAD";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.HorizontalAlign = HorizontalAlign.Center;
            c.CssClass = "GrayRowStyle";
            c.Text = "HOURS";
            r.Cells.Add(c);
            // Blank Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.CssClass = "GrayRowStyle";
                c.Text = "&nbsp;";
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Needed Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "NEEDED";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                if (alIndex[intK] < 1)
                {
                    strStyle = "CellUnderSyle";
                }
                else if (alIndex[intK] >= 1 & alIndex[intK] < 1.1)
                {
                    strStyle = "CellScheduledStyle";
                }
                else if (alIndex[intK] >= 1.1 & alIndex[intK] < 1.25)
                {
                    strStyle = "Cell1025Style";
                }
                else if (alIndex[intK] >= 1.25)
                {
                    strStyle = "CellOver25Style";
                }
                c.CssClass = strStyle;
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Text = alNeeded[intK].ToString("###,##0.##");
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Available Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "AVAILABLE";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                c.CssClass = "RowStyle";
                c.Text = alAvailable[intK].ToString("###,##0.##");
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Get Styles for Surplus and Index Week columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                if (alIndex[intK] < 1)
                {
                    strStyle = "CellUnderSyle";
                }
                else if (alIndex[intK] >= 1 & alIndex[intK] < 1.1)
                {
                    strStyle = "CellScheduledStyle";
                }
                else if (alIndex[intK] >= 1.1 & alIndex[intK] < 1.25)
                {
                    strStyle = "Cell1025Style";
                }
                else if (alIndex[intK] >= 1.25)
                {
                    strStyle = "CellOver25Style";
                }
            }
            // Surplus/Shortage Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "SURPLUS/SHORTAGE";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                //
                if (alIndex[intK] < 1)
                {
                    strStyle = "CellUnderSyle";
                }
                else if (alIndex[intK] >= 1 & alIndex[intK] < 1.1)
                {
                    strStyle = "CellScheduledStyle";
                }
                else if (alIndex[intK] >= 1.1 & alIndex[intK] < 1.25)
                {
                    strStyle = "Cell1025Style";
                }
                else if (alIndex[intK] >= 1.25)
                {
                    strStyle = "CellOver25Style";
                }
                //
                c.CssClass = strStyle;
                c.Text = Math.Abs(alSurplusShortage[intK]).ToString("###,##0.##");
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Index Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "INDEX";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                //
                if (alIndex[intK] < 1)
                {
                    strStyle = "CellUnderSyle";
                }
                else if (alIndex[intK] >= 1 & alIndex[intK] < 1.1)
                {
                    strStyle = "CellScheduledStyle";
                }
                else if (alIndex[intK] >= 1.1 & alIndex[intK] < 1.25)
                {
                    strStyle = "Cell1025Style";
                }
                else if (alIndex[intK] >= 1.25)
                {
                    strStyle = "CellOver25Style";
                }
                //
                c.CssClass = strStyle;
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Text = alIndex[intK].ToString("00");
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Blank Row
            r = new TableRow();
            // Spacer Column
            c = new TableCell();
            c.Text = "<img src='images/spacer.gif' height='10' />";
            c.ColumnSpan = 12;
            r.Cells.Add(c);
            t.Rows.Add(r);
            //
            // EMPLOYEE WORKLOAD Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "GrayRowStyle";
            c.Text = "EMPLOYEE WORKLOAD";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.HorizontalAlign = HorizontalAlign.Center;
            c.CssClass = "GrayRowStyle";
            c.Text = "PEOPLE";
            r.Cells.Add(c);
            // Blank Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.CssClass = "GrayRowStyle";
                c.Text = "&nbsp;";
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Under Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "UNDER";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                //
                strStyle = "CellUnderSyle";
                c.CssClass = strStyle;
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Text = alUnder[intK].ToString();
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Scheduled Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "EVEN";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                //
                strStyle = "CellScheduledStyle";
                c.CssClass = strStyle;
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Text = alScheduled[intK].ToString();
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // 10 To 25 % Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "10 - 25%";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                //
                strStyle = "Cell1025Style";
                c.CssClass = strStyle;
                c.Text = al10To25[intK].ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Over 25% Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "25% +";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                //
                strStyle = "CellOver25Style";
                c.CssClass = strStyle;
                c.Text = alOver25[intK].ToString();
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);

            // Blank Row
            r = new TableRow();
            // Spacer Column
            c = new TableCell();
            c.Text = "<img src='images/spacer.gif' height='10' />";
            c.ColumnSpan = 12;
            r.Cells.Add(c);
            t.Rows.Add(r);
            //
            // Un Assigned By Hours
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "GrayRowStyle";
            c.Text = "UNASSIGNED";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.HorizontalAlign = HorizontalAlign.Center;
            c.CssClass = "GrayRowStyle";
            c.Text = "HOURS";
            r.Cells.Add(c);
            // Blank Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.CssClass = "GrayRowStyle";
                c.Text = "&nbsp;";
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // UnASssigned Engineer Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "ENGINEERS";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                c.CssClass = "RowStyle";
                c.Text = alUnAssignedEng[intK].ToString("###,##0.##");
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Blank Row
            r = new TableRow();
            // Spacer Column
            c = new TableCell();
            c.Text = "<img src='images/spacer.gif' height='10' />";
            c.ColumnSpan = 12;
            r.Cells.Add(c);
            t.Rows.Add(r);
            // Add table to Cell
            cEng.Controls.Add(t);
            //
            // Add all to container table
            rEng.Cells.Add(cEng);
            this.tblEngineers.Rows.Add(rEng);
        }

        private void ConstructTableByPeople(WeekYear wyFirst, WeekYear wyLast)
        {
            DataSet ds = default(DataSet);
            DataTable dt = default(DataTable);
            //
            TableRow rEng = default(TableRow);
            TableCell cEng = default(TableCell);
            //
            Table t = default(Table);
            TableRow r = default(TableRow);
            TableCell c = default(TableCell);
            //
            var alNeeded = new List<decimal>(); // ArrayList alNeeded = new ArrayList();
            var alAvailable = new List<decimal>();
            var alSurplusShortage = new List<decimal>();
            var alIndex = new List<decimal>();
            var alUnder = new List<int>();
            var alScheduled = new List<int>();
            var al10To25 = new List<int>();
            var alOver25 = new List<int>();
            var alUnAssignedEng = new List<decimal>();
            int intK = 0;
            decimal decAvailable = 0;
            var dtmStart = DateTime.Parse(wyFirst.MondayDate);
            var dtmEnd = default(System.DateTime);
            //
            string strHeaderStyle = "";
            string strStyle = "";
            //
            decimal decHours = 0;
            string strHours = "";
            decimal val = 0;
            //
            string strCurWeekLabel = schedule.GetCurrentWeekLabel();
            bool blnCurWeek = false;
            // Set week labels
            SetWeekDateArrays(wyFirst, wyLast);
            // Reset all array lists
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                alNeeded.Add(0);
                alAvailable.Add(0);
                alSurplusShortage.Add(0);
                alUnAssignedEng.Add(0);
            }
            // Overall WorkLoad Needed
            ds = GetOverallWorkLoadByHours_Needed(wyFirst, wyLast);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                {
                    alNeeded[intK] = dt.Rows[0][intK + 1].GetValueOrDefault<decimal>() / 40;
                }
            }
            // Overall WorkLoad Available
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                dtmEnd = dtmStart.AddDays(6);
                alAvailable[intK] = GetOverallWorkLoadByHours_Available(dtmStart, dtmEnd) / 40;
                dtmStart = dtmStart.AddDays(7);
            }
            // Surplus/Shortage Hours
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                alSurplusShortage[intK] = (alNeeded[intK].GetValueOrDefault<decimal>() - alAvailable[intK].GetValueOrDefault<decimal>());
            }
            // Surplus/Shortage Index
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                alIndex.Add(alNeeded[intK] / alAvailable[intK]);
            }
            // Employee WorkLoad Under
            alUnder = GetEmployeeWorkLoadByPeople_Under(wyFirst, wyLast);
            // Employee WorkLoad Scheduled
            alScheduled = GetEmployeeWorkLoadByPeople_Scheduled(wyFirst, wyLast);
            // Employee WorkLoad 10 To 25 %
            al10To25 = GetEmployeeWorkLoadByPeople_10To25(wyFirst, wyLast);
            // Employee WorkLoad Over 25 %
            alOver25 = GetEmployeeWorkLoadByPeople_Over25(wyFirst, wyLast);
            //
            // Unassigned Engineer By Hours
            ds = GetUnAssignedEngineer_ByHours(wyFirst, wyLast);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                {
                    alUnAssignedEng[intK] = dt.Rows[0][intK + 1].GetValueOrDefault<decimal>() / 40;
                }
            }
            // Set table width
            this.tblEngineers.Width = Unit.Pixel(855);
            // Add row to container table
            rEng = new TableRow();
            cEng = new TableCell();
            //
            // New Table Inside
            t = new Table();
            t.CellSpacing = 0;
            t.CellPadding = 0;
            t.BorderWidth = Unit.Pixel(0);
            //t.Width = Unit.Pixel(705)
            // Header Row
            r = new TableRow();
            r.Height = Unit.Pixel(13);
            //
            // Blank Column
            c = new TableCell();
            c.Width = Unit.Pixel(200);
            c.HorizontalAlign = HorizontalAlign.Center;
            c.CssClass = "ChildHeaderStyle";
            c.Text = "&nbsp;";
            r.Cells.Add(c);
            // Blank Column
            c = new TableCell();
            c.Width = Unit.Pixel(155);
            c.HorizontalAlign = HorizontalAlign.Center;
            c.CssClass = "ChildHeaderStyle";
            c.Text = "&nbsp;";
            r.Cells.Add(c);
            // Week Hours Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                c.Width = Unit.Pixel(50);
                c.HorizontalAlign = HorizontalAlign.Center;
                c.CssClass = "ChildHeaderStyle";
                if (strWeekLabel[intK] == strCurWeekLabel)
                {
                    c.CssClass = "ChildCurWeekHeaderStyle";
                }
                c.Text = strWeekLabel[intK];
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // OVERALL WORKLOAD Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "GrayRowStyle";
            c.Text = "OVERALL WORKLOAD";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.HorizontalAlign = HorizontalAlign.Center;
            c.CssClass = "GrayRowStyle";
            c.Text = "PEOPLE";
            r.Cells.Add(c);
            // Blank Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.CssClass = "GrayRowStyle";
                c.Text = "&nbsp;";
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Needed Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "NEEDED";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                if (alIndex[intK].GetValueOrDefault<decimal>() < 1)
                {
                    strStyle = "CellUnderSyle";
                }
                else if (alIndex[intK].GetValueOrDefault<decimal>() >= 1 & alIndex[intK].GetValueOrDefault<decimal>() < 1.1m)
                {
                    strStyle = "CellScheduledStyle";
                }
                else if (alIndex[intK].GetValueOrDefault<decimal>() >= 1.1m & alIndex[intK].GetValueOrDefault<decimal>() < 1.25m)
                {
                    strStyle = "Cell1025Style";
                }
                else if (alIndex[intK].GetValueOrDefault<decimal>() >= 1.25m)
                {
                    strStyle = "CellOver25Style";
                }
                c.CssClass = strStyle;
                c.Text = alNeeded[intK].GetValueOrDefault<decimal>().ToString("###,##0.0");
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Available Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "AVAILABLE";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                c.CssClass = "RowStyle";
                c.Text = alAvailable[intK].GetValueOrDefault<decimal>().ToString("###,##0.0");
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Surplus/Shortage Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "SURPLUS/SHORTAGE";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                //
                if (alIndex[intK].GetValueOrDefault<decimal>() < 1)
                {
                    strStyle = "CellUnderSyle";
                }
                else if (alIndex[intK].GetValueOrDefault<decimal>() >= 1 & alIndex[intK].GetValueOrDefault<decimal>() < 1.1m)
                {
                    strStyle = "CellScheduledStyle";
                }
                else if (alIndex[intK].GetValueOrDefault<decimal>() >= 1.1m & alIndex[intK].GetValueOrDefault<decimal>() < 1.25m)
                {
                    strStyle = "Cell1025Style";
                }
                else if (alIndex[intK].GetValueOrDefault<decimal>() >= 1.25m)
                {
                    strStyle = "CellOver25Style";
                }
                c.CssClass = strStyle;
                c.Text = Math.Abs(alSurplusShortage[intK].GetValueOrDefault<decimal>()).ToString("###,###.##");
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Index Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "INDEX";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                //
                if (alIndex[intK].GetValueOrDefault<decimal>() < 1)
                {
                    strStyle = "CellUnderSyle";
                }
                else if (alIndex[intK].GetValueOrDefault<decimal>() >= 1 & alIndex[intK].GetValueOrDefault<decimal>() < 1.1m)
                {
                    strStyle = "CellScheduledStyle";
                }
                else if (alIndex[intK].GetValueOrDefault<decimal>() >= 1.1m & alIndex[intK].GetValueOrDefault<decimal>() < 1.25m)
                {
                    strStyle = "Cell1025Style";
                }
                else if (alIndex[intK].GetValueOrDefault<decimal>() >= 1.25m)
                {
                    strStyle = "CellOver25Style";
                }
                c.CssClass = strStyle;
                c.Text = alIndex[intK].GetValueOrDefault<decimal>().ToString(); ///, 2);
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Blank Row
            r = new TableRow();
            // Spacer Column
            c = new TableCell();
            c.Text = "<img src='images/spacer.gif' height='10' />";
            c.ColumnSpan = 12;
            r.Cells.Add(c);
            t.Rows.Add(r);
            //
            // EMPLOYEE WORKLOAD Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "GrayRowStyle";
            c.Text = "EMPLOYEE WORKLOAD";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.HorizontalAlign = HorizontalAlign.Center;
            c.CssClass = "GrayRowStyle";
            c.Text = "PEOPLE";
            r.Cells.Add(c);
            // Blank Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.CssClass = "GrayRowStyle";
                c.Text = "&nbsp;";
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Under Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "UNDER";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                //
                strStyle = "CellUnderSyle";
                c.CssClass = strStyle;
                c.Text = alUnder[intK].GetValueOrDefault<decimal>().ToString("###,###");
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Scheduled Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "EVEN";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                //
                strStyle = "CellScheduledStyle";
                c.CssClass = strStyle;
                c.Text = alScheduled[intK].GetValueOrDefault<decimal>().ToString("###,###");
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // 10 To 25 % Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "10 - 25%";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                //
                strStyle = "Cell1025Style";
                c.CssClass = strStyle;
                c.Text = al10To25[intK].GetValueOrDefault<decimal>().ToString("###,###");
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Over 25% Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "AlternateRowStyle";
            c.Text = "25% +";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                //
                strStyle = "CellOver25Style";
                c.CssClass = strStyle;
                c.Text = alOver25[intK].GetValueOrDefault<decimal>().ToString("###,###");
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Blank Row
            r = new TableRow();
            // Spacer Column
            c = new TableCell();
            c.Text = "<img src='images/spacer.gif' height='10' />";
            c.ColumnSpan = 12;
            r.Cells.Add(c);
            t.Rows.Add(r);
            //
            // Un Assigned By Hours
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "GrayRowStyle";
            c.Text = "UNASSIGNED";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.HorizontalAlign = HorizontalAlign.Center;
            c.CssClass = "GrayRowStyle";
            c.Text = "PEOPLE";
            r.Cells.Add(c);
            // Blank Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.CssClass = "GrayRowStyle";
                c.Text = "&nbsp;";
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // UnASssigned Engineer Row
            r = new TableRow();
            //
            // Title
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "&nbsp";
            r.Cells.Add(c);
            //
            c = new TableCell();
            c.CssClass = "RowStyle";
            c.Text = "ENGINEERS";
            r.Cells.Add(c);
            // Week Columns
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                c = new TableCell();
                c.CssClass = "RowStyle";
                c.Text = alUnAssignedEng[intK].GetValueOrDefault<decimal>().ToString("###,###.##");
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
            }
            t.Rows.Add(r);
            // Blank Row
            r = new TableRow();
            // Spacer Column
            c = new TableCell();
            c.Text = "<img src='images/spacer.gif' height='10' />";
            c.ColumnSpan = 12;
            r.Cells.Add(c);
            t.Rows.Add(r);
            // Add table to Cell
            cEng.Controls.Add(t);
            //
            // Add all to container table
            rEng.Cells.Add(cEng);
            this.tblEngineers.Rows.Add(rEng);
        }

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Page.IsPostBack)
                return;
            //
            WeekYear wyFirst = default(WeekYear);
            WeekYear wyLast = default(WeekYear);
            if (Session["wyFirst"] == null | Session["wyLast"] == null)
            {
                wyFirst = schedule.GetWeekYear(System.DateTime.Now);
                wyLast = schedule.GetWeekYearLast(wyFirst);
            }
            else
            {
                wyFirst = Session["wyFirst"] as WeekYear;
                wyLast = Session["wyLast"] as WeekYear;
            }
            Session["wyFirst"] = wyFirst;
            Session["wyLast"] = wyLast;
            ConstructTableByHours(wyFirst, wyLast);
            this.wdtWeek.Value = wyFirst.MondayDate;
            //GetOverallWorkLoadByHours_Available(wyFirst, wyLast)

            SetupIndexChart();
        }

        protected void SetupIndexChart()
        {
            DateTime startDate = schedule.GetWeekMondayDate(System.DateTime.Now);
            IndexChart chart = new IndexChart(startDate);

            imgChart.ImageUrl = chart.GetChartUrl();
        }

        protected void btnPrevious_Click(object sender, System.EventArgs e)
        {
            // Move back 4 weeks
            WeekYear wyFirst = schedule.GetWeekYearFirst(Session["wyFirst"] as WeekYear, false);
            WeekYear wyLast = schedule.GetWeekYearLast(wyFirst);
            Session["wyFirst"] = wyFirst;
            Session["wyLast"] = wyLast;
            ConstructTableByHours(wyFirst, wyLast);
            this.wdtWeek.Value = wyFirst.MondayDate;
        }

        protected void btnNext_Click(object sender, System.EventArgs e)
        {
            WeekYear wyFirst = schedule.GetWeekYearFirst(Session["wyFirst"] as WeekYear, true);
            WeekYear wyLast = schedule.GetWeekYearLast(wyFirst);
            Session["wyFirst"] = wyFirst;
            Session["wyLast"] = wyLast;
            ConstructTableByHours(wyFirst, wyLast);
            this.wdtWeek.Value = wyFirst.MondayDate;
        }

        protected void wdtWeek_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
        {
            WeekYear wyFirst = schedule.GetWeekYear((DateTime)this.wdtWeek.Value);
            WeekYear wyLast = schedule.GetWeekYearLast(wyFirst);
            Session["wyFirst"] = wyFirst;
            Session["wyLast"] = wyLast;
            ConstructTableByHours(wyFirst, wyLast);
        }

        protected void btnHours_Click(object sender, System.EventArgs e)
        {
            WeekYear wyFirst = default(WeekYear);
            WeekYear wyLast = default(WeekYear);
            if (Session["wyFirst"] == null | Session["wyLast"] == null)
            {
                wyFirst = schedule.GetWeekYear(System.DateTime.Now);
                wyLast = schedule.GetWeekYearLast(wyFirst);
            }
            else
            {
                wyFirst = Session["wyFirst"] as WeekYear;
                wyLast = Session["wyLast"] as WeekYear;
            }
            Session["wyFirst"] = wyFirst;
            Session["wyLast"] = wyLast;
            ConstructTableByHours(wyFirst, wyLast);
        }

        protected void btnPeople_Click(object sender, System.EventArgs e)
        {
            WeekYear wyFirst = default(WeekYear);
            WeekYear wyLast = default(WeekYear);
            if (Session["wyFirst"] == null | Session["wyLast"] == null)
            {
                wyFirst = schedule.GetWeekYear(System.DateTime.Now);
                wyLast = schedule.GetWeekYearLast(wyFirst);
            }
            else
            {
                wyFirst = Session["wyFirst"] as WeekYear;
                wyLast = Session["wyLast"] as WeekYear;
            }
            Session["wyFirst"] = wyFirst;
            Session["wyLast"] = wyLast;
            ConstructTableByPeople(wyFirst, wyLast);
        }

        protected void btnThisWeek_Click(object sender, System.EventArgs e)
        {
            WeekYear wyFirst = default(WeekYear);
            WeekYear wyLast = default(WeekYear);
            wyFirst = schedule.GetWeekYear(System.DateTime.Now);
            wyLast = schedule.GetWeekYearLast(wyFirst);
            Session["wyFirst"] = wyFirst;
            Session["wyLast"] = wyLast;
            ConstructTableByHours(wyFirst, wyLast);
            this.wdtWeek.Value = wyFirst.MondayDate;
        }
    }
}

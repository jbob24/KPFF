
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAccount
{
    partial class ProjectsPrint : System.Web.UI.Page
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
                strMondayDates[intI] = dr.GetValueOrDefault<string>("WeekNoStartDate");
                strWeekLabel[intI] = dr.GetValueOrDefault<string>("WeekNoMonth").ToString() + "/" + dr.GetValueOrDefault<int>("WeekNoDay").ToString("00");
                intI += 1;
            }
            dr.Close();
            dr = null;
            conn = null;
        }

        private void ConstructTable(WeekYear wyFirst, WeekYear wyLast)
        {
            string strSQL = null;
            System.Data.SqlClient.SqlConnection connSQL = default(System.Data.SqlClient.SqlConnection);
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet dsEngineers = default(DataSet);
            DataTable dtEngineers = default(DataTable);
            DataSet dsProjects = default(DataSet);
            DataTable dtProjects = default(DataTable);
            DataRow dtRow = default(DataRow);
            int intEmployeeID = 0;
            int intProjID = 0;
            //
            int intWeekFirst = wyFirst.Week;
            int intYearFirst = wyFirst.Year;
            string strDateFirst = wyFirst.MondayDate;
            int intWeekLast = wyLast.Week;
            int intYearLast = wyLast.Year;
            string strDateLast = wyLast.MondayDate;
            int intI = 0;
            int intJ = 0;
            int intK = 0;
            int intID = 0;
            int intCol = 0;
            int intColChild = 0;
            //
            TableRow rEng = default(TableRow);
            TableCell cEng = default(TableCell);
            //
            Table t = default(Table);
            TableRow r = default(TableRow);
            TableCell c = default(TableCell);
            const int cWidth = 975;
            //
            string strHeaderStyle = null;
            string strStyle = null;
            //
            decimal decHours = default(decimal);
            var hours = new List<decimal>();
            string strHours = null;
            object val = null;
            //
            string strCurWeekLabel = schedule.GetCurrentWeekLabel();
            bool blnCurWeek = false;
            //
            // Reset ArrayList
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                hours.Add(0); // alHours.Add(0);
            }
            // Set week labels
            SetWeekDateArrays(wyFirst, wyLast);
            // Set table width
            this.tblEngineers.Width = Unit.Pixel(cWidth);
            // Add row to container table
            rEng = new TableRow();
            cEng = new TableCell();
            //
            //strSQL = "SELECT TOP 20 ([ID]) AS ProjectID, ProjectNo, "
            strSQL = "SELECT ([ID]) AS ProjectID, ProjectNo, ";
            strSQL += "UPPER(CASE WHEN (LEN(ProjectName) > 20) THEN LEFT(ProjectName, 20) + '...' ELSE ProjectName END) AS ProjectName, PICCode, PM1Code";
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                strSQL += ", (SELECT SUM([Hours]) ";
                strSQL += "FROM v_EmployeeScheduleByWeek s ";
                strSQL += "WHERE s.ProjectID = tblProjects.ID ";
                strSQL += "AND (s.WeekNoStartDate ";
                strSQL += "BETWEEN '" + strDateFirst + "' ";
                strSQL += "AND '" + strDateLast + "') ";
                strSQL += "AND (s.WeekNo = " + intWeeks[intK] + ")) AS [" + strWeekLabel[intK] + "] ";
            }
            strSQL += "FROM tblProjects ";
            strSQL += "WHERE Active=1 ";
            strSQL += "ORDER BY ProjectNo";
            //
            da = new SqlDataAdapter(strSQL, strConn);
            dsProjects = new DataSet();
            da.Fill(dsProjects);
            dtProjects = dsProjects.Tables[0];
            if (dtProjects.Rows.Count > 0)
            {
                t = new Table();
                t.CellSpacing = 0;
                t.CellPadding = 0;
                t.BorderWidth = Unit.Pixel(0);
                t.Width = Unit.Pixel(cWidth);
                // Column Headers
                r = new TableRow();
                r.Height = Unit.Pixel(13);
                //
                // Job #
                c = new TableCell();
                c.Width = Unit.Pixel(90);
                c.HorizontalAlign = HorizontalAlign.Center;
                c.CssClass = "HeaderStyle";
                c.Text = "Job #";
                r.Cells.Add(c);
                // Project
                c = new TableCell();
                c.Width = Unit.Pixel(210);
                c.HorizontalAlign = HorizontalAlign.Center;
                c.CssClass = "HeaderStyle";
                c.Text = "Project";
                r.Cells.Add(c);
                // PIC
                c = new TableCell();
                c.Width = Unit.Pixel(100);
                c.CssClass = "HeaderStyle";
                c.Text = "PIC";
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
                // PM1
                c = new TableCell();
                c.Width = Unit.Pixel(100);
                c.CssClass = "HeaderStyle";
                c.Text = "PM";
                c.HorizontalAlign = HorizontalAlign.Center;
                r.Cells.Add(c);
                // Week Hours Columns
                for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                {
                    c = new TableCell();
                    c.Width = Unit.Pixel(40);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.CssClass = "WeekHeaderStyle";
                    if (dtProjects.Columns[intK + 5].Caption == strCurWeekLabel)
                    {
                        c.CssClass = "ChildCurWeekHeaderStyle";
                    }
                    c.Text = strWeekLabel[intK];
                    r.Cells.Add(c);
                }
                //
                t.Rows.Add(r);
                cEng.Controls.Add(t);
                // Now grab data and populate table
                for (intI = 0; intI <= dtProjects.Rows.Count - 1; intI++)
                {
                    // Get ProjectID
                    intProjID = dtProjects.Rows[intI]["ProjectID"].GetValueOrDefault<int>();
                    //
                    t = new Table();
                    t.CellSpacing = 0;
                    t.CellPadding = 0;
                    t.BorderWidth = Unit.Pixel(0);
                    t.Width = Unit.Pixel(cWidth);
                    //
                    //If intI Mod 2 = 0 Then
                    //    strStyle = "RowStyle"
                    //Else
                    //    strStyle = "AlternateRowStyle"
                    //End If
                    //
                    strStyle = "GrayRowStyle";
                    //
                    r = new TableRow();
                    r.Height = Unit.Pixel(13);
                    r.CssClass = strStyle;
                    // Job #
                    c = new TableCell();
                    c.Width = Unit.Pixel(90);
                    c.CssClass = strStyle;
                    c.Text = dtProjects.Rows[intI]["ProjectNo"].GetValueOrDefault<string>(); // basToolbox.Nz(dtProjects.Rows[intI]["ProjectNo"], "&nbsp;");
                    r.Cells.Add(c);
                    // Project
                    c = new TableCell();
                    c.Width = Unit.Pixel(210);
                    c.CssClass = strStyle;
                    c.Text = dtProjects.Rows[intI]["ProjectName"].GetValueOrDefault<string>(); // basToolbox.Nz(dtProjects.Rows[intI]["ProjectName"], "&nbsp;");
                    r.Cells.Add(c);
                    // PIC
                    c = new TableCell();
                    c.Width = Unit.Pixel(100);
                    c.CssClass = strStyle;
                    c.Text = dtProjects.Rows[intI]["PICCode"].GetValueOrDefault<string>();// basToolbox.Nz(dtProjects.Rows[intI]["PICCode"], "&nbsp;");
                    r.Cells.Add(c);
                    // PM1
                    c = new TableCell();
                    c.Width = Unit.Pixel(100);
                    c.CssClass = strStyle;
                    c.Text = dtProjects.Rows[intI]["PM1Code"].GetValueOrDefault<string>(); // basToolbox.Nz(dtProjects.Rows[intI]["PM1Code"], "&nbsp;");
                    r.Cells.Add(c);
                    // Week Hours Columns
                    for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                    {
                        c = new TableCell();
                        c.Width = Unit.Pixel(40);
                        decHours = dtProjects.Rows[intI][intK + 5].GetValueOrDefault<decimal>(); // basToolbox.Nz(dtProjects.Rows[intI][intK + 5], 0);
                        hours[intK] += decHours; // alHours[intK] += decHours;
                        strHours = decHours.ToString(); // basToolbox.Nz(dtProjects.Rows[intI][intK + 5], "&nbsp;");
                        c.Text = strHours;
                        c.HorizontalAlign = HorizontalAlign.Center;
                        c.CssClass = strStyle;
                        r.Cells.Add(c);
                    }
                    //
                    t.Rows.Add(r);
                    cEng.Controls.Add(t);
                    //
                    // Now add Child Records
                    strSQL = "SELECT TOP 100 PERCENT (0) AS ID, e.ProjectID, e.EmployeeID, ";
                    strSQL += "e.EmployeeName, ";
                    strSQL += "('Engineer') AS Role";
                    for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                    {
                        strSQL += ", SUM(CASE WeekNo WHEN " + intWeeks[intK] + " THEN Hours ELSE 0 END) AS [" + strWeekLabel[intK] + "] ";
                    }
                    strSQL += ", ([EmployeeID]) AS IDField ";
                    strSQL += "FROM v_EmployeeScheduleByWeek e ";
                    strSQL += "WHERE e.ProjectID = " + intProjID + " ";
                    strSQL += "AND (e.WeekNoStartDate ";
                    strSQL += "BETWEEN '" + strDateFirst + "' ";
                    strSQL += "AND '" + strDateLast + "') ";
                    strSQL += "GROUP BY e.ProjectID, ";
                    strSQL += "e.EmployeeID, e.EmployeeName ";
                    strSQL += "ORDER BY e.EmployeeName";

                    da = new SqlDataAdapter(strSQL, strConn);
                    dsEngineers = new DataSet();
                    da.Fill(dsEngineers);
                    dtEngineers = dsEngineers.Tables[0];
                    if (dtEngineers.Rows.Count > 0)
                    {
                        //
                        t = new Table();
                        t.CellSpacing = 0;
                        t.CellPadding = 0;
                        t.BorderWidth = Unit.Pixel(0);
                        t.Width = Unit.Pixel(cWidth);
                        // Now grab data and populate table
                        for (intJ = 0; intJ <= dtEngineers.Rows.Count - 1; intJ++)
                        {
                            intEmployeeID = dtEngineers.Rows[intJ]["EmployeeID"].GetValueOrDefault<int>();
                            //
                            if (intJ % 2 == 0)
                            {
                                strStyle = "RowStyle";
                            }
                            else
                            {
                                strStyle = "AlternateRowStyle";
                            }
                            //
                            r = new TableRow();
                            r.Height = Unit.Pixel(13);
                            // Blank Column
                            c = new TableCell();
                            c.Width = Unit.Pixel(25);
                            c.CssClass = "ChildHeaderStyle";
                            c.Text = "&nbsp;";
                            r.Cells.Add(c);
                            // Employee Name
                            c = new TableCell();
                            c.Width = Unit.Pixel(269);
                            c.CssClass = strStyle;
                            c.Text = dtEngineers.Rows[intJ]["EmployeeName"].GetValueOrDefault<string>(); // basToolbox.Nz(dtEngineers.Rows[intJ]["EmployeeName"], "");
                            r.Cells.Add(c);
                            // Type
                            c = new TableCell();
                            c.Width = Unit.Pixel(205);
                            c.CssClass = strStyle;
                            c.Text = dtEngineers.Rows[intJ]["Role"].GetValueOrDefault<string>(); // basToolbox.Nz(dtEngineers.Rows[intJ]["Role"], "");
                            r.Cells.Add(c);
                            // Week Hours Columns
                            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                            {
                                c = new TableCell();
                                c.Width = Unit.Pixel(40);
                                decHours = dtEngineers.Rows[intJ][intK + 5].GetValueOrDefault<decimal>(); // basToolbox.Nz(dtEngineers.Rows[intJ][intK + 5], 0);
                                strHours = decHours.ToString(); // basToolbox.Nz(dtEngineers.Rows[intJ][intK + 5], "&nbsp;");
                                val = schedule.GetEmployeeWeekTotal(intEmployeeID, intWeekIDs[intK]);
                                strStyle = this.schedule.GetWeekHoursStyle(val.GetValueOrDefault<int>()); // schedule.GetWeekHoursStyle(val);
                                c.Text = strHours;
                                c.HorizontalAlign = HorizontalAlign.Center;
                                c.CssClass = strStyle;
                                r.Cells.Add(c);
                            }
                            //
                            t.Rows.Add(r);
                        }
                        cEng.Controls.Add(t);
                        //
                    }
                    // Add Blank Row
                    t = new Table();
                    t.CellSpacing = 0;
                    t.CellPadding = 0;
                    t.BorderWidth = Unit.Pixel(0);
                    t.Width = Unit.Pixel(cWidth);
                    //
                    r = new TableRow();
                    r.Height = Unit.Pixel(10);
                    // Blank Column
                    c = new TableCell();
                    c.Text = "<img src='images/spacer.gif' height='10' />";
                    c.Width = Unit.Pixel(cWidth);
                    r.Cells.Add(c);
                    //
                    t.Rows.Add(r);
                    //
                    cEng.Controls.Add(t);
                }
                cEng.Controls.Add(t);
                // Add Footer
                t = new Table();
                t.CellSpacing = 0;
                t.CellPadding = 0;
                t.BorderWidth = Unit.Pixel(0);
                t.Width = Unit.Pixel(cWidth);
                // Footer  Row
                r = new TableRow();
                r.Height = Unit.Pixel(13);
                //
                // Blank Column
                c = new TableCell();
                c.CssClass = "HeaderStyle";
                c.Width = Unit.Pixel(575);
                c.Text = "&nbsp;";
                r.Cells.Add(c);
                // Week Hours Columns
                for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                {
                    c = new TableCell();
                    c.Width = Unit.Pixel(40);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.CssClass = "HeaderStyle";
                    c.Text = hours[intK].ToString(); // alHours[intK];
                    c.HorizontalAlign = HorizontalAlign.Center;
                    r.Cells.Add(c);
                }
                //
                t.Rows.Add(r);
                cEng.Controls.Add(t);
            }
            else
            {
                // No Data Display Text
            }
            rEng.Cells.Add(cEng);
            this.tblEngineers.Rows.Add(rEng);
        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
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
                ConstructTable(wyFirst, wyLast);
            }
        }
    }
}
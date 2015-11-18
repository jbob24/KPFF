using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections.Generic;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAccount
{
    partial class AllEngineersPrint : System.Web.UI.Page
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
                strWeekLabel[intI] = dr.GetValueOrDefault<int>("WeekNoMonth").ToString() + "/" + dr.GetValueOrDefault<int>("WeekNoDay").ToString("00");
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
            //
            string strHeaderStyle = null;
            string strStyle = null;
            //
            decimal decHours = default(decimal);
            var alHours = new List<decimal>(); // ArrayList alHours = new ArrayList();
            string strHours = null;
            object val = null;
            //
            string strCurWeekLabel = schedule.GetCurrentWeekLabel();
            bool blnCurWeek = false;
            // Reset ArrayList
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                alHours.Add(0);
            }
            // Set week labels
            SetWeekDateArrays(wyFirst, wyLast);
            // Set table width
            this.tblEngineers.Width = Unit.Pixel(850);
            // Add row to container table
            rEng = new TableRow();
            cEng = new TableCell();
            //
            strSQL = "SELECT tblEmployees.EmployeeID, tblEmployees.EmployeeName, ";
            strSQL += "tblEmployeeTypes.EmployeeType ";
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                strSQL += ", (SELECT SUM ([Hours]) ";
                strSQL += "FROM v_EmployeeScheduleByWeek_ActiveProjects s ";
                strSQL += "WHERE s.EmployeeID = tblEmployees.EmployeeID ";
                strSQL += "AND (s.WeekNoStartDate ";
                strSQL += "BETWEEN '" + strDateFirst + "' ";
                strSQL += "AND '" + strDateLast + "') ";
                strSQL += "AND (s.WeekNo = " + intWeeks[intK] + ")) AS [" + strWeekLabel[intK] + "] ";
            }
            strSQL += "FROM tblEmployees LEFT OUTER JOIN tblEmployeeTypes ";
            strSQL += "ON tblEmployees.EmployeeTypeID = tblEmployeeTypes.ID ";
            strSQL += "WHERE tblEmployees.Active = 1 ";
            strSQL += "ORDER BY tblEmployees.EmployeeName";
            //
            da = new SqlDataAdapter(strSQL, strConn);
            dsEngineers = new DataSet();
            da.Fill(dsEngineers);
            dtEngineers = dsEngineers.Tables[0];
            if (dtEngineers.Rows.Count > 0)
            {
                t = new Table();
                t.CellSpacing = 0;
                t.CellPadding = 0;
                t.BorderWidth = Unit.Pixel(0);
                t.Width = Unit.Pixel(850);
                // Column Headers
                r = new TableRow();
                r.Height = Unit.Pixel(13);
                //
                // EmployeeName
                c = new TableCell();
                c.Width = Unit.Pixel(230);
                c.HorizontalAlign = HorizontalAlign.Center;
                c.CssClass = "ChildHeaderStyle";
                c.Text = "Engineer";
                r.Cells.Add(c);
                // Employee Type
                c = new TableCell();
                c.Width = Unit.Pixel(125);
                c.HorizontalAlign = HorizontalAlign.Center;
                c.CssClass = "ChildHeaderStyle";
                c.Text = "Type";
                r.Cells.Add(c);
                // Week Hours Columns
                for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                {
                    c = new TableCell();
                    c.Width = Unit.Pixel(35);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.CssClass = "ChildHeaderStyle";
                    if (dtEngineers.Columns[intK + 3].Caption == strCurWeekLabel)
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
                for (intI = 0; intI <= dtEngineers.Rows.Count - 1; intI++)
                {
                    // Get EmployeeID
                    intEmployeeID = dtEngineers.Rows[intI]["EmployeeID"].GetValueOrDefault<int>();
                    //
                    t = new Table();
                    t.CellSpacing = 0;
                    t.CellPadding = 0;
                    t.BorderWidth = Unit.Pixel(0);
                    t.Width = Unit.Pixel(850);
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
                    // EmployeeName
                    c = new TableCell();
                    c.Width = Unit.Pixel(230);
                    c.CssClass = strStyle;
                    c.Text = dtEngineers.Rows[intI]["EmployeeName"].GetValueOrDefault<string>();
                    r.Cells.Add(c);
                    // Employee Type
                    c = new TableCell();
                    c.Width = Unit.Pixel(125);
                    c.CssClass = strStyle;
                    c.Text = dtEngineers.Rows[intI]["EmployeeType"].GetValueOrDefault<string>();
                    r.Cells.Add(c);
                    // Week Hours Columns
                    for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                    {
                        c = new TableCell();
                        c.Width = Unit.Pixel(35);
                        decHours = dtEngineers.Rows[intI][intK + 3].GetValueOrDefault<decimal>();
                        alHours[intK] += decHours;
                        strHours = dtEngineers.Rows[intI][intK + 3].GetValueOrDefault<string>();
                        strStyle = schedule.GetWeekHoursStyle((int)decHours);
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
                    strSQL = "SELECT TOP 100 PERCENT e.ProjectID, e.ProjectNo, UPPER(CASE WHEN (LEN(e.ProjectName) > 20) THEN LEFT(e.ProjectName, 20) + '...' ELSE e.ProjectName END) AS ProjectName, EmployeeID ";
                    for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                    {
                        strSQL += ", (SELECT SUM([Hours]) ";
                        strSQL += "FROM v_EmployeeScheduleByWeek s ";
                        strSQL += "WHERE s.ProjectID = e.ProjectID ";
                        strSQL += "AND s.EmployeeID = e.EmployeeID ";
                        strSQL += "AND (s.WeekNoStartDate ";
                        strSQL += "BETWEEN '" + strDateFirst + "' ";
                        strSQL += "AND '" + strDateLast + "') ";
                        strSQL += "AND (s.WeekNo = " + intWeeks[intK] + ")) AS [" + strWeekLabel[intK] + "] ";
                    }
                    strSQL += "FROM v_EmployeeScheduleByWeek e ";
                    strSQL += "WHERE e.EmployeeID = " + intEmployeeID + " ";
                    strSQL += "GROUP BY e.ProjectID, e.ProjectNo, e.ProjectName, e.EmployeeID ";
                    strSQL += "ORDER BY e.ProjectNo, e.ProjectName";
                    da = new SqlDataAdapter(strSQL, strConn);
                    dsEngineers = new DataSet();
                    da.Fill(dsEngineers);
                    dtProjects = dsEngineers.Tables[0];
                    if (dtProjects.Rows.Count > 0)
                    {
                        //
                        t = new Table();
                        t.CellSpacing = 0;
                        t.CellPadding = 0;
                        t.BorderWidth = Unit.Pixel(0);
                        t.Width = Unit.Pixel(850);
                        // '' '' '' '' '' Add Header row
                        //' '' '' '' ''r = New TableRow
                        //' '' '' '' ''r.Height = Unit.Pixel(13)
                        // '' '' '' '' ''
                        // '' '' '' '' '' Blank Column
                        //' '' '' '' ''c = New TableCell
                        //' '' '' '' ''c.Width = Unit.Pixel(25)
                        //' '' '' '' ''c.CssClass = "ChildHeaderStyle"
                        //' '' '' '' ''c.Text = ""
                        //' '' '' '' ''r.Cells.Add(c)
                        // '' '' '' '' '' Project No
                        //' '' '' '' ''c = New TableCell
                        //' '' '' '' ''c.Width = Unit.Pixel(100)
                        //' '' '' '' ''c.CssClass = "ChildHeaderStyle"
                        //' '' '' '' ''c.Text = "Job #"
                        //' '' '' '' ''r.Cells.Add(c)
                        // '' '' '' '' '' Project Name
                        //' '' '' '' ''c = New TableCell
                        //' '' '' '' ''c.Width = Unit.Pixel(230)
                        //' '' '' '' ''c.CssClass = "ChildHeaderStyle"
                        //' '' '' '' ''c.Text = "Project"
                        //' '' '' '' ''r.Cells.Add(c)
                        // '' '' '' '' ''
                        // '' '' '' '' '' Week Hours Columns
                        //' '' '' '' ''For intK = 0 To clsSchedule.cWeekSpan
                        //' '' '' '' ''    c = New TableCell
                        //' '' '' '' ''    c.Width = Unit.Pixel(35)
                        //' '' '' '' ''    c.CssClass = "ChildHeaderStyle"
                        //' '' '' '' ''    If dtProjects.Columns(intK + 4).Caption = strCurWeekLabel Then
                        //' '' '' '' ''        c.CssClass = "ChildCurWeekHeaderStyle"
                        //' '' '' '' ''    End If
                        //' '' '' '' ''    c.Text = strWeekLabel[intK]
                        //' '' '' '' ''    r.Cells.Add(c)
                        //' '' '' '' ''Next
                        // '' '' '' '' ''
                        //' '' '' '' ''t.Rows.Add(r)
                        //' '' '' '' ''cEng.Controls.Add(t)
                        //
                        t = new Table();
                        t.CellSpacing = 0;
                        t.CellPadding = 0;
                        t.BorderWidth = Unit.Pixel(0);
                        t.Width = Unit.Pixel(850);
                        // Now grab data and populate table
                        for (intJ = 0; intJ <= dtProjects.Rows.Count - 1; intJ++)
                        {
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
                            c.Text = "";
                            r.Cells.Add(c);
                            // Project No
                            c = new TableCell();
                            c.Width = Unit.Pixel(100);
                            c.CssClass = strStyle;
                            c.Text = dtProjects.Rows[intJ]["ProjectNo"].GetValueOrDefault<string>();
                            r.Cells.Add(c);
                            // Project Name
                            c = new TableCell();
                            c.Width = Unit.Pixel(230);
                            c.CssClass = strStyle;
                            c.Text = dtProjects.Rows[intJ]["ProjectName"].GetValueOrDefault<string>();
                            r.Cells.Add(c);
                            // Week Hours Columns
                            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                            {
                                c = new TableCell();
                                c.Width = Unit.Pixel(35);
                                decHours = dtProjects.Rows[intJ][intK + 4].GetValueOrDefault<decimal>();
                                strHours = decHours.ToString();
                                val = dtEngineers.Rows[intI][intK + 3].GetValueOrDefault<int>();
                                strStyle = schedule.GetWeekHoursStyle((int)val);
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
                    t.Width = Unit.Pixel(850);
                    //
                    r = new TableRow();
                    r.Height = Unit.Pixel(10);
                    // Blank Column
                    c = new TableCell();
                    c.Text = "<img src='images/spacer.gif' height='10' />";
                    c.ColumnSpan = 12;
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
                t.Width = Unit.Pixel(850);
                // Footer  Row
                r = new TableRow();
                r.Height = Unit.Pixel(13);
                //
                // Blank Column
                c = new TableCell();
                c.Width = Unit.Pixel(25);
                c.CssClass = "ChildHeaderStyle";
                c.Text = "&nbsp;";
                r.Cells.Add(c);
                // Legend
                c = new TableCell();
                c.CssClass = "ChildHeaderStyle";
                c.Width = Unit.Pixel(330);
                c.Text = "<img src='../images/GridLegend.jpg' />";
                r.Cells.Add(c);
                // Week Hours Columns
                for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                {
                    c = new TableCell();
                    c.Width = Unit.Pixel(35);
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.CssClass = "ChildHeaderStyle";
                    c.Text = alHours[intK].GetValueOrDefault<decimal>().ToString("###,###.##");
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
                    wyFirst = Session["wyFirst"].GetValueOrDefault<WeekYear>();
                    wyLast = Session["wyLast"].GetValueOrDefault<WeekYear>();
                }
                Session["wyFirst"] = wyFirst;
                Session["wyLast"] = wyLast;
                ConstructTable(wyFirst, wyLast);
            }
        }
    }
}
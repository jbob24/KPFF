
using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using Microsoft.Win32;
using System.Configuration;
using System.Web.UI.WebControls;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAdmin
{
    partial class AllEngineers_PrintList : PageBase
    {
        #region "Web Form Designer generated code"
        //This call is required by the Web Form Designer.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            //this.connSQL = new System.Data.SqlClient.SqlConnection(strConn);
            //InitializeDataSource();
        }

        private void Page_Init(System.Object sender, System.EventArgs e)
        {
            //CODEGEN: This method call is required by the Web Form Designer
            //Do not modify it using the code editor.
            InitializeComponent();
        }

        #endregion

        #region " Custom Routines"
        private void PopulateEngineers(DataTable dt)
        {
            //Dim dt As DataTable = dsEngineers.Tables["Engineers"]
            DataRow dtRow = default(DataRow);
            //
            TableRow r = default(TableRow);
            TableCell c = default(TableCell);
            System.Web.UI.WebControls.Image img = default(System.Web.UI.WebControls.Image);
            string strStyle = null;
            //
            int intCol = -1;
            //
            int intRow = 0;
            int intK = 0;
            string strMondayWeekLabel = this.Schedule.GetCurrentWeekLabel();
            decimal[] decTotalWeekHours = new decimal[20];
            object val = null;
            //
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                if (this.WeekDate.WeekLabels[intK] == strMondayWeekLabel)
                {
                    intCol = intK;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            // Start Header Row
            r = new TableRow();
            strStyle = "ChildHeaderStyle";
            // Engineer
            c = new TableCell();
            c.HorizontalAlign = HorizontalAlign.Center;
            c.Width = Unit.Pixel(230);
            c.Text = "Engineer";
            c.CssClass = strStyle;
            r.Cells.Add(c);
            // Type
            c = new TableCell();
            c.HorizontalAlign = HorizontalAlign.Center;
            c.Width = Unit.Pixel(125);
            c.Text = "Type";
            c.CssClass = strStyle;
            r.Cells.Add(c);
            // Week Headers
            for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
            {
                strStyle = "ChildWeekHeaderStyle";
                c = new TableCell();
                c.HorizontalAlign = HorizontalAlign.Center;
                c.Width = Unit.Pixel(35);
                c.Text = this.WeekDate.WeekLabels[intK];
                c.CssClass = strStyle;
                if (intCol == intK)
                {
                    c.CssClass = "ChildCurWeekHeaderStyle";
                }
                r.Cells.Add(c);
            }
            // 
            this.tblEngineers.Rows.Add(r);
            // End Header Row
            // 
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    intRow += 1;
                    if (intRow % 2 == 0)
                    {
                        // even row
                        strStyle = "AlternateRowStyle";
                    }
                    else
                    {
                        // else odd row
                        strStyle = "RowStyle";
                    }
                    // Start Items Row
                    r = new TableRow();
                    // Engineer
                    c = new TableCell();
                    c.Text = "&nbsp;" + row["EmployeeName"].GetValueOrDefault<string>();// basToolbox.Nz(row["EmployeeName"], "");
                    c.CssClass = strStyle;
                    r.Cells.Add(c);
                    // Type
                    c = new TableCell();
                    c.Text = "&nbsp;" + row["EmployeeType"].GetValueOrDefault<string>();
                    c.CssClass = strStyle;
                    r.Cells.Add(c);
                    // Week Hours
                    for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                    {
                        c = new TableCell();
                        c.HorizontalAlign = HorizontalAlign.Center;
                        decTotalWeekHours[intK] += row["Week" + (intK + 1)].GetValueOrDefault<decimal>(); // basToolbox.Nz(row["Week" + (intK + 1)], 0);
                        c.Text = row["Week" + (intK + 1)].GetValueOrDefault<string>(); // basToolbox.Nz(row["Week" + (intK + 1)], "");
                        val = row["Week" + (intK + 1)].GetValueOrDefault<int>(); // basToolbox.Nz(row["Week" + (intK + 1)], 0);
                        strStyle = this.Schedule.GetWeekHoursStyle((int)val); // schedule.GetWeekHoursStyle(val);
                        c.CssClass = strStyle;
                        r.Cells.Add(c);
                    }
                    //
                    this.tblEngineers.Rows.Add(r);
                }
                // Footer
                r = new TableRow();
                strStyle = "ChildFooterStyle";
                // Engineer
                c = new TableCell();
                c.ColumnSpan = 2;
                c.CssClass = strStyle;
                c.Text = "&nbsp;&nbsp;&nbsp;&nbsp;";
                img = new System.Web.UI.WebControls.Image();
                img.ImageUrl = "../images/GridLegend.jpg";
                c.Controls.Add(img);
                r.Cells.Add(c);
                // Week Hours
                for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++)
                {
                    c = new TableCell();
                    c.HorizontalAlign = HorizontalAlign.Center;
                    c.Text = decTotalWeekHours[intK].ToString("#,###.##");
                    c.CssClass = strStyle;
                    r.Cells.Add(c);
                }
                //
                this.tblEngineers.Rows.Add(r);

            }
            else
            {
            }
        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //WeekYear wyFirst = Session["wyFirst"];
            //WeekYear wyLast = Session["wyLast"];

            this.WeekDate = new WeekDate(this.WyFirst, this.WyLast); // wyFirst, wyLast);
            //Engineer = New Engineer(schedule, weekDate)

            if (!IsPostBack)
            {
                PopulateEngineers(Engineer.GetSchedulesAllEngineers("", "").Tables["Engineers"]);
            }
        }
    }
}

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using Microsoft.Win32;
using System.Configuration;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAdmin
{
    partial class Projects_PrintList : KPFF.PMP.Entities.PageBase
    {
        //
        public string strConn = ConfigurationManager.AppSettings["ConnectionString"];
        protected System.Data.SqlClient.SqlConnection connSQL;
        protected SqlDataAdapter daProjects;
        protected DataSet dsProjects;
        protected DataTable dtProjects;
        protected DataView dvProjects;
        protected DataTable dtSchedule;
        protected SqlDataSource sqlds;
        public clsSchedule schedule = new clsSchedule();
        //

        #region "Web Form Designer generated code"
        //This call is required by the Web Form Designer.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.connSQL = new System.Data.SqlClient.SqlConnection(strConn);
            InitializeDataSource();
        }

        private void Page_Init(System.Object sender, System.EventArgs e)
        {
            //CODEGEN: This method call is required by the Web Form Designer
            //Do not modify it using the code editor.
            InitializeComponent();
        }

        #endregion

        #region " Custom Routines"

        public void InitializeDataSource()
        {
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


            // Add primary Key
            DataColumn[] keys = new DataColumn[2];
            keys[0] = dtProjects.Columns["ProjectID"];
            dtProjects.PrimaryKey = keys;
            //
            // Add Tables to dataset
            dsProjects.Tables.Add(dtProjects);
            //
        }

        private void PopulateDataset(WeekYear wyFirst, WeekYear wyLast, string strFilterField, string strFilterText)
        {
            string strSQL = null;
            string strFilter = "";
            //
            int intWeekFirst = wyFirst.Week;
            int intYearFirst = wyFirst.Year;
            string strDateFirst = wyFirst.MondayDate;
            int intWeekLast = wyLast.Week;
            int intYearLast = wyLast.Year;
            string strDateLast = wyLast.MondayDate;
            int intI = 0;
            int intID = 0;
            //
            strSQL = "SELECT ([ID]) AS ProjectID, ProjectNo, ";
            strSQL += "UPPER(CASE WHEN (LEN(ProjectName) > 30) THEN LEFT(ProjectName, 30) + '...' ELSE ProjectName END) AS ProjectName, PICCode, PM1Code ";
            for (intI = 0; intI <= clsSchedule.cWeekSpan; intI++)
            {
                strSQL += ", (SELECT SUM ([Hours]) ";
                strSQL += "FROM v_EmployeeScheduleByWeek s ";
                strSQL += "WHERE s.ProjectID = tblProjects.ID ";
                strSQL += "AND (s.WeekNoStartDate ";
                strSQL += "BETWEEN '" + strDateFirst + "' ";
                strSQL += "AND '" + strDateLast + "') ";
                strSQL += "AND (s.WeekNo = " + WeekDate.WeekIDs[intI] + ")) AS [Week" + (intI + 1) + "] ";
            }
            strSQL += "FROM tblProjects ";
            if (!string.IsNullOrEmpty(strFilterText))
            {
                strFilter = GetFilter(strFilterField, strFilterText);
                strSQL += "WHERE Active = 1 AND ";
                strSQL += strFilter + " ";
            }
            else
            {
                strSQL += "WHERE Active = 1 ";
            }
            strSQL += "ORDER BY ProjectNo";
            //
            daProjects = new SqlDataAdapter(strSQL, strConn);
            //
            daProjects.Fill(dsProjects, "Projects");
            //
        }


        private string GetFilter(string strFilterField, string strFilterText)
        {
            string strFilter = "";
            switch (strFilterField)
            {
                case "ProjectNo":
                    strFilter = "CONVERT(nvarchar(20), CONVERT(decimal(19,2), ProjectNo)) LIKE '" + strFilterText + "%' ";
                    break;
                case "ProjectName":
                    strFilter = "ProjectName LIKE '" + strFilterText + "%' ";
                    break;
                case "PICCode":
                    strFilter = "PICCode LIKE '" + strFilterText + "%' ";
                    break;
                case "PM1Code":
                    strFilter = "PM1Code LIKE '" + strFilterText + "%' ";
                    break;
            }
            //
            return strFilter;
        }

        private void PopulateProjects()
	{
		DataTable dt = dsProjects.Tables["Projects"];
		//DataRow dtRow = default(DataRow);
		//
		TableRow r = default(TableRow);
		TableCell c = default(TableCell);
		System.Web.UI.WebControls.Image img = default(System.Web.UI.WebControls.Image);
		string strStyle = null;
		//
		int intCol = -1;
		string strLabel = null;
		//
		int intRow = 0;
		int intK = 0;
		string strMondayWeekLabel = schedule.GetCurrentWeekLabel();
		decimal[] decTotalWeekHours = new decimal[20];
		//
		for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++) {
            if (WeekDate.WeekLabels[intK] == strMondayWeekLabel)
            {
				intCol = intK;
				break; // TODO: might not be correct. Was : Exit For
			}
		}
		// Start Header Row
		r = new TableRow();
		strStyle = "HeaderStyle";
		// Job #
		c = new TableCell();
		c.HorizontalAlign = HorizontalAlign.Center;
		c.Width = Unit.Pixel(85);
		c.Text = "Job #";
		c.CssClass = strStyle;
		r.Cells.Add(c);
		// PROJECT
		c = new TableCell();
		c.HorizontalAlign = HorizontalAlign.Center;
		c.Width = Unit.Pixel(260);
		c.Text = "PROJECT";
		c.CssClass = strStyle;
		r.Cells.Add(c);
		// PIC
		c = new TableCell();
		c.HorizontalAlign = HorizontalAlign.Center;
		c.Width = Unit.Pixel(70);
		c.Text = "PIC";
		c.CssClass = strStyle;
		r.Cells.Add(c);
		// PM
		c = new TableCell();
		c.HorizontalAlign = HorizontalAlign.Center;
		c.Width = Unit.Pixel(70);
		c.Text = "PM";
		c.CssClass = strStyle;
		r.Cells.Add(c);
		// Week Headers
		for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++) {
			c = new TableCell();
			c.HorizontalAlign = HorizontalAlign.Center;
			c.Width = Unit.Pixel(35);
            c.Text = WeekDate.WeekLabels[intK]; // strWeekLabel[intK];
			c.CssClass = strStyle;
			if (intCol == intK) {
				c.CssClass = "WeekCurMondayCellStyle";
			}
			r.Cells.Add(c);
		}
		// 
		this.tblProjects.Rows.Add(r);
		// End Header Row
		// 
		if (dt.Rows.Count > 0) {
            foreach (DataRow dtRow in dt.Rows)
            {
				intRow += 1;
				if (intRow % 2 == 0) {
					// even row
					strStyle = "AlternateRowStyle";
				} else {
					// else odd row
					strStyle = "RowStyle";
				}
				// Start Items Row
				r = new TableRow();
				// Job #
				c = new TableCell();
                c.Text = "&nbsp" + dtRow["ProjectNo"].GetValueOrDefault<string>(); 
				c.CssClass = strStyle;
				r.Cells.Add(c);
				// PROJECT
				c = new TableCell();
                c.Text = "&nbsp" + dtRow["ProjectName"].GetValueOrDefault<string>(); 
				c.CssClass = strStyle;
				r.Cells.Add(c);
				// PIC
				c = new TableCell();
                c.Text = "&nbsp" + dtRow["PICCode"].GetValueOrDefault<string>();
                c.CssClass = strStyle;
                r.Cells.Add(c);
                // PM
                c = new TableCell();
                c.Text = "&nbsp" + dtRow["PM1Code"].GetValueOrDefault<string>(); 
				c.CssClass = strStyle;
				r.Cells.Add(c);
				// Week Hours
				for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++) {
					c = new TableCell();
					c.HorizontalAlign = HorizontalAlign.Center;
                    decTotalWeekHours[intK] += dtRow["Week" + (intK + 1)].GetValueOrDefault<decimal>();
                    c.Text = dtRow["Week" + (intK + 1)].GetValueOrDefault<decimal>().ToString();
					c.CssClass = strStyle;
					if (intCol == intK) {
						c.CssClass = "WeekCurMondayCellStyle";
					}
					r.Cells.Add(c);
				}
				//
				this.tblProjects.Rows.Add(r);
			}
			// Footer
			r = new TableRow();
			strStyle = "FooterStyle";
			// Job #
			c = new TableCell();
			c.Text = "";
			c.CssClass = strStyle;
			r.Cells.Add(c);
			// PROJECT
			c = new TableCell();
			c.Text = "";
			c.CssClass = strStyle;
			r.Cells.Add(c);
			// PIC
			c = new TableCell();
			c.Text = "";
			c.CssClass = strStyle;
			r.Cells.Add(c);
			// PM
			c = new TableCell();
			c.Text = "";
			c.CssClass = strStyle;
			r.Cells.Add(c);
			// Week Hours
			for (intK = 0; intK <= clsSchedule.cWeekSpan; intK++) {
				c = new TableCell();
				c.HorizontalAlign = HorizontalAlign.Center;
				c.Text = decTotalWeekHours[intK].ToString("#,###.##");
				c.CssClass = strStyle;
				r.Cells.Add(c);
			}
			//
			this.tblProjects.Rows.Add(r);

		} else {
		}
	}

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                WeekYear wyFirst = Session["wyFirst"] as WeekYear;
                WeekYear wyLast = Session["wyLast"] as WeekYear;
                SearchFilter sFilter = new SearchFilter();
                sFilter = Session["sFilter"] as SearchFilter;

                if (sFilter == null)
                {
                    sFilter = new SearchFilter();
                }

                // Populate Grid
                PopulateDataset(wyFirst, wyLast, sFilter.Field, sFilter.Text);
                //FormatGrid()
                PopulateProjects();
            }
        }
    }
}
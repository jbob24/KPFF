
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
using System;
using KPFF.PMP.Entities;


namespace KPFF.PMP.MyAccount
{
    partial class ArchivedProjects : System.Web.UI.Page
    {
        //
        public string strConn = ConfigurationManager.AppSettings["ConnectionString"];
        public int[] intWeeks = new int[10];
        public int[] intYears = new int[10];
        public string[] strMondayDates = new string[10];
        public string[] strWeekLabel = new string[10];
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
                intWeeks[intI] = dr.GetValueOrDefault<int>("WeekNo");
                intYears[intI] = dr.GetValueOrDefault<int>("WeekNoYear");
                strMondayDates[intI] = dr.GetValueOrDefault<DateTime>("WeekNoStartDate").ToShortDateString();
                strWeekLabel[intI] = dr.GetValueOrDefault<int>("WeekNoMonth") + "/" + dr.GetValueOrDefault<int>("WeekNoDay").ToString("00");
                intI += 1;
            }
            dr.Close();
            dr = null;
            conn = null;
        }

        private void PopulateFilter()
        {
            ListItem lstItem = default(ListItem);
            this.ddlFilter.Items.Clear();
            //
            lstItem = new ListItem("Job #", "ProjectNo");
            this.ddlFilter.Items.Add(lstItem);
            //
            lstItem = new ListItem("Project", "ProjectName");
            this.ddlFilter.Items.Add(lstItem);
            //
            lstItem = new ListItem("PIC", "PICCode");
            this.ddlFilter.Items.Add(lstItem);
            //
            lstItem = new ListItem("PM", "PM1Code");
            this.ddlFilter.Items.Add(lstItem);
            //
        }

        private Infragistics.WebUI.UltraWebGrid.ValueList EmployeeTypes()
        {
            Infragistics.WebUI.UltraWebGrid.ValueList vList = new Infragistics.WebUI.UltraWebGrid.ValueList();
            // Add Blank Row
            vList.ValueListItems.Add(0, "");
            // Get rest of items
            string strSQL = null;
            strSQL = "SELECT ID, EmployeeType ";
            strSQL += "FROM tblEmployeeTypes ";
            strSQL += "WHERE EmployeeType <> 'Admin'";
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                vList.ValueListItems.Add(dr.GetValueOrDefault<int>("ID"), dr.GetValueOrDefault<string>("EmployeeType"));
            }
            dr.Close();
            conn = null;
            cmd = null;
            dr = null;
            //
            return vList;
        }

        private Infragistics.WebUI.UltraWebGrid.ValueList EmployeeCodes()
        {
            Infragistics.WebUI.UltraWebGrid.ValueList vList = new Infragistics.WebUI.UltraWebGrid.ValueList();
            // Add Blank Row
            vList.ValueListItems.Add(0, "");
            // Get rest of items
            string strSQL = null;
            strSQL = "SELECT EmployeeID, EmployeeCode ";
            strSQL += "FROM tblEmployees ";
            strSQL += "ORDER BY EmployeeCode";
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                vList.ValueListItems.Add(dr.GetValueOrDefault<int>("EmployeeID"), dr.GetValueOrDefault<string>("EmployeeCode"));
            }
            dr.Close();
            conn = null;
            cmd = null;
            dr = null;
            //
            return vList;
        }

        private Infragistics.WebUI.UltraWebGrid.ValueList Employees()
        {
            Infragistics.WebUI.UltraWebGrid.ValueList vList = new Infragistics.WebUI.UltraWebGrid.ValueList();
            // Add Blank Row
            vList.ValueListItems.Add(0, "");
            // Get rest of items
            string strSQL = null;
            strSQL = "SELECT EmployeeID, EmployeeName ";
            strSQL += "FROM tblEmployees ";
            strSQL += "ORDER BY EmployeeCode";
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                vList.ValueListItems.Add(dr.GetValueOrDefault<string>("EmployeeID"), dr.GetValueOrDefault<string>("EmployeeName"));
            }
            dr.Close();
            conn = null;
            cmd = null;
            dr = null;
            //
            return vList;
        }

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
            // Set week labels
            SetWeekDateArrays(wyFirst, wyLast);
            //
            strSQL = "SELECT ([ID]) AS ProjectID, ProjectNo, ";
            strSQL += "UPPER(ProjectName) AS ProjectName, PICCode, PM1Code ";
            for (intI = 0; intI <= clsSchedule.cWeekSpan; intI++)
            {
                strSQL += ", (SELECT SUM ([Hours]) ";
                strSQL += "FROM v_EmployeeScheduleByWeek s ";
                strSQL += "WHERE s.ProjectID = tblProjects.ID ";
                strSQL += "AND (s.WeekNoStartDate ";
                strSQL += "BETWEEN '" + strDateFirst + "' ";
                strSQL += "AND '" + strDateLast + "') ";
                strSQL += "AND (s.WeekNo = " + intWeeks[intI] + ")) AS [Week" + (intI + 1) + "] ";
            }
            strSQL += "FROM tblProjects ";
            if (!string.IsNullOrEmpty(strFilterText))
            {
                strFilter = GetFilter(strFilterField, strFilterText);
                strSQL += "WHERE Active = 0 AND ";
                strSQL += strFilter + " ";
            }
            else
            {
                strSQL += "WHERE Active = 0 ";
            }
            strSQL += "ORDER BY ProjectNo";
            //
            daProjects = new SqlDataAdapter(strSQL, strConn);
            //
            daProjects.Fill(dsProjects, "Projects");
            //
            this.uwgProjects.DataSource = dsProjects;
            this.uwgProjects.DataMember = "Projects";
        }

        private void InitializeGrid()
        {
            clsSchedule cSchedule = new clsSchedule();
            int intHeight = 0;
            int intI = 0;
            UltraGridLayout uwgLayout = this.uwgProjects.DisplayLayout;
            string weekLabel = null;
            int gridWidth = 595;
            //
            uwgLayout.RowHeightDefault = Unit.Pixel(13);

            // sets the height of the control
            if (Convert.ToInt32(Session["ScreenH"]) != 0)
            {
                intHeight = cSchedule.GetGridHeight(Session["ScreenH"].GetValueOrDefault<string>());
                this.uwgProjects.Height = Unit.Pixel(intHeight);
            }

            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //changes the sort icons            
            uwgLayout.ImageUrls.SortDescending = "./images/spacer.gif";
            uwgLayout.ImageUrls.SortAscending = "./images/spacer.gif";
            // First Band
            uwgLayout.Bands[0].AllowUpdate = AllowUpdate.No;
            this.uwgProjects.Bands[0].DefaultColWidth = Unit.Pixel(100);
            //
            // 0
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectID").Hidden = true;

            //1
            UltraGridColumn restoreColumn = this.uwgProjects.Bands[0].Columns.FromKey("Restore");

            if ((restoreColumn == null))
            {
                restoreColumn = new UltraGridColumn();
                this.uwgProjects.Bands[0].Columns.Insert(1, restoreColumn);
            }

            restoreColumn.Header.Caption = "Restore";
            restoreColumn.Key = "Restore";
            restoreColumn.Type = ColumnType.CheckBox;
            restoreColumn.CellStyle.HorizontalAlign = HorizontalAlign.Center;
            restoreColumn.Width = 70;
            restoreColumn.AllowUpdate = AllowUpdate.Yes;

            // 2
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectNo").Header.Caption = "JOB #";
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectNo").Width = Unit.Pixel(85);
            // 3
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectName").Header.Caption = "PROJECT";
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectName").Width = Unit.Pixel(260);
            // 4
            this.uwgProjects.Bands[0].Columns.FromKey("PICCode").Header.Caption = "PIC";
            this.uwgProjects.Bands[0].Columns.FromKey("PICCode").Width = Unit.Pixel(70);
            // 5
            this.uwgProjects.Bands[0].Columns.FromKey("PM1Code").Header.Caption = "PM";
            this.uwgProjects.Bands[0].Columns.FromKey("PM1Code").Width = Unit.Pixel(70);
            for (intI = 6; intI <= 15; intI++)
            {
                this.uwgProjects.Bands[0].Columns[intI].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                weekLabel = strWeekLabel[intI - 6];

                if ((weekLabel.Length > 4))
                {
                    this.uwgProjects.Bands[0].Columns[intI].Width = Unit.Pixel(45);
                    gridWidth += 45;
                }
                else
                {
                    this.uwgProjects.Bands[0].Columns[intI].Width = Unit.Pixel(35);
                    gridWidth += 35;
                }

                this.uwgProjects.Bands[0].Columns[intI].Header.Style.CssClass = "WeekHeaderStyle";
                this.uwgProjects.Bands[0].Columns[intI].Header.Caption = weekLabel;
                this.uwgProjects.Bands[0].Columns[intI].DataType = "System.Decimal";
            }

            this.uwgProjects.Width = Unit.Pixel(gridWidth);
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

        private void FormatGrid()
        {
            // Databind Grid        
            this.uwgProjects.DataBind();
            InitDefaultMondayColumn();
        }

        private void InitDefaultMondayColumn()
        {
            string strWeekLabel = schedule.GetCurrentWeekLabel();
            int intCol = 0;
            bool blnCurWeek = false;
            //
            foreach (UltraGridColumn uwgCol in this.uwgProjects.Bands[0].Columns)
            {
                if (uwgCol.Header.Caption == strWeekLabel)
                {
                    intCol = uwgCol.Index;
                    blnCurWeek = true;
                }
            }
            if (!blnCurWeek)
                return;
            //
            foreach (UltraGridRow uwgRow in this.uwgProjects.Rows)
            {
                if (uwgRow.Band.Index == 0)
                {
                    uwgRow.Cells[intCol].Style.CssClass = "WeekCurMondayCellStyle";
                }
            }
        }

        protected void btnThisWeek_Click(object sender, System.EventArgs e)
        {
            WeekYear wyFirst = default(WeekYear);
            WeekYear wyLast = default(WeekYear);
            SearchFilter sArchivedFilter = new SearchFilter();
            //
            wyFirst = schedule.GetWeekYear(System.DateTime.Now);
            wyLast = schedule.GetWeekYearLast(wyFirst);
            if ((Session["sArchivedFilter"] != null))
            {
                sArchivedFilter = Session["sArchivedFilter"].GetValueOrDefault<SearchFilter>();
            }
            // Set Session Variables
            Session["wyFirst"] = wyFirst;
            Session["wyLast"] = wyLast;
            Session["sArchivedFilter"] = sArchivedFilter;
            //
            this.wdtWeek.Value = wyFirst.MondayDate;
            // Populate Grid
            PopulateDataset(wyFirst, wyLast, sArchivedFilter.Field, sArchivedFilter.Text);
            FormatGrid();
            this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateFilter();
                WeekYear wyFirst = default(WeekYear);
                WeekYear wyLast = default(WeekYear);
                SearchFilter sArchivedFilter = new SearchFilter();
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
                //If Not Session["sArchivedFilter"] Is Nothing Then
                //    sArchivedFilter = Session["sArchivedFilter"]
                //End If
                // Set Session Variables
                Session["wyFirst"] = wyFirst;
                Session["wyLast"] = wyLast;
                Session["sArchivedFilter"] = sArchivedFilter;
                //
                this.wdtWeek.Value = wyFirst.MondayDate;
                // Populate Grid
                PopulateDataset(wyFirst, wyLast, sArchivedFilter.Field, sArchivedFilter.Text);
                FormatGrid();
                this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
            }
        }

        protected void uwgProjects_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            InitializeGrid();
        }

        protected void uwgProjects_InitializeRow(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
        {
            string strURL = null;
            if (e.Row.Band.Index == 0)
            {
                strURL = "<a title='" + e.Row.Cells.FromKey("ProjectName").Value.ToString() + "' onClick=" + (char)34 + "javascript: window.open('ProjectDetail.aspx?PID=" + e.Row.Cells[0].Value.ToString() + "','Report','toolbar=no, directories=no, location=no, status=no, menubar=no, resizable=yes, scrollbars=yes, width=850, height=412, top=5, left=5');" + (char)34 + " href='#'>" + e.Row.Cells.FromKey("ProjectName").Value.ToString() + "</a>";
                e.Row.Cells.FromKey("ProjectName").Value = strURL;
            }
        }

        private void uwgProjects_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            this.uwgProjects.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            SearchFilter sArchivedFilter = Session["sArchivedFilter"].GetValueOrDefault<SearchFilter>();
            PopulateDataset(Session["wyFirst"].GetValueOrDefault<WeekYear>(), Session["wyLast"].GetValueOrDefault<WeekYear>(), sArchivedFilter.Field, sArchivedFilter.Text);
            FormatGrid();
            this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
        }

        protected void btnNext_Click(object sender, System.EventArgs e)
        {
            // Move forward 4 Weeks
            // Move back 4 weeks
            WeekYear wyFirst = schedule.GetWeekYearFirst(Session["wyFirst"].GetValueOrDefault<WeekYear>(), true);
            WeekYear wyLast = schedule.GetWeekYearLast(wyFirst);
            Session["wyFirst"] = wyFirst;
            Session["wyLast"] = wyLast;
            SearchFilter sArchivedFilter = Session["sArchivedFilter"].GetValueOrDefault<SearchFilter>();
            PopulateDataset(wyFirst, wyLast, sArchivedFilter.Field, sArchivedFilter.Text);
            FormatGrid();
            this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
            // Set Date
            this.wdtWeek.Value = wyFirst.MondayDate;
        }

        protected void btnPrevious_Click(object sender, System.EventArgs e)
        {
            // Move back 4 weeks
            WeekYear wyFirst = schedule.GetWeekYearFirst(Session["wyFirst"].GetValueOrDefault<WeekYear>(), false);
            WeekYear wyLast = schedule.GetWeekYearLast(wyFirst);
            Session["wyFirst"] = wyFirst;
            Session["wyLast"] = wyLast;
            SearchFilter sArchivedFilter = Session["sArchivedFilter"].GetValueOrDefault<SearchFilter>();
            PopulateDataset(wyFirst, wyLast, sArchivedFilter.Field, sArchivedFilter.Text);
            FormatGrid();
            this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
            // Set Date
            this.wdtWeek.Value = wyFirst.MondayDate;
        }

        protected void btnGo_Click(object sender, System.EventArgs e)
        {
            SearchFilter sArchivedFilter = new SearchFilter();
            WeekYear wyFirst = Session["wyFirst"].GetValueOrDefault<WeekYear>();
            WeekYear wyLast = Session["wyLast"].GetValueOrDefault<WeekYear>();
            if (string.IsNullOrEmpty(this.txtSearch.Text))
            {
                sArchivedFilter.Field = "";
                sArchivedFilter.Text = "";
            }
            else
            {
                sArchivedFilter.Field = this.ddlFilter.SelectedValue;
                sArchivedFilter.Text = this.txtSearch.Text;
            }
            Session["sArchivedFilter"] = sArchivedFilter;
            PopulateDataset(wyFirst, wyLast, sArchivedFilter.Field, sArchivedFilter.Text);
            FormatGrid();
            this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
            // Set Date
            this.wdtWeek.Value = wyFirst.MondayDate;
        }

        protected void wdtWeek_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
        {
            WeekYear wyFirst = schedule.GetWeekYear(DateTime.Parse(this.wdtWeek.Value.ToString()));
            WeekYear wyLast = schedule.GetWeekYearLast(wyFirst);
            Session["wyFirst"] = wyFirst;
            Session["wyLast"] = wyLast;
            SearchFilter sArchivedFilter = Session["sArchivedFilter"].GetValueOrDefault<SearchFilter>();
            PopulateDataset(wyFirst, wyLast, sArchivedFilter.Field, sArchivedFilter.Text);
            FormatGrid();
            this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
        }

        protected void btnRestoreProjects_Click(object sender, System.EventArgs e)
        {
            // Move back 4 weeks
            WeekYear wyFirst = Session["wyFirst"].GetValueOrDefault<WeekYear>();
            WeekYear wyLast = Session["wyLast"].GetValueOrDefault<WeekYear>();
            SearchFilter sArchivedFilter = Session["sArchivedFilter"].GetValueOrDefault<SearchFilter>();
            PopulateDataset(wyFirst, wyLast, sArchivedFilter.Field, sArchivedFilter.Text);
            FormatGrid();
            this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
            // Set Date
            this.wdtWeek.Value = wyFirst.MondayDate;
        }

        protected void uwgProjects_UpdateGrid(object sender, Infragistics.WebUI.UltraWebGrid.UpdateEventArgs e)
        {
            UltraGridRow uwgRow = default(UltraGridRow);
            UltraGridRowsEnumerator updatedRows = default(UltraGridRowsEnumerator);
            int intProjectID = 0;
            int empID = Session["EmployeeID"].GetValueOrDefault<int>();
            Project Project = new Project();

            // Get Updated rows for Employee Schedule rows
            updatedRows = e.Grid.Bands[0].GetBatchUpdates();
            // 
            while (updatedRows.MoveNext())
            {
                uwgRow = updatedRows.Current;
                //
                intProjectID = uwgRow.Cells[0].GetValueOrDefault<int>();
                if (intProjectID > 0)
                {
                    Project.UnArchiveProject(empID, intProjectID);
                }
            }
        }
    }
}
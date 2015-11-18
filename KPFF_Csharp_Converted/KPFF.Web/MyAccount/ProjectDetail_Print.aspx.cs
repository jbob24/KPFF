
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using System.Configuration;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAccount
{
    partial class ProjectDetail_Print : PageBase
    {

        #region " Schedule Routines"
        public string strConn = ConfigurationManager.AppSettings["ConnectionString"];
        public bool blnUpdated = false;
        protected System.Data.SqlClient.SqlConnection conn;
        protected SqlDataAdapter daSchedule;
        protected DataSet dsProjects;
        protected DataTable dtProjects;
        protected DataTable dtSchedule;
        protected SqlDataSource sqlds;
        public clsSchedule schedule = new clsSchedule();
        public string strSort = "";
        //
        private void PopulateForm(int intProjectID)
	{
		string strSQL = "";
		clsGeneral General = new clsGeneral();

		strSQL = "SELECT tblProjects.ProjectNo, tblProjects.ProjectName ";
		strSQL += "FROM tblProjects ";
		strSQL += "WHERE (tblProjects.ID = " + intProjectID + ")";

		DataSet ds = General.FillDataset(strSQL);
		DataTable dt = ds.Tables[0];

		if (dt.Rows.Count > 0) {
			this.lblProjectNo.Text = dt.Rows[0]["ProjectNo"].GetValueOrDefault<string>();
            this.lblProjectName.Text = dt.Rows[0]["ProjectName"].GetValueOrDefault<string>();
		}
	}

        private Infragistics.WebUI.UltraWebGrid.ValueList EmployeeRoles()
        {
            Infragistics.WebUI.UltraWebGrid.ValueList vList = new Infragistics.WebUI.UltraWebGrid.ValueList();
            // Add Items
            vList.ValueListItems.Add("Engineer", "Engineer");
            vList.ValueListItems.Add("Project Manager", "Project Manager");
            //
            return vList;
        }

        public void InitializeDataSource()
        {
            // Initialize dataset object
            dsProjects = new System.Data.DataSet();
            //
            DataColumn dtCol = default(DataColumn);
            // Schedule Table
            dtSchedule = new DataTable("Schedule");
            //
            dtCol = new DataColumn("ID");
            {
                dtCol.DataType = typeof(int);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("ProjectID");
            {
                dtCol.DataType = typeof(int);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("EmployeeID");
            {
                dtCol.DataType = typeof(int);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("EmployeeName");
            {
                dtCol.DataType = typeof(string);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Role");
            {
                dtCol.DataType = typeof(string);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week1");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week2");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week3");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week4");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week5");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week6");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week7");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week8");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week9");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week10");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week11");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week12");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week13");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week14");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week15");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);

            //
            dtCol = new DataColumn("Week16");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week17");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week18");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week19");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("Week20");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);

            //
            dtCol = new DataColumn("IDField");
            {
                dtCol.DataType = typeof(int);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            // Add Tables to dataset
            dsProjects.Tables.Add(dtSchedule);
            //
        }

        private void PopulateDataset(int intProjID, WeekYear wyFirst, WeekYear wyLast, string strSortColumn = "", string strSortType = "")
        {
            int intI = 0;
            int intID = 0;

            DataSet dsProjects = Project.GetProjectAssignments(intProjID);


            // Auto increment ID field
            for (intI = 0; intI <= dsProjects.Tables["Schedule"].Rows.Count - 1; intI++)
            {
                intID += 1;
                dsProjects.Tables["Schedule"].Rows[intI]["ID"] = intID;
            }

            DataView scheduleView = dsProjects.Tables["Schedule"].DefaultView;

            scheduleView.Sort = "order asc, week1 desc";
                        
            this.uwgProjects.DataSource = scheduleView.Table;
            this.uwgProjects.DataMember = scheduleView.Table.TableName;
        }

        private void InitializeGrid()
        {
            int intI = 0;
            UltraGridLayout uwgLayout = this.uwgProjects.DisplayLayout;
            //
            uwgLayout.RowHeightDefault = Unit.Pixel(13);
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            // Band
            //changes the sort icons            
            uwgLayout.ImageUrls.SortDescending = "./images/spacer.gif";
            uwgLayout.ImageUrls.SortAscending = "./images/spacer.gif";
            //
            // Activate sorting
            this.uwgProjects.Bands[0].AllowSorting = AllowSorting.Yes;
            this.uwgProjects.Bands[0].HeaderClickAction = HeaderClickAction.SortSingle;
            // Styles
            this.uwgProjects.Bands[0].HeaderStyle.CssClass = "ChildHeaderStyle";
            this.uwgProjects.Bands[0].FooterStyle.CssClass = "ChildFooterStyle";
            // Show footer for Schedule band
            this.uwgProjects.DisplayLayout.Bands[0].ColFootersVisible = ShowMarginInfo.Yes;
            // 0
            this.uwgProjects.Bands[0].Columns.FromKey("ID").Hidden = true;
            // 1 ProjectID
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectID").Hidden = true;
            // 2 EmployeeID
            this.uwgProjects.Bands[0].Columns.FromKey("EmployeeID").Hidden = true;
            // 3 EmployeeName
            this.uwgProjects.Bands[0].Columns.FromKey("EmployeeName").Header.Caption = "Employee";
            this.uwgProjects.Bands[0].Columns.FromKey("EmployeeName").Width = Unit.Pixel(210);
            // Disable updating
            this.uwgProjects.Bands[0].Columns.FromKey("EmployeeName").AllowUpdate = AllowUpdate.No;
            // Legend in the footer
            ColumnFooter cf = default(ColumnFooter);
            cf = this.uwgProjects.Bands[0].Columns.FromKey("EmployeeName").Footer;
            cf.RowLayoutColumnInfo.SpanX = 2;
            cf.Caption = "&nbsp;&nbsp;&nbsp;&nbsp;<img src='../images/GridLegend.jpg' />";
            cf.Style.HorizontalAlign = HorizontalAlign.Left;
            // 4
            this.uwgProjects.Bands[0].Columns.FromKey("Role").Header.Caption = "Role";
            this.uwgProjects.Bands[0].Columns.FromKey("Role").Width = Unit.Pixel(120);
            this.uwgProjects.Bands[0].Columns.FromKey("Role").Type = ColumnType.DropDownList;
            this.uwgProjects.Bands[0].Columns.FromKey("Role").ValueList = EmployeeRoles();
            this.uwgProjects.Bands[0].Columns.FromKey("Role").ValueList.DisplayStyle = ValueListDisplayStyle.DisplayText;
            // Week column widths
            for (intI = 5; intI <= 24; intI++)
            {
                this.uwgProjects.Bands[0].Columns[intI].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                this.uwgProjects.Bands[0].Columns[intI].Header.Caption = WeekDate.WeekLabels[intI - 5];
                this.uwgProjects.Bands[0].Columns[intI].DataType = "System.Decimal";
                this.uwgProjects.Bands[0].Columns[intI].Width = Unit.Pixel(35);
                this.uwgProjects.Bands[0].Columns[intI].Footer.Total = SummaryInfo.Sum;
                this.uwgProjects.Bands[0].Columns[intI].Header.Style.CssClass = "ChildWeekHeaderStyle";
            }
            // 15 IDField
            this.uwgProjects.Bands[0].Columns.FromKey("IDField").Hidden = true;
            //
            // Current Week Column
            int intCol = 0;
            bool blnCurWeek = DefaultMondayColumn(intCol);
            if (blnCurWeek)
            {
                this.uwgProjects.Bands[0].Columns[intCol].Header.Style.CssClass = "ChildCurWeekHeaderStyle";
                //Me.uwgEngineers.Bands[0].Columns(intCol).Footer.Style.CssClass = "ChildCurWeekFooterStyle"
            }
        }

        private void FormatGrid()
        {
            int intCols = 0;
            string strStyle = null;
            decimal val = 0;
            // Databind Grid        
            this.uwgProjects.DataBind();
            //
            clsSchedule schedule = new clsSchedule();
            int intEmployeeID = 0;
            //
            InitializeGrid();
            //
            foreach (UltraGridRow uwgRow in this.uwgProjects.Rows)
            {
                if (uwgRow.Band.Index == 0)
                {
                    uwgRow.Band.RowStyle.CssClass = "ChildRowStyle";
                    uwgRow.Band.RowAlternateStyle.CssClass = "ChildAlternateRowStyle";
                    intEmployeeID = uwgRow.Cells[15].Value.GetValueOrDefault<int>();
                    for (intCols = 5; intCols <= 24; intCols++)
                    {
                        val = schedule.GetEmployeeWeekTotal(intEmployeeID, WeekDate.WeekIDs[intCols - 5]);
                        strStyle = schedule.GetWeekHoursStyle((int)val);
                        uwgRow.Cells[intCols].Style.CssClass = strStyle;
                    }
                }
            }
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

        private bool DefaultMondayColumn(int intCol)
        {
            string strWeekLabel = schedule.GetCurrentWeekLabel();
            bool blnCurWeek = false;
            //
            foreach (UltraGridColumn uwgCol in this.uwgProjects.Bands[0].Columns)
            {
                if (uwgCol.Header.Caption == strWeekLabel)
                {
                    intCol = uwgCol.Index;
                    blnCurWeek = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            //
            return blnCurWeek;
        }

        private void SetSortType()
        {
            strSort = "";
            if (Session["SortType"] == null)
            {
                strSort = "";
                Session["SortType"] = "ASC";
            }
            else
            {
                if (Session["SortType"].GetValueOrDefault<string>() == "ASC")
                {
                    strSort = " DESC";
                    Session["SortType"] = "DESC";
                }
                else if (Session["SortType"].GetValueOrDefault<string>() == "DESC")
                {
                    strSort = "";
                    Session["SortType"] = "ASC";
                }
            }
        }
        #endregion

        protected void Page_Init(System.Object sender, System.EventArgs e)
        {
            InitializeDataSource();
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Page.IsPostBack)
                return;
            // Sort Type Session Variable
            Session["SortType"] = null;
            //
            int intProjectID = Request.Params["PID"].GetValueOrDefault<int>();
            //
            WeekYear wyFirst = Session["wyFirst"] as WeekYear;
            WeekYear wyLast = Session["wyLast"] as WeekYear;
            if (!this.IsPostBack)
            {
                //
                PopulateForm(intProjectID);
                InitializeDataSource();
                PopulateDataset(intProjectID, wyFirst, wyLast);
                FormatGrid();
                this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
            }
        }

        protected void uwgProjects_SortColumn(object sender, Infragistics.WebUI.UltraWebGrid.SortColumnEventArgs e)
        {
            //cancel the grid's default sorting action
            e.Cancel = true;
            // Set Sort Type
            SetSortType();
            //
            // Date Controls for Reassign
            int intProjectID = Request.Params["PID"].GetValueOrDefault<int>();
            WeekYear wyFirst = Session["wyFirst"].GetValueOrDefault<WeekYear>();
            WeekYear wyLast = Session["wyLast"].GetValueOrDefault<WeekYear>();
            InitializeDataSource();
            PopulateDataset(intProjectID, wyFirst, wyLast, this.uwgProjects.DisplayLayout.Bands[0].Columns[e.ColumnNo].Key, strSort);
            FormatGrid();
            this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
        }
    }
}
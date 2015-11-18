
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using System.Configuration;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAccount
{
    partial class MySchedule_Print : KPFF.PMP.Entities.PageBase
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
            dtCol = new DataColumn("ProjectNo");
            {
                dtCol.DataType = typeof(decimal);
            }
            dtSchedule.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("ProjectName");
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
            // Add Tables to dataset
            dsProjects.Tables.Add(dtSchedule);
        }

        private void PopulateDataset(int intEmpID, WeekYear wyFirst, WeekYear wyLast, string strSortColumn = "", string strSortType = "")
        {

            int intI = 0;
            int intID = 0;
            DataSet dsProjects = Project.GetActiveScheduleByEmployeeID(intEmpID);

            for (intI = 0; intI <= dsProjects.Tables["Schedule"].Rows.Count - 1; intI++)
            {
                intID += 1;
                dsProjects.Tables["Schedule"].Rows[intI]["ID"] = intID;
            }
            //
            DataView scheduleView = dsProjects.Tables["Schedule"].DefaultView;


            if (!string.IsNullOrEmpty(strSortColumn))
            {
                scheduleView.Sort = strSortColumn + strSortType;
            }
            else
            {
                scheduleView.Sort = "week1 desc, ProjectName asc";
            }

            this.uwgProjects.DataSource = scheduleView;
            this.uwgProjects.DataMember = scheduleView.Table.TableName;
        }

        private void InitializeGrid()
        {
            clsSchedule cSchedule = new clsSchedule();
            int intHeight = 0;
            int intI = 0;
            UltraGridLayout uwgLayout = this.uwgProjects.DisplayLayout;
            //
            //
            uwgLayout.RowHeightDefault = Unit.Pixel(13);
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            // Activate sorting
            this.uwgProjects.Bands[0].AllowSorting = AllowSorting.Yes;
            this.uwgProjects.Bands[0].HeaderClickAction = HeaderClickAction.SortSingle;
            //
            this.uwgProjects.Bands[0].HeaderStyle.CssClass = "ChildHeaderStyle";
            this.uwgProjects.Bands[0].FooterStyle.CssClass = "ChildFooterStyle";

            //changes the sort icons            
            uwgLayout.ImageUrls.SortDescending = "./images/spacer.gif";
            uwgLayout.ImageUrls.SortAscending = "./images/spacer.gif";
            //
            // Show footer for Schedule band
            this.uwgProjects.DisplayLayout.Bands[0].ColFootersVisible = ShowMarginInfo.Yes;
            // 0
            this.uwgProjects.Bands[0].Columns.FromKey("ID").Hidden = true;
            // 1
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectID").Hidden = true;
            // 2 ProjecNo
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectNo").Header.Caption = "Job #";
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectNo").Width = Unit.Pixel(100);
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectNo").AllowUpdate = AllowUpdate.No;
            ColumnFooter cf = default(ColumnFooter);
            cf = this.uwgProjects.Bands[0].Columns.FromKey("ProjectNo").Footer;
            cf.RowLayoutColumnInfo.SpanX = 2;
            cf.Caption = "&nbsp;&nbsp;&nbsp;&nbsp;<img src='../images/GridLegend.jpg' />";
            cf.Style.HorizontalAlign = HorizontalAlign.Left;
            // 3 ProjectName
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectName").Header.Caption = "Project";
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectName").Width = Unit.Pixel(200);
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectName").AllowUpdate = AllowUpdate.No;
            // Week column widths
            for (intI = 4; intI <= 23; intI++)
            {
                this.uwgProjects.Bands[0].Columns[intI].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                this.uwgProjects.Bands[0].Columns[intI].Width = Unit.Pixel(35);
                this.uwgProjects.Bands[0].Columns[intI].Footer.Total = SummaryInfo.Sum;
                this.uwgProjects.Bands[0].Columns[intI].Header.Caption = WeekDate.WeekLabels[intI - 4];
                this.uwgProjects.Bands[0].Columns[intI].DataType = "System.Decimal";
                this.uwgProjects.Bands[0].Columns[intI].Header.Style.CssClass = "ChildWeekHeaderStyle";
            }
            //
            // Current Week Column
            int intCol = 0;
            bool blnCurWeek = DefaultMondayColumn(intCol);
            if (blnCurWeek)
            {
                this.uwgProjects.Bands[0].Columns[intCol].Header.Style.CssClass = "ChildCurWeekHeaderStyle";
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
            int intEmployeeID = Session["EmployeeID"].GetValueOrDefault<int>();
            //
            InitializeGrid();
            //
            //
            foreach (UltraGridRow uwgRow in this.uwgProjects.Rows)
            {
                if (uwgRow.Band.Index == 0)
                {
                    uwgRow.Band.RowStyle.CssClass = "ChildRowStyle";
                    uwgRow.Band.RowAlternateStyle.CssClass = "ChildAlternateRowStyle";
                    for (intCols = 4; intCols <= 13; intCols++)
                    {
                        val = schedule.GetEmployeeWeekTotal(intEmployeeID, WeekDate.WeekIDs[intCols - 4]);
                        strStyle = schedule.GetWeekHoursStyle((int)val);
                        uwgRow.Cells[intCols].Style.CssClass = strStyle;
                    }
                }
            }
            //
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
            int intEmployeeID = Session["EmployeeID"].GetValueOrDefault<int>();
            WeekYear wyFirst = Session["wyFirst"].GetValueOrDefault<WeekYear>();
            WeekYear wyLast = Session["wyLast"].GetValueOrDefault<WeekYear>();
            if (!this.IsPostBack)
            {
                InitializeDataSource();
                PopulateDataset(intEmployeeID, wyFirst, wyLast);
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
            int intEmployeeID = Session["EmployeeID"].GetValueOrDefault<int>();
            WeekYear wyFirst = Session["wyFirst"].GetValueOrDefault<WeekYear>();
            WeekYear wyLast = Session["wyLast"].GetValueOrDefault<WeekYear>();
            InitializeDataSource();
            PopulateDataset(intEmployeeID, wyFirst, wyLast, this.uwgProjects.DisplayLayout.Bands[0].Columns[e.ColumnNo].Key, strSort);
            FormatGrid();
            this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
        }
    }
}
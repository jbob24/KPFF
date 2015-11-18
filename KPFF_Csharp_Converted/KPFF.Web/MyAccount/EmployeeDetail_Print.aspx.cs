
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using KPFF.PMP.Entities;
using System.Configuration;
using System;

namespace KPFF.PMP.MyAccount
{
    partial class EmployeeDetail_Print : PageBase
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
        public string strSort = "";
        //
        private void PopulateForm(int intEmployeeID)
        {
            SqlConnection con = new SqlConnection(Configuration.ConnectionString);

            con.Open();
            SqlDataReader dr = Engineer.GetByEmployeeID(intEmployeeID, con);

            if (dr.HasRows)
            {
                dr.Read();
                lblName.Text = string.Format("{0} {1}", dr.GetValueOrDefault<string>("EmployeeFirst"), dr.GetValueOrDefault<string>("EmployeeLast"));
            }

            con.Close();
            dr.Close();
            con = null;
            dr = null;
        }

        private void PopulateDataset(int intEmpID, string strSortColumn = "", string strSortType = "")
        {
            //
            int intI = 0;
            int intID = 0;

            dsProjects = Project.GetScheduleByEmployeeID(intEmpID);

            for (intI = 0; intI <= dsProjects.Tables["Schedule"].Rows.Count - 1; intI++)
            {
                intID += 1;
                dsProjects.Tables["Schedule"].Rows[intI]["ID"] = intID;
            }
            //
            if (!string.IsNullOrEmpty(strSortColumn))
            {
                dtSchedule.DefaultView.Sort = strSortColumn + strSortType;
            }
            //
            this.uwgProjects.DataSource = dsProjects;
            this.uwgProjects.DataMember = dsProjects.Tables["Schedule"].TableName;
        }

        private void InitializeGrid()
        {
            int intI = 0;
            UltraGridLayout uwgLayout = this.uwgProjects.DisplayLayout;
            //
            uwgLayout.RowHeightDefault = Unit.Pixel(13);
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //
            // Activate sorting
            this.uwgProjects.Bands[0].AllowSorting = AllowSorting.Yes;
            this.uwgProjects.Bands[0].HeaderClickAction = HeaderClickAction.SortSingle;
            //
            this.uwgProjects.Bands[0].HeaderStyle.CssClass = "ChildHeaderStyle";
            this.uwgProjects.Bands[0].FooterStyle.CssClass = "ChildFooterStyle";
            //changes the sort icons            
            uwgLayout.ImageUrls.SortDescending = "./images/spacer.gif";
            uwgLayout.ImageUrls.SortAscending = "./images/spacer.gif";
            // Show footer for Schedule band
            this.uwgProjects.DisplayLayout.Bands[0].ColFootersVisible = ShowMarginInfo.Yes;
            // 0
            this.uwgProjects.Bands[0].Columns.FromKey("ID").Hidden = true;
            // 1 ProjectID
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectID").Hidden = true;
            // 2 ProjectNo
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectNo").Header.Caption = "Job #";
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectNo").Width = Unit.Pixel(100);
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectNo").AllowUpdate = AllowUpdate.No;
            ColumnFooter cf = default(ColumnFooter);
            cf = this.uwgProjects.Bands[0].Columns.FromKey("ProjectNo").Footer;
            cf.RowLayoutColumnInfo.SpanX = 2;
            cf.Caption = "&nbsp;&nbsp;&nbsp;&nbsp;<img src='../images/GridLegend.jpg' />";
            // 3 ProjectName
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectName").Header.Caption = "Project";
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectName").Width = Unit.Pixel(200);
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectName").AllowUpdate = AllowUpdate.No;
            // Week column widths
            for (intI = 4; intI <= 13; intI++)
            {
                this.uwgProjects.Bands[0].Columns[intI].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                this.uwgProjects.Bands[0].Columns[intI].Width = Unit.Pixel(35);
                this.uwgProjects.Bands[0].Columns[intI].Footer.Total = SummaryInfo.Sum;
                this.uwgProjects.Bands[0].Columns[intI].Header.Caption = WeekDate.WeekLabels[intI - 4];
                this.uwgProjects.Bands[0].Columns[intI].DataType = "System.Decimal";
                this.uwgProjects.Bands[0].Columns[intI].Header.Style.CssClass = "ChildWeekHeaderStyle";
            }
            // 14 IDField
            this.uwgProjects.Bands[0].Columns[14].Hidden = true;
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
            //
            clsSchedule schedule = new clsSchedule();
            int intEmployeeID = Request.Params["EID"].GetValueOrDefault<int>();
            // Databind Grid        
            this.uwgProjects.DataBind();
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
        }

        private void InitDefaultMondayColumn()
        {
            string strWeekLabel = Schedule.GetCurrentWeekLabel();
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
            string strWeekLabel = Schedule.GetCurrentWeekLabel();
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
            //InitializeDataSource()
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Page.IsPostBack)
                return;
            // Sort Type Session Variable
            Session["SortType"] = null;
            //
            int intEmployeeID = Request.Params["EID"].GetValueOrDefault<int>();
            //
            if (!this.IsPostBack)
            {
                PopulateForm(intEmployeeID);
                //InitializeDataSource()
                PopulateDataset(intEmployeeID);
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
            int intEmployeeID = Request.Params["EID"].GetValueOrDefault<int>();

            // Populate Employee Schedule grid
            //InitializeDataSource()
            PopulateDataset(intEmployeeID, this.uwgProjects.DisplayLayout.Bands[0].Columns[e.ColumnNo].Key, strSort);
            FormatGrid();
            this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
        }
    }
}
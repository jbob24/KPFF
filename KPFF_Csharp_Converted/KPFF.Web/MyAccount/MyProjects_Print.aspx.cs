
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using Microsoft.Win32;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAccount
{
    partial class MyProjects_Print : PageBase
    {

        #region "Web Form Designer generated code"
        //This call is required by the Web Form Designer.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
        }

        private void Page_Init(System.Object sender, System.EventArgs e)
        {
            //CODEGEN: This method call is required by the Web Form Designer
            //Do not modify it using the code editor.
            InitializeComponent();
        }

        #endregion

        #region " Custom Routines"
        private void PopulateDataset()
        {
            int empID = Session["EmployeeID"].GetValueOrDefault<int>();


            var dsProjects = Project.GeMyProjects(empID);
            var dtProjects = dsProjects.Tables["Projects"];
            DataView dvProjects = dtProjects.DefaultView;
            dvProjects.Sort = "ProjectNo";
            this.uwgProjects.DataSource = dvProjects;

            FormatGrid();

        }

        private void InitializeGrid()
        {
            clsSchedule cSchedule = new clsSchedule();
            int intHeight = 0;
            int intI = 0;
            UltraGridLayout uwgLayout = this.uwgProjects.DisplayLayout;
            //
            uwgLayout.RowHeightDefault = Unit.Pixel(13);

            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            // First Band
            uwgLayout.Bands[0].AllowUpdate = AllowUpdate.No;
            this.uwgProjects.Bands[0].DefaultColWidth = Unit.Pixel(100);
            // Show footer
            this.uwgProjects.DisplayLayout.Bands[0].ColFootersVisible = ShowMarginInfo.Yes;
            this.uwgProjects.Bands[0].FooterStyle.CssClass = "FooterStyle";
            // 0
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectID").Hidden = true;
            // 1
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectNo").Header.Caption = "JOB #";
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectNo").Width = Unit.Pixel(85);
            // 2
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectName").Header.Caption = "PROJECT";
            this.uwgProjects.Bands[0].Columns.FromKey("ProjectName").Width = Unit.Pixel(260);
            // 3
            this.uwgProjects.Bands[0].Columns.FromKey("PICCode").Header.Caption = "PIC";
            this.uwgProjects.Bands[0].Columns.FromKey("PICCode").Width = Unit.Pixel(70);
            // 4
            this.uwgProjects.Bands[0].Columns.FromKey("PM1Code").Header.Caption = "PM";
            this.uwgProjects.Bands[0].Columns.FromKey("PM1Code").Width = Unit.Pixel(70);
            for (intI = 5; intI <= 24; intI++)
            {
                this.uwgProjects.Bands[0].Columns[intI].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                this.uwgProjects.Bands[0].Columns[intI].Width = Unit.Pixel(35);
                this.uwgProjects.Bands[0].Columns[intI].Header.Style.CssClass = "WeekHeaderStyle";
                this.uwgProjects.Bands[0].Columns[intI].Header.Caption = WeekDate.WeekLabels[intI - 5];
                this.uwgProjects.Bands[0].Columns[intI].DataType = "System.Decimal";
                this.uwgProjects.Bands[0].Columns[intI].Footer.Total = SummaryInfo.Sum;
            }
        }

        private void FormatGrid()
        {
            // Databind Grid        
            this.uwgProjects.DataBind();
            //
            InitDefaultMondayColumn();
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

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDataset();
                FormatGrid();
                this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
            }
        }

        protected void uwgProjects_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            InitializeGrid();
        }

        protected void uwgProjects_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            this.uwgProjects.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            PopulateDataset();
            FormatGrid();
            this.uwgProjects.DisplayLayout.ActiveRow = this.uwgProjects.Rows[0];
        }

    }
}
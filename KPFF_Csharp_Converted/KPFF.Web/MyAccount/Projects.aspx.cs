
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
using Microsoft.Win32;
using KPFF.PMP.Entities;
using System;

namespace KPFF.PMP.MyAccount
{
    partial class Projects : PageBase
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

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            PopulateDataset();
            this.wdtWeek.Value = WeekDate.WYFirst.MondayDate;
        }

        private void PopulateDataset()
        {
            var dsProjects = Project.GetAllProjects();
            DataView scheduleView = dsProjects.Tables["Projects"].DefaultView;
            scheduleView.Sort = "week1 desc, ProjectName asc";

            hoursGrid.ProjectData = scheduleView.Table;
            hoursGrid.WeekDate = WeekDate;
            hoursGrid.Schedule = Schedule;
            hoursGrid.Engineer = Engineer;
            hoursGrid.ShowLastModified = true;
            hoursGrid.DisplayFooter = false;
            hoursGrid.AllowProjectSelect = false;
            hoursGrid.BindGrid();
        }

        #endregion
        
        protected void btnNext_Click(object sender, System.EventArgs e)
        {
            WyFirst = Schedule.GetWeekYearFirst(WyFirst, true);
            WyLast = Schedule.GetWeekYearLast(WyFirst);

            BindGrid();
        }

        protected void btnPrevious_Click(object sender, System.EventArgs e)
        {
            WyFirst = Schedule.GetWeekYearFirst(WyFirst, false);
            WyLast = Schedule.GetWeekYearLast(WyFirst);

            BindGrid();
        }

        protected void wdtWeek_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
        {
            WyFirst = Schedule.GetWeekYear(this.wdtWeek.Value.GetValueOrDefault<DateTime>());
            WyLast = Schedule.GetWeekYearLast(WyFirst);

            BindGrid();
        }

        protected void btnThisWeek_Click(object sender, System.EventArgs e)
        {
            WyFirst = Schedule.GetWeekYear(System.DateTime.Now);
            WyLast = Schedule.GetWeekYearLast(WyFirst);

            BindGrid();
        }
    }
}
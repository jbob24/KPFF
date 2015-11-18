
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

namespace KPFF.PMP.MyAdmin
{
    partial class Projects : PageBase
    {

        #region " Custom Routines"

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ActiveProj"] = 1;
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
            int intActive = 0;
            DataSet dsProjects = default(DataSet);

            if (Session["ActiveProj"].GetValueOrDefault<int>() == 0)
            {
                dsProjects = Project.GetInactiveProjects();
            }
            else
            {
                dsProjects = Project.GetAllProjects();
            }

            DataView scheduleView = dsProjects.Tables["Projects"].DefaultView;
            scheduleView.Sort = "ProjectName asc";

            hoursGrid.ProjectData = scheduleView.Table;
            hoursGrid.WeekDate = WeekDate;
            hoursGrid.Schedule = Schedule;
            hoursGrid.Engineer = Engineer;
            hoursGrid.ShowLastModified = true;
            hoursGrid.DisplayFooter = false;
            hoursGrid.AllowProjectSelect = true;
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

        protected void btnActiveInactive_Click(object sender, System.EventArgs e)
        {
            ToggleActiveInactive();
        }

        private void ToggleActiveInactive()
        {
            int intActive = 0;
            if (Session["ActiveProj"].GetValueOrDefault<int>() == 0)
            {
                intActive = 1;
            }

            Session["ActiveProj"] = intActive;
            if (intActive == 0)
            {
                this.btnActiveInactive.Text = "View Active";
                this.btnArchiveProjects.Text = "Make Active";
            }
            else
            {
                this.btnActiveInactive.Text = "View Inactive";
                this.btnArchiveProjects.Text = "Make Inactive";
            }

            BindGrid();
        }

        protected void btnArchiveProjects_Click(object sender, System.EventArgs e)
        {
            //' Move back 4 weeks
            //Dim wyFirst As WeekYear = Session["wyFirst"]
            //Dim wyLast As WeekYear = Session["wyLast"]
            //Dim sFilter As SearchFilter = Session["sFilter"]
            //PopulateDataset(wyFirst, wyLast, sFilter.Field, sFilter.Text, Session["ActiveProj"])
            //FormatGrid()
            //Me.uwgProjects.DisplayLayout.ActiveRow = Me.uwgProjects.Rows(0)
            //' Set Date
            //Me.wdtWeek.Value = wyFirst.MondayDate

            hoursGrid.ArchiveProjects(EmployeeID);
            RefreshPage();
        }

        protected void RefreshPage()
        {
            Response.Redirect("projects.aspx");
        }
    }
}
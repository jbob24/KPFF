
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
//Imports Infragistics.WebUI.UltraWebGrid
using System.Linq;
using KPFF.PMP;
using System;

namespace KPFF.PMP.MyAccount
{
    partial class MySchedule : KPFF.PMP.Entities.PageBase
    {

        #region "Fill Sub Controls"

        private void PopulateProjectList()
        {
            SqlConnection con = new SqlConnection(Configuration.ConnectionString);

            con.Open();
            KPFF.PMP.Entities.ProjectDataList projects = Project.GetActiveProjectsList(con);

            object projectsByName = from p in projects orderby p.ProjectName select p;
            object projectsByNumber = from p in projects orderby p.ProjectNumber select p;

            this.cboProjectsByName.DataSource = projectsByName;
            this.cboProjectsByName.DataTextField = "ProjectDescription";
            this.cboProjectsByName.DataValueField = "ID";
            this.cboProjectsByName.DataBind();

            this.cboProjectsByNumber.DataSource = projectsByNumber;
            this.cboProjectsByNumber.DataTextField = "ProjectDescription";
            this.cboProjectsByNumber.DataValueField = "ID";
            this.cboProjectsByNumber.DataBind();

            con.Close();
            con = null;
        }

        private void PopulateEmployees_ReAssign()
        {
            SqlConnection con = new SqlConnection(Configuration.ConnectionString);

            con.Open();
            SqlDataReader dr = Engineer.GetAllEngineers(con);

            this.cboEmployee.DataSource = dr;
            this.cboEmployee.DataTextField = "EmployeeName";
            this.cboEmployee.DataValueField = "EmployeeID";
            this.cboEmployee.DataBind();

            con.Close();
            dr.Close();
            con = null;
            dr = null;
        }
        #endregion


        protected void Page_Init(System.Object sender, System.EventArgs e)
        {
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!(EmployeeID == 0))
                {
                    PopulateProjectList();
                    PopulateEmployees_ReAssign();
                }
                // Date Controls for Reassign
                this.wdtFrom.Value = WyFirst.MondayDate;
                this.wdtTo.Value = WyLast.MondayDate;
                // Populate Employee Schedule grid
                BindGrid();
            }
        }


        protected void btnNext_Click(object sender, System.EventArgs e)
        {
            // Move forward 4 Weeks
            WyFirst = Schedule.GetWeekYearFirst(WyFirst, true);
            WyLast = Schedule.GetWeekYearLast(WyFirst);

            BindGrid();
        }


        protected void btnPrevious_Click(object sender, System.EventArgs e)
        {
            // Move back 4 weeks
            WyFirst = Schedule.GetWeekYearFirst(WyFirst, false);
            WyLast = Schedule.GetWeekYearLast(WyFirst);

            BindGrid();
        }

        protected void wdtWeek_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
        {
            WyFirst = Schedule.GetWeekYear(DateTime.Parse(this.wdtWeek.Value.ToString()));
            WyLast = Schedule.GetWeekYearLast(WyFirst);

            BindGrid();
        }

        protected void btnThisWeek_Click(object sender, System.EventArgs e)
        {
            WyFirst = Schedule.GetWeekYear(System.DateTime.Now);
            WyLast = Schedule.GetWeekYearLast(WyFirst);

            BindGrid();
        }

        protected void btnUpdateGrid_Click(object sender, System.EventArgs e)
        {
            hoursGrid.UpdateData(EmployeeID, EmployeeID);
            RefreshPage();
        }

        protected void btnReAssignEmployee_Click(object sender, System.EventArgs e)
        {
            int intEmployeeID = Convert.ToInt32(this.cboEmployee.SelectedValue);
            string strFromDate = this.wdtFrom.Value.ToString();
            string strToDate = this.wdtTo.Value.ToString();

            if (this.chkAll.Checked)
            {
                strFromDate = "";
                strToDate = "";
            }

            hoursGrid.ReassignProjects(intEmployeeID, EmployeeID, EmployeeID, strFromDate, strToDate);

            RefreshPage();
        }


        protected void RefreshPage()
        {
            Response.Redirect("MySchedule.aspx");
        }


        protected void btnUnAssignProjects_Click(object sender, System.EventArgs e)
        {
            if (hoursGrid.UnassignProjects(EmployeeID, EmployeeID))
            {
                RefreshPage();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "UnassignErrorVar", "displayUnassignError = true;", true);
            }
        }


        protected void btnAddProject_Click(object sender, System.EventArgs e)
        {
            if (EmployeeID == 0)
                return;

            int intProjectID = 0;

            if ((rbByName.Checked))
            {
                intProjectID = Convert.ToInt32(cboProjectsByName.SelectedValue);
            }
            else
            {
                intProjectID = Convert.ToInt32(cboProjectsByNumber.SelectedValue);
            }


            WyFirst = Schedule.GetWeekYear(System.DateTime.Now);
            WyLast = Schedule.GetWeekYearLast(WyFirst);

            var intWeekID = WeekDate.WeekIDs[0];

            Engineer.AddProject(EmployeeID, intProjectID, intWeekID, EmployeeID);

            RefreshPage();
        }


        private void BindGrid()
        {
            PopulateDataset(EmployeeID);
            this.wdtWeek.Value = WeekDate.WYFirst.MondayDate;
        }

        private void PopulateDataset(int intEmpID, string strSortColumn = "", string strSortType = "")
        {
            //
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

            hoursGrid.ProjectData = scheduleView.Table;
            hoursGrid.WeekDate = WeekDate;
            hoursGrid.Schedule = Schedule;
            hoursGrid.Engineer = Engineer;
            hoursGrid.HoursPerWeek = Engineer.HoursPerWeek;
            hoursGrid.BindGrid();
        }
    }
}
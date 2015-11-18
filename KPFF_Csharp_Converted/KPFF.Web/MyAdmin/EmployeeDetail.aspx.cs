using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
using System.Linq;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAdmin
{
    partial class EmployeeDetail : PageBase
    {

        private int _selectedEmpID;
        private int SelectedEmpID
        {
            get
            {
                _selectedEmpID = Request.Params["EID"].GetValueOrDefault<int>();
                return _selectedEmpID;
            }
            set { _selectedEmpID = value; }
        }

        #region " Custom Routines "

        private void PopulateProjectList()
        {
            SqlConnection con = new SqlConnection(Configuration.ConnectionString);

            con.Open();
            var projects = Project.GetActiveProjectsList(con);
            var projectsByName = from p in projects orderby p.ProjectName select p;
            var projectsByNumber = from p in projects orderby p.ProjectNumber select p;

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

        private void PopulateForm(int intEmployeeID)
        {
            SqlConnection con = new SqlConnection(Configuration.ConnectionString);

            con.Open();
            SqlDataReader dr = Engineer.GetByEmployeeID(intEmployeeID, con);

            if (dr.HasRows)
            {
                dr.Read();
                lblName.Text = string.Format("{0} {1}", dr.GetValueOrDefault<string>("EmployeeFirst"), dr.GetValueOrDefault<string>("EmployeeLast"));
                lblLastUpdated.Text = string.Format("{0} by {1}", dr.GetValueOrDefault<DateTime>("LastModified").ToShortDateString(), dr.GetValueOrDefault<string>("LastModifiedBy"));
                lblTitle.Text = dr.GetValueOrDefault<string>("Title");
                lblEmployeeStartDate.Text = dr.GetValueOrDefault<DateTime>("EmploymentStartDate").ToShortDateString();
                lblYearsOfExperience.Text = dr.GetValueOrDefault<int>("YearsOfExperience").ToString();
                lblEducation.Text = dr.GetValueOrDefault<string>("Education");
                lblLicenses.Text = dr.GetValueOrDefault<string>("Licenses");
                lblProfessionalMemberships.Text = dr.GetValueOrDefault<string>("ProfessionalMemberships");
                lblProfessionalCommittees.Text = dr.GetValueOrDefault<string>("ProfessionalCommittees");
                lblRemarks.Text = dr.GetValueOrDefault<string>("Comments");
                lblHoursPerWeek.Text = dr.GetValueOrDefault<decimal>("HoursPerWeek").ToString();
                lblPhoneExtension.Text = dr.GetValueOrDefault<string>("PhoneExtension");
                lblEmail.Text = string.Format("<a href='mailto:{0}'>{0}</a>", dr.GetValueOrDefault<string>("EmailAddress"));
            }

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
                    PopulateForm(SelectedEmpID);
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

        protected void btnUpdateGrid_Click(object sender, System.EventArgs e)
        {
            hoursGrid.UpdateData(SelectedEmpID, EmployeeID);
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

            hoursGrid.ReassignProjects(intEmployeeID, SelectedEmpID, EmployeeID, strFromDate, strToDate);

            RefreshPage();
        }


        protected void RefreshPage()
        {
            Response.Redirect("EmployeeDetail.aspx?EID=" + SelectedEmpID);
        }


        protected void btnUnAssignProjects_Click(object sender, System.EventArgs e)
        {
            if (hoursGrid.UnassignProjects(SelectedEmpID, EmployeeID))
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
            if (SelectedEmpID == 0)
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

            Engineer.AddProject(SelectedEmpID, intProjectID, intWeekID, EmployeeID);

            RefreshPage();
        }


        private void BindGrid()
        {
            PopulateDataset();
            this.wdtWeek.Value = WeekDate.WYFirst.MondayDate;
        }

        private void PopulateDataset()
        {
            //
            int intI = 0;
            int intID = 0;
            DataSet dsProjects = Project.GetScheduleByEmployeeID(SelectedEmpID);


            for (intI = 0; intI <= dsProjects.Tables["Schedule"].Rows.Count - 1; intI++)
            {
                intID += 1;
                dsProjects.Tables["Schedule"].Rows[intI]["ID"] = intID;
            }
            //
            DataView scheduleView = dsProjects.Tables["Schedule"].DefaultView;

            scheduleView.Sort = "week1 desc, ProjectName asc";

            hoursGrid.ProjectData = scheduleView.Table;
            hoursGrid.WeekDate = WeekDate;
            hoursGrid.Schedule = Schedule;
            hoursGrid.Engineer = Engineer;
            // SelectedEmployee
            hoursGrid.HoursPerWeek = decimal.Parse(lblHoursPerWeek.Text);
            hoursGrid.BindGrid();
        }

    }

}
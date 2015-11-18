using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KPFF.PMP.Entities;

namespace KPFF.PMP.UserControls
{
    public partial class EngineerDetail_v2 : System.Web.UI.UserControl
    {
        public DataTable ProjectData { get; set; }

        public Engineer Employee { get; set; }
        public WeekDate WeekDate { get; set; }
        public Engineer Engineer { get; set; }
        public clsSchedule Schedule { get; set; }
        public List<Engineer> AllEngineers { get; set; }
        public List<ProjectData> ProjectsByName { get; set; }
        public List<ProjectData> ProjectsByNumber { get; set; }

        public void BindData()
        {
            if (Employee == null)
            {
                throw new Exception("Employee is missing.");
            }

            ProjectData = GetEngineerData();

            empName.Text = Employee.EmployeeName;

            var empIdString = Employee.EmployeeID.ToString();

            ProjectsAdd.Attributes.Add("EngId", empIdString);
            EmployeeReAssign.Attributes.Add("EngId", empIdString);

            btnAssignProject.Attributes.Add("EngId", empIdString);
            btnReAssignProject.Attributes.Add("EngId", empIdString);

            cboEmployee.DataSource = AllEngineers;
            cboEmployee.DataTextField = "EmployeeName";
            cboEmployee.DataValueField = "EmployeeID";
            cboEmployee.DataBind();

            this.cboProjectsByName.DataSource = ProjectsByName;
            this.cboProjectsByName.DataTextField = "ProjectDescription";
            this.cboProjectsByName.DataValueField = "ID";
            this.cboProjectsByName.DataBind();

            this.cboProjectsByNumber.DataSource = ProjectsByNumber;
            this.cboProjectsByNumber.DataTextField = "ProjectDescription";
            this.cboProjectsByNumber.DataValueField = "ID";
            this.cboProjectsByNumber.DataBind();

            //closeEngImg.CommandArgument = _employeeId.ToString();
            engItem.Attributes.Add("EngId", empIdString);
            closeEngImg.Attributes.Add("EngId", empIdString);

            hoursGrid.ProjectData = ProjectData;
            hoursGrid.WeekDate = WeekDate;
            hoursGrid.Schedule = Schedule;
            hoursGrid.Engineer = Engineer;
            hoursGrid.EmpId = Employee.EmployeeID;
            // SelectedEmployee
            hoursGrid.HoursPerWeek = Employee.HoursPerWeek;
            hoursGrid.BindGrid();
        }

        private DataTable GetEngineerData()
        {
            int intI = 0;
            int intID = 0;
            DataSet dsProjects = new Project(WeekDate).GetScheduleByEmployeeID(Employee.EmployeeID); // Project.GetScheduleByEmployeeID(EmployeeId);


            for (intI = 0; intI <= dsProjects.Tables["Schedule"].Rows.Count - 1; intI++)
            {
                intID += 1;
                dsProjects.Tables["Schedule"].Rows[intI]["ID"] = intID;
            }

            DataView scheduleView = dsProjects.Tables["Schedule"].DefaultView;

            scheduleView.Sort = "week1 desc, ProjectName asc";

            return scheduleView.Table;
        }

        protected void btnUpdateGrid_Click(object sender, System.EventArgs e)
        {
            if (Employee.EmployeeID > 0)
            {
                hoursGrid.UpdateData(Employee.EmployeeID, 0);
                //RefreshPage();
            }
        }

        protected void btnReAssignEmployee_Click(object sender, System.EventArgs e)
        {
            int intEmployeeID = Convert.ToInt32(this.cboEmployee.SelectedValue);
            string strFromDate = weekFrom.Text; // this.wdtFrom.Value.ToString();
            string strToDate = weekTo.Text; // this.wdtTo.Value.ToString();

            if (this.chkAll.Checked)
            {
                strFromDate = "";
                strToDate = "";
            }

            hoursGrid.ReassignProjects(intEmployeeID, Employee.EmployeeID, Employee.EmployeeID, strFromDate, strToDate);

            //RefreshPage();
            BindData();
        }


        protected void RefreshPage()
        {
            //Response.Redirect("EmployeeDetail.aspx?EID=" + SelectedEmpID);
        }


        protected void btnUnAssignProjects_Click(object sender, System.EventArgs e)
        {
            if (hoursGrid.UnassignProjects(Employee.EmployeeID, Employee.EmployeeID))
            {
                //RefreshPage();
                BindData();
            }
            else
            {
                //TODO: need to figure this out
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "UnassignErrorVar", "displayUnassignError = true;", true);
                errorLbl.Text = "Unable to unassign a project with future hours assigned. Delete or reassign future hours before unassigning.";
            }
        }


        protected void btnAddProject_Click(object sender, System.EventArgs e)
        {
            if (Employee.EmployeeID <= 0)
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
            
            
            var intWeekID = WeekDate.WeekIDs[0];

            Engineer.AddProject(Employee.EmployeeID, intProjectID, intWeekID, Employee.EmployeeID);

            //RefreshPage();
            BindData();
        }


        protected void btnNext_Click(object sender, System.EventArgs e)
        {
            //// Move forward 4 Weeks
            //WyFirst = Schedule.GetWeekYearFirst(WyFirst, true);
            //WyLast = Schedule.GetWeekYearLast(WyFirst);

            //BindGrid();
        }


        protected void btnPrevious_Click(object sender, System.EventArgs e)
        {
            //// Move back 4 weeks
            //WyFirst = Schedule.GetWeekYearFirst(WyFirst, false);
            //WyLast = Schedule.GetWeekYearLast(WyFirst);

            //BindGrid();
        }

        protected void wdtWeek_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
        {
            //WyFirst = Schedule.GetWeekYear(DateTime.Parse(this.wdtWeek.Value.ToString()));
            //WyLast = Schedule.GetWeekYearLast(WyFirst);

            //BindGrid();
        }

        protected void btnThisWeek_Click(object sender, System.EventArgs e)
        {
            //WyFirst = Schedule.GetWeekYear(System.DateTime.Now);
            //WyLast = Schedule.GetWeekYearLast(WyFirst);

            //BindGrid();
        }
    }
}
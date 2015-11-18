
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
using KPFF.PMP.Entities;
using System;
using System.Configuration;

namespace KPFF.PMP.MyAdmin
{
    partial class ProjectDetail : PageBase
    {


        private int _selectedProjectID;
        private int SelectedProjectID
        {
            get
            {
                _selectedProjectID = Request.Params["PID"].GetValueOrDefault<int>();
                return _selectedProjectID;
            }
            set { _selectedProjectID = value; }
        }

        #region " Custom Routines "

        private void PopulateEmployees()
        {
            SqlConnection con = new SqlConnection(Configuration.ConnectionString);

            con.Open();
            SqlDataReader dr = Engineer.GetAllEngineers(con);

            this.cboEmployee.DataSource = dr;
            this.cboEmployee.DataTextField = "EmployeeName";
            this.cboEmployee.DataValueField = "EmployeeID";
            this.cboEmployee.DataBind();

            dr = Engineer.GetAllEngineers(con);

            this.cboEmployeeName.DataSource = dr;
            this.cboEmployeeName.DataTextField = "EmployeeName";
            this.cboEmployeeName.DataValueField = "EmployeeID";
            this.cboEmployeeName.DataBind();

            con.Close();
            dr.Close();
            con = null;
            dr = null;
        }

        private void PopulateForm()
        {
            string strSQL = "";
            clsGeneral General = new clsGeneral();

            strSQL = "SELECT tblProjects.*, tblPhase.Phase, tblClients.ClientName, ";
            strSQL += "(select MAX(LastModifiedDate) from tblSchedule s where s.ProjectID = tblProjects.ID) as LastModified,";
            strSQL += "(select distinct EmployeeName from tblEmployees e inner join tblSchedule s on s.LastModifiedByUserID = e.EmployeeID where s.ProjectID = tblProjects.ID and s.LastModifiedDate = (select MAX(LastModifiedDate) from tblSchedule ss where ss.ProjectID = tblProjects.ID)) as LastModifiedBy ";
            strSQL += "FROM tblPhase RIGHT OUTER JOIN ";
            strSQL += "tblProjects ON tblPhase.ID = tblProjects.PhaseID LEFT OUTER JOIN ";
            strSQL += "tblClients ON tblProjects.ClientID = tblClients.ID ";
            strSQL += "WHERE (tblProjects.ID = " + SelectedProjectID + ")";

            DataSet ds = General.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                lblProjectName.Text = dt.Rows[0]["ProjectName"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["ProjectName"], "");
                lblProjectNo.Text = dt.Rows[0]["ProjectNo"].GetValueOrDefault<string>(); //basToolbox.Nz(dt.Rows[0]["ProjectNo"], "");
                lblPIC.Text = dt.Rows[0]["PICCode"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["PICCode"], "");
                lblPM.Text = dt.Rows[0]["PM1Code"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["PM1Code"], "");
                lblClientName.Text = dt.Rows[0]["ClientName"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["ClientName"], "");
                lblProjectLocation.Text = dt.Rows[0]["ProjectLocation"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["ProjectLocation"], "");
                lblConstructionType.Text = dt.Rows[0]["ConstructionType"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["ConstructionType"], "");
                lblProjectType.Text = dt.Rows[0]["ProjectType"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["ProjectType"], "");
                lblPhase.Text = dt.Rows[0]["Phase"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["Phase"], "");

                var estStartOfConstruction = dt.Rows[0]["EstimatedStartDate"].GetValueOrDefault<DateTime>();
                if (estStartOfConstruction != null && estStartOfConstruction != DateTime.MinValue)
                {
                    lblEstStartOfConstruction.Text = estStartOfConstruction.ToShortDateString(); // basToolbox.Nz(dt.Rows[0]["EstimatedStartDate"], "");
                }

                var estCompletionOfConstruction = dt.Rows[0]["EstimatedCompletionDate"].GetValueOrDefault<DateTime>();
                if (estCompletionOfConstruction != null && estCompletionOfConstruction != DateTime.MinValue)
                {
                    lblEstCompletionOfConstruction.Text = estCompletionOfConstruction.ToShortDateString(); // basToolbox.Nz(dt.Rows[0]["EstimatedCompletionDate"], "");
                }

                lblFeeAmount.Text = dt.Rows[0]["FeeAmount"].GetValueOrDefault<decimal>().ToString("C"); // Strings.FormatNumber(basToolbox.Nz(dt.Rows[0]["FeeAmount"], 0), 2);
                lblFeeStructure.Text = dt.Rows[0]["FeeStructure"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["FeeStructure"], "");
                lblRemarks.Text = dt.Rows[0]["Comments"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["Comments"], "");

                var lastModifiedDate = dt.Rows[0]["LastModified"].GetValueOrDefault<DateTime>();
                if (lastModifiedDate != null && lastModifiedDate != DateTime.MinValue)
                {
                    lblLastUpdated.Text = string.Format("{0} by {1}", lastModifiedDate.ToShortDateString(), dt.Rows[0]["LastModifiedBy"].GetValueOrDefault<string>()); //  basToolbox.Nz(dt.Rows[0]["LastModified"], ""), basToolbox.Nz(dt.Rows[0]["LastModifiedBy"], ""));
                }
            }
        }

        #endregion

        #region " Schedule Routines"
        public string strConn = ConfigurationManager.AppSettings["ConnectionString"];
        public int[] intWeekIDs = new int[20];
        public int[] intWeeks = new int[20];
        public int[] intYears = new int[20];
        public string[] strMondayDates = new string[20];
        public string[] strWeekLabel = new string[20];
        public bool blnUpdated = false;
        protected System.Data.SqlClient.SqlConnection conn;
        protected SqlDataAdapter daSchedule;
        protected DataSet dsProjects;
        protected DataTable dtProjects;
        protected DataTable dtSchedule;
        protected SqlDataSource sqlds;
        public clsSchedule schedule = new clsSchedule();

        public string strSort = "";
        private void PopulateDataset()
        {
            int intI = 0;
            int intID = 0;

            DataSet dsProjects = Project.GetProjectAssignments(SelectedProjectID);


            // Auto increment ID field
            for (intI = 0; intI <= dsProjects.Tables["Schedule"].Rows.Count - 1; intI++)
            {
                intID += 1;
                dsProjects.Tables["Schedule"].Rows[intI]["ID"] = intID;
            }

            DataView scheduleView = dsProjects.Tables["Schedule"].DefaultView;

            scheduleView.Sort = "order asc, week1 desc";


            hoursGrid.EmployeeData = scheduleView.Table;
            hoursGrid.WeekDate = WeekDate;
            hoursGrid.Schedule = schedule;
            hoursGrid.Engineer = Engineer;
            hoursGrid.BindGrid();
        }

        private void BindGrid()
        {
            PopulateDataset();
            this.wdtWeek.Value = WeekDate.WYFirst.MondayDate;
        }
        #endregion

        protected void Page_Init(System.Object sender, System.EventArgs e)
        {
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!(SelectedProjectID == 0))
                {
                    PopulateForm();
                    PopulateEmployees();
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
            WyFirst = schedule.GetWeekYearFirst(WyFirst, true);
            WyLast = schedule.GetWeekYearLast(WyFirst);

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
            hoursGrid.UpdateData(SelectedProjectID, EmployeeID);
            RefreshPage();
        }


        protected void btnAddEmployee_Click(object sender, System.EventArgs e)
        {
            int intEmployeeID = Convert.ToInt32(cboEmployeeName.SelectedValue);
            var intWeekID = WeekDate.WeekIDs[0];

            Engineer.AddProject(intEmployeeID, SelectedProjectID, intWeekID, Session["EmployeeID"].GetValueOrDefault<int>());

            RefreshPage();
        }

        protected void RefreshPage()
        {
            Response.Redirect("ProjectDetail.aspx?PID=" + SelectedProjectID);
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

            hoursGrid.ReassignProjects(intEmployeeID, SelectedProjectID, EmployeeID, strFromDate, strToDate);

            RefreshPage();
        }


        protected void btnUnAssignEmployees_Click(object sender, System.EventArgs e)
        {
            if (hoursGrid.UnassignProjects(SelectedProjectID, EmployeeID))
            {
                RefreshPage();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "UnassignErrorVar", "displayEmployeeUnassignError = true;", true);
            }
        }
    }
}

using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
using Microsoft.Win32;
using KPFF.PMP;
using System.Web.UI.WebControls;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAdmin
{
    partial class AllEngineers : PageBase
    {

        #region "Web Form Designer generated code"
        //This call is required by the Web Form Designer.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
        }

        private void Page_Init(System.Object sender, System.EventArgs e)
        {
            InitializeComponent();
        }

        #endregion

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
            DataSet dsEngineers = new DataSet();

            if (Request.QueryString.HasKeys())
            {
                string fromDate = Request.QueryString["from"];
                string toDate = Request.QueryString["to"];
                string availHours = Request.QueryString["hours"];

                this.FilterStart.Value = fromDate;
                this.FilterEnd.Value = toDate;
                this.FilterAvailHours.Text = availHours;

                dsEngineers = Engineer.GetSchedulesAllEngineers(fromDate, toDate, availHours);

                Label lblFilterInfo = new Label();
                PlaceHolder headHolder = (PlaceHolder)hoursGrid.FindControl("headHolder");

                lblFilterInfo.Text = string.Format("FILTERED VIEW: {0}H AVAILABLE FROM {1} TO {2}. SHOWING {3} STAFF AVAILABLE.", availHours, fromDate, toDate, dsEngineers.Tables["Engineers"].Rows.Count.ToString());
                lblFilterInfo.CssClass = "filterInfo";

                headHolder.Controls.Add(lblFilterInfo);
            }
            else
            {
                dsEngineers = Engineer.GetSchedulesAllEngineers("", "");
            }
            
            DataView engineersView = dsEngineers.Tables["Engineers"].DefaultView;
            engineersView.Sort = "EmployeeName asc";

            hoursGrid.EngineerData = engineersView.Table;
            hoursGrid.WeekDate = WeekDate;
            hoursGrid.Schedule = Schedule;
            hoursGrid.Engineer = Engineer;
            hoursGrid.BindGrid();
        }

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
            var date = DateTime.Parse(this.wdtWeek.Value.ToString());
            WyFirst = Schedule.GetWeekYear(date);
            WyLast = Schedule.GetWeekYearLast(WyFirst);

            BindGrid();
        }

        protected void btnThisWeek_Click(object sender, System.EventArgs e)
        {
            WyFirst = Schedule.GetWeekYear(System.DateTime.Now);
            WyLast = Schedule.GetWeekYearLast(WyFirst);

            BindGrid();
        }

        protected void btnFilterEmployees_Click(object sender, System.EventArgs e)
        {
            string fromDate = this.FilterStart.Value.ToString();
            string toDate = this.FilterEnd.Value.ToString();
            string availHoursStr = this.FilterAvailHours.Text;
            decimal availHours = 0;

            if ((decimal.TryParse(availHoursStr, out availHours)))
            {
                Response.Redirect(string.Format("AllEngineers.aspx?from={0}&to={1}&hours={2}", fromDate, toDate, availHoursStr));
            }
            else
            {
                Response.Redirect("AllEngineers.aspx");
            }
        }

        protected void btnClearEmployeeFilters_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("AllEngineers.aspx");
        }
    }
}
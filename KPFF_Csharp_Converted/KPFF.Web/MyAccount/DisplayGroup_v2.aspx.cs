using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KPFF.PMP.Entities;
using System.Data;
using KPFF.PMP.UserControls;

namespace KPFF.PMP.MyAccount
{
    public partial class DisplayGroup_v2 : System.Web.UI.Page
    {
        #region Properties
        private List<Engineer> _allEngineers;
        private List<ProjectData> _projectsByName;
        private List<ProjectData> _projectsByNumber;

        private clsSchedule _schedule;
        public clsSchedule Schedule
        {
            get
            {
                if (_schedule == null)
                {
                    _schedule = new clsSchedule();
                }
                return _schedule;
            }
            set { _schedule = value; }
        }


        private Project _project;
        protected Project Project
        {
            get
            {
                if (_project == null)
                {
                    _project = new Project(WeekDate);
                }
                return _project;
            }
            set { _project = value; }
        }


        private Engineer _engineer;
        protected Engineer Engineer
        {
            get
            {
                if (_engineer == null)
                {
                    _engineer = new Engineer(Schedule, WeekDate, EmployeeID);
                }
                return _engineer;
            }
            set { _engineer = value; }
        }


        private WeekDate _weekDate;
        protected WeekDate WeekDate
        {
            get
            {
                if (_weekDate == null)
                {
                    _weekDate = new WeekDate(WyFirst, WyLast);
                }
                else if (_weekDate != null && (!object.ReferenceEquals(_weekDate.WYFirst, WyFirst) | !object.ReferenceEquals(_weekDate.WYLast, WyLast)))
                {
                    _weekDate = new WeekDate(WyFirst, WyLast);
                }

                return _weekDate;
            }
            set { _weekDate = value; }
        }

        private WeekYear _wyFirst;
        protected WeekYear WyFirst
        {
            get
            {
                if (ViewState["weekyearfirst"] == null)
                {
                    if (Session["wyFirst"] == null)
                    {
                        _wyFirst = Schedule.GetWeekYear(System.DateTime.Now);
                    }
                    else
                    {
                        _wyFirst = Session["wyFirst"] as WeekYear;
                    }
                    
                    ViewState["weekyearfirst"] = _wyFirst;
                }
                else
                {
                    _wyFirst = ViewState["weekyearfirst"] as WeekYear;
                }

                return _wyFirst;
            }
            set
            {
                _wyFirst = value;
                ViewState["weekyearfirst"] = _wyFirst;
            }
        }

        private WeekYear _wyLast;
        protected WeekYear WyLast
        {
            get
            {
                if (ViewState["weekyearlast"] == null)
                {
                    _wyLast = Schedule.GetWeekYearLast(WyFirst);
                    ViewState["weekyearlast"] = _wyLast;
                }
                else
                {
                    _wyLast = ViewState["weekyearlast"] as WeekYear;
                }

                return _wyLast;
            }
            set
            {
                _wyLast = value;
                ViewState["weekyearlast"] = _wyLast;
            }
        }

        private int _empID;
        protected int EmployeeID
        {
            get
            {
                _empID = Session["EmployeeId"].GetValueOrDefault<int>(); // Nz(Session["EmployeeID"], 0);

                return _empID;
            }
            set
            {
                _empID = value;
                Session["EmployeeID"] = _empID;
            }
        }

        private List<int> _removedEngineeers;
        protected List<int> RemovedEngineers
        {
            get
            {
                if (_removedEngineeers == null)
                {
                    _removedEngineeers = new List<int>();
                    if (!string.IsNullOrEmpty(hdnRemovedEngs.Value))
                    {
                        var vals = hdnRemovedEngs.Value.Split('|');

                        foreach (var val in vals)
                        {
                            int id = 0;

                            if (int.TryParse(val, out id))
                            {
                                _removedEngineeers.Add(id);
                            }
                        }
                    }
                }
                return _removedEngineeers;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            _allEngineers = Engineer.GetAllEngineers();
            GetProjectListData();

            var engIds = new List<int>();

            if (!string.IsNullOrEmpty(hdnEngOrder.Value.Trim()))
            {
                var sortOrder = hdnEngOrder.Value.Split('|');

                foreach (var so in sortOrder)
                {
                    int id = 0;

                    if (int.TryParse(so, out id))
                    {
                        if (!RemovedEngineers.Contains(id))
                        {
                            engIds.Add(id);
                        }
                    }
                }
            }

            phEngineers.Controls.Clear();
                        
            if (!string.IsNullOrEmpty(Request.Params["GID"]))
            {
                var gidstr = Request.Params["GID"];
                int gid = 0;

                if (int.TryParse(gidstr, out gid))
                {
                    var group = EngineerGroup.GetById(gid);

                    foreach (var eng in group.Members.Where(m => m.IsActive && !RemovedEngineers.Contains(m.EmployeeId)))
                    {
                        if (!engIds.Contains(eng.EmployeeId))
                        {
                            engIds.Add(eng.EmployeeId);
                        }
                    }
                }

            }

            if (!string.IsNullOrEmpty(Request.Params["EID"]))
            {
                var eids = Request.Params["EID"].Split('|');

                foreach (var eid in eids)
                {
                    if (!string.IsNullOrEmpty(eid))
                    {
                        int empid = 0;

                        if (int.TryParse(eid, out empid))
                        {
                            if (empid > 0 && !RemovedEngineers.Contains(empid))
                            {
                                if (!engIds.Contains(empid))
                                {
                                    engIds.Add(empid);
                                }
                            }
                        }
                    }
                }
            }

            //using (var scope = new System.Transactions.TransactionScope())
            //{
                foreach (var id in engIds)
                {
                    AddEngineerDetail(id);
                }
                //scope.Complete();
            //}

            if (!Page.IsPostBack)
            {
                //this.wdtWeek.Value = WeekDate.WYFirst.MondayDate;
                SetWeekCalDate(WeekDate.WYFirst.MondayDate);
            }
        }

        private void GetProjectListData()
        {
            var projects = KPFF.PMP.Project.GetActiveProjectsList();
            _projectsByName = projects.OrderBy(p => p.ProjectName).ToList(); // from p in projects orderby p.ProjectName select p;
            _projectsByNumber = projects.OrderBy(p => p.ProjectNumber).ToList(); // from p in projects orderby p.ProjectNumber select p;
        }

        private void AddEngineerDetail(int empId)
        {
            var engineer = Engineer.GetByEmployeeId(empId);

            EngineerDetail_v2 detail = (EngineerDetail_v2)LoadControl("~/UserControls/EngineerDetail_v2.ascx");
            detail.ID = string.Format("engDetail_{0}", engineer.EmployeeID);
            detail.AllEngineers = _allEngineers;
            detail.ProjectsByName = _projectsByName;
            detail.ProjectsByNumber = _projectsByNumber;
            detail.WeekDate = this.WeekDate;
            detail.Engineer = this.Engineer;
            detail.Schedule = this.Schedule;
            detail.Employee = engineer;
            //detail.ProjectData = GetEngineerData(engineer.EmployeeID);

            detail.BindData();

            phEngineers.Controls.Add(detail);
        }

        protected void detail_EngineerRemoved(object sender, EventArgs e)
        {
        }

        //private DataTable GetEngineerData(int empId)
        //{
        //    int intI = 0;
        //    int intID = 0;
        //    DataSet dsProjects = Project.GetScheduleByEmployeeID(empId);


        //    for (intI = 0; intI <= dsProjects.Tables["Schedule"].Rows.Count - 1; intI++)
        //    {
        //        intID += 1;
        //        dsProjects.Tables["Schedule"].Rows[intI]["ID"] = intID;
        //    }

        //    DataView scheduleView = dsProjects.Tables["Schedule"].DefaultView;

        //    scheduleView.Sort = "week1 desc, ProjectName asc";

        //    return scheduleView.Table;
        //}

        protected void btnNext_Click(object sender, System.EventArgs e)
        {
            // Move forward 4 Weeks
            WyFirst = Schedule.GetWeekYearFirst(WyFirst, true);
 
            UpdateData();
        }


        protected void btnPrevious_Click(object sender, System.EventArgs e)
        {
            //// Move back 4 weeks
            WyFirst = Schedule.GetWeekYearFirst(WyFirst, false);

            UpdateData();
        }

        //protected void wdtWeek_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
        //{
        //    WyFirst = Schedule.GetWeekYear(DateTime.Parse(this.wdtWeek.Value.ToString()));

        //    UpdateData();
        //}

        protected void btnThisWeek_Click(object sender, System.EventArgs e)
        {
            WyFirst = Schedule.GetWeekYear(System.DateTime.Now);

            UpdateData();
        }

        private void UpdateData()
        {
            WyLast = Schedule.GetWeekYearLast(WyFirst);
            //this.wdtWeek.Value = WeekDate.WYFirst.MondayDate;

            SetWeekCalDate(WeekDate.WYFirst.MondayDate);
            BindData();
        }

        private void SetWeekCalDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                DateTime mondayDate = new DateTime();
                if (DateTime.TryParse(date, out mondayDate))
                {
                    weekCal.Text = mondayDate.ToShortDateString();
                }
            }
        }

        protected void weekCalBtn_Click(object sender, EventArgs e)
        {
            var date = new DateTime();

            if (DateTime.TryParse(weekCal.Text, out date))
            {
                WyFirst = Schedule.GetWeekYear(date);

                UpdateData();
            }
        }
    }
}
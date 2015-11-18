
using KPFF.PMP.Entities;

namespace KPFF.PMP.Entities
{
    public class PageBase : System.Web.UI.Page
    {
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
                if (Session["wyFirst"] == null)
                {
                    _wyFirst = Schedule.GetWeekYear(System.DateTime.Now);
                    Session["wyFirst"] = _wyFirst;
                }
                else
                {
                    _wyFirst = Session["wyFirst"] as WeekYear;
                }

                return _wyFirst;
            }
            set
            {
                _wyFirst = value;
                Session["wyFirst"] = _wyFirst;
            }
        }

        private WeekYear _wyLast;
        protected WeekYear WyLast
        {
            get
            {
                if (Session["wyLast"] == null)
                {
                    _wyLast = Schedule.GetWeekYearLast(WyFirst);
                    Session["wyLast"] = _wyLast;
                }
                else
                {
                    _wyLast = Session["wyLast"] as WeekYear;
                }

                return _wyLast;
            }
            set
            {
                _wyLast = value;
                Session["wyLast"] = _wyLast;
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


    }
}


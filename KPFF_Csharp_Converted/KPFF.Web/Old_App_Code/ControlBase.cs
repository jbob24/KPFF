
using Microsoft.VisualBasic;
using KPFF.PMP.Entities;

namespace KPFF.PMP
{
    public class ControlBase : System.Web.UI.UserControl
    {


        private WeekDate _weekDate;
        public WeekDate WeekDate
        {
            get
            {
                if (_weekDate == null)
                {
                    _weekDate = new WeekDate(WyFirst, WyLast);
                }
                //else if (_weekDate != null && (!object.ReferenceEquals(_weekDate.WYFirst, WyFirst) | !object.ReferenceEquals(_weekDate.WYLast, WyLast)))
                //{
                    //_weekDate = new WeekDate(WyFirst, WyLast);
                //}

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
                        ViewState["weekyearfirst"] = _wyFirst;
                    }
                    else
                    {
                        _wyFirst = Session["wyFirst"].GetValueOrDefault<WeekYear>();
                    }
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
                    if (Session["wyLast"] == null)
                    {
                        _wyLast = Schedule.GetWeekYearLast(WyFirst);
                        ViewState["weekyearlast"] = _wyLast;
                    }
                    else
                    {
                        _wyLast = Session["wyLast"].GetValueOrDefault<WeekYear>();
                    }
                }

                return _wyLast;
            }
            set
            {
                _wyLast = value;
                ViewState["weekyearlast"] = _wyLast;
            }
        }

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

        private Engineer _engineer;
        public Engineer Engineer
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

        private int _empID;
        protected int EmployeeID
        {
            get
            {
                _empID = Session["EmployeeID"].GetValueOrDefault<int>();

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
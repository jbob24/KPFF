using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KPFF.Entities;
using KPFF.Web.Model;

namespace KPFF.Web
{
    public class PageBase : System.Web.UI.Page
    {
        //private ClsSchedule _schedule;
        //public ClsSchedule Schedule
        //{
        //    get
        //    {
        //        if ((_schedule == null))
        //        {
        //            _schedule = new ClsSchedule();
        //        }
        //        return _schedule;
        //    }
        //    set
        //    {
        //        _schedule = value;
        //    }
        //}
        
        //private Project _project;

        //protected Project Project
        //{
        //    get
        //    {
        //        if ((_project == null))
        //        {
        //            _project = new Project(WeekDate);
        //        }
        //        return _project;
        //    }
        //    set
        //    {
        //        _project = value;
        //    }
        //}

        //private Engineer _engineer;

        //protected Engineer Engineer
        //{
        //    get
        //    {
        //        if ((_engineer == null))
        //        {
        //            _engineer = new Engineer(Schedule, WeekDate, EmployeeID);
        //        }
        //        return _engineer;
        //    }
        //    set
        //    {
        //        _engineer = value;
        //    }
        //}

        //private WeekDate _weekDate;

        //protected WeekDate WeekDate
        //{
        //    get
        //    {
        //        if ((_weekDate == null))
        //        {
        //            _weekDate = new WeekDate(WyFirst, WyLast);
        //        }
        //        else if (_weekDate.WYFirst != WyFirst || _weekDate.WYLast != WyLast)
        //        {
        //            _weekDate = new WeekDate(WyFirst, WyLast);
        //        }
        //        return _weekDate;
        //    }
        //    set
        //    {
        //        _weekDate = value;
        //    }
        //}

        //private WeekYear _wyFirst;

        //protected WeekYear WyFirst
        //{
        //    get {
        //        if (Session["wyFirst"] == null) 
        //        {
        //            _wyFirst = Schedule.GetWeekYear(DateTime.Now);
        //             Session["wyFirst"] = _wyFirst;
        //        }
        //        else 
        //        {
        //            _wyFirst = Session["wyFirst"] as WeekYear;
        //        }
        //        return _wyFirst;
        //    }
        //    set
        //    {
        //        _wyFirst = value;
        //        Session["wyFirst"] = _wyFirst;
        //    }
        //}

        //private WeekYear _wyLast;

        //protected WeekYear WyLast
        //{
        //    get
        //    {
        //        if ((Session["wyLast"] == null))
        //        {
        //            _wyLast = Schedule.GetWeekYearLast(WyFirst);
        //            Session["wyLast"] = _wyLast;
        //        }
        //        else
        //        {
        //            _wyLast = Session["wyLast"] as WeekYear;
        //        }
        //        return _wyLast;
        //    }
        //    set
        //    {
        //        _wyLast = value;
        //        Session["wyLast"] = _wyLast;
        //    }
        //}

        //private int _empID;

        //protected int EmployeeID
        //{
        //    get
        //    {
        //        if (Session["User"] != null)
        //        {
        //            var user = Session["User"] as User;
        //            _empID = user.EmployeeId;
        //        }
        //        return _empID;
        //    }
        //    set
        //    {
        //        _empID = value;
        //        Session["EmployeeID"]= _empID;
        //    }
        //}


        public UserSession UserSession { get; set; }

        private List<Week> _weeks;
        public List<Week> Weeks
        {
            get
            {
                if (UserSession != null && UserSession.FirstWeek != null && UserSession.FirstWeek.MondayDate != DateTime.MinValue)
                {
                    return KPFF.Business.Week.GetWeeks(KPFF.Business.KPFFCache.Current)
                        .Where(w => w.MondayDate >= UserSession.FirstWeek.MondayDate && w.MondayDate <= UserSession.LastWeek.MondayDate).OrderBy(w => w.MondayDate).ToList();
                }

                return null;
            }
        }

        public Week CurrentWeek
        {
            get
            {
                return KPFF.Business.Week.GetWeeks(KPFF.Business.KPFFCache.Current)
                    .SingleOrDefault(w => w.MondayDate <= DateTime.Now && w.MondayDate >= DateTime.Now);
            }
        }
    }
}
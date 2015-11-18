using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KPFF.PMP.Web.Entities;

namespace KPFF.PMP.MyAccount
{
    public partial class DisplayGroup_v3 : System.Web.UI.Page
    {
        #region ******************************* Public Properties *******************************
        //private clsSchedule _schedule;
        //public clsSchedule Schedule
        //{
        //    get
        //    {
        //        if (_schedule == null)
        //        {
        //            _schedule = new clsSchedule();
        //        }
        //        return _schedule;
        //    }
        //    set { _schedule = value; }
        //}

        //private WeekDate _weekDate;
        //protected WeekDate WeekDate
        //{
        //    get
        //    {
        //        if (_weekDate == null)
        //        {
        //            _weekDate = new WeekDate(WyFirst, WyLast);
        //        }
        //        else if (_weekDate != null && (!object.ReferenceEquals(_weekDate.WYFirst, WyFirst) | !object.ReferenceEquals(_weekDate.WYLast, WyLast)))
        //        {
        //            _weekDate = new WeekDate(WyFirst, WyLast);
        //        }

        //        return _weekDate;
        //    }
        //    set { _weekDate = value; }
        //}

        //private WeekYear _wyFirst;
        //protected WeekYear WyFirst
        //{
        //    get
        //    {
        //        if (ViewState["weekyearfirst"] == null)
        //        {
        //            if (Session["wyFirst"] == null)
        //            {
        //                _wyFirst = Schedule.GetWeekYear(System.DateTime.Now);
        //            }
        //            else
        //            {
        //                _wyFirst = Session["wyFirst"] as WeekYear;
        //            }

        //            ViewState["weekyearfirst"] = _wyFirst;
        //        }
        //        else
        //        {
        //            _wyFirst = ViewState["weekyearfirst"] as WeekYear;
        //        }

        //        return _wyFirst;
        //    }
        //    set
        //    {
        //        _wyFirst = value;
        //        ViewState["weekyearfirst"] = _wyFirst;
        //    }
        //}

        //private WeekYear _wyLast;
        //protected WeekYear WyLast
        //{
        //    get
        //    {
        //        if (ViewState["weekyearlast"] == null)
        //        {
        //            _wyLast = Schedule.GetWeekYearLast(WyFirst);
        //            ViewState["weekyearlast"] = _wyLast;
        //        }
        //        else
        //        {
        //            _wyLast = ViewState["weekyearlast"] as WeekYear;
        //        }

        //        return _wyLast;
        //    }
        //    set
        //    {
        //        _wyLast = value;
        //        ViewState["weekyearlast"] = _wyLast;
        //    }
        //}
        #endregion

        #region ******************************* Public Events *******************************
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadPage();
            }   
        }
        #endregion

        #region ******************************* Public Methods *******************************

        #endregion

        #region ******************************* Private Methods *******************************
        private void LoadPage()
        {
            //SetWeekCalDate(WeekDate.WYFirst.MondayDate);
            weekCal.Text = DateTime.Now.GetMondayDate().ToShortDateString();
        }

        //private void SetWeekCalDate(string date)
        //{
        //    if (!string.IsNullOrEmpty(date))
        //    {
        //        DateTime mondayDate = new DateTime();
        //        if (DateTime.TryParse(date, out mondayDate))
        //        {
        //            weekCal.Text = mondayDate.ToShortDateString();
        //        }
        //    }
        //}
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using KPFF.Web.Model;

namespace KPFF.Web.MyAccount
{
    public partial class _TemplateMaster : System.Web.UI.MasterPage
    {
        //private User _user;
        //public User User 
        //{
        //    get
        //    {
        //        if (_user == null)
        //        {
        //            if (Session["User"] != null)
        //            {
        //                _user = Session["User"] as User;
        //            }
        //        }

        //        return _user;
        //    }
        //    set { _user = value; }

        //}

        public UserSession UserSession { get; set; }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (UserSession.Current == null || UserSession.Current.EmployeeId == 0)
            {
                Response.Redirect("../index.htm");
            }
            ValidateEmployeeAccess();
        }

        private void ValidateEmployeeAccess()
        {
            bool displayFiscalSummary = UserSession.Current.Employee.HasPMFiscalSummaryAddress || UserSession.Current.Employee.HasPICFiscalSummaryAddress; // User.HasPMFiscalSummaryAccess || User.HasPICFiscalSummaryAccess;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "displayFiscalSummary", string.Format("displayFiscalSummary = \'{0}\'", displayFiscalSummary), true);
        }
    }
}
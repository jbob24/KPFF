using System;

namespace KPFF.PMP.MyAdmin
{
    partial class TemplateMaster : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Convert.ToInt32(Session["EmployeeID"]) == 0)
            {
                Response.Redirect("../index.htm");
            }
        }

    }
}
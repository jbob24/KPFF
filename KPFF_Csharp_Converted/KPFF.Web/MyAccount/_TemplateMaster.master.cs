
using System.Data.SqlClient;
using System;
using System.Web.UI;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAccount
{
    partial class TemplateMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Convert.ToInt32(Session["EmployeeID"]) == 0)
            {
                Response.Redirect("../index.htm");
            }

            ValidateEmployeeAccess();
        }


        private void ValidateEmployeeAccess()
        {
            Int32 employeeId = Convert.ToInt32(Session["EmployeeID"]);
            SqlConnection con = new SqlConnection(Configuration.ConnectionString);
            var Engineer = new Engineer();
            bool pmFiscalSummary = false;
            bool picFiscalSummary = false;

            con.Open();
            SqlDataReader dr = Engineer.GetByEmployeeID(employeeId, con);

            if (dr.HasRows)
            {
                dr.Read();
                pmFiscalSummary = dr.GetValueOrDefault<bool>("PMFiscalSummary");
                picFiscalSummary = dr.GetValueOrDefault<bool>("PICFiscalSummary");
            }

            con.Close();
            dr.Close();
            con = null;
            dr = null;

            bool displayFiscalSummary = pmFiscalSummary | picFiscalSummary;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "displayFiscalSummary", string.Format("displayFiscalSummary = '{0}'", displayFiscalSummary), true);
        }
    }

}


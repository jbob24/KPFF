
using System.Data.SqlClient;
using System;
using System.Web.UI;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAccount
{
    partial class BQReportMaster : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Session["EmployeeID"].GetValueOrDefault<int>() == 0)
            {
                Response.Redirect("../index.htm");
            }

            ValidateEmployeeAccess();
        }


        private void ValidateEmployeeAccess()
        {
            var employeeId = Session["EmployeeID"].GetValueOrDefault<int>();
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
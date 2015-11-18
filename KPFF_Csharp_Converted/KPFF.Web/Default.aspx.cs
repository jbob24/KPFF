using System.Data;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP
{
    partial class Default : System.Web.UI.Page
    {

        #region " Custom Routines "

        private bool ValidateForm()
        {

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                lblError.Text = "Please enter your username.";
                lblError.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblError.Text = "Please enter your password.";
                lblError.Visible = true;
                return false;
            }

            lblError.Text = "";
            lblError.Visible = false;
            return true;
        }

        #endregion

        protected void btnLogin_Click(object sender, System.EventArgs e)
        {
            string strSQL = "";
            clsGeneral General = new clsGeneral();

            if (!ValidateForm())
                return;

            //Validate Account Exists
            strSQL = "SELECT tblUsers.UserLevel, tblUsers.EmployeeID, tblEmployees.EmployeeFirst, tblEmployees.EmployeeLast ";
            strSQL += "FROM tblEmployees INNER JOIN ";
            strSQL += "tblUsers ON tblEmployees.EmployeeID = tblUsers.EmployeeID ";
            strSQL += "WHERE (tblUsers.UserName = '" + txtUserName.Text + "') ";
            strSQL += "AND (tblUsers.Password = '" + txtPassword.Text + "') ";

            DataSet ds = General.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                Session["UserLevel"] = dt.Rows[0]["UserLevel"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["UserLevel"], "");
                Session["EmployeeID"] = dt.Rows[0]["EmployeeID"].GetValueOrDefault<int>();
                Session["UserName"] = dt.Rows[0]["EmployeeFirst"].GetValueOrDefault<string>() + " " + dt.Rows[0]["EmployeeLast"].GetValueOrDefault<string>();
            }
            else
            {
                lblError.Text = "Invalid login.";
                lblError.Visible = true;
            }

            ds.Dispose();
            ds = null;
            dt.Dispose();
            dt = null;

            if (cbRememberMe.Checked)
            {
                Response.Cookies["KPFFAuthorization"]["Login"] = txtUserName.Text;
                Response.Cookies["KPFFAuthorization"]["Password"] = txtPassword.Text;
                Response.Cookies["KPFFAuthorization"].Expires = DateTime.Now.AddDays(10);
            }
            else
            {
                Response.Cookies["KPFFAuthorization"].Expires = DateTime.Now.AddDays(-1);
            }

            if (Session["UserLevel"].GetValueOrDefault<string>() == "Admin")
            {
                Response.Redirect("MyAdmin/Clients.aspx");
            }
            else if (Session["UserLevel"].GetValueOrDefault<string>() == "User")
            {
                Response.Redirect("MyAccount/MyProjects.aspx");
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Page.IsPostBack)
                return;
            // Reset Session Variables
            Session["ScreenH"] = Request.Params["Height"];
            Session["ScreenW"] = Request.Params["Width"];
            Session["UserLevel"] = null;
            Session["EmployeeID"] = null;
            Session["UserName"] = null;
            Session["wyFirst"] = null;
            Session["wyLast"] = null;
            Session["sFilter"] = null;
            Session["ActiveProj"] = null;

            if (((Request.Cookies["KPFFAuthorization"] != null)))
            {
                txtUserName.Text = Request.Cookies["KPFFAuthorization"]["Login"];
                txtPassword.Text = Request.Cookies["KPFFAuthorization"]["Password"];
                txtPassword.Attributes["value"] = Request.Cookies["KPFFAuthorization"]["Password"];
                cbRememberMe.Checked = true;
            }

        }

    }
}
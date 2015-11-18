using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPFF.Web
{
    public partial class Default : System.Web.UI.Page
    {
        private bool ValidateForm()
        {
            if ((txtUserName.Text == ""))
            {
                lblError.Text = "Please enter your username.";
                lblError.Visible = true;
                return false;
            }
            if ((txtPassword.Text == ""))
            {
                lblError.Text = "Please enter your password.";
                lblError.Visible = true;
                return false;
            }
            lblError.Text = "";
            lblError.Visible = false;
            return true;
        }

        protected void btnLogin_Click(object sender, System.EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            var user = KPFF.Data.Entities.User.GetByUsernamePassword(txtUserName.Text, txtPassword.Text);

            if (user != null)
            {
                Session["User"] = user;

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
                if (user.UserLevel == "Admin")
                {
                    Response.Redirect("MyAdmin/Clients.aspx");
                }
                else if (user.UserLevel == "User")
                {
                    //Response.Redirect("MyAccount/MyProjects.aspx");
                    Response.Redirect("MyAccount/AllEngineers.aspx");
                }
            }
            else
            {
                lblError.Text = "Invalid login.";
                lblError.Visible = true;
            }
        }


        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }
            //  Reset Session Variables
            Session["ScreenH"] = KPFF.Data.Utilities.Nz(Request["Height"], "");
            Session["ScreenW"] = KPFF.Data.Utilities.Nz(Request["Width"], "");
            //Session["UserLevel"] = null;
            //Session["EmployeeID"] = null;
            //Session["UserName"] = null;
            Session["User"] = null;
            Session["wyFirst"] = null;
            Session["wyLast"] = null;
            Session["sFilter"] = null;
            Session["ActiveProj"] = null;
            if (!(Request.Cookies["KPFFAuthorization"] == null))
            {
                txtUserName.Text = Request.Cookies["KPFFAuthorization"]["Login"];
                txtPassword.Text = Request.Cookies["KPFFAuthorization"]["Password"];
                txtPassword.Attributes["value"] = Request.Cookies["KPFFAuthorization"]["Password"];
                cbRememberMe.Checked = true;
            }
        }
    }
}
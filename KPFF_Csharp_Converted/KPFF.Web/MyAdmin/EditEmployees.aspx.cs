
using System.Data;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAdmin
{
    partial class EditEmployees : System.Web.UI.Page
    {

        #region " Custom Routines "

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                lblError.Text = "Please enter Employee Name.";
                lblError.Visible = true;
                return false;
            }

            lblError.Text = "";
            lblError.Visible = false;
            return true;
        }

        private void PopulateState()
        {
            string[] strStates = {
			"",
			"Alabama",
			"Alaska",
			"Arizona",
			"Arkansas",
			"California",
			"Colorado",
			"Connecticut",
			"D.C.",
			"Delaware",
			"Florida",
			"Georgia",
			"Hawaii",
			"Idaho",
			"Illinois",
			"Indiana",
			"Iowa",
			"Kansas",
			"Kentucky",
			"Louisiana",
			"Maine",
			"Maryland",
			"Massachusetts",
			"Michigan",
			"Minnesota",
			"Mississippi",
			"Missouri",
			"Montana",
			"Nebraska",
			"Nevada",
			"New Hampshire",
			"New Jersey",
			"New Mexico",
			"New York",
			"North Carolina",
			"North Dakota",
			"Ohio",
			"Oklahoma",
			"Oregon",
			"Pennsylvania",
			"Rhode Island",
			"South Carolina",
			"South Dakota",
			"Tennessee",
			"Texas",
			"Utah",
			"Vermont",
			"Virginia",
			"Washington",
			"West Virginia",
			"Wisconsin",
			"Wyoming"
		};

            cboState.DataSource = strStates;
            cboState.DataBind();
        }
        private void PopulateEmployeeType()
        {
            int intUserID = Convert.ToInt32(Request.Params["UserID"]);
            string strSQL = "";
            clsGeneral General = new clsGeneral();

            strSQL = "SELECT * ";
            strSQL += "FROM tblEmployeeTypes ";

            DataSet ds = General.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);

            cboEmployeeType.DataTextField = "EmployeeType";
            cboEmployeeType.DataValueField = "id";
            cboEmployeeType.DataSource = dv;
            cboEmployeeType.DataBind();

        }

        private void PopulateForm()
        {
            int intEmployeeId = Convert.ToInt32(Request.Params["EmployeeId"]);
            string strSQL = "";
            clsGeneral General = new clsGeneral();

            intEmployeeId = 1;

            strSQL = "SELECT * ";
            strSQL += "FROM tblEmployees ";
            strSQL += "WHERE EmployeeID = " + intEmployeeId;

            DataSet ds = General.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                txtEmployeeName.Text = dt.Rows[0]["EmployeeName"].GetValueOrDefault<string>();
                cboEmployeeType.Items.FindByText(dt.Rows[0]["EmployeeType"].GetValueOrDefault<string>()).Selected = true;
                txtAddress.Text = dt.Rows[0]["Address"].GetValueOrDefault<string>();
                txtCity.Text = dt.Rows[0]["City"].GetValueOrDefault<string>();
                if (!string.IsNullOrEmpty(dt.Rows[0]["State"].GetValueOrDefault<string>()))
                {
                    cboState.Items.FindByText(dt.Rows[0]["State"].GetValueOrDefault<string>()).Selected = true;
                }
                txtZip.Text = dt.Rows[0]["Zip"].GetValueOrDefault<string>();
                txtHomePhone.Text = dt.Rows[0]["HomePhone"].GetValueOrDefault<string>();
                txtCellPhone.Text = dt.Rows[0]["CellPhone"].GetValueOrDefault<string>();
                txtComments.Text = dt.Rows[0]["Comments"].GetValueOrDefault<string>();
                //rbtnList.Text = Zz(dt.Rows[0]["EmployeeName"), "")
            }
            else
            {
                lblError.Text = "Employee does not exist.";
                lblError.Visible = true;
            }

            dt.Dispose();
            ds.Dispose();
            dt = null;
            ds = null;
        }


        public void UpdateEmployee()
        {
            string strSQL = "";
            clsGeneral General = new clsGeneral();
            int intEmployeeID = Convert.ToInt32(Request.Params["EmployeeId"]);

            strSQL = "UPDATE tblEmployees ";
            strSQL += "SET UserName = '" + txtEmployeeName.Text + "' ";
            strSQL += "WHERE intEmployeeID = " + intEmployeeID;

            General.UpdateRecord(strSQL);

            lblError.Text = "Employee Updated!";
            lblError.Visible = true;

        }

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            PopulateState();
            PopulateEmployeeType();
            PopulateForm();
        }


        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            //Validate for user input
            if (!ValidateForm())
                return;

            UpdateEmployee();
        }
    }
}
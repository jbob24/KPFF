using System;
using System.Data;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAdmin
{
    partial class EmployeeEdit : System.Web.UI.Page
    {

        public string strSQL = "";
        #region " Custom Routines "

        private bool ValidateForm()
        {
            System.DateTime dtmDateTimeUS = default(System.DateTime);
            System.Globalization.CultureInfo format = new System.Globalization.CultureInfo("en-US", true);

            if (string.IsNullOrEmpty(txtEmployeeCode.Text))
            {
                lblError.Text = "Please enter employee code.";
                lblError.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                lblError.Text = "Please enter employee first name.";
                lblError.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                lblError.Text = "Please enter employee last name.";
                lblError.Visible = true;
                return false;
            }

            if (!string.IsNullOrEmpty(txtEmployeeStartDate.Text))
            {
                try
                {
                    dtmDateTimeUS = System.DateTime.Parse(txtEmployeeStartDate.Text, format);
                }
                catch (Exception ex)
                {
                    lblError.Text = "Please enter a valid employment start date - ex: mm/dd/yyyy.";
                    lblError.Visible = true;
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtEmployeeEndDate.Text))
            {
                try
                {
                    dtmDateTimeUS = System.DateTime.Parse(txtEmployeeEndDate.Text, format);
                }
                catch (Exception ex)
                {
                    lblError.Text = "Please enter a valid employment end date - ex: mm/dd/yyyy.";
                    lblError.Visible = true;
                    return false;
                }
            }

            if (rbtnUserYes.Checked == true)
            {
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    lblError.Text = "Please enter user name.";
                    lblError.Visible = true;
                    return false;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    lblError.Text = "Please enter password.";
                    lblError.Visible = true;
                    return false;
                }
            }

            lblError.Text = "";
            lblError.Visible = false;
            return true;
        }

        private void PopulateEmployeeTypes()
        {
            clsGeneral General = new clsGeneral();

            strSQL = "SELECT * FROM tblEmployeeTypes ";

            DataSet ds = General.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];
            DataView dv = dt.DefaultView;
            dv.Sort = "ID";

            cboEmployeeType.DataSource = dv;
            cboEmployeeType.DataTextField = "EmployeeType";
            cboEmployeeType.DataValueField = "ID";
            cboEmployeeType.DataBind();

            dt.Dispose();
            dt = null;
            ds.Dispose();
            ds = null;
        }

        private void PopulateEmployee(System.Object intEmployeeID)
	{
		clsGeneral General = new clsGeneral();

		strSQL = "SELECT * FROM tblEmployees ";
		strSQL += "WHERE EmployeeID = " + intEmployeeID;

		DataSet ds = General.FillDataset(strSQL);
		DataTable dt = ds.Tables[0];

		if (dt.Rows.Count > 0) {
			txtEmployeeCode.Text = dt.Rows[0]["EmployeeCode"].GetValueOrDefault<string>();
			txtFirstName.Text = dt.Rows[0]["EmployeeFirst"].GetValueOrDefault<string>();
			txtLastName.Text = dt.Rows[0]["EmployeeLast"].GetValueOrDefault<string>();
			cboEmployeeType.SelectedValue = dt.Rows[0]["EmployeeTypeID"].GetValueOrDefault<int>().ToString();
			txtAddress.Text = dt.Rows[0]["Address"].GetValueOrDefault<string>();
			txtCity.Text = dt.Rows[0]["City"].GetValueOrDefault<string>();
			txtState.Text = dt.Rows[0]["State"].GetValueOrDefault<string>();
			txtZip.Text = dt.Rows[0]["Zip"].GetValueOrDefault<string>();
			txtHomePhone.Text = dt.Rows[0]["HomePhone"].GetValueOrDefault<string>();
			txtCellPhone.Text = dt.Rows[0]["CellPhone"].GetValueOrDefault<string>();
			txtPhoneExtension.Text = dt.Rows[0]["PhoneExtension"].GetValueOrDefault<string>();
			txtEmail.Text = dt.Rows[0]["EmailAddress"].GetValueOrDefault<string>();
			txtTitle.Text = dt.Rows[0]["Title"].GetValueOrDefault<string>();
			txtEmployeeStartDate.Text = dt.Rows[0]["EmploymentStartDate"].GetValueOrDefault<string>();
			txtEmployeeEndDate.Text = dt.Rows[0]["EmploymentEndDate"].GetValueOrDefault<string>();
			txtYearsOfExperience.Text = dt.Rows[0]["YearsOfExperience"].GetValueOrDefault<string>();
			txtEducation.Text = dt.Rows[0]["Education"].GetValueOrDefault<string>();
			txtLicenses.Text = dt.Rows[0]["Licenses"].GetValueOrDefault<string>();
			txtProfMemberships.Text = dt.Rows[0]["ProfessionalMemberships"].GetValueOrDefault<string>();
			txtProfCommittees.Text = dt.Rows[0]["ProfessionalCommittees"].GetValueOrDefault<string>();
			txtHoursPerWeek.Text = dt.Rows[0]["HoursPerWeek"].GetValueOrDefault<decimal>().ToString();
			txtComments.Text = dt.Rows[0]["Comments"].GetValueOrDefault<string>();
			if (dt.Rows[0]["Active"].GetValueOrDefault<int>() == 0) {
				rbtnActiveEmployeeNo.Checked = true;
				rbtnActiveEmployeeYes.Checked = false;
			} else {
				rbtnActiveEmployeeYes.Checked = true;
				rbtnActiveEmployeeNo.Checked = false;
			}
			if (dt.Rows[0]["PMFiscalSummary"].GetValueOrDefault<bool>() == true) {
				pmFiscalSummary.Checked = true;
			}
            if (dt.Rows[0]["PICFiscalSummary"].GetValueOrDefault<bool>() == true)
            {
				picFiscalSummary.Checked = true;
			}
		}

		dt.Dispose();
		dt = null;
		ds.Dispose();
		ds = null;
	}

        private void PopulateUser(int intEmployeeID)
        {
            clsGeneral General = new clsGeneral();

            strSQL = "SELECT UserName, Password ";
            strSQL += "FROM tblUsers ";
            strSQL += "WHERE EmployeeID = " + intEmployeeID;

            DataSet ds = General.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                rbtnUserNo.Checked = false;
                rbtnUserYes.Checked = true;
                pnlUserAccount.Visible = true;
                txtUserName.Text = dt.Rows[0]["Username"].GetValueOrDefault<string>();
                txtPassword.Text = dt.Rows[0]["Password"].GetValueOrDefault<string>();
            }
            else
            {
                rbtnUserNo.Checked = true;
                rbtnUserYes.Checked = false;
                pnlUserAccount.Visible = false;
            }

        }

        private void UpdateEmployee(int intEmployeeID)
        {
            clsGeneral General = new clsGeneral();
            int intActive = 0;
            int pmFiscalSummaryInt = 0;
            int picFiscalSummaryInt = 0;


            if (rbtnActiveEmployeeYes.Checked == true)
            {
                intActive = 1;
            }
            else
            {
                intActive = 0;
            }

            if (pmFiscalSummary.Checked)
            {
                pmFiscalSummaryInt = 1;
            }

            if (picFiscalSummary.Checked)
            {
                picFiscalSummaryInt = 1;
            }


            strSQL = "UPDATE tblEmployees ";
            strSQL += "SET ";
            strSQL += "EmployeeCode = '" + txtEmployeeCode.Text.GetValueOrDefault<string>() + "', ";
            strSQL += "EmployeeFirst = '" + txtFirstName.Text.GetValueOrDefault<string>() + "', ";
            strSQL += "EmployeeLast = '" + txtLastName.Text.GetValueOrDefault<string>() + "', ";
            strSQL += "EmployeeName = '" + txtLastName.Text.GetValueOrDefault<string>() + ", " + txtFirstName.Text + "', ";
            strSQL += "EmployeeTypeID = " + cboEmployeeType.SelectedValue + ", ";
            strSQL += "Address = '" + txtAddress.Text + "', ";
            strSQL += "City = '" + txtCity.Text + "', ";
            strSQL += "State = '" + txtState.Text + "', ";
            strSQL += "Zip = '" + txtZip.Text + "', ";
            strSQL += "HomePhone = '" + txtHomePhone.Text + "', ";
            strSQL += "CellPhone = '" + txtCellPhone.Text + "', ";
            strSQL += "PhoneExtension = '" + txtPhoneExtension.Text + "', ";
            strSQL += "EmailAddress = '" + txtEmail.Text + "', ";
            strSQL += "Title = '" + txtTitle.Text + "', ";
            if (string.IsNullOrEmpty(txtEmployeeStartDate.Text))
            {
                strSQL += "EmploymentStartDate = NULL, ";
            }
            else
            {
                strSQL += "EmploymentStartDate = '" + txtEmployeeStartDate.Text + "', ";
            }
            if (string.IsNullOrEmpty(txtEmployeeEndDate.Text))
            {
                strSQL += "EmploymentEndDate = NULL, ";
            }
            else
            {
                strSQL += "EmploymentEndDate = '" + txtEmployeeEndDate.Text + "', ";
            }
            strSQL += "YearsOfExperience = '" + txtYearsOfExperience.Text + "', ";
            strSQL += "Education = '" + txtEducation.Text + "', ";
            strSQL += "Licenses = '" + txtLicenses.Text + "', ";
            strSQL += "ProfessionalMemberships = '" + txtProfMemberships.Text + "', ";
            strSQL += "ProfessionalCommittees = '" + txtProfCommittees.Text + "', ";
            strSQL += "HoursPerWeek = " + txtHoursPerWeek.Text + ", ";
            strSQL += "Comments = '" + txtComments.Text + "', ";
            strSQL += "Active = " + intActive + ", ";
            strSQL += "PMFiscalSummary = " + pmFiscalSummaryInt + ", ";
            strSQL += "PICFiscalSummary = " + picFiscalSummaryInt + " ";
            strSQL += "WHERE EmployeeID = " + intEmployeeID;

            General.UpdateRecord(strSQL);
        }

        private void AddUpdateUser(int intEmployeeID)
        {
            clsGeneral General = new clsGeneral();
            bool blnUserExists = false;

            //Check to see if User Account Exists
            strSQL = "SELECT ID ";
            strSQL += "FROM tblUsers ";
            strSQL += "WHERE EmployeeID = " + intEmployeeID;

            DataSet ds = General.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                blnUserExists = true;
            }
            else
            {
                blnUserExists = false;
            }

            ds.Dispose();
            ds = null;
            dt.Dispose();
            dt = null;


            if (blnUserExists == true)
            {
                strSQL = "UPDATE tblUsers ";
                strSQL += "SET ";
                strSQL += "UserName = '" + txtUserName.Text + "', ";
                strSQL += "Password = '" + txtPassword.Text + "' ";
                strSQL += "WHERE EmployeeID = " + intEmployeeID;
                General.UpdateRecord(strSQL);
            }
            else
            {
                strSQL = "INSERT INTO tblUsers ";
                strSQL += "(UserName, Password, UserLevel, EmployeeID) ";
                strSQL += "VALUES ( ";
                strSQL += "'" + txtUserName.Text + "', ";
                strSQL += "'" + txtPassword.Text + "', ";
                strSQL += "'User', ";
                strSQL += intEmployeeID + ")";
                General.AddRecord(strSQL);
            }

        }

        private void DeleteUser(int intEmployeeID)
        {
            clsGeneral General = new clsGeneral();

            strSQL = "DELETE FROM tblUsers ";
            strSQL += "WHERE EmployeeID = " + intEmployeeID;

            General.DeleteRecord(strSQL);
        }

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            int intEmployeeID = Request.Params["EID"].GetValueOrDefault<int>();

            if (!(intEmployeeID == 0))
            {
                PopulateEmployeeTypes();
                PopulateEmployee(intEmployeeID);
                PopulateUser(intEmployeeID);
            }

        }

        protected void rbtnUserNo_CheckedChanged(object sender, System.EventArgs e)
        {
            pnlUserAccount.Visible = false;
        }

        protected void rbtnUserYes_CheckedChanged(object sender, System.EventArgs e)
        {
            pnlUserAccount.Visible = true;
        }

        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            int intEmployeeID = Request.Params["EID"].GetValueOrDefault<int>();

            if (!(intEmployeeID == 0))
            {
                if (!ValidateForm())
                    return;
                UpdateEmployee(intEmployeeID);
                if (rbtnUserYes.Checked == true)
                {
                    AddUpdateUser(intEmployeeID);
                }
                else
                {
                    DeleteUser(intEmployeeID);
                }
            }

            Response.Write("<script language='javascript'>opener.childClose(); window.close();</script>");
            Response.Flush();
        }

    }
}
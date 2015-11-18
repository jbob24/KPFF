using System.Data;
using System.Data.SqlClient;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAdmin
{
    partial class EmployeeAdd : System.Web.UI.Page
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

        private void AddEmployee()
        {
            clsGeneral General = new clsGeneral();
            SqlConnection conn = default(SqlConnection);
            SqlCommand cmd = default(SqlCommand);
            string strConn = General.strConn;
            int intEmployeeID = 0;
            string strFirst = this.txtFirstName.Text;
            string strLast = this.txtLastName.Text;
            string strName = null;
            int intActive = 0;
            if (this.rbtnActiveEmployeeYes.Checked)
            {
                intActive = 1;
            }
            strName = strLast + ", " + strFirst;
            //
            strSQL = "INSERT INTO tblEmployees ";
            strSQL += "(EmployeeCode, EmployeeFirst, EmployeeLast, EmployeeName, ";
            strSQL += "EmployeeTypeID, Address, City, State, Zip, ";
            strSQL += "HomePhone, CellPhone, Title, EmploymentStartDate, EmploymentEndDate, YearsOfExperience, ";
            strSQL += "Education, Licenses, ProfessionalMemberships, ProfessionalCommittees, HoursPerWeek, ";
            strSQL += "Comments, Active) ";
            strSQL += "VALUES (";
            strSQL += "'" + this.txtEmployeeCode.Text + "', ";
            strSQL += "'" + strFirst + "', ";
            strSQL += "'" + strLast + "', ";
            strSQL += "'" + strName + "', ";
            strSQL += this.cboEmployeeType.SelectedValue + ", ";
            strSQL += "'" + this.txtAddress.Text + "', ";
            strSQL += "'" + this.txtCity.Text + "', ";
            strSQL += "'" + this.txtState.Text + "', ";
            strSQL += "'" + this.txtZip.Text + "', ";
            strSQL += "'" + this.txtHomePhone.Text + "', ";
            strSQL += "'" + this.txtCellPhone.Text + "', ";
            strSQL += "'" + this.txtTitle.Text + "', ";
            //
            if (string.IsNullOrEmpty(this.txtEmployeeStartDate.Text))
            {
                strSQL += "NULL, ";
            }
            else
            {
                strSQL += "'" + this.txtEmployeeStartDate.Text + "', ";
            }
            if (string.IsNullOrEmpty(this.txtEmployeeEndDate.Text))
            {
                strSQL += "NULL, ";
            }
            else
            {
                strSQL += "'" + this.txtEmployeeEndDate.Text + "', ";
            }
            //
            strSQL += "'" + this.txtYearsOfExperience.Text + "', ";
            strSQL += "'" + this.txtEducation.Text + "', ";
            strSQL += "'" + this.txtLicenses.Text + "', ";
            strSQL += "'" + this.txtProfMemberships.Text + "', ";
            strSQL += "'" + this.txtProfCommittees.Text + "', ";
            strSQL += this.txtHoursPerWeek.Text + ", ";
            strSQL += "'" + this.txtComments.Text + "', ";
            strSQL += intActive + ") ";
            strSQL += "SELECT @EmployeeID = @@identity";
            conn = new SqlConnection(strConn);
            cmd = new SqlCommand(strSQL, conn);
            SqlParameter prmEmpID = new SqlParameter("@EmployeeID", SqlDbType.Int);
            prmEmpID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(prmEmpID);
            //
            conn.Open();
            cmd.ExecuteNonQuery();
            //
            intEmployeeID = prmEmpID.Value.GetValueOrDefault<int>();
            //
            conn.Close();
            conn = null;
            cmd = null;
            //
            if (this.rbtnUserYes.Checked == true)
            {
                // Add User
                strSQL = "INSERT INTO tblUsers ";
                strSQL += "(UserName, Password, UserLevel, EmployeeID) ";
                strSQL += "VALUES (";
                strSQL += "'" + this.txtUserName.Text + "', ";
                strSQL += "'" + this.txtPassword.Text + "', ";
                strSQL += "'User', ";
                strSQL += intEmployeeID + ")";
                General.AddRecord(strSQL);
            }
            //
        }
        #endregion

        protected void rbtnUserNo_CheckedChanged(object sender, System.EventArgs e)
        {
            pnlUserAccount.Visible = false;
        }

        protected void rbtnUserYes_CheckedChanged(object sender, System.EventArgs e)
        {
            pnlUserAccount.Visible = true;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Page.IsPostBack)
                return;
            PopulateEmployeeTypes();
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            if (!ValidateForm())
                return;
            AddEmployee();

            Response.Write("<script language='javascript'>opener.childClose(); window.close();</script>");
            Response.Flush();
        }
    }
}
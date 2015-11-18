using System.Data;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAccount
{
    public partial class ProjectDetailEdit : System.Web.UI.Page
    {


        private int _empID;
        protected int EmployeeID
        {
            get
            {
                _empID = Session["EmployeeID"].GetValueOrDefault<int>();

                return _empID;
            }
            set
            {
                _empID = value;
                Session["EmployeeID"] = _empID;
            }
        }

        #region " Custom Routines "

        private void PopulateCombos()
        {
            string strSQL = "";
            clsGeneral General = new clsGeneral();
            DataRow dtRow = default(DataRow);

            //Populate Client Combo
            strSQL = "SELECT ID, ClientName ";
            strSQL += "FROM tblClients";

            DataSet ds = General.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];
            DataView dv = dt.DefaultView;
            dv.Sort = "ClientName";

            cboClientName.DataSource = dv;
            cboClientName.DataTextField = "ClientName";
            cboClientName.DataValueField = "ID";
            cboClientName.DataBind();

            //Populate Phase Combo
            strSQL = "SELECT ID, Phase ";
            strSQL += "FROM tblPhase ";
            strSQL += "ORDER BY Sequence";

            ds = new DataSet();
            ds = General.FillDataset(strSQL);
            dt = new DataTable();
            dt = ds.Tables[0];
            dv = new DataView();
            dv = dt.DefaultView;

            cboPhase.DataSource = dv;
            cboPhase.DataTextField = "Phase";
            cboPhase.DataValueField = "ID";
            cboPhase.DataBind();

            //Populate Fee Structure Combo
            strSQL = "SELECT FeeStructureType ";
            strSQL += "FROM tblFeeStructures ";
            strSQL += "ORDER BY Sequence";

            ds = new DataSet();
            ds = General.FillDataset(strSQL);
            dt = new DataTable();
            dt = ds.Tables[0];
            dv = new DataView();
            dv = dt.DefaultView;

            cboFeeStructure.DataSource = dv;
            cboFeeStructure.DataTextField = "FeeStructureType";
            cboFeeStructure.DataValueField = "FeeStructureType";
            cboFeeStructure.DataBind();

            //Populate PIC Combo
            strSQL = "SELECT EmployeeID, EmployeeName ";
            strSQL += "FROM tblEmployees ";
            strSQL += "ORDER BY EmployeeName";

            ds = new DataSet();
            ds = General.FillDataset(strSQL);
            dt = new DataTable();
            dt = ds.Tables[0];
            dtRow = dt.NewRow();
            dtRow[0] = 0;
            dtRow[1] = "";
            dt.Rows.Add(dtRow);
            dv = new DataView();
            dv = dt.DefaultView;
            dv.Sort = "EmployeeName";

            cboPIC.DataSource = dv;
            cboPIC.DataTextField = "EmployeeName";
            cboPIC.DataValueField = "EmployeeID";
            cboPIC.DataBind();

            //Populate PM Combo
            strSQL = "SELECT EmployeeID, EmployeeName ";
            strSQL += "FROM tblEmployees ";
            strSQL += "ORDER BY EmployeeName";

            ds = new DataSet();
            ds = General.FillDataset(strSQL);
            dt = new DataTable();
            dt = ds.Tables[0];
            dtRow = dt.NewRow();
            dtRow[0] = 0;
            dtRow[1] = "";
            dt.Rows.Add(dtRow);
            dv = new DataView();
            dv = dt.DefaultView;
            dv.Sort = "EmployeeName";

            cboPM.DataSource = dv;
            cboPM.DataTextField = "EmployeeName";
            cboPM.DataValueField = "EmployeeID";
            cboPM.DataBind();

            dt.Dispose();
            dt = null;
            ds.Dispose();
            ds = null;
        }

        private void PopulateForm(int intProjectID)
	{
		string strSQL = "";
		clsGeneral General = new clsGeneral();

		strSQL = "SELECT tblProjects.*, tblPhase.Phase, tblClients.ClientName ";
		strSQL += "FROM tblPhase RIGHT OUTER JOIN ";
		strSQL += "tblProjects ON tblPhase.ID = tblProjects.PhaseID LEFT OUTER JOIN ";
		strSQL += "tblClients ON tblProjects.ClientID = tblClients.ID ";
		strSQL += "WHERE (tblProjects.ID = " + intProjectID + ")";

		DataSet ds = General.FillDataset(strSQL);
		DataTable dt = ds.Tables[0];

		if (dt.Rows.Count > 0) {
			txtProjectName.Text = dt.Rows[0]["ProjectName"].GetValueOrDefault<string>();
            txtProjectNo.Text = dt.Rows[0]["ProjectNo"].GetValueOrDefault<string>();
            lblProjectNo.Text = dt.Rows[0]["ProjectNo"].GetValueOrDefault<string>();
			
            if (!string.IsNullOrEmpty(dt.Rows[0]["PM1"].GetValueOrDefault<string>()))
            {
                cboPM.SelectedValue = dt.Rows[0]["PM1"].GetValueOrDefault<string>();
			}

			if (!string.IsNullOrEmpty(dt.Rows[0]["PIC"].GetValueOrDefault<string>()))
            {
                cboPIC.SelectedValue = dt.Rows[0]["PIC"].GetValueOrDefault<string>();
			}
			
            cboClientName.SelectedValue = dt.Rows[0]["ClientID"].GetValueOrDefault<string>();
			txtProjectLocation.Text = dt.Rows[0]["ProjectLocation"].GetValueOrDefault<string>();
			txtConstructionType.Text = dt.Rows[0]["ConstructionType"].GetValueOrDefault<string>();
			txtProjectType.Text = dt.Rows[0]["ProjectType"].GetValueOrDefault<string>();
			cboPhase.SelectedValue = dt.Rows[0]["PhaseID"].GetValueOrDefault<string>();
			txtEstStartOfConstruction.Text = dt.Rows[0]["EstimatedStartDate"].GetValueOrDefault<string>();
			txtEstCompletionOfConstruction.Text = dt.Rows[0]["EstimatedCompletionDate"].GetValueOrDefault<string>();
			txtFeeAmount.Text = dt.Rows[0]["FeeAmount"].GetValueOrDefault<decimal>().ToString("c");
			if (string.IsNullOrEmpty(dt.Rows[0]["FeeStructure"].GetValueOrDefault<string>())) 
            {
			} else {
				cboFeeStructure.SelectedValue = dt.Rows[0]["FeeStructure"].GetValueOrDefault<string>();
			}
			txtRemarks.Text = dt.Rows[0]["Comments"].GetValueOrDefault<string>();
		}

	}

        private bool ValidateForm()
        {
            System.DateTime dtmDateTimeUS = default(System.DateTime);
            System.Globalization.CultureInfo format = new System.Globalization.CultureInfo("en-US", true);

            if (string.IsNullOrEmpty(txtProjectName.Text))
            {
                lblError.Text = "Please enter project name.";
                lblError.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtProjectNo.Text))
            {
                lblError.Text = "Please enter project number.";
                lblError.Visible = true;
                return false;
            }

            if (txtProjectNo.Text != lblProjectNo.Text)
            {
                //Check Dupe Job #
                string strSQL = "";
                clsGeneral General = new clsGeneral();
                strSQL = "SELECT ID FROM tblProjects ";
                strSQL += "WHERE ProjectNo = '" + txtProjectNo.Text + "'";
                DataSet ds = General.FillDataset(strSQL);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    lblError.Text = "Project number already exists.";
                    lblError.Visible = true;
                    return false;
                }
            }

            if (string.IsNullOrEmpty(cboClientName.SelectedItem.Text))
            {
                lblError.Text = "Please select a client.";
                lblError.Visible = true;
                return false;
            }

            if (!string.IsNullOrEmpty(txtEstStartOfConstruction.Text))
            {
                try
                {
                    dtmDateTimeUS = System.DateTime.Parse(txtEstStartOfConstruction.Text, format);
                }
                catch (Exception ex)
                {
                    lblError.Text = "Please enter a valid start of construction date - ex: mm/dd/yyyy.";
                    lblError.Visible = true;
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txtEstCompletionOfConstruction.Text))
            {
                try
                {
                    dtmDateTimeUS = System.DateTime.Parse(txtEstCompletionOfConstruction.Text, format);
                }
                catch (Exception ex)
                {
                    lblError.Text = "Please enter a valid completion of construction date - ex: mm/dd/yyyy.";
                    lblError.Visible = true;
                    return false;
                }
            }

            lblError.Text = "";
            lblError.Visible = false;
            return true;
        }

        private void UpdateProject(int intProjectID)
        {
            string strSQL = "";
            clsGeneral General = new clsGeneral();
            clsSchedule Schedule = new clsSchedule();
            if (intProjectID == 0)
                return;

            Project Project = new Project();

            System.Globalization.CultureInfo dateFormat = new System.Globalization.CultureInfo("en-US", true);
            DateTime estStartDate = new DateTime();
            DateTime estCompletionDate = new DateTime();
            decimal feeAmount = default(decimal);
            int pic = 0;
            string picCode = string.Empty;
            int pm = 0;
            string pmCode = string.Empty;

            if (!string.IsNullOrEmpty(txtEstStartOfConstruction.Text))
            {
                estStartDate = DateTime.Parse(txtEstStartOfConstruction.Text);
            }
            if (!string.IsNullOrEmpty(txtEstCompletionOfConstruction.Text))
            {
                estCompletionDate = DateTime.Parse(txtEstCompletionOfConstruction.Text);
            }
            if (!string.IsNullOrEmpty(txtFeeAmount.Text))
            {
                decimal.TryParse(txtFeeAmount.Text.Replace("$", "").Replace(",", ""), out feeAmount);
            }

            int.TryParse(cboPIC.SelectedValue, out pic);
            if (pic != 0)
            {
                picCode = Schedule.GetEmployeeCode(pic);
            }
            int.TryParse(cboPM.SelectedValue, out pm);
            if (pm != 0)
            {
                pmCode = Schedule.GetEmployeeCode(pm);
            }

            Project.UpdateProject(intProjectID, Convert.ToDecimal(txtProjectNo.Text), txtProjectName.Text, Convert.ToInt32(cboClientName.SelectedValue), txtProjectLocation.Text, txtConstructionType.Text, txtProjectType.Text, Convert.ToInt32(cboPhase.SelectedValue), estStartDate, estCompletionDate,
            feeAmount, cboFeeStructure.SelectedValue, txtRemarks.Text, pic, picCode, pm, pmCode, EmployeeID);
        }
        #endregion

        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            int intProjectID = Request.Params["PID"].GetValueOrDefault<int>();
            if (!(intProjectID == 0))
            {
                if (!ValidateForm())
                    return;
                UpdateProject(intProjectID);
            }
            //Response.Write("<script language='javascript'>reloadOpener();</script>")
            //Response.Flush()

            Page.ClientScript.RegisterStartupScript(this.GetType(), "reloadOpener", "reloadOpener();", true);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Page.IsPostBack)
                return;
            int intProjectID = Request.Params["PID"].GetValueOrDefault<int>();
            //
            if (!(intProjectID == 0))
            {
                PopulateCombos();
                PopulateForm(intProjectID);
            }
        }
    }
}
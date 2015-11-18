
using System.Data;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAdmin
{
    partial class ProjectDetailEdit : System.Web.UI.Page
    {

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

            if (dt.Rows.Count > 0)
            {
                txtProjectName.Text = dt.Rows[0]["ProjectName"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["ProjectName"], "");
                txtProjectNo.Text = dt.Rows[0]["ProjectNo"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["ProjectNo"], "");
                lblProjectNo.Text = dt.Rows[0]["ProjectNo"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["ProjectNo"], "");
                if (dt.Rows[0]["PM1"] != DBNull.Value) //object.ReferenceEquals(dt.Rows[0]["PM1"], DBNull.Value)))
                {
                    cboPM.SelectedValue = dt.Rows[0]["PM1"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["PM1"], 0);
                }
                if (dt.Rows[0]["PIC"] != DBNull.Value) //(!object.ReferenceEquals(dt.Rows[0]["PIC"], DBNull.Value)))
                {
                    cboPIC.SelectedValue = dt.Rows[0]["PIC"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["PIC"], 0);
                }
                cboClientName.SelectedValue = dt.Rows[0]["ClientID"].GetValueOrDefault<int>().ToString(); // basToolbox.Nz(dt.Rows[0]["ClientID"], 1);
                txtProjectLocation.Text = dt.Rows[0]["ProjectLocation"].GetValueOrDefault<string>();// basToolbox.Nz(dt.Rows[0]["ProjectLocation"], "");
                txtConstructionType.Text = dt.Rows[0]["ConstructionType"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["ConstructionType"], "");
                txtProjectType.Text = dt.Rows[0]["ProjectType"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["ProjectType"], "");
                cboPhase.SelectedValue = dt.Rows[0]["PhaseID"].GetValueOrDefault<int>().ToString(); // basToolbox.Nz(dt.Rows[0]["PhaseID"], 1);

                var estStartOfConstruction = dt.Rows[0]["EstimatedStartDate"].GetValueOrDefault<DateTime>();
                var estCompletionOfConstruction = dt.Rows[0]["EstimatedCompletionDate"].GetValueOrDefault<DateTime>();

                if (estStartOfConstruction != null && estStartOfConstruction != DateTime.MinValue)
                {
                    txtEstStartOfConstruction.Text = estStartOfConstruction.ToShortDateString(); // basToolbox.Nz(dt.Rows[0]["EstimatedStartDate"], "");
                }

                if (estCompletionOfConstruction != null && estCompletionOfConstruction != DateTime.MinValue)
                {
                    txtEstCompletionOfConstruction.Text = estCompletionOfConstruction.ToShortDateString(); // basToolbox.Nz(dt.Rows[0]["EstimatedCompletionDate"], "");
                }

                txtFeeAmount.Text = dt.Rows[0]["FeeAmount"].GetValueOrDefault<decimal>().ToString("C"); // Strings.FormatNumber(basToolbox.Nz(dt.Rows[0]["FeeAmount"], 0), 2);
                if (dt.Rows[0]["FeeStructure"] != DBNull.Value) //  object.ReferenceEquals(dt.Rows[0]["FeeStructure"], DBNull.Value))
                {
                    cboFeeStructure.SelectedValue = dt.Rows[0]["FeeStructure"].GetValueOrDefault<int>().ToString(); // basToolbox.Nz(dt.Rows[0]["FeeStructure"], 1);
                }
                txtRemarks.Text = dt.Rows[0]["Comments"].GetValueOrDefault<string>(); // basToolbox.Nz(dt.Rows[0]["Comments"], "");
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

            strSQL = "UPDATE tblProjects ";
            strSQL += "SET ProjectName = '" + txtProjectName.Text + "', ";
            strSQL += "ProjectNo = '" + txtProjectNo.Text + "', ";
            strSQL += "ClientID = " + Convert.ToInt32(cboClientName.SelectedValue) + ", ";
            strSQL += "ProjectLocation = '" + txtProjectLocation.Text + "', ";
            strSQL += "ConstructionType = '" + txtConstructionType.Text + "', ";
            strSQL += "ProjectType = '" + txtProjectType.Text + "', ";
            strSQL += "PhaseID = " + Convert.ToInt32(cboPhase.SelectedValue) + ", ";
            if (!string.IsNullOrEmpty(txtEstStartOfConstruction.Text))
            {
                strSQL += "EstimatedStartDate = '" + txtEstStartOfConstruction.Text + "', ";
            }
            else
            {
                strSQL += "EstimatedStartDate = NULL, ";
            }
            if (!string.IsNullOrEmpty(txtEstCompletionOfConstruction.Text))
            {
                strSQL += "EstimatedCompletionDate = '" + txtEstCompletionOfConstruction.Text + "', ";
            }
            else
            {
                strSQL += "EstimatedCompletionDate = NULL, ";
            }
            if (!string.IsNullOrEmpty(txtFeeAmount.Text))
            {
                decimal feeAmount = 0;
                decimal.TryParse(txtFeeAmount.Text.Replace("$", "").Replace(",", ""), out feeAmount);
                strSQL += "FeeAmount = " + feeAmount.ToString() + ", ";                
            }
            else
            {
                strSQL += "FeeAmount = NULL, ";
            }
            strSQL += "FeeStructure = '" + cboFeeStructure.SelectedValue + "', ";

            if (Convert.ToInt32(cboPIC.SelectedValue) != 0)
            {
                strSQL += "PIC = " + cboPIC.SelectedValue + ", ";
                strSQL += "PICCode = '" + Schedule.GetEmployeeCode(Convert.ToInt32(cboPIC.SelectedValue)) + "', ";
            }
            else
            {
                strSQL += "PIC = NULL, ";
                strSQL += "PICCode = NULL, ";
            }

            if (Convert.ToInt32(cboPM.SelectedValue) != 0)
            {
                strSQL += "PM1 = " + cboPM.SelectedValue + ", ";
                strSQL += "PM1Code = '" + Schedule.GetEmployeeCode(Convert.ToInt32(cboPM.SelectedValue)) + "', ";
            }
            else
            {
                strSQL += "PM1 = NULL, ";
                strSQL += "PM1Code = NULL, ";
            }

            strSQL += "Comments = '" + txtRemarks.Text + "' ";
            strSQL += "WHERE ID = " + intProjectID;

            General.FillDataset(strSQL);
        }
        #endregion

        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            int intProjectID = Convert.ToInt32(Request.Params["PID"]);
            if (!(intProjectID == 0))
            {
                if (!ValidateForm())
                    return;
                UpdateProject(intProjectID);
            }
            Response.Write("<script language='javascript'>window.close();</script>");
            Response.Flush();
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Page.IsPostBack)
                return;
            int intProjectID = Request.Params["PID"].GetValueOrDefault<int>();// Convert.ToInt32(basToolbox.Nz(Request.Params["PID", 0]);
            //
            if (!(intProjectID == 0))
            {
                PopulateCombos();
                PopulateForm(intProjectID);
            }
        }
    }
}
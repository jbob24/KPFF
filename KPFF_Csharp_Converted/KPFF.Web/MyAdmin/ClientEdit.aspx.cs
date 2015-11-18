using System.Data;
using System;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAdmin
{
    partial class ClientEdit : System.Web.UI.Page
    {

        #region " Custom Routines "

        private void PopulateCombos()
        {
            string strSQL = "";
            clsGeneral General = new clsGeneral();

            strSQL = "SELECT * FROM tblClientTypes ";

            DataSet ds = General.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];
            DataView dv = dt.DefaultView;
            dv.Sort = "ID";

            cboClientType.DataSource = dv;
            cboClientType.DataTextField = "ClientType";
            cboClientType.DataValueField = "ID";
            cboClientType.DataBind();

            dt.Dispose();
            dt = null;
            ds.Dispose();
            ds = null;
        }

        private void PopulateForm(int intClientID)
        {
            string strSQL = "";
            clsGeneral General = new clsGeneral();

            strSQL = "SELECT * FROM tblClients ";
            strSQL += "WHERE ID = " + intClientID;

            DataSet ds = General.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                txtClientName.Text = dt.Rows[0]["ClientName"].GetValueOrDefault<string>();
                cboClientType.SelectedValue = dt.Rows[0]["ClientTypeID"].GetValueOrDefault<int>().ToString();
                txtAddress.Text = dt.Rows[0]["Address"].GetValueOrDefault<string>();
                txtCity.Text = dt.Rows[0]["City"].GetValueOrDefault<string>();
                txtState.Text = dt.Rows[0]["State"].GetValueOrDefault<string>();
                txtZip.Text = dt.Rows[0]["Zip"].GetValueOrDefault<string>();
                txtOfficePhone.Text = dt.Rows[0]["OfficePhone"].GetValueOrDefault<string>();
                txtFax.Text = dt.Rows[0]["Fax"].GetValueOrDefault<string>();
                txtComments.Text = dt.Rows[0]["Comments"].GetValueOrDefault<string>();
            }

        }

        private bool ValidateForm()
        {

            if (string.IsNullOrEmpty(txtClientName.Text))
            {
                lblError.Text = "Please enter client name.";
                lblError.Visible = true;
                return false;
            }

            lblError.Text = "";
            lblError.Visible = false;
            return true;
        }

        private void UpdateClient(int intClientID)
        {
            clsGeneral General = new clsGeneral();
            string strSQL = "";

            strSQL = "UPDATE tblClients ";
            strSQL += "SET ClientName = '" + txtClientName.Text + "', ";
            strSQL += "ClientTypeID = " + cboClientType.SelectedValue + ", ";
            strSQL += "Address = '" + txtAddress.Text + "', ";
            strSQL += "City = '" + txtCity.Text + "', ";
            strSQL += "State = '" + txtState.Text + "', ";
            strSQL += "Zip = '" + txtZip.Text + "', ";
            strSQL += "OfficePhone = '" + txtOfficePhone.Text + "', ";
            strSQL += "Fax = '" + txtFax.Text + "', ";
            strSQL += "Comments = '" + txtComments.Text + "' ";
            strSQL += "WHERE ID = " + intClientID;

            General.UpdateRecord(strSQL);

        }

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            int intClientID = Convert.ToInt32(Request.Params["CID"]);

            if (!(intClientID == 0))
            {
                PopulateCombos();
                PopulateForm(intClientID);
            }

        }

        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            int intClientID = Convert.ToInt32(Request.Params["CID"]);

            if (!(intClientID == 0))
            {
                if (!ValidateForm())
                    return;
                UpdateClient(intClientID);
            }

            Response.Write("<script language='javascript'>opener.childClose(); window.close();</script>");
            Response.Flush();
        }
    }
}
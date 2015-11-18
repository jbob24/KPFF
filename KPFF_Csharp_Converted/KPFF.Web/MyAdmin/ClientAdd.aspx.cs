using System.Data;

namespace KPFF.PMP.MyAdmin
{
    partial class ClientAdd : System.Web.UI.Page
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

        private void AddClient()
        {
            clsGeneral General = new clsGeneral();
            string strSQL = "";

            strSQL = "INSERT INTO tblClients ";
            strSQL += "(ClientName, ClientTypeID, Address, City, State, Zip, OfficePhone, ";
            strSQL += "Fax, Comments) ";
            strSQL += "VALUES ";
            strSQL += "('" + txtClientName.Text + "', ";
            strSQL += cboClientType.SelectedValue + ", ";
            strSQL += "'" + txtAddress.Text + "', ";
            strSQL += "'" + txtCity.Text + "', ";
            strSQL += "'" + txtState.Text + "', ";
            strSQL += "'" + txtZip.Text + "', ";
            strSQL += "'" + txtOfficePhone.Text + "', ";
            strSQL += "'" + txtFax.Text + "', ";
            strSQL += "'" + txtComments.Text + "') ";

            General.AddRecord(strSQL);
        }

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            PopulateCombos();

        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            if (!ValidateForm())
                return;
            AddClient();

            Response.Write("<script language='javascript'>opener.childClose(); window.close();</script>");
            Response.Flush();
        }
    }
}
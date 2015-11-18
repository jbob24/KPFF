using System.Data;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAdmin
{
    partial class Clients : System.Web.UI.Page
    {

        #region " Custom Routines "

        public object GetTotalRecords()
        {
            int intTotalRecords = 0;
            clsGeneral General = new clsGeneral();
            string strSQL = "";

            strSQL = "SELECT tblClients.ID ";
            strSQL += "FROM tblClients INNER JOIN tblClientTypes ON tblClients.ClientTypeID = tblClientTypes.ID ";
            strSQL += "ORDER BY tblClients.ClientName";

            DataSet ds = General.FillDataset(strSQL);
            DataTable dt = ds.Tables[0];

            intTotalRecords = dt.Rows.Count;

            ds.Dispose();
            ds = null;
            dt.Dispose();
            dt = null;

            return intTotalRecords;
        }

        #endregion

        protected void dgClients_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            string strSQL = "";
            clsGeneral General = new clsGeneral();
            int intID = e.Keys[0].GetValueOrDefault<int>();

            if (!(intID == 0))
            {
                //Check to see if Child records exist
                strSQL = "SELECT ID FROM tblProjects ";
                strSQL += "WHERE ClientID = " + intID;
                DataSet ds = General.FillDataset(strSQL);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblGridError.Text = "Client: <b>" + e.Values[0] + "</b> has children records. Unable to delete.";
                    lblGridError.Visible = true;
                    e.Cancel = true;
                }
                else
                {
                    lblGridError.Text = "";
                    lblGridError.Visible = false;
                }
                ds.Dispose();
                ds = null;
                dt.Dispose();
                dt = null;
            }
        }
    }
}
using System.Data;
using System.Web.UI.WebControls;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAdmin
{
    partial class Employees : System.Web.UI.Page
    {

        #region " Custom Routines "

        public object GetTotalRecords()
        {
            int intTotalRecords = 0;
            clsGeneral General = new clsGeneral();
            string strSQL = "";

            strSQL = "SELECT tblEmployees.EmployeeID ";
            strSQL += "FROM tblEmployees LEFT OUTER JOIN tblEmployeeTypes ON tblEmployees.EmployeeTypeID = tblEmployeeTypes.ID ";
            strSQL += "ORDER BY tblEmployees.EmployeeCode";

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

        protected void dgEmployees_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Controls.Count > 0)
                {
                    if ((e.Row.Cells[6].Controls[0]) is LinkButton)
                    {
                        LinkButton btnDelete = (LinkButton)e.Row.Cells[6].Controls[0];
                        btnDelete.Attributes["onclick"] = "return confirm('Are you sure you want to delete this Employee and all its schedule?');";
                    }
                }
            }
        }

        protected void dgEmployees_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            string strSQL = "";
            clsGeneral General = new clsGeneral();
            int intEmployeeID = e.Keys[0].GetValueOrDefault<int>();

            if (!(intEmployeeID == 0))
            {
                //Check to see if Child records exist
                strSQL = "DELETE FROM tblSchedule ";
                strSQL += "WHERE EmployeeID = " + intEmployeeID;
                General.DeleteRecord(strSQL);
            }
        }
    }
}
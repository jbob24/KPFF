
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
using Microsoft.Win32;
using KPFF.PMP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KPFF.PMP.MyAdmin
{
    partial class EngineerGroups : PageBase
    {

        #region " Custom Routines"

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            PopulateDataset();
        }

        private void PopulateDataset()
        {
            var groups = new List<EngineerGroup>();
            groups = EngineerGroup.GetAllActive();
            gridGroups.DataSource = groups.OrderBy(g => g.Name);
            gridGroups.DataBind();
        }

        #endregion

        protected void btnDeactivateGroup_Click(object sender, System.EventArgs e)
        {
            foreach (GridViewRow row in gridGroups.Rows)
            {
                var groupId = gridGroups.DataKeys[row.RowIndex].Values[0].GetValueOrDefault<int>();
                var chkGroup = (CheckBox)row.FindControl("chkGroup");

                if (((chkGroup != null) & chkGroup.Checked))
                {
                    var group = EngineerGroup.GetById(groupId);
                    group.IsActive = false;
                    group.Update();
                }
            }

            RefreshPage();
        }

        protected void RefreshPage()
        {
            Response.Redirect("engineergroups.aspx");
        }

        protected void gridGroups_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Controls.Count > 0)
                {
                    if ((e.Row.Cells[4].Controls[0]) is LinkButton)
                    {
                        LinkButton btnDelete = (LinkButton)e.Row.Cells[4].Controls[0];
                        btnDelete.Attributes["onclick"] = "return confirm('Are you sure you want to delete this Group?');";
                    }
                }
            }
        }

        protected void gridGroups_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            var groupId = ((GridView)sender).DataKeys[e.RowIndex].Value.GetValueOrDefault<int>();

            if (groupId > 0)
            {
                var group = EngineerGroup.GetById(groupId);
                group.IsActive = false;
                group.Update();
            }

            RefreshPage();
        }

        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                return;
            }

            var newGroup = new EngineerGroup();
            newGroup.Name = txtName.Text;
            newGroup.Description = txtDescription.Text;

            newGroup.Insert();

            RefreshPage();
        }
    }
}
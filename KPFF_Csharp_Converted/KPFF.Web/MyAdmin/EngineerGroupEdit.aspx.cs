using System;
using System.Data;
using KPFF.PMP.Entities;
using System.Web.UI.WebControls;
using System.Linq;

namespace KPFF.PMP.MyAdmin
{
    partial class EngineerGroupEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            var groupId = 0;
            groupId = Request.Params["GID"].GetValueOrDefault<int>();

            if (groupId > 0)
            {
                var group = EngineerGroup.GetById(groupId);

                if (group != null)
                {
                    txtName.Text = group.Name;
                    txtDescription.Text = group.Description;

                    var engineers = KPFF.PMP.Entities.Engineer.GetAllEngineers();

                    if (engineers != null)
                    {
                        ddlEmployees.DataSource = engineers;
                        ddlEmployees.DataTextField = "EmployeeName";
                        ddlEmployees.DataValueField = "EmployeeID";

                        ddlEmployees.DataBind();

                        gridMembers.DataSource = group.Members.Where(m => m.IsActive == true);
                        gridMembers.DataBind();
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                var groupId = Request.Params["GID"].GetValueOrDefault<int>();

                if (groupId > 0)
                {
                    var group = EngineerGroup.GetById(groupId);

                    if (group != null)
                    {
                        group.Name = txtName.Text;
                        group.Description = txtDescription.Text;
                        group.Update();

                        RefreshPage(groupId);
                    }
                }
            }
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            int groupId = Request.Params["GID"].GetValueOrDefault<int>();

            if (groupId > 0)
            {
                var group = EngineerGroup.GetById(groupId);

                if (group != null)
                {
                    var empId = ddlEmployees.SelectedValue.GetValueOrDefault<int>();

                    if (empId > 0)
                    {
                        var member = group.Members.SingleOrDefault(m => m.EmployeeId == empId);

                        if (member != null)
                        {
                            member.IsActive = true;
                            group.UpdateMember(member);
                        }
                        else
                        {
                            group.AddMember(new EngineerGroupMember(empId, "", true));
                        }

                        RefreshPage(groupId);
                    }
                }
            }
        }

        protected void gridMembers_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Controls.Count > 0)
                {
                    if ((e.Row.Cells[2].Controls[0]) is LinkButton)
                    {
                        LinkButton btnDelete = (LinkButton)e.Row.Cells[2].Controls[0];
                        btnDelete.Attributes["onclick"] = "return confirm('Are you sure you want to delete this member?');";
                    }
                }
            }
        }

        protected void gridMembers_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            var groupId = Request.Params["GID"].GetValueOrDefault<int>();

            if (groupId > 0)
            {
                var empId = ((GridView)sender).DataKeys[e.RowIndex].Value.GetValueOrDefault<int>();

                if (empId > 0)
                {
                    var group = EngineerGroup.GetById(groupId);
                    var member = group.Members.SingleOrDefault(m => m.EmployeeId == empId);

                    if (member != null)
                    {
                        member.IsActive = false;
                        group.UpdateMember(member);
                    }
                }

            }
            RefreshPage(groupId);
        }

        protected void RefreshPage(int groupId)
        {
            Response.Redirect(string.Format("engineergroupedit.aspx?GID={0}", groupId));
        }
    }
}
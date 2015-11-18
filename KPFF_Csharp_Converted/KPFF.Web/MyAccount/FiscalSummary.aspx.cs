
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KPFF.PMP.Entities;

namespace KPFF.PMP.MyAccount
{
    partial class FiscalSummary : PageBase
    {

        bool _pmFiscalSummary = false;

        bool _picFiscalSummary = false;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            ValidateEmployeeAccess();

            if (!_pmFiscalSummary & !_picFiscalSummary)
            {
                Response.Redirect("myprojects.aspx");
                return;
            }

            if (!_picFiscalSummary)
            {
                picTab.Visible = false;
            }

            //If Not IsPostBack Then
            BindGrid();
            //End If
        }

        private void ValidateEmployeeAccess()
        {
            int employeeId = this.EmployeeID;
            SqlConnection con = new SqlConnection(Configuration.ConnectionString);
            object Engineer = new Engineer();

            con.Open();
            SqlDataReader dr = this.Engineer.GetByEmployeeID(employeeId, con);

            if (dr.HasRows)
            {
                dr.Read();
                _pmFiscalSummary = dr.GetValueOrDefault<bool>("PMFiscalSummary");
                _picFiscalSummary = dr.GetValueOrDefault<bool>("PICFiscalSummary");
            }

            con.Close();
            dr.Close();
            con = null;
            dr = null;
        }


        private void BindGrid()
        {
            if (tabs.ActiveTab.ID == "pmTab")
            {
                List<BillQuickProject> pmProjects = default(List<BillQuickProject>);
                List<BillQuickProject> pmClients = default(List<BillQuickProject>);
                List<BillQuickProject> pms = default(List<BillQuickProject>);

                if (_picFiscalSummary)
                {
                    pmProjects = BillQuickProject.GetProjectsByPIC(EmployeeID, BillQuickProject.GetTimeFrame(rbPMTimeFrame.SelectedValue));
                    pmClients = BillQuickProject.GetClientsByPIC(EmployeeID, BillQuickProject.GetTimeFrame(rbPMTimeFrame.SelectedValue));
                    pms = BillQuickProject.GetPMsByPIC(EmployeeID, BillQuickProject.GetTimeFrame(rbPMTimeFrame.SelectedValue));
                }
                else
                {
                    pmProjects = BillQuickProject.GetProjectsByPM(EmployeeID, BillQuickProject.GetTimeFrame(rbPMTimeFrame.SelectedValue));
                    pmClients = BillQuickProject.GetClientsByPM(EmployeeID, BillQuickProject.GetTimeFrame(rbPMTimeFrame.SelectedValue));
                    pms = BillQuickProject.GetPMsByPM(EmployeeID, BillQuickProject.GetTimeFrame(rbPMTimeFrame.SelectedValue));
                }

                myPMProjects.DisplayPM = true;

                myPMProjects.Projects = pmProjects;
                myPMProjects.BindGrid();

                myPMClients.Projects = pmClients;
                myPMClients.BindGrid();

                myPMs.Projects = pms;
                myPMs.BindGrid();

                pnlPMProjects.Visible = true;
                pnlPMClients.Visible = true;
                pnlPms.Visible = true;
            }
            else
            {
                var picProjects = BillQuickProject.GetProjects(BillQuickProject.GetTimeFrame(rbPICTimeFrame.SelectedValue));
                var picClients = BillQuickProject.GetClients(BillQuickProject.GetTimeFrame(rbPICTimeFrame.SelectedValue));
                var pics = BillQuickProject.GetPICs(BillQuickProject.GetTimeFrame(rbPICTimeFrame.SelectedValue));

                myPICProjects.DisplayPIC = true;

                myPICProjects.Projects = picProjects;
                myPICProjects.BindGrid();

                myPICClients.Projects = picClients;
                myPICClients.BindGrid();

                myPICs.Projects = pics;
                myPICs.BindGrid();

                pnlPICProjects.Visible = true;
                pnlPICClients.Visible = true;
                pnlPICs.Visible = true;
            }



            //' if pic access
            //If _picFiscalSummary Then



            //Else
            //    ' else

            //End If


        }


        protected void rbTimeFrame_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //BindGrid(tabs.ActiveTab.ID)
        }

        protected void tabs_ActiveTabChanged(object sender, System.EventArgs e)
        {
            //BindGrid(tabs.ActiveTab.ID)
        }
    }
}
//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik, @toddanglin
//Facebook: facebook.com/telerik
//=======================================================

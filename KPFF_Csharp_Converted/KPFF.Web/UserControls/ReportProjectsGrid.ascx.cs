
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace KPFF.PMP.UserControls
{
    public partial class ReportProjectsGrid : System.Web.UI.UserControl
    {

        private List<BillQuickProject> _projects;
        public List<BillQuickProject> Projects
        {
            get { return _projects; }
            set { _projects = value; }
        }

        private bool _displayPM;
        public bool DisplayPM
        {
            get { return _displayPM; }
            set { _displayPM = value; }
        }

        private bool _displayPIC;
        public bool DisplayPIC
        {
            get { return _displayPIC; }
            set { _displayPIC = value; }
        }



        public void BindGrid()
        {
            gridProjects.DataSource = Projects;
            gridProjects.DataBind();

            if ((gridProjects.Rows.Count > 0))
            {
                gridProjects.UseAccessibleHeader = true;
                gridProjects.HeaderRow.TableSection = TableRowSection.TableHeader;
                gridProjects.FooterRow.TableSection = TableRowSection.TableFooter;
            }

            if (!DisplayPM)
            {
                gridProjects.Columns[2].Visible = false;
            }

            if (!DisplayPIC)
            {
                gridProjects.Columns[3].Visible = false;
            }
        }
    }
}
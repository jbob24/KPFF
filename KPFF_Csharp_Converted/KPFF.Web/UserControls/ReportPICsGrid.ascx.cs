
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace KPFF.PMP.UserControls
{
    public partial class ReportPICsGrid : System.Web.UI.UserControl
    {

        private List<BillQuickProject> _projects;
        public List<BillQuickProject> Projects
        {
            get { return _projects; }
            set { _projects = value; }
        }


        public void BindGrid()
        {
            picProjects.DataSource = Projects;
            picProjects.DataBind();

            if ((picProjects.Rows.Count > 0))
            {
                picProjects.UseAccessibleHeader = true;
                picProjects.HeaderRow.TableSection = TableRowSection.TableHeader;
                picProjects.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
    }
}
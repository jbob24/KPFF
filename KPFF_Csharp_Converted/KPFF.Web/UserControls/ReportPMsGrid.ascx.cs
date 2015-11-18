
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace KPFF.PMP.UserControls
{
    public partial class ReportPMsGrid : System.Web.UI.UserControl
    {

        private List<BillQuickProject> _projects;
        public List<BillQuickProject> Projects
        {
            get { return _projects; }
            set { _projects = value; }
        }


        public void BindGrid()
        {
            gridPMs.DataSource = Projects;
            gridPMs.DataBind();

            if ((gridPMs.Rows.Count > 0))
            {
                gridPMs.UseAccessibleHeader = true;
                gridPMs.HeaderRow.TableSection = TableRowSection.TableHeader;
                gridPMs.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
    }
}
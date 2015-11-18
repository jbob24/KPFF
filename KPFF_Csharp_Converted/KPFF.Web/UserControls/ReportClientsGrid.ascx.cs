
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace KPFF.PMP.UserControls
{
    public partial class ReportClientsGrid : System.Web.UI.UserControl
    {

        private List<BillQuickProject> _projects;
        public List<BillQuickProject> Projects
        {
            get { return _projects; }
            set { _projects = value; }
        }


        public void BindGrid()
        {
            gridClients.DataSource = Projects;
            gridClients.DataBind();

            if ((gridClients.Rows.Count > 0))
            {
                gridClients.UseAccessibleHeader = true;
                gridClients.HeaderRow.TableSection = TableRowSection.TableHeader;
                gridClients.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
    }
}
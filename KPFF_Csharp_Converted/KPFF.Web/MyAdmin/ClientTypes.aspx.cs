
using System.Data;
using System.Data.SqlClient;
using Infragistics.WebUI.Shared;
using Infragistics.WebUI.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using System;
using System.Web.UI.WebControls;
using System.Configuration;

namespace KPFF.PMP.MyAdmin
{
    partial class ClientTypes : System.Web.UI.Page
    {

        #region "Web Form Designer generated code"
        //This call is required by the Web Form Designer.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            InitializeDataSource();
        }

        private void Page_Init(System.Object sender, System.EventArgs e)
        {
            //CODEGEN: This method call is required by the Web Form Designer
            //Do not modify it using the code editor.
            InitializeComponent();
        }

        #endregion

        #region " Custom Routines"
        // Data variables
        string strConn = ConfigurationManager.AppSettings["ConnectionString"];
        protected SqlDataAdapter da;
        protected SqlCommandBuilder cb;
        protected DataSet dsClientTypes;
        protected DataTable dtClientTypes;
        //
        private void InitializeDataSource()
        {
            string strSQL = null;
            strSQL = "SELECT * FROM tblClientTypes ";
            strSQL += "ORDER BY ClientType";
            da = new SqlDataAdapter(strSQL, strConn);
            cb = new SqlCommandBuilder(da);
            dsClientTypes = new DataSet();
            // Create table to add data key for lookup purposes
            dtClientTypes = new DataTable("ClientType");
            DataColumn dtCol = default(DataColumn);
            //
            dtCol = new DataColumn("ID");
            dtCol.DataType = typeof(int);
            dtCol.AutoIncrement = true;
            dtCol.AutoIncrementSeed = 1;
            dtCol.AutoIncrementStep = 1;
            dtClientTypes.Columns.Add(dtCol);
            //
            dtCol = new DataColumn("ClientType");
            dtCol.DataType = typeof(string);
            dtClientTypes.Columns.Add(dtCol);
            //
            // Add primary Key
            DataColumn[] keys = new DataColumn[2];
            keys[0] = dtClientTypes.Columns["ID"];
            dtClientTypes.PrimaryKey = keys;
            //
            dsClientTypes.Tables.Add(dtClientTypes);
        }

        private void PopulateDataSource()
	{
		if (dsClientTypes.Tables["ClientType"].Rows.Count <= 0) {
			da.Fill(dsClientTypes.Tables["ClientType"]);
		}
	}

        private void DataBindGrid()
        {
            // Databind Grid
            this.uwgClientTypes.DataSource = dsClientTypes;
            this.uwgClientTypes.DataBind();
        }

        private void InitGridLayout()
        {
            // Customize Grid Layout
            Infragistics.WebUI.UltraWebGrid.UltraGridLayout uwgLayout = default(Infragistics.WebUI.UltraWebGrid.UltraGridLayout);
            uwgLayout = this.uwgClientTypes.DisplayLayout;

            //Layout Styles
            //Me.uwgClientTypes.Bands[0].HeaderStyle.CssClass = "WeekHeaderStyle"

            //
            this.uwgClientTypes.Bands[0].Columns.FromKey("ID").Hidden = true;
            //
            this.uwgClientTypes.Bands[0].Columns.FromKey("ClientType").Width = Unit.Pixel(290);
            this.uwgClientTypes.Bands[0].Columns.FromKey("ClientType").Header.Caption = "Client Types";
        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (this.Page.IsPostBack)
                return;
            PopulateDataSource();
            DataBindGrid();
        }

        protected void uwgPhases_DeleteRowBatch(object sender, Infragistics.WebUI.UltraWebGrid.RowEventArgs e)
        {
            PopulateDataSource();
            //
            UltraGridRow uwgRow = e.Row;
            DataRow dtRow = default(DataRow);
            try
            {
                dtRow = dtClientTypes.Rows.Find(uwgRow.DataKey);
                dtRow.Delete();
            }
            catch (Exception ex)
            {
            }
        }

        protected void uwgPhases_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            InitGridLayout();
        }

        protected void uwgPhases_UpdateGrid(object sender, Infragistics.WebUI.UltraWebGrid.UpdateEventArgs e)
	{
		PopulateDataSource();
		//
		UltraGridRow uwgRow = default(UltraGridRow);
		DataRow dtRow = default(DataRow);
		UltraGridRowsEnumerator updatedRows = default(UltraGridRowsEnumerator);
		//
		// Get Updated rows
		updatedRows = e.Grid.Bands[0].GetBatchUpdates();
		// for each row in the Updated rows check if the current row is an addedrow, 
		// if so create the row and add it to the dataset
		while (updatedRows.MoveNext()) {
			uwgRow = updatedRows.Current;
			int i = 0;
			if (uwgRow.DataChanged == DataChanged.Added) {
				dtRow = dtClientTypes.NewRow();
				for (i = 1; i <= uwgRow.Cells.Count - 1; i++) {
					if (uwgRow.Cells[i].Value == null) {
						dtRow[i] = DBNull.Value;
					} else {
						dtRow[i] = uwgRow.Cells[i].Value;
					}
				}
				dtClientTypes.Rows.Add(dtRow);
			} else if (uwgRow.DataChanged == DataChanged.Modified) {
				dtRow = dtClientTypes.Rows.Find(uwgRow.DataKey);
				if ((dtRow != null)) {
					for (i = 1; i <= uwgRow.Cells.Count - 1; i++) {
						if (uwgRow.Cells[i].Value == null) {
							dtRow[i] = DBNull.Value;
						} else {
							dtRow[i] = uwgRow.Cells[i].Value;
						}
					}
				}
			}
		}
		//
		// Now update database
		da.Update(dsClientTypes.Tables["ClientType"]);
		// Populate Grid
		DataBindGrid();
		this.uwgClientTypes.DisplayLayout.ActiveRow = this.uwgClientTypes.Rows[0];
	}

    }
}
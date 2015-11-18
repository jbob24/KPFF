
using System.Data;
using System.Data.SqlClient;

using Microsoft.VisualBasic;
using System.Configuration;

public class clsGeneral
{

	public string strConn = ConfigurationManager.AppSettings["ConnectionString"];
	private string strSQL;
	private SqlConnection conn;
	private SqlCommand cmdSelect;
	private SqlCommand cmdUpdate;
	private SqlCommand cmdInsert;
	private SqlCommand cmdDelete;
	private SqlDataAdapter da;

	private DataSet ds;
	public DataSet FillDataset(string strSQL)
	{
		conn = new SqlConnection(strConn);
		cmdSelect = new SqlCommand(strSQL, conn);
		da = new SqlDataAdapter();
		da.SelectCommand = cmdSelect;
		ds = new DataSet();
		da.Fill(ds);
		//
		return ds;
	}

	public void AddRecord(string strSQL)
	{
		conn = new SqlConnection(strConn);
		conn.ConnectionString = strConn;
		conn.Open();
		//
		SqlCommand cmdInsert = new SqlCommand(strSQL, conn);
		cmdInsert.ExecuteNonQuery();
		conn.Close();
		conn.Dispose();
		cmdInsert.Dispose();
		conn = null;
		cmdInsert = null;
	}

	public void UpdateRecord(string strSQL)
	{
		conn = new SqlConnection(strConn);
		cmdUpdate = new SqlCommand(strSQL, conn);
		conn.Open();
		cmdUpdate.ExecuteNonQuery();
		//
		conn.Close();
		conn = null;
		cmdUpdate = null;
	}

	public void DeleteRecord(string strSQL)
	{
		conn = new SqlConnection(strConn);
		cmdDelete = new SqlCommand(strSQL, conn);
		conn.Open();
		cmdDelete.ExecuteNonQuery();
		//
		conn.Close();
		conn.Dispose();
		cmdDelete.Dispose();
		conn = null;
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik, @toddanglin
//Facebook: facebook.com/telerik
//=======================================================

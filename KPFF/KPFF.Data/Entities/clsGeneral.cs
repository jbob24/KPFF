using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace KPFF.Data.Entities
{
    public class clsGeneral
    {
        public string strConn = ConfigurationSettings.AppSettings["ConnectionString"];
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
    }
}

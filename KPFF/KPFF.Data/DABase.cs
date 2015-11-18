using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using KPFF.Data;

namespace KPFF.Data
{
    public class DABase
    {
        protected WeekDate WeekDate { get; set; }
        protected ClsSchedule Schedule { get; set; }

        protected string conString = Configuration.ConnectionString;

        public DABase(ClsSchedule schedule, WeekDate weekDate)
        {
            Schedule = schedule;
            WeekDate = weekDate;
        }

        public DABase()
        { }

        public DataSet GetDataSetByStoredProcedure(DataSet dset, string procedureName, Dictionary<string, string> parms)
        {
            try
            {
                var con = new SqlConnection(conString);
                var da = new SqlDataAdapter(procedureName, con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                AddParamaters(da.SelectCommand, parms);
                da.Fill(dset, dset.Tables[0].TableName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured in GetDataSerByStoredProcedure.", ex);
            }

            return dset;
        }


        public SqlDataReader GetDataReaderByStoredProcedure(string procedureName, Dictionary<string, string> parms, SqlConnection con)
        {
            SqlDataReader rdr;

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                var cmd = new SqlCommand(procedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                AddParamaters(cmd, parms);

                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured in GetDataReaderByStoredProcedure.", ex);
            }

            return rdr;
        }

        public bool ExecuteProcedure(string procedureName, Dictionary<string, string> parms)
        {
            try
            {
                var con = new SqlConnection(conString);
                var cmd = new SqlCommand(procedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;

                AddParamaters(cmd, parms);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured in ExecuteProcedure.", ex);
            }
        }

        public object ExecuteScalar(string procedureName, Dictionary<string, string> parms)
        {
            try
            {
                var con = new SqlConnection(conString);
                var cmd = new SqlCommand(procedureName, con);
                object value;

                cmd.CommandType = CommandType.StoredProcedure;
                AddParamaters(cmd, parms);

                con.Open();
                value = cmd.ExecuteScalar();
                con.Close();

                return value;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured in ExecuteProcedure.", ex);
            }
        }

        private void AddParamaters(SqlCommand cmd, Dictionary<string, string> parms)
        {
            foreach (KeyValuePair<string, string> pair in parms)
            {
                if (pair.Value != DBNull.Value.ToString())
                {
                    cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(pair.Key, DBNull.Value);
                }
            }
        }
    }
}

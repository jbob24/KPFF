using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace KPFF.PMP.Entities
{
    public class DABase
    {
        protected WeekDate weekDate;
        protected clsSchedule schedule = new clsSchedule();

        protected string conString = Configuration.ConnectionString;
        public DABase(clsSchedule schedule, WeekDate weekDate)
        {
            this.schedule = schedule;
            this.weekDate = weekDate;
        }

        public DABase(WeekDate weekDate)
        {
            this.weekDate = weekDate;
        }



        public DABase()
        {
        }

        protected DataSet GetDataSetByStoredProcedure(DataSet dSet, string procedureName, Dictionary<string, string> @params)
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                var da = new SqlDataAdapter(procedureName, con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                AddParamaters(da.SelectCommand, @params);

                da.Fill(dSet, dSet.Tables[0].TableName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured in GetDataSerByStoredProcedure.", ex);
            }

            return dSet;
        }

        protected static SqlDataReader GetDataReaderByStoredProcedure(string procedureName, Dictionary<string, string> @params, SqlConnection con)
        {
            SqlDataReader rdr = default(SqlDataReader);

            try
            {
                if (!(con.State == ConnectionState.Open))
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand(procedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;

                AddParamaters(cmd, @params);

                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured in GetDataReaderByStoredProcedure.", ex);
            }

            return rdr;
        }

        protected bool ExecuteProcedure(string procedureName, Dictionary<string, string> @params)
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand(procedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;

                AddParamaters(cmd, @params);

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

        protected object ExecuteScalar(string procedureName, Dictionary<string, string> @params)
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand(procedureName, con);
                object value = null;

                cmd.CommandType = CommandType.StoredProcedure;

                AddParamaters(cmd, @params);

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

        private static void AddParamaters(SqlCommand cmd, Dictionary<string, string> @params)
        {
            foreach (KeyValuePair<string, string> pair in @params)
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

    public static class ExtensionMethods
    {
        public static T GetValue<T>(this SqlDataReader reader, string name)
        {
            //return (T)Convert.ChangeType(reader[name], typeof(T));
            return reader.GetReaderValue<T>(reader.GetOrdinal(name));
        }

        public static T GetReaderValue<T>(this SqlDataReader reader, int ordinal)
        {
            return (T)Convert.ChangeType(reader[ordinal], typeof(T));
        }

        public static T GetValueOrDefault<T>(this SqlDataReader reader, string name)
        {
            //T value;

            //if (reader.TryGetReaderValue<T>(name, out value))
            //{
            //    return value;
            //}
            //else
            //{
            //    return default(T);
            //}

            return reader.GetValueOrDefault<T>(reader.GetOrdinal(name));
        }

        public static T GetValueOrDefault<T>(this SqlDataReader reader, int ordinal)
        {
            T value;

            if (reader.TryGetReaderValue<T>(ordinal, out value))
            {
                return value;
            }
            else
            {
                return default(T);
            }
        }

        public static bool TryGetReaderValue<T>(this SqlDataReader reader, int ordinal, out T output)
        {
            try
            {
                output = reader.GetReaderValue<T>(ordinal);
                return true;
            }

            catch (Exception ex)
            {
                if (ex is InvalidCastException || ex is FormatException || ex is OverflowException)
                {
                    output = default(T);
                    return false;
                }

                else
                    throw;
            }
        }

        public static bool TryGetReaderValue<T>(this SqlDataReader reader, string name, out T output)
        {
            try
            {
                reader.TryGetReaderValue<T>(reader.GetOrdinal(name), out output);
                return true;
            }

            catch (Exception ex)
            {
                if (ex is InvalidCastException || ex is FormatException || ex is OverflowException)
                {
                    output = default(T);
                    return false;
                }

                else
                    throw;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KPFF.Data;

namespace KPFF.Data
{
    public class DataBase : IDisposable
    {
        private string _commandText;
        private SqlConnection _connection;
        private string _connectionString = Configuration.ConnectionString;
        private Dictionary<string, string> _params;
        private SqlCommand _command;

        public DataBase(string commandText, Dictionary<string, string> parms)
        {
            this._commandText = commandText;
            this._params = parms;
            
            _connection = new SqlConnection(_connectionString);
            _connection.Open();

            this._command = new SqlCommand(this._commandText, _connection);
            this._command.CommandType = CommandType.StoredProcedure;
            AddParamaters();
        }

        public SqlDataReader ExecuteReader()
        {
            try
            {
                return _command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured in ExecuteReader.", ex);
            }
        }

        public bool ExecuteProcedure()
        {
            try
            {
                return _command.ExecuteNonQuery() == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured in ExecuteProcedure.", ex);
            }
        }

        public void Dispose()
        {
            _connection.Close();
        }

        private void AddParamaters()
        {
            if (this._params != null)
            {
                foreach (KeyValuePair<string, string> pair in _params)
                {
                    if (pair.Value != DBNull.Value.ToString())
                    {
                        this._command.Parameters.AddWithValue(pair.Key, pair.Value);
                    }
                    else
                    {
                        this._command.Parameters.AddWithValue(pair.Key, DBNull.Value);
                    }
                }
            }
        }
    }
}

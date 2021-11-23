using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseHelper
{
    /// <summary>
    /// Database Helper Connect instance.
    /// </summary>
    public class Connect
    {
        /// <summary>
        /// Supported database servers.
        /// </summary>
        private readonly DB_SERVERS Server;

        /// <summary>
        /// Database server connection string.
        /// </summary>
        private string ConnectionString { get; }

        /// <summary>
        /// Database Servers object.
        /// </summary>
        public enum DB_SERVERS
        {
            /// <summary>
            /// Microsoft Sql Server.
            /// </summary>
            MS_SQL_SERVER,
            /// <summary>
            /// MySql Server.
            /// </summary>
            MYSQL_SERVER
        }

        /// <summary>
        /// Database Helper constructor to connect with the database server.
        /// </summary>
        /// <param name="server">Supported Database Server</param>
        /// <param name="connectionString">Database Server Connection String</param>
        public Connect(DB_SERVERS server, string connectionString)
        {
            Server = server;
            ConnectionString = connectionString;
        }

        /// <summary>
        /// This method can be used for insert, update and delete.
        /// It will return true, if query successful executed otherwise false.
        /// </summary>
        /// <param name="query">String query or procedure name</param>
        /// <param name="parameters">IDictionary parameters</param>
        /// <param name="isStoreProcedure">true, if query is store procedure, by default is false.</param>
        /// <param name="timeout">Command timeout, by default is 30 seconds.</param>
        /// <returns></returns>
        public bool InsertOrUpdateOrDeleteData(string query, IDictionary<string, object> parameters, bool isStoreProcedure = false, int timeout = 0)
        {
            int isQueryExecuted = 0;
            if (string.IsNullOrEmpty(ConnectionString))
            {
                return false;
            }
            switch (Server)
            {
                case DB_SERVERS.MS_SQL_SERVER:
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        con.Open();
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            command.CommandType = isStoreProcedure ? CommandType.StoredProcedure : CommandType.Text;
                            command.CommandTimeout = timeout > 0 ? timeout : 30;
                            if (parameters != null)
                            {
                                IDbDataParameter dbParam;
                                foreach (KeyValuePair<string, object> param in parameters)
                                {
                                    dbParam = command.CreateParameter();
                                    dbParam.ParameterName = param.Key;
                                    dbParam.Value = param.Value ?? DBNull.Value;
                                    command.Parameters.Add(dbParam);
                                }
                            }
                            isQueryExecuted = command.ExecuteNonQuery();
                        }
                        con.Close();
                    }
                    break;
                case DB_SERVERS.MYSQL_SERVER:
                    using (MySqlConnection con = new MySqlConnection(ConnectionString))
                    {
                        con.Open();
                        using (MySqlCommand command = new MySqlCommand(query, con))
                        {
                            command.CommandType = isStoreProcedure ? CommandType.StoredProcedure : CommandType.Text;
                            command.CommandTimeout = timeout > 0 ? timeout : 30;
                            if (parameters != null)
                            {
                                IDbDataParameter dbParam;
                                foreach (KeyValuePair<string, object> param in parameters)
                                {
                                    dbParam = command.CreateParameter();
                                    dbParam.ParameterName = param.Key;
                                    dbParam.Value = param.Value ?? DBNull.Value;
                                    command.Parameters.Add(dbParam);
                                }
                            }
                            isQueryExecuted = command.ExecuteNonQuery();
                        }
                        con.Close();
                    }
                    break;

            }
            
            return isQueryExecuted > 0;
        }

        /// <summary>
        /// This method can be used for select data from database.
        /// It will return DataTable object, if query successful executed otherwise null.
        /// </summary>
        /// <param name="query">String query or procedure name</param>
        /// <param name="parameters">IDictionary parameters</param>
        /// <param name="isStoreProcedure">true, if query is store procedure, by default is false.</param>
        /// <param name="timeout">Command timeout, by default is 30 seconds.</param>
        /// <returns></returns>
        public DataTable GetData(string query, IDictionary<string, object> parameters, bool isStoreProcedure = false, int timeout = 0)
        {
            DataTable Table = new DataTable();
            IDataReader DataReader = null;
            switch (Server)
            {
                case DB_SERVERS.MS_SQL_SERVER:
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        con.Open();
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            command.CommandType = isStoreProcedure ? CommandType.StoredProcedure : CommandType.Text;
                            command.CommandTimeout = timeout > 0 ? timeout : 30;
                            if (parameters != null)
                            {
                                IDbDataParameter dbParam;
                                foreach (KeyValuePair<string, object> param in parameters)
                                {
                                    dbParam = command.CreateParameter();
                                    dbParam.ParameterName = param.Key;
                                    dbParam.Value = param.Value ?? DBNull.Value;
                                    command.Parameters.Add(dbParam);
                                }
                            }
                            DataReader = command.ExecuteReader();
                        }
                        if (DataReader != null)
                            Table.Load(DataReader);
                        con.Close();
                    }
                    break;
                case DB_SERVERS.MYSQL_SERVER:
                    using (MySqlConnection con = new MySqlConnection(ConnectionString))
                    {
                        con.Open();
                        using (MySqlCommand command = new MySqlCommand(query, con))
                        {
                            command.CommandType = isStoreProcedure ? CommandType.StoredProcedure : CommandType.Text;
                            command.CommandTimeout = timeout > 0 ? timeout : 30;
                            if (parameters != null)
                            {
                                IDbDataParameter dbParam;
                                foreach (KeyValuePair<string, object> param in parameters)
                                {
                                    dbParam = command.CreateParameter();
                                    dbParam.ParameterName = param.Key;
                                    dbParam.Value = param.Value ?? DBNull.Value;
                                    command.Parameters.Add(dbParam);
                                }
                            }
                            DataReader = command.ExecuteReader();
                        }
                        if (DataReader != null)
                            Table.Load(DataReader);
                        con.Close();
                    }
                    break;
            }
            
            return Table != null && Table.Rows.Count > 0 ? Table : null;
        }

        /// <summary>
        /// This method can be used for select multiple(like two select queries) data from database.
        /// It will return DataSet object, if query successful executed otherwise null.
        /// Suggestion: Use transaction if both query select data is required!
        /// </summary>
        /// <param name="query">String query or procedure name</param>
        /// <param name="parameters">IDictionary parameters</param>
        /// <param name="isStoreProcedure">true, if query is store procedure, by default is false.</param>
        /// <param name="timeout">Command timeout, by default is 30 seconds.</param>
        /// <returns></returns>
        public DataSet GetMultipleData(string query, IDictionary<string, object> parameters, bool isStoreProcedure = false, int timeout = 0)
        {
            DataSet DataSet = new DataSet();
            switch (Server)
            {
                case DB_SERVERS.MS_SQL_SERVER:
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        con.Open();
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            command.CommandType = isStoreProcedure ? CommandType.StoredProcedure : CommandType.Text;
                            command.CommandTimeout = timeout > 0 ? timeout : 30;
                            if (parameters != null)
                            {
                                IDbDataParameter dbParam;
                                foreach (KeyValuePair<string, object> param in parameters)
                                {
                                    dbParam = command.CreateParameter();
                                    dbParam.ParameterName = param.Key;
                                    dbParam.Value = param.Value ?? DBNull.Value;
                                    command.Parameters.Add(dbParam);
                                }
                            }
                            using SqlDataAdapter sda = new SqlDataAdapter(command);
                            sda.Fill(DataSet);
                        }
                        con.Close();
                    }
                    break;
                case DB_SERVERS.MYSQL_SERVER:
                    using (MySqlConnection con = new MySqlConnection(ConnectionString))
                    {
                        con.Open();
                        using (MySqlCommand command = new MySqlCommand(query, con))
                        {
                            command.CommandType = isStoreProcedure ? CommandType.StoredProcedure : CommandType.Text;
                            command.CommandTimeout = timeout > 0 ? timeout : 30;
                            if (parameters != null)
                            {
                                IDbDataParameter dbParam;
                                foreach (KeyValuePair<string, object> param in parameters)
                                {
                                    dbParam = command.CreateParameter();
                                    dbParam.ParameterName = param.Key;
                                    dbParam.Value = param.Value ?? DBNull.Value;
                                    command.Parameters.Add(dbParam);
                                }
                            }
                            using MySqlDataAdapter sda = new MySqlDataAdapter(command);
                            sda.Fill(DataSet);
                        }
                        con.Close();
                    }
                    break;

            }
            
            return DataSet != null && DataSet.Tables.Count > 0 ? DataSet : null;
        }

    }
}

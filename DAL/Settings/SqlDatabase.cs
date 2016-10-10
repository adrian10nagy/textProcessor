using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Settings
{
    public class SqlDatabase<ConnectionType, CommandType, ParameterType>
        where ConnectionType : IDbConnection, new()
        where CommandType : IDbCommand, new()
        where ParameterType : IDbDataParameter, new()
    {
        #region variabiles

        public static string ConnectionStringSet { get; set; }
        public static System.Data.CommandType CommandTypeSet { get; set; }
        public delegate void ReadRow(IDataReader reader);

        #endregion

        public SqlDatabase()
        {
            Set(Configure.ConnectionString, System.Data.CommandType.StoredProcedure);
        }

        void Set(string connectionString, System.Data.CommandType commandType)
        {
            ConnectionStringSet = connectionString;
            CommandTypeSet = commandType;
        }


        //return Object
        #region Execute Scalar
        public Object ExecuteScalar(string query)
        {
            return ExecuteScalar(query, null);
        }

        public Object ExecuteScalar(string query, ParameterType[] parameters)
        {
            return ExecuteScalar(ConnectionStringSet, CommandTypeSet, query, parameters);
        }

        public Object ExecuteScalar(string connectionString, System.Data.CommandType commandType, string query, ParameterType[] parameters)
        {
            Object result = null;

            using (var connection = new ConnectionType())
            using (var command = new CommandType())
            {
                command.Connection = connection;
                command.CommandType = commandType;
                command.CommandText = query;
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                connection.ConnectionString = connectionString;
                connection.Open();

                result = command.ExecuteScalar();

                connection.Close();
            }
            return result;
        }
        #endregion


        //returns void OR list of Objects
        #region Execute
        public List<Object> Execute(string query, ParameterType[] parameters)
        {
            return Execute(ConnectionStringSet, CommandTypeSet, query, parameters);
        }

        public List<Object> Execute(string connectionString, System.Data.CommandType commandType, string query, ParameterType[] parameters)
        {

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.ConnectionString = connectionString;
                command.CommandText = query;
                command.CommandType = commandType;
                if (parameters != null)
                    Array.ForEach(parameters, p => command.Parameters.Add(p));
                connection.Open();
                //command.Connection = connection;


                using (var reader = command.ExecuteReader())
                {
                    var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                    var Object = new Object();

                    List<Object> personList = new List<Object>();


                    while (reader.Read())
                    {
                        foreach (var column in columns)
                        {
                            var col = reader[column];
                        }
                    }
                }
                var results = new List<Object>();

                connection.Close();
                return results;
            }
        }
        #endregion

        public void Execute(string query, ParameterType[] parameters, ReadRow rowMethod)
        {
            Execute(ConnectionStringSet, System.Data.CommandType.StoredProcedure, query, parameters, rowMethod);
        }

        public void Execute(System.Data.CommandType commandType, string query, ParameterType[] parameters, ReadRow rowMethod)
        {
            Execute(ConnectionStringSet, commandType, query, parameters, rowMethod);
        }
        public void Execute(string connectionString, string query, ParameterType[] parameters, ReadRow rowMethod)
        {
            Execute(connectionString, System.Data.CommandType.StoredProcedure, query, parameters, rowMethod);
        }

        public void Execute(string connectionString, System.Data.CommandType commandType, string query, ParameterType[] parameters, ReadRow rowMethod)
        {
            using (var connection = new ConnectionType())
            using (var command = new CommandType())
            {
                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = commandType;
                if (parameters != null)
                    Array.ForEach(parameters, p => command.Parameters.Add(p));
                //if (CommandTimeout > 0)
                //    command.CommandTimeout = CommandTimeout;

                connection.ConnectionString = connectionString;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        if (rowMethod != null)
                            rowMethod(reader);
                }
            }
        }
    }
}

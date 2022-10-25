using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHeldInventory_LibraryCore.Db
{
    public class SqlDataAccess : IDataAccess
    {
        private readonly IConfiguration configuration;
        public SqlDataAccess(IConfiguration _config)
        {
            configuration = _config;
        }
        public async Task<DataTable> AsyncExecuteProcedureReturnDataTable(string connectionStringName, string procName, params SqlParameter[] parameters)
        {
            string connectionString = configuration.GetConnectionString(connectionStringName);

            DataTable result = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = procName;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        result.Load(reader);
                        reader.Close();
                    }
                }
            }
            return result;
        }

        public DataSet ExecuteProcedureReturnDataSet(string dbConn, string procName, params SqlParameter[] parameters)
        {
            DataSet result = null;
            string connectionString = configuration.GetConnectionString(dbConn);
            bool outCount = false;

            List<SqlParameter> outParamaters = new List<SqlParameter>();
            DataTable outParametersTable = new DataTable("OutParameters");
            outParametersTable.Columns.Add("ParamaterName", Type.GetType("System.String"));
            outParametersTable.Columns.Add("ParamaterValue", Type.GetType("System.String"));

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = procName;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                            foreach (var parameter in parameters)
                            {
                                if (parameter.Direction.ToString() == "Output")
                                {
                                    outCount = true;
                                    break;
                                }
                            }
                        }
                        result = new DataSet();
                        sda.Fill(result);

                        if (outCount)
                        {
                            foreach (var item in parameters)
                            {
                                outParamaters.Add(new SqlParameter(item.ParameterName, item.Value));
                                outParametersTable.Rows.Add(item.ParameterName.Replace("@", ""), item.SqlValue);
                            }
                            result.Tables.Add(outParametersTable);

                        }
                    }
                }
            }
            return result;
        }

        public DataTable ExecuteProcedureReturnDataTable(string connectionStringName, string procName, params SqlParameter[] parameters)
        {
            DataTable result = new DataTable();
            string connectionString = configuration.GetConnectionString(connectionStringName);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = procName;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                        reader.Close();
                    }
                }
            }
            return result;
        }

        public List<SqlParameter> ExecuteProcedureReturnOutputParameters(string connectionStringName, string procName, params SqlParameter[] parameters)
        {
            string connectionString = configuration.GetConnectionString(connectionStringName);
            List<SqlParameter> result = new List<SqlParameter>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    sqlConnection.Open();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = procName;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    command.ExecuteNonQuery();
                    foreach (var item in parameters)
                    {
                        result.Add(new SqlParameter(item.ParameterName, item.Value));
                    }
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
            return result;
        }
    }
}

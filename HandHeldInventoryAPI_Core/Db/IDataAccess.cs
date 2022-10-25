using Microsoft.Data.SqlClient;
using System.Data;

namespace HandHeldInventoryAPI_Core.Db
{
    public interface IDataAccess
    {
        DataSet ExecuteProcedureReturnDataSet(string dbConn, string procName, params SqlParameter[] parameters);
        DataTable ExecuteProcedureReturnDataTable(string connectionStringName, string procName, params SqlParameter[] parameters);
        Task<DataTable> AsyncExecuteProcedureReturnDataTable(string connectionStringName, string procName, params SqlParameter[] parameters);
        List<SqlParameter> ExecuteProcedureReturnOutputParameters(string connectionStringName, string procName, params SqlParameter[] parameters);
    }
}
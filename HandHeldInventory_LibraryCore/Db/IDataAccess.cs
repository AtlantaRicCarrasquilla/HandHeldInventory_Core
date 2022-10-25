using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHeldInventory_LibraryCore.Db
{
    public interface IDataAccess
    {
        DataSet ExecuteProcedureReturnDataSet(string dbConn, string procName, params SqlParameter[] parameters);
        DataTable ExecuteProcedureReturnDataTable(string connectionStringName, string procName, params SqlParameter[] parameters);
        Task<DataTable> AsyncExecuteProcedureReturnDataTable(string connectionStringName, string procName, params SqlParameter[] parameters);
        List<SqlParameter> ExecuteProcedureReturnOutputParameters(string connectionStringName, string procName, params SqlParameter[] parameters);
    }
}

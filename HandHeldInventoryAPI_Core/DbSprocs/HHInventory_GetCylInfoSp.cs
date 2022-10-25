using HandHeldInventoryAPI_Core.DataModels;
using HandHeldInventoryAPI_Core.Db;
using HandHeldInventoryAPI_Core.Utilities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HandHeldInventoryAPI_Core.DbSprocs
{
    public class HHInventory_GetCylInfoSp : IHHInventory_GetCylInfoSp
    {
        private IDataAccess _dataAccess;
        private ConnectionStringData _connectionString;

        public HHInventory_GetCylInfoSp(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }
        public List<HHInventory_GetCylInfoDM> SP_GetCylinderItems(string barcode)
        {
            List<HHInventory_GetCylInfoDM> dmList = new List<HHInventory_GetCylInfoDM>();
            BindDataUtility bdu = new BindDataUtility();
            SqlParameter outFlag = new SqlParameter("@outMessage", SqlDbType.Int);
            outFlag.Value = 0;
            outFlag.Direction = ParameterDirection.Output;

            SqlParameter outMessage = new SqlParameter("@outFlag", SqlDbType.Int);
            outMessage.Value = 0;
            outMessage.Direction = ParameterDirection.Output;

            SqlParameter[] parameters = {
                new SqlParameter("@inBarcode",barcode)
                , new SqlParameter("@outFlag",null)
                , new SqlParameter("@outMessage",null)
            };
            parameters[1].Direction = ParameterDirection.Output;
            parameters[1].SqlDbType = SqlDbType.Int;
            parameters[1].Size = 255;
            parameters[1].Direction = ParameterDirection.Output;

            parameters[2].SqlDbType = SqlDbType.VarChar;
            parameters[2].Size = 255;
            parameters[2].Direction = ParameterDirection.Output;

            DataSet ds = _dataAccess.ExecuteProcedureReturnDataSet(_connectionString.SqlConnectionName, "HHInventory_GetCylinder", parameters);
            DataTable results = ds.Tables[0];
            DataTable outParameters = ds.Tables[1];
            if (results.Rows.Count != 0)
            {
                dmList = bdu.BindDatatableToList<HHInventory_GetCylInfoDM>(results);

                dmList[0].outFlag = outParameters.Rows[1].ItemArray[1].ToString();
                dmList[0].outMessage = outParameters.Rows[2].ItemArray[1].ToString();
            }
            return dmList;
        }
        public List<HHInventory_GetCylInfoDM> SP_GetCylinderItemsOutput(string barcode)
        {
            List<HHInventory_GetCylInfoDM> dmList = new List<HHInventory_GetCylInfoDM>();
            SqlParameter outFlag = new SqlParameter("@outFlag", SqlDbType.Int);
            outFlag.Value = 0;
            outFlag.Direction = ParameterDirection.Output;

            SqlParameter outMessage = new SqlParameter("@outFlag", SqlDbType.Int);
            outMessage.Value = 0;
            outMessage.Direction = ParameterDirection.Output;

            SqlParameter[] parameters = {
                new SqlParameter("@inBarcode",barcode)
                , new SqlParameter("@outFlag",null)
                , new SqlParameter("@outMessage",null)
            };
            parameters[1].Direction = ParameterDirection.Output;
            parameters[1].SqlDbType = SqlDbType.Int;
            parameters[1].Size = 255;
            parameters[1].Direction = ParameterDirection.Output;

            parameters[2].SqlDbType = SqlDbType.VarChar;
            parameters[2].Size = 255;
            parameters[2].Direction = ParameterDirection.Output;

            List<SqlParameter> outParamaters = _dataAccess.ExecuteProcedureReturnOutputParameters(_connectionString.SqlConnectionName, "HHInventory_getCylInfo", parameters);

            DataSet ds = _dataAccess.ExecuteProcedureReturnDataSet(_connectionString.SqlConnectionName, "HHInventory_getCylInfo", parameters);
            DataTable results = ds.Tables[0];
            if (results.Rows.Count != 0)
            {
                BindDataUtility bdu = new BindDataUtility();
                dmList = bdu.BindDatatableToList<HHInventory_GetCylInfoDM>(results);
            }

            return dmList;
        }
    }
}

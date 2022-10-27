using HandHeldInventory_LibraryCore.Db;
using HandHeldInventory_LibraryCore.Utilities;
using HandHeldInventory_LibraryCore.ViewModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHeldInventory_LibraryCore.DbSprocs
{
    public class HHInventory_GetCylinderSP : IHHInventory_GetCylinderSP
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public HHInventory_GetCylinderSP(IDataAccess dataAccess, ConnectionStringData connectionStringData)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionStringData;
        }
        public List<HHInventory_GetCylinderVM> SP_HHInventory_GetCylinder(string barcode)
        {
            List<HHInventory_GetCylinderVM> vm = new List<HHInventory_GetCylinderVM>();

            SqlParameter outFlag = new SqlParameter("@outFlag", SqlDbType.Int);
            outFlag.Value = 0;
            outFlag.Size = 255;
            outFlag.Direction = ParameterDirection.Output;

            SqlParameter outMessage = new SqlParameter("@outMessage", SqlDbType.VarChar);
            outMessage.Value = 0;
            outMessage.Size = 255;
            outMessage.Direction = ParameterDirection.Output;

            SqlParameter[] parameters = {
                new SqlParameter("@inBarcode",barcode)
                , outFlag
                , outMessage
            };

            DataSet ds = _dataAccess.ExecuteProcedureReturnDataSet(_connectionString.SqlConnectionName, "HHInventory_GetCylinder", parameters);
            DataTable dt = ds.Tables[0];
            DataTable outParameters = ds.Tables[1];
            vm = Converter.DataConvertToListObj<HHInventory_GetCylinderVM>(dt);
            vm[0].OutFlag = Convert.ToInt32(outParameters.Rows[1].ItemArray[1]);
            vm[0].OutMessage = Convert.ToString(outParameters.Rows[2].ItemArray[1]);
            return vm;
        }
    }
}

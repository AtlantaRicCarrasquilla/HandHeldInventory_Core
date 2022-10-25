using HandHeldInventory_LibraryCore.Db;
using HandHeldInventory_LibraryCore.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHeldInventory_LibraryCore.DbSprocs
{
    public class HHInventory_UpdatePhysicalInventoryTblSP
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;
        public HHInventory_UpdatePhysicalInventoryTblSP(IDataAccess dataAccess, ConnectionStringData connectionStringData)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionStringData;
        }

        public string SP_HHInventory_UpdatePhysicalInventoryTbl(HHInventory_UpdatePhysicalInventoryDTO dto)
        {
            SqlParameter outId = new SqlParameter("@outId", SqlDbType.Int);
            outId.Value = 0;
            outId.Direction = ParameterDirection.Output;
            outId.Size = 255;

            SqlParameter[] parameters = {
                new SqlParameter("@inBarcode",dto.Barcode)
                , new SqlParameter("@inCUBr",dto.CUBr)
                , new SqlParameter("@inReturnLocID",dto.ReturnLocId)
                , new SqlParameter("@inBinID",dto.BinId)
                , new SqlParameter("@inScanWho",dto.ScanWho)
                , new SqlParameter("@inDbItem",dto.DbItem)
                , new SqlParameter("@inDbCylStatusID",dto.DbCylStatusId)
                , new SqlParameter("@inDbCustOwned",dto.DbCustOwned)
                , new SqlParameter("@inDbCustNo",dto.DbCustNo)
                , new SqlParameter("@inDbGasID",dto.DbGasId)
                , new SqlParameter("@inDbGasName",dto.DbGasName)
                , new SqlParameter("@inDbMixedGasPrimeCompID",dto.DbMixedGasPrimeCompID)
                , new SqlParameter("@inDbMixedGasPrimeCompName",dto.DbMixedGasPrimeCompName)
                , new SqlParameter("@inDbPurity",dto.DbPurity)
                , new SqlParameter("@inDbNet",dto.DbNet)
                , new SqlParameter("@inDbNetBoilOff",dto.DbNetBoilOff)
                , new SqlParameter("@inDbEmpty",dto.DbEmpty)
                , new SqlParameter("@inActualItem",dto.ActualItem)
                , new SqlParameter("@inActualCylStatusID",dto.ActualCylStatusID)
                , new SqlParameter("@inActualCustOwned",dto.ActualCustOwned)
                , new SqlParameter("@inActualEmpty",dto.ActualEmpty)
                , new SqlParameter("@inActualGasID",dto.ActualGasID)
                , new SqlParameter("@inActualMixedGasPrimeCompID",dto.ActualMixedGasPrimeCompID)
                , new SqlParameter("@inActualPurity",dto.ActualPurity)
                , new SqlParameter("@inActualNetBoilOff",dto.ActualNetBoilOff)
                , new SqlParameter("@inActualGasName",dto.ActualGasName)
                , new SqlParameter("@inActualMixedGasPrimeCompName",dto.ActualMixedGasPrimeCompName)
                , new SqlParameter("@inComment",dto.Comment)
                ,outId
            };

            List<SqlParameter> outParam = _dataAccess.ExecuteProcedureReturnOutputParameters(_connectionString.SqlConnectionName, "HHInventory_UpdatePhysicalInventoryTbl", parameters);
            return Convert.ToString(outParam[28].SqlValue); ;
        }
    }
}

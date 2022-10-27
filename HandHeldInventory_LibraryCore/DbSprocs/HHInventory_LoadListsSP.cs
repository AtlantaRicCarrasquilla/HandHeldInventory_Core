using HandHeldInventory_LibraryCore.DataModels;
using HandHeldInventory_LibraryCore.Db;
using HandHeldInventory_LibraryCore.Utilities;
using HandHeldInventory_LibraryCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHeldInventory_LibraryCore.DbSprocs
{
    public class HHInventory_LoadListsSP : IHHInventory_LoadListsSP
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionString;

        public HHInventory_LoadListsSP(IDataAccess dataAccess, ConnectionStringData connectionStringData)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionStringData;
        }

        public HHInventory_LoadListsVM SP_HHInventory_LoadLists()
        {
            HHInventory_LoadListsVM vm = new HHInventory_LoadListsVM();

            DataSet ds = _dataAccess.ExecuteProcedureReturnDataSet(_connectionString.SqlConnectionName, "HHInventory_LoadLists", null);
            DataTable cylinderSizesDT = ds.Tables[0];
            DataTable cylinderStatusesDT = ds.Tables[1];
            DataTable gasesDT = ds.Tables[2];

            vm.CylinderSizes = Converter.DataConvertToListObj<CylinderSize>(cylinderSizesDT);
            vm.CylinderStatues = Converter.DataConvertToListObj<CylinderStatus>(cylinderStatusesDT);
            vm.Gases = Converter.DataConvertToListObj<Gas>(gasesDT);

            return vm;
        }
    }
}

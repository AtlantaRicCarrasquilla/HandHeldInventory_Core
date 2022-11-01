using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandHeldInventory_LibraryCore.DataModels;
using HandHeldInventory_LibraryCore.Db;
using HandHeldInventory_LibraryCore.DbSprocs;
using HandHeldInventory_LibraryCore.ViewModels;
using HandHeldInventory_LibraryCore.DTOs;

namespace HandHeldInventoryConsole_Core
{
    class Program
    {
        public static IConfiguration? _config;
        public static SqlDataAccess? _sqldb;
        public static ConnectionStringData? _conn;
        static void Main(string[] args)
        {
            GetAppSettingsFile();
            //SP_HHInventory_LoadLists();
            SP_HHInventory_GetCylinder("000176");
            HHInventory_UpdatePhysicalInventoryDTO dto = new HHInventory_UpdatePhysicalInventoryDTO();
            dto.Barcode = "000176";
            dto.CUBr = 1;
            dto.ScanWho = "ric";
            dto.ReturnLocId = 9;
            dto.BinId = 1;
            dto.DbItem = "test";
            dto.DbCylStatusId = 9;
            dto.DbCustOwned = "N";
            dto.DbCustNo = "test";
            dto.DbGasId = 0;
            dto.DbGasName = "Empty";
            dto.DbMixedGasPrimeCompID = 0;
            dto.DbMixedGasPrimeCompName = "Empty";
            dto.DbPurity = 0;
            dto.DbNet = 0;
            dto.DbNetBoilOff = 0;
            dto.DbEmpty = "Y";
            dto.ActualItem = "test2";
            dto.ActualCylStatusID = 0;
            dto.ActualCustOwned = "Y";
            dto.ActualEmpty = "N";
            dto.ActualGasID = 0;
            dto.ActualGasName = "Empty";
            dto.ActualMixedGasPrimeCompID = 0;
            dto.ActualGasName = "Empty";
            dto.ActualPurity = 0;
            dto.ActualNetBoilOff = 0;
            dto.ActualMixedGasPrimeCompName = "Empty";
            dto.Comment = "this is a test";
            //SP_HHInventory_UpdatePhysicalInventoryTbl(dto);
            Console.ReadLine();
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _config = builder.Build();
            _sqldb = new SqlDataAccess(_config);
            _conn = new ConnectionStringData();
        }
        static void SP_HHInventory_UpdatePhysicalInventoryTbl(HHInventory_UpdatePhysicalInventoryDTO dto)
        {
            HHInventory_UpdatePhysicalInventoryTblSP sp = new HHInventory_UpdatePhysicalInventoryTblSP(_sqldb, _conn);
            string result = sp.SP_HHInventory_UpdatePhysicalInventoryTbl(dto);
            Console.WriteLine(result);
        }
        static void SP_HHInventory_LoadLists()
        {
            HHInventory_LoadListsSP sp = new HHInventory_LoadListsSP(_sqldb, _conn);
            HHInventory_LoadListsVM vm = sp.SP_HHInventory_LoadLists();

            vm.CylinderSizes.ForEach(item =>
            {
                Console.WriteLine($"Id {item.Id}");
                Console.WriteLine($"Cylinder Size Id {item.CylSizeId}");
            });
        }
        static void SP_HHInventory_GetCylinder(string barcode)
        {
            HHInventory_GetCylinderSP sp = new HHInventory_GetCylinderSP(_sqldb, _conn);
            IEnumerable<HHInventory_GetCylinderVM> vm = sp.SP_HHInventory_GetCylinder(barcode);

            foreach (HHInventory_GetCylinderVM vmItem in vm)
            {
                Console.WriteLine($"Barcode : {vmItem.Barcode}");
                Console.WriteLine($"ItemNum : {vmItem.ItemNum}");
                Console.WriteLine($"Cylinder Status : {vmItem.CylStatus}");
                Console.WriteLine($"Out Flag : {vmItem.OutFlag}");
                Console.WriteLine($"Out Message : {vmItem.OutMessage}");
            }
        }
    }
}

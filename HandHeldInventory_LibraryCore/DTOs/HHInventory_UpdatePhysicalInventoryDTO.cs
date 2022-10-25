using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHeldInventory_LibraryCore.DTOs
{
    public class HHInventory_UpdatePhysicalInventoryDTO
    {
        public string? Barcode { get; set; }
        public int CUBr { get; set; }
        public Int64 ReturnLocId { get; set; }
        public Int64 BinId { get; set; }
        public string? ScanWho { get; set; }
        public string? DbItem { get; set; }
        public int DbCylStatusId { get; set; }
        public string? DbCustNo { get; set; }
        public string? DbCustOwned { get; set; }
        public int DbGasId { get; set; }
        public string? DbGasName { get; set; }
        public int DbMixedGasPrimeCompID { get; set; }
        public string? DbMixedGasPrimeCompName { get; set; }
        public decimal DbPurity { get; set; } = 0;
        public decimal DbNet { get; set; } = 0;
        public decimal DbNetBoilOff { get; set; } = 0;
        public string? DbEmpty { get; set; }
        public string? ActualItem { get; set; }
        public int ActualCylStatusID { get; set; }
        public string? ActualCustOwned { get; set; }
        public string? ActualEmpty { get; set; }
        public int ActualGasID { get; set; } = 0;
        public int ActualMixedGasPrimeCompID { get; set; } = 0;
        public decimal ActualPurity { get; set; } = 0;
        public decimal ActualNetBoilOff { get; set; } = 0;
        public string? ActualGasName { get; set; }
        public string? ActualMixedGasPrimeCompName { get; set; }
        public string? Comment { get; set; }
    }
}

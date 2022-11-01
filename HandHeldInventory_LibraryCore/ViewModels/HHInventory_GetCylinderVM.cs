using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHeldInventory_LibraryCore.ViewModels
{
    public class HHInventory_GetCylinderVM
    {
        public string? Barcode { get; set; }
        public string? ItemNum { get; set; }
        public int CylStatus { get; set; }
        public string? StatusName { get; set; }
        public string? CustOwned { get; set; }
        public string? CustId { get; set; }
        public string? BusinessAddr { get; set; }
        public int GasId { get; set; } = 0;
        public string? GasName { get; set; }
        public decimal Net { get; set; } = 0;
        public decimal NetBoilOff { get; set; } = 0;
        public int MixedGasPrimeCompId { get; set; } = 0;
        public string? MixedGasPrimeCompName { get; set; }
        public decimal Purity { get; set; } = 0;
        public int OutFlag { get; set; } = 0;
        public string? OutMessage { get; set; }

    }
}

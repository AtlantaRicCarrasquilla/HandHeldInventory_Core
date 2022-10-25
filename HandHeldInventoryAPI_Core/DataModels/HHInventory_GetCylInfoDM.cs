namespace HandHeldInventoryAPI_Core.DataModels
{
    public class HHInventory_GetCylInfoDM
    {
        public string Barcode { get; set; }
        public string ItemNum { get; set; }
        public string StatusName { get; set; }
        public string CustOwned { get; set; }
        public string CustId { get; set; }
        public string BusinessAddr { get; set; }
        public int GasId { get; set; }
        public string GasName { get; set; }
        public decimal Net { get; set; }
        public decimal NetBoilOff { get; set; }
        public int MixedGasPrimeCompId { get; set; }
        public string MixedGasPrimeCompName { get; set; }
        public decimal Purity { get; set; }
        public string outFlag { get; set; }
        public string outMessage { get; set; }
    }
}

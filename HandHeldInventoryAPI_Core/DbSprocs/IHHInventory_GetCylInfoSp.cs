using HandHeldInventoryAPI_Core.DataModels;

namespace HandHeldInventoryAPI_Core.DbSprocs
{
    public interface IHHInventory_GetCylInfoSp
    {
        List<HHInventory_GetCylInfoDM> SP_GetCylinderItems(string barcode);
    }
}
using HandHeldInventory_LibraryCore.ViewModels;

namespace HandHeldInventory_LibraryCore.DbSprocs
{
    public interface IHHInventory_GetCylinderSP
    {
        List<HHInventory_GetCylinderVM> SP_HHInventory_GetCylinder(string barcode);
    }
}
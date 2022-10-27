using HandHeldInventory_LibraryCore.DTOs;

namespace HandHeldInventory_LibraryCore.DbSprocs
{
    public interface IHHInventory_UpdatePhysicalInventoryTblSP
    {
        string SP_HHInventory_UpdatePhysicalInventoryTbl(HHInventory_UpdatePhysicalInventoryDTO dto);
    }
}
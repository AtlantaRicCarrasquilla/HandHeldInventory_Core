using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HandHeldInventory_LibraryCore.DbSprocs;
using HandHeldInventory_LibraryCore.ViewModels;
using HandHeldInventory_LibraryCore.DTOs;

namespace HHI_APICoreWithLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HH_Inventory : ControllerBase
    {
        private readonly IHHInventory_LoadListsSP hHInventory_LoadListsSP;
        private readonly IHHInventory_GetCylinderSP hHInventory_GetCylinderSP;
        private readonly IHHInventory_UpdatePhysicalInventoryTblSP hHInventory_UpdatePhysicalInventoryTblSP;

        public HH_Inventory(IHHInventory_LoadListsSP _hHInventory_LoadListsSP, IHHInventory_GetCylinderSP hHInventory_GetCylinderSP, IHHInventory_UpdatePhysicalInventoryTblSP hHInventory_UpdatePhysicalInventoryTblSP)
        {
            hHInventory_LoadListsSP = _hHInventory_LoadListsSP;
            this.hHInventory_GetCylinderSP = hHInventory_GetCylinderSP;
            this.hHInventory_UpdatePhysicalInventoryTblSP = hHInventory_UpdatePhysicalInventoryTblSP;
        }

        [HttpPost]
        [Route("HHInventory_LoadLists")]
        public HHInventory_LoadListsVM HHInventory_LoadLists()
        {
            return hHInventory_LoadListsSP.SP_HHInventory_LoadLists();
        }

        [HttpPost]
        [Route("HHInventory_GetCylinder")]
        public List<HHInventory_GetCylinderVM> HHInventory_GetCylinder(string barcode)
        {
            return hHInventory_GetCylinderSP.SP_HHInventory_GetCylinder(barcode);
        }

        [HttpPost]
        [Route("HHInventory_UpdatePhysicalInventoryTbl")]
        public string HHInventory_UpdatePhysicalInventoryTbl(HHInventory_UpdatePhysicalInventoryDTO dto)
        {
            return hHInventory_UpdatePhysicalInventoryTblSP.SP_HHInventory_UpdatePhysicalInventoryTbl(dto);
        }
    }
}

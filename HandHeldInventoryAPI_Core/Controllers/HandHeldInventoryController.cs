using HandHeldInventoryAPI_Core.DataModels;
using HandHeldInventoryAPI_Core.DbSprocs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HandHeldInventoryAPI_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandHeldInventoryController : ControllerBase
    {
        private readonly IHHInventory_GetCylInfoSp _HHInventory_GetCylInfoSp;
        public HandHeldInventoryController(IHHInventory_GetCylInfoSp hhInventory_GetCylInfoSp)
        {
            _HHInventory_GetCylInfoSp = hhInventory_GetCylInfoSp;
        }

        [HttpPost]
        [Route("GetCylinderItems")]
        public List<HHInventory_GetCylInfoDM> GetCylinderItems(string barcode)
        {
            return _HHInventory_GetCylInfoSp.SP_GetCylinderItems(barcode);
        }

    }
}

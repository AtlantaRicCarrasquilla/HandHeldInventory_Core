using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HHI_APICoreWithLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HH_Inventory : ControllerBase
    {

        [HttpPost]
        [Route("HHInventory_GetCylinder")]
        public string HHInventory_GetCylinder()
        {
            return "Hello World";
        }
    }
}

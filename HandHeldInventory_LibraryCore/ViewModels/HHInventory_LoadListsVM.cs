using HandHeldInventory_LibraryCore.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHeldInventory_LibraryCore.ViewModels
{
    public class HHInventory_LoadListsVM
    {
        public List<CylinderSize> CylinderSizes { get; set; }
        public List<CylinderStatus> CylinderStatues { get; set; }
        public List<Gas> Gases { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandHeldInventory_LibraryCore.Utilities
{
    static class Converter
    {
        public static List<T> DataConvertToListObj<T>(DataTable dt)
        {
            BindDataUtility bdu = new BindDataUtility();
            List<T> list = new List<T>();
            if (dt.Rows.Count != 0)
            {
                list = bdu.BindDatatableToList<T>(dt);
            }
            return list;
        }
    }
}

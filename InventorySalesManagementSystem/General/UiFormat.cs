using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySalesManagementSystem.General
{
    static public class UiFormat
    {
        public static string FormatNullableValue(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "----" : value;
        }
    }
}

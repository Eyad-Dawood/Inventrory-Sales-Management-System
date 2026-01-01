using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services.Helpers
{
    public class NullMapperHelper
    {
        public static string? NormalizeString(string? input)
        {
            return string.IsNullOrWhiteSpace(input) ? null : input.Trim();
        }
    }
}

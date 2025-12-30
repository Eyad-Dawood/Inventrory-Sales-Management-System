using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Utilities
{
    static public class EnumExtinsions
    {
        public static string GetDisplayName(this Enum value)
        {
            var member = value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault();

            return member?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName()
                ?? value.ToString();
        }

    }
}

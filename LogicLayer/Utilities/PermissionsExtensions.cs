using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Utilities
{
    public static class PermissionsExtensions
    {
        public static List<string> ToDisplayNames(this Permission Permissions)
        {
            return Enum.GetValues(typeof(Permission))
                .Cast<Permission>()
                .Where(p =>  (Permissions & p) == p)
                .Select(p => p.GetDisplayName())
                .ToList();
        }

        public static string ToDisplayText(this Permission Permissions)
        {
            return string.Join(" ، ", Permissions.ToDisplayNames());
        }

    }
}

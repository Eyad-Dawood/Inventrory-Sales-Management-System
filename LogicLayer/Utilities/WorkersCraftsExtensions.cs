using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Utilities
{
    public static class WorkersCraftsExtensions
    {
        public static List<string> ToDisplayNames(this WorkersCraftsEnum WorkersCrafts)
        {
            return Enum.GetValues(typeof(WorkersCraftsEnum))
                .Cast<WorkersCraftsEnum>()
                //Dont Get None
                .Where(p => (int)p > 0 && (WorkersCrafts & p) == p)
                .Select(p => p.GetDisplayName())
                .ToList();
        }

        public static string ToDisplayText(this WorkersCraftsEnum WorkersCrafts)
        {
            return string.Join(" و ", WorkersCrafts.ToDisplayNames());
        }
    }
}

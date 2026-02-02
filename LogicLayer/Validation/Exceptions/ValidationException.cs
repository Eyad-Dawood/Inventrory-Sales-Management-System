using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Validation.Exceptions
{
    public class ValidationException : Exception
    {
        public IReadOnlyList<string> Errors { get; }

        public ValidationException(IEnumerable<string> errors)
            : base("فشل التحقق")
        {
            Errors = errors.ToList();
        }

        public ValidationException(string error)
            : base("فشل التحقق")
        {
            Errors = new List<string>() { error };
        }
    }
}

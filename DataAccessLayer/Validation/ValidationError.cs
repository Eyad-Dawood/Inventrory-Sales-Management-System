using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Validation
{
    public enum ValidationErrorCode
    {
        RequiredFieldMissing,
        ValueOutOfRange,
        InvalidFormat,
        DuplicateEntry
    }

    public class ValidationError
    {
        public Type ObjectType { get; set; }
        public string PropertyName { get; set; }
        public ValidationErrorCode Code { get; set; }
    }
}

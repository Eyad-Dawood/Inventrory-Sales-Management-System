using DataAccessLayer.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions
{
    public interface IValidatable
    {
        bool Validate(List<ValidationError> errors);
    }
}

using DataAccessLayer.Abstractions;
using DataAccessLayer.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Validation.Exceptions;

namespace LogicLayer.Validation
{
    public static class ValidationHelper
    {
        public static void ValidateEntity<T>(T entity)
            where T : IValidatable
        {
            if (entity == null)
                throw new NotFoundException(typeof(T));

            List<ValidationError> errors = new();

            if (!entity.Validate(errors))
            {
                var errorManager = new ErrorMessagesManager();
                var textErrors = errorManager.WriteValidationErrorsInArabic(errors);

                throw new ValidationException(textErrors);
            }
        }
    }
}

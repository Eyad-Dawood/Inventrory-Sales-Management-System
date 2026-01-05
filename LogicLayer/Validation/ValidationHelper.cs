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

      
        /// <exception cref="ArgumentNullException">
        /// Thrown when the provided entity is null.
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when the entity fails validation rules.
        /// </exception>
        public static void ValidateEntity<T>(T entity)
            where T : IValidatable
        {
            if (entity == null)
                throw new ArgumentNullException();

            List<ValidationError> errors = new();

            if (!entity.Validate(errors))
            {
                var textErrors = ErrorMessagesManager.WriteValidationErrorsInArabic(errors);

                throw new ValidationException(textErrors);
            }
        }

       
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the provided Values out Of Range
        /// </exception>
        public static void ValidatePageginArguments(int PageNumber,int RowsPerPage)
        {
            ValidatePageNumber(PageNumber);

            ValidateRowsPerPage(RowsPerPage);
        }
        public static void ValidatePageNumber(int PageNumber)
        {
            if (PageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(PageNumber));
            }
        }
        public static void ValidateRowsPerPage(in int RowsPerPage)
        {
            if (RowsPerPage < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(RowsPerPage));
            }
        }
    }
}

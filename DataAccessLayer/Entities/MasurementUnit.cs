using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    [DisplayName("وحدة القياس")]
    public class MasurementUnit : IValidatable
    {
        [Key]
        public int MasurementUnitId { get; set; }
        [MaxLength(300)]
        [DisplayName("إسم وحدة القياس")]
        public string UnitName { get; set; }

        public bool Validate(List<ValidationError> errors)
        {
            //Validate Name
            if (String.IsNullOrWhiteSpace(UnitName))
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(ProductType),
                        PropertyName = nameof(UnitName),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    }
                    );
            }

            if (UnitName.Length > 300)
            {
                errors.Add(
                new ValidationError
                {
                    ObjectType = typeof(ProductType),
                    PropertyName = nameof(UnitName),
                    Code = ValidationErrorCode.ValueOutOfRange
                }
                );
            }

            return errors.Count == 0;
        }
    }
}

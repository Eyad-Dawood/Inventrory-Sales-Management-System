using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Validation;
using System.ComponentModel;
using DataAccessLayer.Abstractions;

namespace DataAccessLayer.Entities.Products
{
    [Display(Name= "تصنيف المنتج")]
    public class ProductType : IValidatable
    {
        [Key]
        public int ProductTypeId { get; set; }
        [MaxLength(300)]
        [Display(Name= "نوع المنتج")]
        public string ProductTypeName { get; set; }

        public bool Validate(List<ValidationError> errors)
        {
            //Validate Name
            if (string.IsNullOrWhiteSpace(ProductTypeName))
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(ProductType),
                        PropertyName = nameof(ProductTypeName),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    }
                    );
            }

            if (ProductTypeName.Length > 300)
            {
                errors.Add(
                new ValidationError
                {
                    ObjectType = typeof(ProductType),
                    PropertyName = nameof(ProductTypeName),
                    Code = ValidationErrorCode.ValueOutOfRange
                }
                );
            }

            return errors.Count == 0;
        }

    }
}

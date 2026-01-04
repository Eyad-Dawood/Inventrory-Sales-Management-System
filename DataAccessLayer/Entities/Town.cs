using DataAccessLayer.Abstractions;
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
    [Display(Name= "البلد/المدينة")]
    public class Town : IValidatable
    {
        [Key]
        public int TownID { get; set; }

        [MaxLength(50)]
        [Display(Name= "إسم البلد/المدينة")]
        public string TownName { get; set; }

        public bool Validate(List<ValidationError> errors)
        {
            // Validate Town Name
            if (string.IsNullOrWhiteSpace(TownName))
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Town),
                        PropertyName = nameof(TownName),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    }
                    );
            }
            return errors.Count == 0;

        }
    }
}

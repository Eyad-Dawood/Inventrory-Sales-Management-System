using DataAccessLayer.Abstractions;
using DataAccessLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    [DisplayName("الشخص")]
    public class Person : IValidatable
    {
        [Key]
        public int PersonId { get; set; }

        [MaxLength(50)]
        [DisplayName("الإسم الأول")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [DisplayName("الإسم الثاني")]
        public string SecondName { get; set; }

        [MaxLength(50)]
        [DisplayName("الإسم الثالث")]
        public string? ThirdName { get; set; }

        [MaxLength(50)]
        [DisplayName("الإسم الرابع")]
        public string? FourthName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                List<string> names = new List<string> { FirstName, SecondName };
                if (!string.IsNullOrWhiteSpace(ThirdName))
                    names.Add(ThirdName);
                if (!string.IsNullOrWhiteSpace(FourthName))
                    names.Add(FourthName);
                return string.Join(" ", names);
            }
        }

        [MaxLength(14)]
        [DisplayName("رقم البطاقة")]
        public string? NationalNumber { get; set; }

        [MaxLength(11)]
        [DisplayName("رقم الهاتف")]
        public string? PhoneNumber { get; set; }


        [ForeignKey(nameof(Town))]
        public int TownID { get; set; }
        [DisplayName("بيانات المدينة")]
        public Town Town { get; set; }


        public bool Validate(List<ValidationError> errors)
        {
            // Validate First Name
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Person),
                        PropertyName = nameof(FirstName),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    }
                    );
            }
            if (FirstName.Length>50)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Person),
                        PropertyName = nameof(FirstName),
                        Code = ValidationErrorCode.ValueOutOfRange
                    }
                    );
            }

            //Validate Second Name
            if (string.IsNullOrWhiteSpace(SecondName))
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Person),
                        PropertyName = nameof(SecondName),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    }
                    );
            }
            if (SecondName.Length > 50)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Person),
                        PropertyName = nameof(SecondName),
                        Code = ValidationErrorCode.ValueOutOfRange
                    }
                    );
            }

            //validate Third Name
            if (!string.IsNullOrWhiteSpace(ThirdName) && ThirdName.Length > 50)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Person),
                        PropertyName = nameof(ThirdName),
                        Code = ValidationErrorCode.ValueOutOfRange
                    }
                    );
            }

            //validate Fourth Name
            if (!string.IsNullOrWhiteSpace(FourthName) && FourthName.Length > 50)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Person),
                        PropertyName = nameof(FourthName),
                        Code = ValidationErrorCode.ValueOutOfRange
                    }
                    );
            }

            //validate National Number
            if (!string.IsNullOrWhiteSpace(NationalNumber) && NationalNumber.Length > 14)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Person),
                        PropertyName = nameof(NationalNumber),
                        Code = ValidationErrorCode.ValueOutOfRange
                    }
                    );
            }

            //validate Phone Number
            if (!string.IsNullOrWhiteSpace(PhoneNumber) && PhoneNumber.Length > 11)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Person),
                        PropertyName = nameof(PhoneNumber),
                        Code = ValidationErrorCode.ValueOutOfRange
                    }
                    );
            }

            //validate Town
            if(Town==null)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Person),
                        PropertyName = nameof(Town),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    }
                    );
            }

            return errors.Count == 0;
        }
    }
}

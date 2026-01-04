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
    public enum Permission
    {
        [Display(Name = "كل الصلاحيات")]
        Admin = 1,

        [Display(Name = "عرض")]
        View = 2,

        [Display(Name = "إضافة")]
        Add = 4,

        [Display(Name = "تعديل")]
        Edit = 8,

        [Display(Name = "حذف")]
        Delete = 16
    }

    [Display(Name= "المستخدم")]
    public class User : IValidatable
    {
        [Key]
        public int UserId { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(255)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Permission Permissions { get; set; }

        public bool Validate(List<ValidationError> errors)
        {
            //Validate UserName
            if(string.IsNullOrWhiteSpace(Username))
            {
                errors.Add(new ValidationError
                {
                    ObjectType = typeof(User),
                    PropertyName = nameof(Username),
                    Code = ValidationErrorCode.RequiredFieldMissing
                });
            }

            //Validate Password
            if (string.IsNullOrWhiteSpace(Password))
            {
                errors.Add(new ValidationError
                {
                    ObjectType = typeof(User),
                    PropertyName = nameof(Password),
                    Code = ValidationErrorCode.RequiredFieldMissing
                });
            }
            if(Password.Length>255)
            {
                errors.Add(new ValidationError
                {
                    ObjectType = typeof(User),
                    PropertyName = nameof(Password),
                    Code = ValidationErrorCode.ValueOutOfRange
                });
            }

            return errors.Count == 0;
        }
    }
}

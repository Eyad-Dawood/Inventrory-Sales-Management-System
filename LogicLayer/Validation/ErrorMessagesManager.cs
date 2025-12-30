using DataAccessLayer.Entities;
using DataAccessLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Validation
{
    public class ErrorMessagesManager
    {
        private string GetArabicPropertyName(Type ObjectType,string propertyName)
        {
            var displayName = ObjectType
                .GetProperty(propertyName)
                ?.GetCustomAttribute<DisplayAttribute>()
                ?.GetName();

            return displayName ?? propertyName;
        }
        private string GetArabicEntityName(Type objectType)
        {
            var displayAttr = objectType.GetCustomAttribute<DisplayAttribute>();
            return displayAttr?.GetName() ?? objectType.Name;
        }
        public List<string> WriteValidationErrorsInArabic(List<ValidationError>Errors)
        {
            List<string> Arabicerrors = new List<string>();

            foreach(ValidationError error in Errors)
            {
                string ArabicPropertyName = GetArabicPropertyName(error.ObjectType,error.PropertyName);
                switch (error.Code)
                {
                    case ValidationErrorCode.RequiredFieldMissing:
                        Arabicerrors.Add($"الحقل {ArabicPropertyName} مطلوب.");
                        break;
                    case ValidationErrorCode.InvalidFormat:
                        Arabicerrors.Add($"الحقل {ArabicPropertyName} يحتوي على تنسيق غير صالح.");
                        break;
                    case ValidationErrorCode.ValueOutOfRange:
                        Arabicerrors.Add($"القيمة في الحقل {ArabicPropertyName} خارج النطاق المسموح.");
                        break;
                    case ValidationErrorCode.DuplicateEntry:
                        Arabicerrors.Add($"القيمة في الحقل {ArabicPropertyName} مكررة.");
                        break;
                    default:
                        Arabicerrors.Add($"حدث خطأ غير معروف في الحقل {ArabicPropertyName}.");
                        break;
                }
            }

            return Arabicerrors;
        }

        public static class ErrorMessages
        {
            static public string NotFoundErrorMessage(Type ObjectType)
            {
                ErrorMessagesManager errorMessagesManager = new ErrorMessagesManager();

                string ArabicEntityName = errorMessagesManager.GetArabicEntityName(ObjectType);
                return $"ال{ArabicEntityName} غير موجود أو يتعذر العثور عليه";
            }
        }
    }
}

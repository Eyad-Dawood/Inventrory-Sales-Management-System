using DataAccessLayer.Entities;
using DataAccessLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicLayer.Validation
{
    public class ErrorMessagesManager
    {
        static private string GetArabicPropertyName(Type ObjectType,string propertyName)
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

        static public string WriteValidationErrorMessageInArabic(ValidationError Error)
        {
            string ArabicPropertyName = GetArabicPropertyName(Error.ObjectType, Error.PropertyName);

            switch (Error.Code)
            {
                case ValidationErrorCode.RequiredFieldMissing:
                    return ($"الحقل {ArabicPropertyName} مطلوب.");

                case ValidationErrorCode.InvalidFormat:
                    return ($"الحقل {ArabicPropertyName} يحتوي على تنسيق غير صالح.");

                case ValidationErrorCode.ValueOutOfRange:
                    return ($"القيمة في الحقل {ArabicPropertyName} خارج النطاق المسموح.");
                    
                case ValidationErrorCode.DuplicateEntry:
                    return ($"القيمة في الحقل {ArabicPropertyName} مكررة.");
                    
                default:
                    return ($"حدث خطأ غير معروف في الحقل {ArabicPropertyName}.");
                   
            }
        }
        static public List<string> WriteValidationErrorsInArabic(List<ValidationError>Errors)
        {
            List<string> Arabicerrors = new List<string>();

            foreach(ValidationError error in Errors)
            {
                Arabicerrors.Add(WriteValidationErrorMessageInArabic(error));
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

            static public string WrongPasswordErrorMessage()
            {
                return "كلمة المرور خاطئة , الرجاء المحاولة مرة أخرى";
            }
        }
    }
}

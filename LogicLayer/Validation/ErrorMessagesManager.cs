using DataAccessLayer.Entities;
using DataAccessLayer.Validation;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        static private string GetArabicPropertyName(Type objectType, string propertyName)
        {
            var property = objectType.GetProperty(
                propertyName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy
            );

            if (property == null)
                return propertyName;

            // 1️ DisplayAttribute
            var displayAttr = property.GetCustomAttribute<DisplayAttribute>();
            if (displayAttr != null)
                return displayAttr.GetName();

            return propertyName;
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
            static public string OperationFailedErrorMessage()
            {
                return "حدث خطأ , راجع السجلات لتفاصيل أكثر";
            }
            static public string NotFoundErrorMessage(Type ObjectType)
            {
                ErrorMessagesManager errorMessagesManager = new ErrorMessagesManager();

                string ArabicEntityName = errorMessagesManager.GetArabicEntityName(ObjectType);
                return $"{ArabicEntityName} غير موجود أو يتعذر العثور عليه";
            }

            static public string WrongPasswordErrorMessage()
            {
                return "كلمة المرور خاطئة , الرجاء المحاولة مرة أخرى";
            }
        }
    }
}

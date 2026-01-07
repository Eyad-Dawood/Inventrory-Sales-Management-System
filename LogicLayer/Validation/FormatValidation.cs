using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicLayer.Validation
{
    public static class FormatValidation
    {
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return true;

            //Start with 01 then 0,1,2,5 Then 8 numbers
            string pattern = @"^01[0125][0-9]{8}$";

            return Regex.IsMatch(phoneNumber, pattern);
        }

        public static bool IsValidNationalNumber(string nationalNumber)
        {
            if (string.IsNullOrWhiteSpace(nationalNumber))
                return true;

            // نمط الرقم القومي المصري:
            // 1. يبدأ بـ 2 (لمواليد 1900-1999) أو 3 (لمواليد 2000-2099).
            // 2. يتبعه 6 أرقام تمثل تاريخ الميلاد (YYMMDD).
            // 3. يتبعه 7 أرقام تمثل كود المحافظة والتسلسل والنوع والرقم التدقيقي.
            string pattern = @"^[23][0-9]{13}$";

            if (!Regex.IsMatch(nationalNumber, pattern))
                return false;

            // تحسين إضافي: التحقق من منطقية تاريخ الميلاد داخل الرقم
            return ValidateDateInNationalId(nationalNumber);
        }

        private static bool ValidateDateInNationalId(string id)
        {
            try
            {
                // استخراج السنة والشهر واليوم
                int century = id[0] == '2' ? 1900 : 2000;
                int year = int.Parse(id.Substring(1, 2)) + century;
                int month = int.Parse(id.Substring(3, 2));
                int day = int.Parse(id.Substring(5, 2));

                // محاولة إنشاء تاريخ للتأكد من صحته (مثلاً لا يوجد شهر 13 أو يوم 32)
                DateTime dt = new DateTime(year, month, day);

                // التأكد من أن التاريخ ليس في المستقبل
                return dt <= DateTime.Now;
            }
            catch
            {
                return false; // إذا حدث خطأ في تحويل التاريخ، فالرقم غير صحيح
            }
        }


        public static bool IsValidDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return decimal.TryParse(value, out _);
        }

    }
}
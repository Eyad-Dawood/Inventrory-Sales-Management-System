using DataAccessLayer.Entities;
using LogicLayer.DTOs.PersonDTO;
using LogicLayer.Validation.Exceptions;

namespace LogicLayer.Validation.Custom_Validation
{
    static public class PersonFormatValidation
    {
        public static void ValidateValues(PersonAddDto addPerson)
        {
            ValidateCore(addPerson.PhoneNumber, addPerson.NationalNumber);
        }

        public static void ValidateValues(PersonUpdateDto updatePerson)
        {
            ValidateCore(updatePerson.PhoneNumber, updatePerson.NationalNumber);
        }

        private static void ValidateCore(string phoneNumber, string nationalNumber)
        {
            var errors = new List<string>();

            if (!FormatValidation.IsValidPhoneNumber(phoneNumber))
            {
                string propertyName = Utilities.NamesManager.GetArabicPropertyName(typeof(Person), nameof(Person.PhoneNumber));
                errors.Add($"{propertyName} تنسيقه غير صحيح");
            }

            if (!FormatValidation.IsValidNationalNumber(nationalNumber))
            {
                string propertyName = Utilities.NamesManager.GetArabicPropertyName(typeof(Person), nameof(Person.NationalNumber));
                errors.Add($"{propertyName} تنسيقه غير صحيح");
            }

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }
    }
}
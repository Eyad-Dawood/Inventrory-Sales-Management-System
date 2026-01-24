using DataAccessLayer.Abstractions;
using DataAccessLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities.Invoices
{
    [Display(Name = "عملية الشراء")]
    public class TakeBatch : IValidatable
    {
        [Key]
        [Display(Name = "معرف عملية الشراء")]
        public int TakeBatchId { get; set; }

        [Display(Name = "تاريخ الشراء")]
        public DateTime TakeDate { get; set; }

        [MaxLength(200)]
        [Display(Name = "إسم المشتري")]
        public string? TakeName { get; set; }


        [MaxLength(350)]
        [Display(Name = "الملاحظات")]
        public string? Notes { get; set; }


        [ForeignKey(nameof(Invoice))]
        public int InvoiceId { get; set; }
        [Display(Name = "بيانات الفاتورة")]
        public Invoice Invoice { get; set; }



        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();

        public bool Validate(List<ValidationError> errors)
        {
            // Validate TakeDate
            if (TakeDate == default)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(TakeBatch),
                        PropertyName = nameof(TakeDate),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            // Validate TakeName
            if (!string.IsNullOrWhiteSpace(TakeName) && TakeName.Length > 200)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(TakeBatch),
                        PropertyName = nameof(TakeName),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Validate Notes
            if (!string.IsNullOrWhiteSpace(Notes) && Notes.Length > 350)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(TakeBatch),
                        PropertyName = nameof(Notes),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Validate Invoice
            if (InvoiceId <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(TakeBatch),
                        PropertyName = nameof(Invoice),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            return errors.Count == 0;
        }

    }
}

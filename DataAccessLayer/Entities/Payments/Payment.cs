using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities.Payments
{
    public enum PaymentReason 
    {
        [Display(Name ="فاتورة")]
        Invoice = 1,

        [Display(Name ="مرتجع")]
        Refund = 3
    }
    [Display (Name = "المدفوعات")]
    public class Payment : IValidatable
    {
        [Key]
        [Display(Name = "معرف العملية")]
        public int PaymentId { get; set; }



        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "المبلغ")]
        public decimal Amount { get; set; }


        [Display(Name = "التاريخ")]
        public DateTime Date {  get; set; }

        [Display(Name = "الملاحظات")]
        [MaxLength(350)]
        public string? Notes { get; set; }

        [Display(Name = "السبب")]
        public PaymentReason PaymentReason { get; set; }


        [Display(Name = "من يد")]
        [MaxLength(50)]
        public string PaidBy { get; set; }

        [Display(Name = "إلى يد")]
        [MaxLength(50)]
        public string RecivedBy { get; set; }

        public int UserId { get; set; }
        [Display(Name = "بيانات المستخدم")]
        public User User { get; set; }


        public int InvoiceId { get; set; }
        [Display(Name = "بيانات الفاتورة")]
        public Invoice Invoice { get; set; }




        public int CustomerId { get; set; }
        [Display(Name = "بيانات العميل")]
        public Customer Customer { get; set; }

        public bool Validate(List<ValidationError> errors)
        {
            // Amount
            if (Amount <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Payment),
                        PropertyName = nameof(Amount),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Date
            if (Date == default)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Payment),
                        PropertyName = nameof(Date),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            // User
            if (UserId <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Payment),
                        PropertyName = nameof(User),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            // Customer
            if (CustomerId <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Payment),
                        PropertyName = nameof(Customer),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            // Notes length
            if (!string.IsNullOrWhiteSpace(Notes) && Notes.Length > 50)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Payment),
                        PropertyName = nameof(Notes),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Validate PaidBy
            if (string.IsNullOrWhiteSpace(PaidBy))
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Payment),
                        PropertyName = nameof(PaidBy),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    }
                    );
            }

            // Validate RecivedBy
            if (string.IsNullOrWhiteSpace(RecivedBy))
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Payment),
                        PropertyName = nameof(RecivedBy),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    }
                    );
            }

            //PaidBy  length
            if (!string.IsNullOrEmpty(PaidBy)&& PaidBy.Length > 50)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Payment),
                        PropertyName = nameof(PaidBy),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            //RecivedBy  length
            if (!string.IsNullOrEmpty(RecivedBy) &&RecivedBy.Length > 350)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Payment),
                        PropertyName = nameof(RecivedBy),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }
            // Invoice logic based on PaymentReason
            if (PaymentReason == PaymentReason.Invoice || PaymentReason == PaymentReason.Refund)
            {
                if (InvoiceId <= 0)
                {
                    errors.Add(
                        new ValidationError
                        {
                            ObjectType = typeof(Payment),
                            PropertyName = nameof(Invoice),
                            Code = ValidationErrorCode.RequiredFieldMissing
                        });
                }
            }

            return errors.Count == 0;
        }
    }
}

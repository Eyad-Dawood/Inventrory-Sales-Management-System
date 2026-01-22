using DataAccessLayer.Entities.Products;
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
    public class Refund
    {
        [Key]
        [Display(Name = "معرف المرتجع")]
        public int RefundId { get; set; }

        [Display(Name =  "التاريخ")]
        public DateTime DateTime { get; set; }

        [Display(Name = "الكمية")]
        [Column(TypeName = "decimal(10,4)")]
        public decimal Quantity { get; set; }

        [Display(Name = "سعر الوحدة")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal RefundPrice { get; set; }

        [Display(Name = "إسم المُرجع")]
        [MaxLength(200)]
        public string? RefundName { get; set; }

        [Display(Name = "الملاحظات")]
        [MaxLength(350)]
        public string? Notes { get; set; }


        [ForeignKey(nameof(Invoice))]
        public int InvoiceId { get; set; }
        [Display(Name = "بيانات الفاتورة")]
        public Invoice Invoice { get; set; }


        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [Display(Name = "بيانات المنتج")]
        public Product Product { get; set; }


        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [Display(Name = "بيانات المستخدم")]
        public User User { get; set; }


        public bool Validate(List<ValidationError> errors)
        {
            // Validate DateTime
            if (DateTime == default)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Refund),
                        PropertyName = nameof(DateTime),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            // Validate Quantity
            if (Quantity <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Refund),
                        PropertyName = nameof(Quantity),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Validate RefundPrice
            if (RefundPrice <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Refund),
                        PropertyName = nameof(RefundPrice),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Validate RefundName (optional but limited)
            if (!string.IsNullOrWhiteSpace(RefundName) && RefundName.Length > 200)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Refund),
                        PropertyName = nameof(RefundName),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Validate Notes (optional but limited)
            if (!string.IsNullOrWhiteSpace(Notes) && Notes.Length > 350)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Refund),
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
                        ObjectType = typeof(Refund),
                        PropertyName = nameof(Invoice),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            // Validate Product
            if (ProductId <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Refund),
                        PropertyName = nameof(Product),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            // Validate User
            if (UserId <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Refund),
                        PropertyName = nameof(User),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            return errors.Count == 0;
        }

    }
}

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
    public enum InvoiceType
    {
        [Display(Name = "تسعير")]
        Evaluation = 1,

        [Display(Name = "بيع")]
        Sale = 2
    }

    public enum InvoiceState
    {
        [Display(Name = "مفتوح")]
        Open = 1,

        [Display(Name = "مغلق")]
        Closed = 2
    }
    [Display(Name = "الفاتورة")]
    public class Invoice : IValidatable
    {
        [Key]
        [Display(Name = "معرف الفاتورة")]
        public int InvoiceId { get; set; }

        [Display(Name = "تاريخ الفتح")]
        public DateTime OpenDate { get; set; }

        [Display(Name = "تاريخ الغلق")]
        public DateTime? CloseDate { get; set; }


        #region Calculated After Every Operation
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "البيع")]
        public decimal TotalSellingPrice {  get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "الشراء")]
        public decimal TotalBuyingPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "بيع(م)")]
        public decimal TotalRefundSellingPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "شراء(م)")]
        public decimal TotalRefundBuyingPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "المدفوع")]
        public decimal TotalPaid { get; set; }
        #endregion

        /// <summary>
        /// Positive = additional charges, Negative = discount
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "خصم")]
        public decimal Discount { get; set; }


        [MaxLength(250)]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }

        [Display(Name = "النوع")]
        public InvoiceType InvoiceType { get; set; }

        [Display(Name =  "الحالة")]
        public InvoiceState InvoiceState { get; set; }


        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        [Display(Name = "بيانات العميل")]
        public Customer Customer { get; set; }

        [ForeignKey(nameof(Worker))]
        public int? WorkerId { get; set; }
        [Display(Name = "بيانات العامل")]
        public Worker? Worker { get; set; }


        [ForeignKey(nameof(OpenUser))]
        public int OpenUserId { get; set; }
        [Display(Name = "بيانات المستخدم فاتح الفاتورة")]
        public User OpenUser { get; set; }


        [ForeignKey(nameof(CloseUser))]
        public int? CloseUserId { get; set; }
        [Display(Name = "بيانات المستخدم مغلق الفاتورة")]
        public User? CloseUser { get; set; }


        public ICollection<TakeBatch> takeBatches { get; set; } = new List<TakeBatch>();

        public bool Validate(List<ValidationError> errors)
        {
            // Validate OpenDate
            if (OpenDate == default)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Invoice),
                        PropertyName = nameof(OpenDate),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            // Validate CloseDate (optional but logical)
            if (CloseDate.HasValue && CloseDate < OpenDate)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Invoice),
                        PropertyName = nameof(CloseDate),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Validate TotalSellingPrice
            if (TotalSellingPrice < 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Invoice),
                        PropertyName = nameof(TotalSellingPrice),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Validate TotalBuyingPrice
            if (TotalBuyingPrice < 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Invoice),
                        PropertyName = nameof(TotalBuyingPrice),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Validate TotalRefundPrice
            if (TotalRefundSellingPrice < 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Invoice),
                        PropertyName = nameof(TotalRefundSellingPrice),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Validate TotalPaied
            if (TotalPaid < 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Invoice),
                        PropertyName = nameof(TotalPaid),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Logical validation: Paid cannot exceed selling price
            if (TotalPaid > TotalSellingPrice)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Invoice),
                        PropertyName = nameof(TotalPaid),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Validate Customer
            if (CustomerId <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Invoice),
                        PropertyName = nameof(Customer),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            // Validate OpenUser
            if (OpenUserId <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Invoice),
                        PropertyName = nameof(OpenUser),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            // Validate Worker (optional but must be valid if provided)
            if (WorkerId.HasValue && WorkerId <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Invoice),
                        PropertyName = nameof(Worker),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Validate CloseUser (optional but must be valid if provided)
            if (CloseUserId.HasValue && CloseUserId <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Invoice),
                        PropertyName = nameof(CloseUser),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            return errors.Count == 0;
        }


    }
}

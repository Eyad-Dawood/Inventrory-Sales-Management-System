using DataAccessLayer.Abstractions;
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
    [Display(Name = "المنتج المباع")]
    public class SoldProduct : IValidatable
    {
        [Display(Name = "معرف العملية")]
        public int SoldProductId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "سعر الشراء للوحدة")]
        public decimal BuyingPricePerUnit { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "سعر البيع للوحدة")]
        public decimal SellingPricePerUnit { get; set; }


        [Column(TypeName = "decimal(10,4)")]
        [Display(Name = "الكمية")]
        public decimal Quantity {  get; set; }

        

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [Display(Name = "بيانات المنتج")]
        public Product Product { get; set; } 



        [ForeignKey(nameof(TakeBatch))]
        public int TakeBatchId { get; set; }
        [Display(Name = "بيانات عملية الشراء")]
        public TakeBatch TakeBatch { get; set; } 


        public bool Validate(List<ValidationError> errors)
        {
            // BuyingPricePerUnit
            if (BuyingPricePerUnit < 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(SoldProduct),
                        PropertyName = nameof(BuyingPricePerUnit),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // SellingPricePerUnit
            if (SellingPricePerUnit < 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(SoldProduct),
                        PropertyName = nameof(SellingPricePerUnit),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Quantity
            if (Quantity <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(SoldProduct),
                        PropertyName = nameof(Quantity),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            // Product
            if (ProductId <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(SoldProduct),
                        PropertyName = nameof(Product),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }
            
            return errors.Count == 0;
        }
    }
}

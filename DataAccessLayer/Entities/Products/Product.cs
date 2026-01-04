using DataAccessLayer.Abstractions;
using DataAccessLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities.Products
{
    public enum Status
        {
        [Display(Name ="متاح")]
        Avilable = 1,
        [Display(Name ="منتهي")]
        OutOfStock = 2,
        [Display(Name ="غير متاح")]
        NotAvilable = 3 }


    [Display(Name= "المنتج")]
    public class Product : IValidatable
    {
        [Key]
        public int ProductId { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal BuyingPrice { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal SellingPrice { get; set; }

        [NotMapped]
        public decimal Profit { get { return SellingPrice-BuyingPrice;}}


        [Column(TypeName = "decimal(10,4)")]
        public decimal QuantityInStorage { get; set; }



        [MaxLength(300)]
        [Display(Name= "إسم المنتج")]
        public string ProductName { get; set; }



        public Status Status { get; set; }



        [ForeignKey(nameof(ProductType))]
        public int ProductTypeId { get; set; }



        [Display(Name= "نوع المنتج")]
        public ProductType ProductType { get; set; }


        [ForeignKey(nameof(MasurementUnit))]
        public int MasurementUnitId { get; set; }


        [Display(Name= "وحدة القياس")]
        public MasurementUnit MasurementUnit { get; set; }


        public bool Validate(List<ValidationError>errors)
        {
            //Validate Name
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Product),
                        PropertyName = nameof(ProductName),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    }
                    );
            }

            if (ProductName.Length > 300)
            {
                errors.Add(
                new ValidationError
                {
                    ObjectType = typeof(Product),
                    PropertyName = nameof(ProductName),
                    Code = ValidationErrorCode.ValueOutOfRange
                }
                );
            }

            //Validate ProductType
            if(ProductTypeId<=0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Product),
                        PropertyName = nameof(ProductType),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            //Validate Unit
            if (MasurementUnitId <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Product),
                        PropertyName = nameof(MasurementUnit),
                        Code = ValidationErrorCode.RequiredFieldMissing
                    });
            }

            //Validate Quantity
            if(QuantityInStorage <0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Product),
                        PropertyName = nameof(QuantityInStorage),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            return errors.Count == 0;
        }
    }
}

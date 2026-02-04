using DataAccessLayer.Abstractions;
using DataAccessLayer.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities.Products
{

    [Display(Name= "المنتج")]
    public class Product : IValidatable
    {
        [Key]
        [Display(Name = "معرف المنتج")]
        public int ProductId { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "سعر الشراء")]
        public decimal BuyingPrice { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "سعر البيع")]
        public decimal SellingPrice { get; set; }

        [NotMapped]
        public decimal Profit { get { return SellingPrice-BuyingPrice;}}


        [Column(TypeName = "decimal(10,4)")]
        [Display(Name = "كمية المخزون")]
        public decimal QuantityInStorage { get; set; }



        [MaxLength(300)]
        [Display(Name= "إسم المنتج")]
        public string ProductName { get; set; }

        [Display(Name = "متاح")]
        public bool IsAvailable { get; set; }

        [ForeignKey(nameof(ProductType))]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }



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

            //Validate SellilngPrice
            if(SellingPrice<=0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Product),
                        PropertyName = nameof(SellingPrice),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }

            //Validate BuyingPrice
            if (BuyingPrice <= 0)
            {
                errors.Add(
                    new ValidationError
                    {
                        ObjectType = typeof(Product),
                        PropertyName = nameof(BuyingPrice),
                        Code = ValidationErrorCode.ValueOutOfRange
                    });
            }


            return errors.Count == 0;
        }
    }
}

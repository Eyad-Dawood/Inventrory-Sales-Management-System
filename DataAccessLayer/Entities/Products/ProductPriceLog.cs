using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities.Products
{
    public class ProductPriceLog
    {
        [Key]
        [Display(Name = "معرف السجل")]
        public int ProductPriceLogId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "سعر الشراء القديم")]
        public decimal OldBuyingPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "سعر الشراء الجديد")]
        public decimal NewBuyingPrice { get; set; }



        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "سعر البيع القديم")]
        public decimal OldSellingPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "سعر البيع الجديد")]
        public decimal NewSellingPrice { get;set; }

        [Display(Name = "تاريخ التعديل")]
        public DateTime LogDate { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set;}
        public Product Product { get; set; }


        [ForeignKey(nameof(User))]
        public int CreatedByUserId { get; set; }
        public User User { get; set; }
    }
}

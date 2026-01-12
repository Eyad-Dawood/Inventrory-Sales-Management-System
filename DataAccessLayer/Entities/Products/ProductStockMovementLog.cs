using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities.Products
{
    public enum StockMovementReason
    {
        [Display(Name = "شراء")]
        Purchase,
        [Display(Name = "بيع")]
        Sale,
        [Display(Name = "تعديل")]
        Adjustment,
        [Display(Name = "ضرر")]
        Damage,
        [Display(Name = "قيمة مبدأية")]
        InitialStock
    }

    public class ProductStockMovementLog
    {
        [Key]
        [Display(Name = "معرف التسجيل")]
        public int ProductStockMovementId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [MaxLength(500)]
        [Display(Name = "الملاحظات")]
        public string? Notes { get; set; }


        [Column(TypeName = "decimal(10,4)")]
        [Display(Name = "الكمية القديمة")]
        public decimal OldQuantity { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        [Display(Name = "الكمية الجديدة")]
        public decimal NewQuantity { get; set; }

        [Display(Name = "السبب")]
        public StockMovementReason Reason { get; set; }

        [ForeignKey(nameof(User))]
        public int CreatedByUserId { get; set; }
        public User User { get; set; }

        [Display(Name = "تاريخ التعديل")]
        public DateTime LogDate { get; set; }
    }
}

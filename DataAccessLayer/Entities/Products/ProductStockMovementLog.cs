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
        public int ProductStockMovementId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }


        [Column(TypeName = "decimal(10,4)")]
        public decimal OldQuantity { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal NewQuantity { get; set; }

        public StockMovementReason Reason { get; set; }

        [ForeignKey(nameof(User))]
        public int CreatedByUserId { get; set; }
        public User User { get; set; }


        public DateTime LogDate { get; set; }
    }
}

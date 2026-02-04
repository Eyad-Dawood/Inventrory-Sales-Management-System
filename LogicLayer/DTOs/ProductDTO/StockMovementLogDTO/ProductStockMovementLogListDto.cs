using System.ComponentModel.DataAnnotations;

namespace LogicLayer.DTOs.ProductDTO.StockMovementLogDTO
{
    public class ProductStockMovementLogListDto
    {
        public int ProductId { get; set; }
        [Display(Name = "إسم المنتج الكامل")]
        public string ProductFullName { get; set; }
        [Display(Name = "مقدار التغير")]
        public decimal QuantityChange { get { return NewQuantity - OldQuantity; } } 
        public decimal OldQuantity { get; set; }
        public decimal NewQuantity { get; set; }
        public string? Notes { get; set; }
        public string Reason { get; set; }

        
        public string CreatedbyUserName { get; set; }
        public DateTime LogDate { get; set; }
    }
}

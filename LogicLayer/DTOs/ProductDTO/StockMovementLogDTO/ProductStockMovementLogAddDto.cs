using DataAccessLayer.Entities.Products;

namespace LogicLayer.DTOs.ProductDTO.StockMovementLogDTO
{
    public class ProductStockMovementLogAddDto
    {
        public int ProductId { get; set; }

        public decimal OldQuantity { get; set; }
        public decimal NewQuantity { get; set; }
        public string? Notes { get; set; }

        public StockMovementReason Reason { get; set; }

        public int CreatedByUserId { get; set; }
    }
}

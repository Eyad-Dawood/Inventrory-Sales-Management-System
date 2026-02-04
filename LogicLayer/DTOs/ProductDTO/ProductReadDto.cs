namespace LogicLayer.DTOs.ProductDTO
{
    public class ProductReadDto
    {
        public int ProductId { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal QuantityInStorage { get; set; }
        public decimal TotalQuantitySold { get; set; }
        public decimal Profit { get
            {
                return SellingPrice - BuyingPrice;
            } }
        public string ProductName { get; set; }
        public bool IsAvailable { get; set; }
        public string ProductTypeName { get; set; }
    }
}

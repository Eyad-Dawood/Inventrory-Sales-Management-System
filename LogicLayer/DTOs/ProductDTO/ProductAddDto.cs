namespace LogicLayer.DTOs.ProductDTO
{
    public class ProductAddDto
    {

        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal QuantityInStorage { get; set; }
        public string ProductName { get; set; }
        public bool IsAvailable { get; set; }
        public int ProductTypeId { get; set; }
    }
}

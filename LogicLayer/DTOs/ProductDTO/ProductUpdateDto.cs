namespace LogicLayer.DTOs.ProductDTO
{
    public class ProductUpdateDto
    {
        public int ProductId { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string ProductName { get; set; }
        public bool IsAvilable { get; set; }
    }
}

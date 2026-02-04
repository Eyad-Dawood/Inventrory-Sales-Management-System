namespace LogicLayer.DTOs.ProductDTO
{
    public class ProductListDto
    {
        public int ProductId { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal QuantityInStorage { get; set; }
        public bool IsAvilable { get; set; }
        public decimal Profit
        {
            get
            {
                return SellingPrice - BuyingPrice;
            }
        }
        public string ProductTypeName { get; set; }
        public string ProductName { get; set; }
    }
}

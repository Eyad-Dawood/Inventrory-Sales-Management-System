namespace LogicLayer.DTOs.InvoiceDTO.SoldProducts
{
    public class SoldProductAddDto
    {
        public decimal Quantity { get; set; }


        public int ProductId { get; set; }
        public int TakeBatchId { get; set; }


        #region Show Only
        public decimal PricePerUnit { get; set; }
        public decimal QuantityInStorage { get; set; }
        public bool IsAvilable { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductName { get; set; }
        public decimal Total { get { return Quantity * PricePerUnit; } }
        public int ProductTypeId { get; set; }
        #endregion

    }
}

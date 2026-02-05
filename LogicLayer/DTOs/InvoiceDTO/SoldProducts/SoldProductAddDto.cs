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

        public decimal Total { get { return Quantity * PricePerUnit; } }
        #endregion

    }
}

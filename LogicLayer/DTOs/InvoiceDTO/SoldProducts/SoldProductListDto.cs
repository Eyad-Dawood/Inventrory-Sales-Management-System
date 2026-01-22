using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO.SoldProducts
{
    public class SoldProductWithBatchListDto
    {
        public int SoldProductId { get; set; }

        public decimal BuyingPricePerUnit { get; set; }
        public decimal SellingPricePerUnit { get; set; }
        public decimal Quantity { get; set; }
        public DateTime TakeDate { get; set; }
        public string? TakeName { get; set; }
        public int InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string? WorkerName { get; set; }

    }
    public class SoldProductWithProductListDto
    {
        public int SoldProductId { get; set; }

        public string ProductFullName { get; set; }
        public decimal BuyingPricePerUnit { get; set; }
        public decimal SellingPricePerUnit { get; set; }
        public string UnitName { get; set; }
        public decimal Quantity { get; set; }
    }
}

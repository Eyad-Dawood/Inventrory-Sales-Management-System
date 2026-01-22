using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO.SoldProducts
{

    //Currently Not Used , Maybe in future
    public class SoldProductReadDto
    {
        public int SoldProductId { get; set; }

        public decimal BuyingPricePerUnit { get; set; }
        public decimal SellingPricePerUnit { get; set; }
        public decimal Quantity { get; set; }
        public DateTime TakeDate { get; set; }
        public string? TakeName { get; set; }
        public int InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string WorkerName { get; set; }
        public string ProductFullName { get; set; }
        public string UnitName { get; set; }
    }
}

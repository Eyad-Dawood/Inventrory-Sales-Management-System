using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities.DTOS
{
    [NotMapped]
    public class InvoiceProductSummary
    {
        public int ProductId { get; set; }
        public string ProductFullName { get; set; }
        public decimal TotalSellingQuantity { get; set; }
        public decimal RefundQuanttiy { get; set; }
        public decimal NetBuyingPrice { get; set; }
        public decimal NetSellingPrice { get; set; }
    }
}

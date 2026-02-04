using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities.DTOS
{
    [NotMapped]
    public class SoldProductForRefund
    {
        public string ProductTypeName { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public decimal QuantityInStorage { get; set; }
        public decimal SellingPricePerUnit { get; set; }
        public decimal Quantity { get; set; }
        public bool IsAvilable { get; set; }
    }
}

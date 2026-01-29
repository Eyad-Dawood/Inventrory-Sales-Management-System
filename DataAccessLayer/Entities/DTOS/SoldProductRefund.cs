using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities.DTOS
{
    public class SoldProductRefund
    {
            public int ProductId { get; set; }
            public string ProductFullName { get; set; }
            public decimal TotalRefundSellingQuantity { get; set; }
            public decimal NetRefundBuyingPrice { get; set; }
            public decimal NetRefundSellingPrice { get; set; }
    }
}

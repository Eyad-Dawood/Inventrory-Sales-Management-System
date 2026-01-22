using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO.SoldProducts
{
    public class SoldProductAddDto
    {
        public decimal Quantity { get; set; }

        public int ProductId { get; set; }

        public int TakeBatchId { get; set; }
    }
}

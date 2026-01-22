using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO.TakeBatches
{
    public class TakeBatchReadDto
    {
        public int TakeBatchId { get; set; }

        public int InvoiceId { get; set; }

        public string CustomerName { get; set; }

        public string WorkerName { get; set; }

        public string UserName { get; set; }

        public string? TakeName { get; set; }

        public DateTime TakeDate { get; set; }

        public string? Notes { get; set; }

        public decimal BatchTotalSellingPrice { get; set; }
    }
}

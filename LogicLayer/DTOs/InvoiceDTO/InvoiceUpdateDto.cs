using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO
{
    public class InvoiceUpdateDto
    {
        public int InvoiceId { get; set; }

        public decimal Addition {  get; set; }
        public string? AdditionNotes { get; set; }
    }
}

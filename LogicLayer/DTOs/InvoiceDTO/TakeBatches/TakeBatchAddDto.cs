using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO.TakeBatches
{
    public class TakeBatchAddDto
    {
        public string? TakeName { get; set; }

        public string? Notes { get; set; }

        public int InvoiceId { get; set; }

        public List<SoldProductAddDto> SoldProductAddDtos { get; set; }
    }
}

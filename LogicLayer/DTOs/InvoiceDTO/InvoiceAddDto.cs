using DataAccessLayer.Entities.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO
{
    public class InvoiceAddDto
    {
        public InvoiceType InvoiceType { get; set; }

        public int CustomerId { get; set; }

        public int? WorkerId { get; set; } = 0;

        public decimal AdditionAmount { get; set; }

        public string AdditonNotes { get; set; }
    }
}

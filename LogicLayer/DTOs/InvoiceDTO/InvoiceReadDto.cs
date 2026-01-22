using DataAccessLayer.Entities.Invoices;
using LogicLayer.DTOs.InvoiceDTO.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO
{
    public class InvoiceReadDto
    {

        public int InvoiceId { get; set; }
        public string CustomerName { get; set; }


        public InvoiceFinance InvoiceFinance { get; set; }

        public InvoiceType InvoiceType { get; set; }
        public InvoiceState InvoiceState { get; set; }

        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }

        public string OpenedByUserName { get; set; }
        public string? ClosedByUserName { get; set; }

    }
}

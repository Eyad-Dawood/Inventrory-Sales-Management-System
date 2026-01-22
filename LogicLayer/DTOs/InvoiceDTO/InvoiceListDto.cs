using DataAccessLayer.Entities.Invoices;
using LogicLayer.DTOs.InvoiceDTO.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO
{
    public class InvoiceListDto
    {
        public int InvoiceId { get; set; }


        public string CustomerName { get; set; }
        public string? WorkerName { get; set; }
        

        public InvoiceFinance InvoiceFinance { get; set; }

        public InvoiceType InvoiceType { get; set; }
        public InvoiceState InvoiceState { get; set; }

        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }


    }
}

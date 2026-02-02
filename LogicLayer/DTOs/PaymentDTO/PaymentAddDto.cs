using DataAccessLayer.Entities.Payments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.PaymentDTO
{
    public class PaymentAddDto
    {
        public decimal Amount { get; set; }
        public string? Notes { get; set; }

        public PaymentReason PaymentReason { get; set; }
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }

        public string PaidBy {  get; set; }
        public string RecivedBy { get; set; }

    }
}

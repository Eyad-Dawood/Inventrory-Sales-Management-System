using DataAccessLayer.Entities.Payments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities.DTOS
{
    public class InvoicePaymentSummary
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public PaymentReason PaymentReason { get; set; }
        public string PaidBy { get; set; }
        public string RecivedBy { get; set; }

    }
}

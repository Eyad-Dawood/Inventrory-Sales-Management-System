using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.PaymentDTO
{
    public class InvoicePaymentSummaryDto
    {
        public int PaymentId { get; set; }

        [Display(Name = "المبلغ")]
        public decimal Amount { get; set; }

        [Display(Name = "التاريخ")]
        public DateTime Date { get; set; }

        [Display(Name = "السبب")]
        public string PaymentReason { get; set; }

        [Display(Name = "من يد")]
        public string PaidBy { get; set; }
        [Display(Name = "إلى يد")]
        public string RecivedBy { get; set; }

    }
}

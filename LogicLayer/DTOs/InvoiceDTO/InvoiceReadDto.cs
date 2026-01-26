using DataAccessLayer.Entities.Invoices;
using LogicLayer.DTOs.InvoiceDTO.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO
{
    public class InvoiceReadDto
    {

        public int InvoiceId { get; set; }

        public int CustomerId { get; set; }
        public int? WorkerId { get; set; }
        public string CustomerName { get; set; }
        public string WorkerName { get; set; }

        public decimal TotalBuyingPrice { get; set; }
        public decimal TotalSellingPrice { get; set; }
        public decimal TotalRefundBuyingPrice { get; set; }
        public decimal TotalRefundSellingPrice { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal Additional { get; set; }
        public string? AdditionalNotes { get; set; }

        // صافي البيع الحقيقي
        [Display(Name = "صافي البيع")]
        public decimal NetSale =>
            TotalSellingPrice - TotalRefundSellingPrice;

        // صافي تكلفة البضاعة الموجودة فعليًا
        [Display(Name = "صافي الشراء")]
        public decimal NetBuying =>
            TotalBuyingPrice - TotalRefundBuyingPrice;

        // الربح المحاسبي
        [Display(Name = "الربح")]
        public decimal NetProfit =>
            NetSale - NetBuying + Additional;

        // الباقي المستحق دفعه
        [Display(Name = "باقي للسداد")]
        public decimal Remaining =>
            AmountDue - (TotalPaid);

        // total يدفعه العميل
        [Display(Name = "المبلغ المستحق النهائي")]
        public decimal AmountDue =>
            NetSale + Additional;


        public string InvoiceType { get; set; }
        public string InvoiceState { get; set; }

        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }

        public string OpenedByUserName { get; set; }
        public string? ClosedByUserName { get; set; }

    }
}

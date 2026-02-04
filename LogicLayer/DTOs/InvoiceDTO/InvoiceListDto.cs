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
        public string? CustomerPhoneNumber { get; set; }
        public string? WorkerName { get; set; }
        public string Town { get; set; }

        public decimal TotalBuyingPrice { get; set; }
        public decimal TotalSellingPrice { get; set; }
        public decimal TotalRefundBuyingPrice { get; set; }
        public decimal TotalRefundSellingPrice { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal Discount { get; set; }


        public int CustomerId { get; set; }
        public int ? WorkerId { get; set; }

        // صافي البيع الحقيقي
        [Display(Name = "صافي البيع")]
        public decimal NetSale { get; set; }

        // صافي تكلفة البضاعة الموجودة فعليًا
        [Display(Name = "صافي الشراء")]
        public decimal NetBuying { get; set; }

        // الربح المحاسبي
        [Display(Name = "الربح")]
        public decimal NetProfit { get; set; }

        // الباقي المستحق دفعه
        [Display(Name = "باقي للسداد")]
        public decimal Remaining { get; set; }

        // total يدفعه العميل
        [Display(Name = "المبلغ المستحق النهائي")]
        public decimal AmountDue { get; set; }

        public string InvoiceType { get; set; }
        public string InvoiceState { get; set; }

        public InvoiceType InvoiceTypeEn {  get; set; }
        public InvoiceState InvoiceStateEn { get; set; }

        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }


    }
}

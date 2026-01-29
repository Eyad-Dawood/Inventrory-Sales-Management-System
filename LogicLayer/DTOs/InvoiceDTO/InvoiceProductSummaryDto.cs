using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO
{
    public class InvoiceProductSummaryDto
    {
        public int ProductId { get; set; }
        [Display(Name = "اسم المنتج")]
        public string ProductFullName { get; set; }
        [Display(Name = "إجمالي الكمية")]
        public decimal TotalSellingQuantity { get; set; }

        [Display(Name = "المرتجع")]
        public decimal RefundQuanttiy { get; set; }

        [Display(Name = "صافي الكمية")]
        public decimal NetSellingQuantity
        {
            get
            {
                return TotalSellingQuantity - RefundQuanttiy;
            }
        }

        [Display(Name = "صافي الشراء")]
        public decimal NetBuyingPrice { get; set; }
        [Display(Name = "صافي البيع")]
        public decimal NetSellingPrice { get; set; }
        [Display(Name ="متوسط سعر وحدة الشراء")]
        public decimal AvrageBuyingPrice { get; set; }
        [Display(Name = "متوسط سعر وحدة البيع")]
        public decimal AvrageSellingPrice { get; set; }
    }
}

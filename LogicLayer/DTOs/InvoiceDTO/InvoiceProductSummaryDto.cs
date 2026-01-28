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
        [Display(Name = "الكمية")]
        public decimal TotalQuantity { get; set; }
        [Display(Name = "الشراء")]
        public decimal TotalBuyingPrice { get; set; }
        [Display(Name = "البيع")]
        public decimal TotalSellingPrice { get; set; }
        [Display(Name ="متوسط سعر وحدة الشراء")]
        public decimal AvrageBuyingPrice { get; set; }
        [Display(Name = "متوسط سعر وحدة البيع")]
        public decimal AvrageSellingPrice { get; set; }
    }
}

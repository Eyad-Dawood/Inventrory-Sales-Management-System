using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO.SoldProducts
{

    //Currently Not Used , Maybe in future
    public class SoldProductReadDto
    {
        public int SoldProductId { get; set; }

        public decimal BuyingPricePerUnit { get; set; }
        public decimal SellingPricePerUnit { get; set; }
        public decimal Quantity { get; set; }
        public DateTime TakeDate { get; set; }
        public string? TakeName { get; set; }
        public int InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string WorkerName { get; set; }
        public string ProductFullName { get; set; }
        public string UnitName { get; set; }
    }

    public class SoldProductMiniReadDto
    {
        public int BatchId { get; set; }
        public string ProductFullName { get; set; }
        [Display(Name = "سعر الوحدة")]
        public decimal SellingPricePerUnit { get; set; }
        [Display(Name = "الإجمالي")]
        public decimal TotalSellingPrice { get; set; }
        [Display(Name ="الكمية")]
        public decimal Quantity { get; set; }
        [Display(Name = "الوحدة")]
        public string UnitName { get; set; }
        [Display(Name = "التاريخ")]
        public DateTime TakeDate { get; set; }
        [Display(Name = "المستلم")]
        public string? Reciver { get; set; }
    }
}

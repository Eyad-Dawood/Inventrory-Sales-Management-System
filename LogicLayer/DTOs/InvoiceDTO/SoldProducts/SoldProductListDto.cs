using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.DTOs.InvoiceDTO.SoldProducts
{
    //This For Selling Products
    public class SoldProductSaleDetailsListDto
    {
        public int SoldProductId { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public decimal QuantityInStorage { get; set; }
        public decimal SellingPricePerUnit { get; set; }
        public string UnitName { get; set; }
        public decimal Quantity { get; set; }
        public bool IsAvilable { get; set; }
    }

    //This For Refund Summary
    public class InvoiceProductRefundSummaryListDto
    {
        public int ProductId { get; set; }
        [Display(Name = "اسم المنتج")]
        public string ProductFullName { get; set; }

        [Display(Name = "إجمالي الكمية")]
        public decimal TotalRefundSellingQuantity { get; set; }

        [Display(Name = "صافي الشراء")]
        public decimal NetRefundBuyingPrice { get; set; }
        [Display(Name = "صافي البيع")]
        public decimal NetRefundSellingPrice { get; set; }
    }

    //This For Invoice Details
    public class SoldProductListDto
    {
        public int BatchId { get; set; }
        public string ProductFullName { get; set; }
        [Display(Name = "سعر الوحدة")]
        public decimal SellingPricePerUnit { get; set; }
        [Display(Name = "الإجمالي")]
        public decimal TotalSellingPrice { get; set; }
        [Display(Name = "الكمية")]
        public decimal Quantity { get; set; }
        [Display(Name = "الوحدة")]
        public string UnitName { get; set; }
        [Display(Name = "التاريخ")]
        public DateTime TakeDate { get; set; }
        [Display(Name = "المستلم")]
        public string? Reciver { get; set; }
    }

}

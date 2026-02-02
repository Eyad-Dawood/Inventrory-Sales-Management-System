using DataAccessLayer.Entities.Invoices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Services.Helpers
{
    public record InvoiceFinanceBus
    {
        public decimal NetSale { get; init; }
        public decimal NetBuying { get; init; }
        public decimal NetProfit { get; init; }
        public decimal Remaining { get; init; }
        public decimal AmountDue { get; init; }
    }

    static public class InvoiceFinanceHelper
    {
        static public decimal NetSale(decimal TotalSellingPrice,decimal TotalRefundSellingPrice) => TotalSellingPrice - TotalRefundSellingPrice;
        static public decimal NetBuying(decimal TotalBuyingPrice, decimal TotalRefundBuyingPrice) => TotalBuyingPrice - TotalRefundBuyingPrice;
        static public decimal NetProfit(decimal NetSale , decimal NetBuying, decimal Discount) => NetSale - NetBuying - Discount;
        static public decimal AmountDue(decimal NetSale, decimal Discount) => NetSale - Discount;
        static public decimal Remaining(decimal AmountDue, decimal TotalPaid) => AmountDue - TotalPaid;
        static public decimal RefundAmount(decimal TotalPaid,decimal AmountDue) => TotalPaid - AmountDue;

        static public InvoiceFinanceBus InvoiceFinance(Invoice invoice)
        {
            decimal netSale = NetSale(invoice.TotalSellingPrice, invoice.TotalRefundSellingPrice);
            decimal netBuying = NetBuying(invoice.TotalBuyingPrice, invoice.TotalRefundBuyingPrice);
            decimal netProfit = NetProfit(netSale, netBuying, invoice.Discount);
            decimal amountDue = AmountDue(netSale, invoice.Discount);
            decimal remaining = Remaining(amountDue, invoice.TotalPaid);

            return new InvoiceFinanceBus()
            {
                AmountDue = amountDue,
                NetBuying = netBuying,
                NetProfit = netProfit,
                NetSale = netSale,
                Remaining = remaining
            };
        }
        static public decimal GetRefundAmount(Invoice invoice)
        {
            decimal netSale = NetSale(invoice.TotalSellingPrice, invoice.TotalRefundSellingPrice);
            decimal amountDue = AmountDue(netSale,invoice.Discount);
            decimal refundAmount = RefundAmount(invoice.TotalPaid ,amountDue);

            return refundAmount;
        }
        static public decimal GetRemainingAmount(Invoice invoice)
        {
            decimal netSale = NetSale(invoice.TotalSellingPrice, invoice.TotalRefundSellingPrice);
            decimal amountDue = AmountDue(netSale, invoice.Discount);
            decimal remaining = Remaining(amountDue, invoice.TotalPaid);

            return remaining;
        }
    }
}

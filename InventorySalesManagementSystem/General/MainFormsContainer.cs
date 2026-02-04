using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.Invoices;
using InventorySalesManagementSystem.Payments;
using InventorySalesManagementSystem.Products;
using InventorySalesManagementSystem.Products.PricesLog;
using InventorySalesManagementSystem.Products.StockMovementLog;
using InventorySalesManagementSystem.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySalesManagementSystem.General
{
    static public class MainFormsContainer
    {
        static public frmCustomerListScreen? frmCustomerListScreen = null;
        static public frmWorkerListScreen? frmWorkerListScreen = null;
        static public frmProductListScreen? frmProductListScreen = null;
        static public frmInvoiceListScreen? frmInvoiceListScreen = null;
        static public frmPriceLogListScreen? frmPriceLogListScreen = null;
        static public FrmStockMovementLogListScreen? frmStockMovementLogListScreen = null;
        static public frmPaymentsListScreen? frmPaymentsListScreen = null;
    }
}

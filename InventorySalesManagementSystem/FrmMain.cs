using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Products;
using DataAccessLayer.Repos;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.General.General_Forms;
using InventorySalesManagementSystem.Invoices;
using InventorySalesManagementSystem.Payments;
using InventorySalesManagementSystem.Products;
using InventorySalesManagementSystem.Products.PricesLog;
using InventorySalesManagementSystem.Products.ProductsTypes;
using InventorySalesManagementSystem.Products.StockMovementLog;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.Global.Users;
using LogicLayer.Services;
using LogicLayer.Services.Invoices;
using LogicLayer.Validation.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace InventorySalesManagementSystem
{
    public partial class FrmMain : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public FrmMain(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }



        private void Workers_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmWorkerListScreen(_serviceProvider, selectButton: false);
                frm.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Customers_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmCustomerListScreen(_serviceProvider, selectButton: false);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Products_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmProductListScreen(_serviceProvider);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PriceLogs_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmPriceLogListScreen(_serviceProvider);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StorageLog_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmStockMovementLogListScreen(_serviceProvider);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Invoices_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmInvoiceListScreen(_serviceProvider, selectButton: false);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Payments_Click(object sender, EventArgs e)
        {
            try
            {
                frmPaymentsListScreen frm = new frmPaymentsListScreen(_serviceProvider);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

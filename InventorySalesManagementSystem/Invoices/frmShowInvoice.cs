using DataAccessLayer.Entities.Invoices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.Invoices
{
    public partial class frmShowInvoice : Form
    {
        public int InvoiceId = 0;
        private readonly IServiceProvider _serviceProvider;

        public frmShowInvoice(IServiceProvider serviceProvider, int invoiceId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            InvoiceId = invoiceId;
        }

        private async Task LoadInvoiceAsync()
        {
            await ucInvoiceDetails1.ShowInvoice(_serviceProvider, InvoiceId);
        }

        private async void frmShowInvoice_Load(object sender, EventArgs e)
        {
            await LoadInvoiceAsync();
        }
    }
}

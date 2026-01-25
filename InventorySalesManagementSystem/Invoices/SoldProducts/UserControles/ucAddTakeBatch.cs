using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.Invoices.SoldProducts.UserControles
{
    public partial class ucAddTakeBatch : UserControl
    {
        public IServiceProvider _serviceProvider { get; private set; }

        [Browsable(true)]
        [DefaultValue(true)]
        public bool ShowInvoiceDetails
        {
            get { return _showInvoiceDetails; }
            set
            {
                _showInvoiceDetails = value;
                gbInvoiceDetails.Visible = value;
            }
        }

        private bool _showInvoiceDetails = true;

        public ucAddTakeBatch()
        {
            InitializeComponent();
        }

        public TakeBatchAddDto GetTakeBatch()
        {
            return new TakeBatchAddDto
            {
                TakeName = txtReciver.Text.Trim(),
                Notes = txtNote.Text.Trim(),
                InvoiceId = -1,//to be set later
                SoldProductAddDtos = ucProductSelector1.GetSoldProducts()
            };
        }

        public void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            ucProductSelector1.Initialize(_serviceProvider);
            this.Enabled = true;
        }
    }
}

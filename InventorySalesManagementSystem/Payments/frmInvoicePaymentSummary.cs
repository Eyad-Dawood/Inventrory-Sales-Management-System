using DataAccessLayer.Entities.Invoices;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.PaymentDTO;
using LogicLayer.Services.Invoices;
using LogicLayer.Services.Payments;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.Payments
{
    public partial class frmInvoicePaymentSummary : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly int _invoiceId;


        public frmInvoicePaymentSummary(IServiceProvider serviceProvider, int InvoiceId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _invoiceId = InvoiceId;
        }

        #region Configure
        protected void ConfigureGrid(DataGridView dgv)
        {
            // Defualt Settings
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font(dgv.Font, FontStyle.Bold);

            dgv.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            // ===== Amount =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoicePaymentSummaryDto.Amount),
                DataPropertyName = nameof(InvoicePaymentSummaryDto.Amount),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(InvoicePaymentSummaryDto), nameof(InvoicePaymentSummaryDto.Amount)),
                FillWeight = 12,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2"
                }
            });

            // ===== PaidBy =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoicePaymentSummaryDto.PaidBy),
                DataPropertyName = nameof(InvoicePaymentSummaryDto.PaidBy),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(InvoicePaymentSummaryDto), nameof(InvoicePaymentSummaryDto.PaidBy)),
                FillWeight = 30
            });

            // ===== RecivedBy =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoicePaymentSummaryDto.RecivedBy),
                DataPropertyName = nameof(InvoicePaymentSummaryDto.RecivedBy),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(InvoicePaymentSummaryDto), nameof(InvoicePaymentSummaryDto.RecivedBy)),
                FillWeight = 30
            });

            
            // ===== PaymentReason =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoicePaymentSummaryDto.PaymentReason),
                DataPropertyName = nameof(InvoicePaymentSummaryDto.PaymentReason),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(InvoicePaymentSummaryDto), nameof(InvoicePaymentSummaryDto.PaymentReason)),
                FillWeight = 10
            });


            // ===== Date =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoicePaymentSummaryDto.Date),
                DataPropertyName = nameof(InvoicePaymentSummaryDto.Date),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(InvoicePaymentSummaryDto), nameof(InvoicePaymentSummaryDto.Date)),
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "yyyy/MM/dd hh:mm",
                },
                FillWeight = 25
            });


        }
        #endregion


        #region MainLogic

        protected async Task<IEnumerable<object>> GetDataAsync()
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<PaymentService>();
                return await service.GetInvoiceProductSummaryAsync(_invoiceId);
            }
        }

        #endregion

        #region Paging Core
        private async Task DisplayAsync()
        {
            var data =
                 await GetDataAsync();

            ucListView1.DisplayData<object>(data);
        }
        #endregion

        private async Task FormLoadAsync()
        {
            ConfigureGrid(ucListView1.DataGridViewControl);
            await DisplayAsync();
        }

        private async void frmInvoicePaymentSummary_Load(object sender, EventArgs e)
        {
            await FormLoadAsync();
        }
    }
}

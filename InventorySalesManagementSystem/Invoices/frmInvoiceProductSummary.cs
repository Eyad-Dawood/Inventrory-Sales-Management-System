using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.Services.Invoices;
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

namespace InventorySalesManagementSystem.Invoices
{
    public partial class frmInvoiceProductSummary : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly int _invoiceId;
        public frmInvoiceProductSummary(IServiceProvider serviceProvider, int InvoiceId)
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


            // ===== ProductFullName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceProductSummaryDto.ProductFullName),
                DataPropertyName = nameof(InvoiceProductSummaryDto.ProductFullName),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(InvoiceProductSummaryDto), nameof(InvoiceProductSummaryDto.ProductFullName)),
                FillWeight = 45
            });

            // ===== Quantity =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceProductSummaryDto.TotalQuantity),
                DataPropertyName = nameof(InvoiceProductSummaryDto.TotalQuantity),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(InvoiceProductSummaryDto), nameof(InvoiceProductSummaryDto.TotalQuantity)),
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N4"
                }
            });

            // ===== TotalBuyingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceProductSummaryDto.TotalBuyingPrice),
                DataPropertyName = nameof(InvoiceProductSummaryDto.TotalBuyingPrice),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(InvoiceProductSummaryDto), nameof(InvoiceProductSummaryDto.TotalBuyingPrice)),
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2"
                }
            });

            // ===== TotalSellingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceProductSummaryDto.TotalSellingPrice),
                DataPropertyName = nameof(InvoiceProductSummaryDto.TotalSellingPrice),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(InvoiceProductSummaryDto), nameof(InvoiceProductSummaryDto.TotalSellingPrice)),
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2"
                }
            });

            // ===== AVGSellingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceProductSummaryDto.AvrageSellingPrice),
                DataPropertyName = nameof(InvoiceProductSummaryDto.AvrageSellingPrice),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(InvoiceProductSummaryDto), nameof(InvoiceProductSummaryDto.AvrageSellingPrice)),
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2"
                }
            });

            // ===== TotalBuyingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceProductSummaryDto.AvrageBuyingPrice),
                DataPropertyName = nameof(InvoiceProductSummaryDto.AvrageBuyingPrice),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(InvoiceProductSummaryDto), nameof(InvoiceProductSummaryDto.AvrageBuyingPrice)),
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2"
                }
            });
        }
        #endregion

        #region MainLogic

        protected async Task<IEnumerable<object>> GetDataAsync()
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<InvoiceService>();
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

        private void frmInvoiceProductSummary_Load(object sender, EventArgs e)
        {
            ConfigureGrid(ucListView1.DataGridViewControl);
            _ = DisplayAsync();
        }
    }
}

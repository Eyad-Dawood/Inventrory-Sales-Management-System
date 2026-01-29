using DataAccessLayer.Entities.Invoices;
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
    public partial class frmRefundProductsSummary : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly int _invoiceId;


        public frmRefundProductsSummary(IServiceProvider serviceProvider, int InvoiceId)
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
                Name = nameof(SoldProductRefundListDto.ProductFullName),
                DataPropertyName = nameof(SoldProductRefundListDto.ProductFullName),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(SoldProductRefundListDto), nameof(SoldProductRefundListDto.ProductFullName)),
                FillWeight = 45
            });

            // ===== TotalRefundSellingQuantity =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductRefundListDto.TotalRefundSellingQuantity),
                DataPropertyName = nameof(SoldProductRefundListDto.TotalRefundSellingQuantity),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(SoldProductRefundListDto), nameof(SoldProductRefundListDto.TotalRefundSellingQuantity)),
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N4"
                }
            });

            // ===== NetRefundBuyingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductRefundListDto.NetRefundBuyingPrice),
                DataPropertyName = nameof(SoldProductRefundListDto.NetRefundBuyingPrice),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(SoldProductRefundListDto), nameof(SoldProductRefundListDto.NetRefundBuyingPrice)),
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
                Name = nameof(SoldProductRefundListDto.NetRefundSellingPrice),
                DataPropertyName = nameof(SoldProductRefundListDto.NetRefundSellingPrice),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(SoldProductRefundListDto), nameof(SoldProductRefundListDto.NetRefundSellingPrice)),
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
                return await service.GetInvoiceRefundProductSummaryAsync(_invoiceId);
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

        private async void frmRefundProductsSummary_Load(object sender, EventArgs e)
        {
           await FormLoadAsync();
        }

        private async Task FormLoadAsync()
        {
            ConfigureGrid(ucListView1.DataGridViewControl);
            await DisplayAsync();
        }
    }
}

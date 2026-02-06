using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.Payments;
using InventorySalesManagementSystem.UserControles;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.Services.Invoices;
using LogicLayer.Services.Products;
using LogicLayer.Validation.Exceptions;
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

namespace InventorySalesManagementSystem.Invoices.SoldProducts.UserControles
{
    public partial class ucInvoiceDetails : UserControl
    {
        private IServiceProvider _serviceProvider;
        private int RowsPerPage = 100;
        private int InvoiceId = 0;
        private InvoiceReadDto InvoiceReadDto;
        private bool Detailed = true;


        private readonly Dictionary<int, Color> _batchColors = new();
        private readonly Color[] _palette =
                {
                Color.FromArgb(240, 248, 255),
                Color.FromArgb(245, 245, 220),
                Color.FromArgb(240, 255, 240),
                Color.FromArgb(255, 250, 240),
                Color.FromArgb(248, 248, 255)
                };

        public ucInvoiceDetails()
        {
            InitializeComponent();
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


            // ===== Quantity =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductListDto.Quantity),
                DataPropertyName = nameof(SoldProductListDto.Quantity),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(SoldProductListDto), nameof(SoldProductListDto.Quantity)),
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N4"
                }
            });


            // ===== ProductFullName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductListDto.ProductFullName),
                DataPropertyName = nameof(SoldProductListDto.ProductFullName),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(Product), nameof(Product.ProductName)),
                FillWeight = 45
            });

            // ===== PricePerUnit =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductListDto.SellingPricePerUnit),
                DataPropertyName = nameof(SoldProductListDto.SellingPricePerUnit),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(SoldProductListDto), nameof(SoldProductListDto.SellingPricePerUnit)),
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2"
                }
            });

            // ===== Total =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductListDto.TotalSellingPrice),
                DataPropertyName = nameof(SoldProductListDto.TotalSellingPrice),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(SoldProductListDto), nameof(SoldProductListDto.TotalSellingPrice)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Format = "N2"
                }
            });


            // ===== TakeDate =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductListDto.TakeDate),
                DataPropertyName = nameof(SoldProductListDto.TakeDate),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(SoldProductListDto), nameof(SoldProductListDto.TakeDate)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "yyyy/MM/dd"
                }
            });

            // ===== Reciver =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductListDto.Reciver),
                DataPropertyName = nameof(SoldProductListDto.Reciver),
                HeaderText = LogicLayer.Utilities
                .NamesManager.GetArabicPropertyName(typeof(SoldProductListDto), nameof(SoldProductListDto.Reciver)),
                FillWeight = 20
            });

        }
        #endregion

        #region MainLogic

        protected async Task<int> GetTotalPagesAsync()
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<SoldProductService>();
                return await service.GetTotalPagesByInvoiceIdAsync(InvoiceId, RowsPerPage, new List<TakeBatchType>() { TakeBatchType.Invoice });
            }
        }

        protected async Task<IEnumerable<object>> GetDataAsync(int page)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<SoldProductService>();
                return await service.GetAllWithDetailsByInvoiceIdAsync(InvoiceId, page, RowsPerPage, new List<TakeBatchType>() { TakeBatchType.Invoice });
            }
        }

        #endregion



        #region Paging Core
        private async Task DisplayPageAsync(int page)
        {
            int totalPages = await GetTotalPagesAsync();


            page = Math.Max(1, Math.Min(page, totalPages));

            var data =
                 await GetDataAsync(page);

            ucListView1.DisplayData<object>(data, page, totalPages);
        }
        #endregion

        #region Events
        private async Task HandleFilterClicked(UcListView.Filter filter)
        {


            await DisplayPageAsync(1);

        }

        private async Task HandlePageChanged(int page, UcListView.Filter filter)
        {
            await DisplayPageAsync(page);
        }

        private async Task HandleOperationFinished(int page, UcListView.Filter filter)
        {
            await DisplayPageAsync(page);
        }
        #endregion

        private void DataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var dgv = sender as DataGridView;
            var row = dgv.Rows[e.RowIndex];

            if (row.DataBoundItem is not SoldProductListDto dto)
                return;

            int batchId = dto.BatchId;

            if (!_batchColors.TryGetValue(batchId, out Color color))
            {
                color = _palette[_batchColors.Count % _palette.Length];
                _batchColors.Add(batchId, color);
            }

            row.DefaultCellStyle.BackColor = color;
        }

        private void ManageDetailUi()
        {
            lbTotalBuyingLable.Visible = Detailed;
            lbTotalBuyingPrice.Visible = Detailed;
            lbTotalBuyingRefundLable.Visible = Detailed;
            lbTotalBuyingRefundPrice.Visible = Detailed;
            lbNetBuyingLable.Visible = Detailed;
            lbNetBuying.Visible = Detailed;


            lbProfitLable.Visible = Detailed;
            lbNetProfit.Visible = Detailed;

            lkNotes.Visible = !string.IsNullOrEmpty(InvoiceReadDto.Notes);
        }
        public async Task ShowInvoice(IServiceProvider serviceProvider, int invoiceId , bool detailed)
        {
            Detailed = detailed;

            _serviceProvider = serviceProvider;
            InvoiceId = invoiceId;
            // Configure DataGridView
            ConfigureGrid(ucListView1.DataGridViewControl);
            // Subscribe to events
            ucListView1.OnFilterClicked += HandleFilterClicked;
            ucListView1.OnNextPage += HandlePageChanged;
            ucListView1.OnPreviousPage += HandlePageChanged;
            ucListView1.OnRefreshAfterOperation += HandleOperationFinished;
            ucListView1.DataGridViewControl.RowPrePaint += DataGridView_RowPrePaint;
            // Initial Display
            _ = DisplayPageAsync(1);

            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<InvoiceService>();
                try
                {
                    var Invoice = await service.GetInvoiceByIdAsync(invoiceId);
                    InvoiceReadDto = Invoice;
                    ManageDetailUi();
                    LoadUi();
                }
                catch (NotFoundException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "During Finding Invoice {InvoiceId} In Show Data Function", InvoiceId);
                    throw;
                }
            }

            this.Enabled = true;
        }

        public void LoadUi()
        {
            lbID.Text = InvoiceReadDto.InvoiceId.ToString();


            lbTotalBuyingPrice.Text = InvoiceReadDto.TotalBuyingPrice.ToString("N2");
            lbTotalBuyingRefundPrice.Text = InvoiceReadDto.TotalRefundBuyingPrice.ToString("N2");
            lbNetBuying.Text = InvoiceReadDto.NetBuying.ToString("N2");

            lbTotalSellingPrice.Text = InvoiceReadDto.TotalSellingPrice.ToString("N2");
            lbTotalRefundSellingPrice.Text = InvoiceReadDto.TotalRefundSellingPrice.ToString("N2");
            lbNetSale.Text = InvoiceReadDto.NetSale.ToString("N2");

            lbCustomer.Text = InvoiceReadDto.CustomerName;
            lbWorker.Text = InvoiceReadDto.WorkerId == null ? "----" : InvoiceReadDto.WorkerName;
            lbInvoicetype.Text = InvoiceReadDto.InvoiceType;
            lbInvoiceState.Text = InvoiceReadDto.InvoiceState;

            lbOpenDate.Text = InvoiceReadDto.OpenDate.ToString("yyyy/MM/dd");
            lbCloseDate.Text = InvoiceReadDto.CloseDate == null ? "----" : ((DateTime)InvoiceReadDto.CloseDate).ToString("yyyy/MM/dd");

            lbDueAmount.Text = InvoiceReadDto.AmountDue.ToString("N2");
            lbPaied.Text = InvoiceReadDto.TotalPaid.ToString("N2");
            lbRemaining.Text = InvoiceReadDto.Remaining.ToString("N2");
            lbNetProfit.Text = InvoiceReadDto.NetProfit.ToString("N2");

            lbDiscount.Text = InvoiceReadDto.Discount.ToString("N2");
        }



        private void lkshowCustomer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (InvoiceReadDto.CustomerId <= 0)
            {
                MessageBox.Show("لا يوجد عميل مرتبط بهذه الفاتورة", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var frm = new frmShowCustomer(_serviceProvider, InvoiceReadDto.CustomerId);
            frm.ShowDialog();
        }

        private void lkShowWorker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (InvoiceReadDto.WorkerId == null || InvoiceReadDto.WorkerId <= 0)
            {
                MessageBox.Show("لا يوجد عامل مرتبط بهذه الفاتورة", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var frm = new frmShowWorker(_serviceProvider, (int)InvoiceReadDto.WorkerId);
            frm.ShowDialog();
        }

        private void lkInvoiceSummary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (InvoiceId <= 0)
            {
                MessageBox.Show("رقم الفاتورة غير صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var frm = new frmInvoiceProductSummary(_serviceProvider, InvoiceId);
            frm.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (InvoiceId <= 0)
            {
                MessageBox.Show("رقم الفاتورة غير صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var frm = new frmRefundProductsSummary(_serviceProvider, InvoiceId);
            frm.ShowDialog();
        }

        private void lkPayments_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (InvoiceId <= 0)
            {
                MessageBox.Show("رقم الفاتورة غير صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var frm = new frmInvoicePaymentSummary(_serviceProvider, InvoiceId);
            frm.ShowDialog();
        }


        private void lkNotes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(InvoiceReadDto.Notes))
            { 
                MessageBox.Show("لا يوجد ملاحظات لعرضها");
                return;
            }
            MessageBox.Show(InvoiceReadDto.Notes);
        }
    }
}

using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Payments;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.General.General_Forms;
using InventorySalesManagementSystem.Invoices;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.PaymentDTO;
using LogicLayer.DTOs.ProductDTO.StockMovementLogDTO;
using LogicLayer.Services.Payments;
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

namespace InventorySalesManagementSystem.Payments
{
    public partial class frmPaymentsListScreen : frmBaseListScreen
    {
        private readonly IServiceProvider _serviceProvider;
        protected override ContextMenuStrip GridContextMenu => cms;
        private const string CustomerNameFilter = nameof(PaymentListDto.CustomerName);

        public frmPaymentsListScreen(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            SelectButton = false;
            AddButton = false;
            lbTitle.Text = "المدفوعات";
        }

        #region Configure
        protected override List<UcListView.FilterItems> ConfigureFilter()
        {
            return new List<UcListView.FilterItems>()
                {
                    new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(PaymentListDto), nameof(PaymentListDto.CustomerName)),
                                                 Value = CustomerNameFilter},
                };
        }
        protected override void ConfigureGrid(DataGridView dgv)
        {
            base.ConfigureGrid(dgv);


            // ===== CustomerName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(PaymentListDto.CustomerName),
                DataPropertyName = nameof(PaymentListDto.CustomerName),
                FillWeight = 40,
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(PaymentListDto), nameof(PaymentListDto.CustomerName))
            });

            // ===== PaidBuyName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(PaymentListDto.PaidBy),
                DataPropertyName = nameof(PaymentListDto.PaidBy),
                FillWeight = 40,
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(PaymentListDto), nameof(PaymentListDto.PaidBy))
            });

            // ===== RecivedByName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(PaymentListDto.RecivedBy),
                DataPropertyName = nameof(PaymentListDto.RecivedBy),
                FillWeight = 40,
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(PaymentListDto), nameof(PaymentListDto.RecivedBy))
            });


            // ===== Amount =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(PaymentListDto.Amount),
                DataPropertyName = nameof(PaymentListDto.Amount),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(PaymentListDto), nameof(PaymentListDto.Amount)),
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // ===== Reason =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(PaymentListDto.PaymentReason),
                DataPropertyName = nameof(PaymentListDto.PaymentReason),
                FillWeight = 10,
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(PaymentListDto), nameof(PaymentListDto.PaymentReason))
            });


            // ===== Date =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(PaymentListDto.Date),
                DataPropertyName = nameof(PaymentListDto.Date),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(PaymentListDto), nameof(PaymentListDto.Date)),
                FillWeight = 25,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "yyyy/MM/dd hh:mm",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

        }
        #endregion

        private List<PaymentReason> GetPaymentReasons()
        {
            List<PaymentReason> reasons = new List<PaymentReason>();


            if (chkInvoice.Checked)
                reasons.Add(PaymentReason.Invoice);

            if (chkRefund.Checked)
                reasons.Add(PaymentReason.Refund);

            return reasons;
        }

        #region Hooks
        protected async override Task<int> GetTotalPagesAsync()
        {
            List<PaymentReason> reasons = GetPaymentReasons();


            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<PaymentService>();
                return await service.GetTotalPageNumberAsync(RowsPerPage, reasons);
            }
        }

        protected async override Task<int> GetTotalFilteredPagesAsync(UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<PaymentService>();

                List<PaymentReason> reasons = GetPaymentReasons();


                return filter.ColumnName switch
                {
                    CustomerNameFilter
                    => await service.GetTotalPageByCustomerNameAndDateAsync(RowsPerPage, filter.Text1Value, filter.dateTime, reasons),


                    _ => 0
                };
            }
        }

        protected async override Task<IEnumerable<object>> GetDataAsync(int page)
        {
            List<PaymentReason> reasons = GetPaymentReasons();



            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<PaymentService>();
                return await service.GetAllPaymentsAsync(page, RowsPerPage, reasons);
            }
        }

        protected async override Task<IEnumerable<object>> GetFilteredDataAsync(int page, UcListView.Filter filter)
        {
            List<PaymentReason> reasons = GetPaymentReasons();


            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<PaymentService>();
                return filter.ColumnName switch
                {
                    CustomerNameFilter
                    => await service.GetAllByCustomerNameAndDateTimeAsync(page, RowsPerPage, filter.Text1Value, filter.dateTime, reasons),


                    _ => new List<PaymentListDto>()
                };
            }
        }
        #endregion

        #region Menu Strip 
        private void ShowCustomerMenuToolStrip_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<PaymentListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Payment)));
                return;
            }

            try
            {
                var frm = new frmShowCustomer(_serviceProvider, item.CustomerId);
                frm.ShowDialog();
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ");
            }
        }

        private void ShowInvoiceToolMenuStrip_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<PaymentListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Payment)));
                return;
            }

            if (item.InvoiceId == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Invoice)));
                return;
            }

            try
            {
                var frm = new frmShowInvoice(_serviceProvider, (int)item.InvoiceId);
                frm.ShowDialog();
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ");
            }
            ucListView1.RefreshAfterOperation();
        }
        #endregion

    }
}

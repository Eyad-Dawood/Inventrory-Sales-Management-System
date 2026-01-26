using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.General.General_Forms;
using InventorySalesManagementSystem.Products;
using InventorySalesManagementSystem.UserControles;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.DTOs.WorkerDTO;
using LogicLayer.Services;
using LogicLayer.Services.Invoices;
using LogicLayer.Validation.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;
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
    public partial class frmInvoiceListScreen : frmBaseListScreen
    {
        private readonly IServiceProvider _serviceProvider;
        protected override ContextMenuStrip GridContextMenu => cms;

        public frmInvoiceListScreen(IServiceProvider serviceProvider, bool selectButton)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            SelectButton = selectButton;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lbTitle.Text = "شاشة الفواتير";
        }

        #region Configure
        protected override List<UcListView.FilterItems> ConfigureFilter()
        {
            return new List<UcListView.FilterItems>()
                {
                    new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager.GetArabicEntityName(typeof(Customer)),
                                                 Value = nameof(Customer)+nameof(Customer.Person.FullName)},
                     new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager.GetArabicEntityName(typeof(Worker)),
                                                 Value = nameof(Worker)+nameof(Worker.Person.FullName)}
                };
        }
        protected override void ConfigureGrid(DataGridView dgv)
        {
            base.ConfigureGrid(dgv);

            // ===== InvoiceId =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.InvoiceId),
                DataPropertyName = nameof(InvoiceListDto.InvoiceId),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(Invoice), nameof(Invoice.InvoiceId)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // ===== CustomerName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.CustomerName),
                DataPropertyName = nameof(InvoiceListDto.CustomerName),
                HeaderText = "العميل",
                FillWeight = 40
            });

            // ===== WorkerName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.WorkerName),
                DataPropertyName = nameof(InvoiceListDto.WorkerName),
                HeaderText = "العامل",
                FillWeight = 40
            });


            // ===== TotalBuyingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.TotalBuyingPrice),
                DataPropertyName = nameof(InvoiceListDto.TotalBuyingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(Invoice), nameof(Invoice.TotalBuyingPrice)),
                FillWeight = 20
            });

            // ===== TotalSellingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.TotalSellingPrice),
                DataPropertyName = nameof(InvoiceListDto.TotalSellingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(Invoice), nameof(Invoice.TotalSellingPrice)),
                FillWeight = 20
            });


            // ===== TotalRefundBuyingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.TotalRefundBuyingPrice),
                DataPropertyName = nameof(InvoiceListDto.TotalRefundBuyingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(Invoice), nameof(Invoice.TotalRefundBuyingPrice)),
                FillWeight = 20
            });

            // ===== TotalRefundSellingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.TotalRefundSellingPrice),
                DataPropertyName = nameof(InvoiceListDto.TotalRefundSellingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(Invoice), nameof(Invoice.TotalRefundSellingPrice)),
                FillWeight = 20
            });


            // ===== NetBuying =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.NetBuying),
                DataPropertyName = nameof(InvoiceListDto.NetBuying),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(InvoiceListDto), nameof(InvoiceListDto.NetBuying)),
                FillWeight = 20
            });

            // ===== NetSale =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.NetSale),
                DataPropertyName = nameof(InvoiceListDto.NetSale),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(InvoiceListDto), nameof(InvoiceListDto.NetSale)),
                FillWeight = 20
            });



            // ===== Additional =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.Additional),
                DataPropertyName = nameof(InvoiceListDto.Additional),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(Invoice), nameof(Invoice.Additional)),
                FillWeight = 20
            });


            // ===== NetProfit =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.NetProfit),
                DataPropertyName = nameof(InvoiceListDto.NetProfit),
                HeaderText = LogicLayer.Utilities.NamesManager
        .GetArabicPropertyName(typeof(InvoiceListDto), nameof(InvoiceListDto.NetProfit)),
                FillWeight = 20
            });

            // ===== AmountDue =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.AmountDue),
                DataPropertyName = nameof(InvoiceListDto.AmountDue),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(InvoiceListDto), nameof(InvoiceListDto.AmountDue)),
                FillWeight = 20
            });


            // ===== TotalPaid =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.TotalPaid),
                DataPropertyName = nameof(InvoiceListDto.TotalPaid),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(Invoice), nameof(Invoice.TotalPaid)),
                FillWeight = 20
            });


            // ===== Remaining =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.Remaining),
                DataPropertyName = nameof(InvoiceListDto.Remaining),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(InvoiceListDto), nameof(InvoiceListDto.Remaining)),
                FillWeight = 20
            });





            // ===== InvoiceType =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.InvoiceType),
                DataPropertyName = nameof(InvoiceListDto.InvoiceType),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(Invoice), nameof(Invoice.InvoiceType)),
                FillWeight = 15
            });

            // ===== InvoiceState =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.InvoiceState),
                DataPropertyName = nameof(InvoiceListDto.InvoiceState),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(Invoice), nameof(Invoice.InvoiceState)),
                FillWeight = 15
            });

            // ===== OpenDate =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.OpenDate),
                DataPropertyName = nameof(InvoiceListDto.OpenDate),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(Invoice), nameof(Invoice.OpenDate)),
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "yyyy/MM/dd"
                },
                FillWeight = 25
            });

            // ===== CloseDate =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(InvoiceListDto.CloseDate),
                DataPropertyName = nameof(InvoiceListDto.CloseDate),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(Invoice), nameof(Invoice.CloseDate)),
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "yyyy/MM/dd"
                },
                FillWeight = 25
            });
        }
        #endregion

        private List<InvoiceType> GetInvoiceTypes()
        {
            List<InvoiceType> types = new List<InvoiceType>();

            if (chkSell.Checked)
                types.Add(InvoiceType.Sale);

            if (chkEvaluation.Checked)
                types.Add(InvoiceType.Evaluation);

            return types;
        }
        private List<InvoiceState> GetInvoiceStates()
        {
            List<InvoiceState> states = new List<InvoiceState>();

            if (chkOpen.Checked)
                states.Add(InvoiceState.Open);

            if (chkClose.Checked)
                states.Add(InvoiceState.Closed);

            return states;
        }


        #region Hooks
        protected async override Task<int> GetTotalPagesAsync()
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<InvoiceService>();
                return await service.GetTotalPageNumberAsync(RowsPerPage);
            }
        }


        protected async override Task<int> GetTotalFilteredPagesAsync(UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<InvoiceService>();

                List<InvoiceType> types = GetInvoiceTypes();
                List<InvoiceState> states = GetInvoiceStates();

                return filter.ColumnName switch
                {
                    nameof(Worker) + nameof(Worker.Person.FullName)
                        => await service.GetTotalPageByWorkerNameAsync(filter.Text1Value, RowsPerPage, types, states),

                    nameof(Customer) + nameof(Customer.Person.FullName)
                        => await service.GetTotalPageByCustomerNameAsync(filter.Text1Value, RowsPerPage, types, states),

                    _ => 0
                };
            }
        }

        protected async override Task<IEnumerable<object>> GetDataAsync(int page)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<InvoiceService>();

                List<InvoiceType> types = GetInvoiceTypes();
                List<InvoiceState> states = GetInvoiceStates();

                return await service.GetAllInvoicesAsync(page, RowsPerPage, types, states);
            }
        }

        protected async override Task<IEnumerable<object>> GetFilteredDataAsync(int page, UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<InvoiceService>();

                List<InvoiceType> types = GetInvoiceTypes();
                List<InvoiceState> states = GetInvoiceStates();

                return filter.ColumnName switch
                {
                    nameof(Worker) + nameof(Worker.Person.FullName)
                        => await service.GetAllByWorkerNameAsync(filter.Text1Value, page, RowsPerPage, types, states),

                    nameof(Customer) + nameof(Customer.Person.FullName)
                        => await service.GetAllByCustomerNameAsync(filter.Text1Value, page, RowsPerPage, types, states),

                    _ => new List<InvoiceListDto>()
                };
            }
        }
        #endregion


        #region ButtonEvents
        protected async override Task HandleAddButtonClicked()
        {
            var frm = new frmAddInvoice(_serviceProvider);
            frm.ShowDialog();

            ucListView1.RefreshAfterOperation();
        }
        #endregion


        #region MeneuStrip

        private void ShowMenustripItem_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<InvoiceListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Invoice)));
                return;
            }

            try
            {
                var frm = new frmShowInvoice(_serviceProvider);
                _ = frm.ShowInvoice(item.InvoiceId);
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


        private void AddNewBatchMenuStripItem_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<InvoiceListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Invoice)));
                return;
            }

            try
            {
                var frm = new frmAddBatchToInvoice(_serviceProvider,item.InvoiceId);
                frm.ShowDialog();
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message);
            }
            catch (OperationFailedException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ");
            }
            ucListView1.RefreshAfterOperation();
        }
    }
}

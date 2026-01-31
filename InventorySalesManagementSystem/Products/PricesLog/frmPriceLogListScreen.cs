using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.General.General_Forms;
using InventorySalesManagementSystem.UserControles;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.ProductDTO.PriceLogDTO;
using LogicLayer.DTOs.ProductDTO.StockMovementLogDTO;
using LogicLayer.DTOs.WorkerDTO;
using LogicLayer.Services;
using LogicLayer.Services.Products;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventorySalesManagementSystem.Products.PricesLog
{
    public partial class frmPriceLogListScreen : frmBaseListScreen
    {
        private readonly IServiceProvider _serviceProvider;
        protected override ContextMenuStrip GridContextMenu => cms;

        public frmPriceLogListScreen(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SelectButton = false;
            AddButton = false;
            lbTitle.Text = "سجلات الأسعار";
        }


        #region Configure
        protected override List<UcListView.FilterItems> ConfigureFilter()
        {
            return new List<UcListView.FilterItems>()
                {
                    new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductPriceLogListDto), nameof(ProductPriceLogListDto.ProductFullName)),
                                                 Value = nameof(ProductPriceLogListDto.ProductFullName)},
                };
        }
        protected override void ConfigureGrid(DataGridView dgv)
        {
            base.ConfigureGrid(dgv);


            // ===== ProductId =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductPriceLogListDto.ProductId),
                DataPropertyName = nameof(ProductPriceLogListDto.ProductId),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.ProductId)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // ===== ProductName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductPriceLogListDto.ProductFullName),
                DataPropertyName = nameof(ProductPriceLogListDto.ProductFullName),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductPriceLogListDto), nameof(ProductPriceLogListDto.ProductFullName)),
                FillWeight = 80
            });

            // ===== OldBuyingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductPriceLogListDto.OldBuyingPrice),
                DataPropertyName = nameof(ProductPriceLogListDto.OldBuyingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductPriceLog), nameof(ProductPriceLog.OldBuyingPrice)),
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    BackColor = Color.FromArgb(235, 245, 255)
                }
            });

            // ===== NewBuyingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductPriceLogListDto.NewBuyingPrice),
                DataPropertyName = nameof(ProductPriceLogListDto.NewBuyingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductPriceLog), nameof(ProductPriceLog.NewBuyingPrice)),
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    BackColor = Color.FromArgb(180, 210, 245)
                }
            });

            // ===== OldSellingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductPriceLogListDto.OldSellingPrice),
                DataPropertyName = nameof(ProductPriceLogListDto.OldSellingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductPriceLog), nameof(ProductPriceLog.OldSellingPrice)),
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    BackColor = Color.FromArgb(240, 250, 240)
                }
            });

            // ===== NewSellingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductPriceLogListDto.NewSellingPrice),
                DataPropertyName = nameof(ProductPriceLogListDto.NewSellingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductPriceLog), nameof(ProductPriceLog.NewSellingPrice)),
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    BackColor = Color.FromArgb(200, 230, 200)
                }
            });


            // ===== CreatedBy =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductPriceLogListDto.CreatedByUserName),
                DataPropertyName = nameof(ProductPriceLogListDto.CreatedByUserName),
                FillWeight = 20,
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(User), nameof(User.Username))
            });

            // ===== LogDate =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductPriceLogListDto.LogDate),
                DataPropertyName = nameof(ProductPriceLogListDto.LogDate),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductPriceLog), nameof(ProductPriceLog.LogDate)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "yyyy/MM/dd",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
        }
        #endregion

        #region Hooks
        protected async override Task<int> GetTotalPagesAsync()
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductPriceLogService>();
                return await service.GetTotalPageNumberAsync(RowsPerPage);
            }
        }

        protected async override Task<int> GetTotalFilteredPagesAsync(UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductPriceLogService>();

                return filter.ColumnName switch
                {
                    nameof(ProductPriceLogListDto.ProductFullName)
                   => await service.GetTotalPageByProductNameAndDateAsync(RowsPerPage, filter.Text1Value,filter.dateTime),


                    _ => 0
                };
            }
        }

        protected async override Task<IEnumerable<object>> GetDataAsync(int page)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductPriceLogService>();
                return await service.GetAllPriceLogsAsync(page,RowsPerPage);
            }
        }

        protected async override Task<IEnumerable<object>> GetFilteredDataAsync(int page, UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductPriceLogService>();
                return filter.ColumnName switch
                {
                    nameof(ProductPriceLogListDto.ProductFullName)
                    => await service.GetAllByProductNameAndDateTimeAsync(page, RowsPerPage, filter.Text1Value,filter.dateTime),


                    _ => new List<ProductPriceLogListDto>()
                };
            }
        }
        #endregion

        #region Menu Strip
        private void ShowMenustripItem_Click(object sender, EventArgs e)
        {
            var selecteditem =
             ucListView1.GetSelectedItem<ProductPriceLogListDto>();


            if (selecteditem == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(ProductPriceLog)));
                return;
            }

            string message = $"الإسم الكامل : {selecteditem.ProductFullName}";
            MessageBox.Show(message);
        }
        #endregion
    }
}

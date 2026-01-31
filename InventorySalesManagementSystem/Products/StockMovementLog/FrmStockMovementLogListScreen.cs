using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.General.General_Forms;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.ProductDTO.PriceLogDTO;
using LogicLayer.DTOs.ProductDTO.StockMovementLogDTO;
using LogicLayer.Services;
using LogicLayer.Services.Products;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventorySalesManagementSystem.Products.StockMovementLog
{
    public partial class FrmStockMovementLogListScreen : frmBaseListScreen
    {
        private readonly IServiceProvider _serviceProvider;
        protected override ContextMenuStrip GridContextMenu => cms;


        public FrmStockMovementLogListScreen(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SelectButton = false;
            AddButton = false;
            lbTitle.Text = "سجلات حركة المخزون";
        }


        #region Configure
        protected override List<UcListView.FilterItems> ConfigureFilter()
        {
            return new List<UcListView.FilterItems>()
                {
                    new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductStockMovementLogListDto), nameof(ProductStockMovementLogListDto.ProductFullName)),
                                                 Value = nameof(ProductStockMovementLogListDto.ProductFullName)},
                };
        }
        protected override void ConfigureGrid(DataGridView dgv)
        {
            base.ConfigureGrid(dgv);


            // ===== ProductId =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductStockMovementLogListDto.ProductId),
                DataPropertyName = nameof(ProductStockMovementLogListDto.ProductId),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.ProductId)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // ===== ProductName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductStockMovementLogListDto.ProductFullName),
                DataPropertyName = nameof(ProductStockMovementLogListDto.ProductFullName),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductStockMovementLogListDto), nameof(ProductStockMovementLogListDto.ProductFullName)),
                FillWeight = 80
            });

            // ===== OldQuantity =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductStockMovementLogListDto.OldQuantity),
                DataPropertyName = nameof(ProductStockMovementLogListDto.OldQuantity),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductStockMovementLog), nameof(ProductStockMovementLog.OldQuantity)),
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N4",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // ===== NewQuantity =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductStockMovementLogListDto.NewQuantity),
                DataPropertyName = nameof(ProductStockMovementLogListDto.NewQuantity),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductStockMovementLog), nameof(ProductStockMovementLog.NewQuantity)),
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N4",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // ===== QuantityChange =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductStockMovementLogListDto.QuantityChange),
                DataPropertyName = nameof(ProductStockMovementLogListDto.QuantityChange),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductStockMovementLogListDto), nameof(ProductStockMovementLogListDto.QuantityChange)),
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N4",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // ===== Reason =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductStockMovementLogListDto.Reason),
                DataPropertyName = nameof(ProductStockMovementLogListDto.Reason),
                FillWeight = 20,
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductStockMovementLog), nameof(ProductStockMovementLog.Reason))
            });

            // ===== CreatedBy =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductStockMovementLogListDto.CreatedbyUserName),
                DataPropertyName = nameof(ProductStockMovementLogListDto.CreatedbyUserName),
                FillWeight = 20,
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(User), nameof(User.Username))
            });

            // ===== LogDate =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductStockMovementLogListDto.LogDate),
                DataPropertyName = nameof(ProductStockMovementLogListDto.LogDate),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductStockMovementLog), nameof(ProductStockMovementLog.LogDate)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "yyyy/MM/dd",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // ===== Notes =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductStockMovementLogListDto.Notes),
                DataPropertyName = nameof(ProductStockMovementLogListDto.Notes),
                HeaderText = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(ProductStockMovementLog), nameof(ProductStockMovementLog.Notes)),
                FillWeight = 15
            });

            dgv.CellFormatting += (s, e) =>
            {
                if (dgv.Columns[e.ColumnIndex].Name ==
                    nameof(ProductStockMovementLogListDto.QuantityChange))
                {
                    e.CellStyle.ForeColor = Color.Black;

                    if (e.Value != null &&
                        decimal.TryParse(e.Value.ToString(), out decimal value))
                    {
                        e.CellStyle.ForeColor = value switch
                        {
                            > 0 => Color.DarkGreen,
                            < 0 => Color.DarkRed,
                            _ => Color.Black
                        };
                    }
                }
            };

        }
        #endregion

        #region Hooks
        protected async override Task<int> GetTotalPagesAsync()
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductStockMovementLogService>();
                return await service.GetTotalPageNumberAsync(RowsPerPage);
            }
        }

        protected async override Task<int> GetTotalFilteredPagesAsync(UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductStockMovementLogService>();

                return filter.ColumnName switch
                {
                    nameof(ProductStockMovementLogListDto.ProductFullName)
                    => await service.GetTotalPageByProductNameAndDateAsync(RowsPerPage, filter.Text1Value,filter.dateTime),


                    _ => 0
                };
            }
        }

        protected async override Task<IEnumerable<object>> GetDataAsync(int page)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductStockMovementLogService>();
                return await service.GetAllProductMovmentsAsync(page, RowsPerPage);
            }
        }

        protected async override Task<IEnumerable<object>> GetFilteredDataAsync(int page, UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductStockMovementLogService>();
                return filter.ColumnName switch
                {
                    nameof(ProductStockMovementLogListDto.ProductFullName)
                    => await service.GetAllByProductNameAndDateTimeAsync(page, RowsPerPage, filter.Text1Value,filter.dateTime),


                    _ => new List<ProductStockMovementLogListDto>()
                };
            }
        }
        #endregion

        #region Menu Strip
        private void ShowMenustripItem_Click(object sender, EventArgs e)
        {
            var selecteditem =
             ucListView1.GetSelectedItem<ProductStockMovementLogListDto>();


            if (selecteditem == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(ProductStockMovementLog)));
                return;
            }

            string message = $"الإسم الكامل : {selecteditem.ProductFullName}" +
                $"\n\n السبب : {(selecteditem.Reason)}"  +
                $"\n\n الملاحظات : {selecteditem.Notes} ";
            MessageBox.Show(message);
        }
        #endregion
    }
}

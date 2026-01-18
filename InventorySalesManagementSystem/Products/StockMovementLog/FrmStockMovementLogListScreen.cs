using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
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

namespace InventorySalesManagementSystem.Products.StockMovementLog
{
    public partial class FrmStockMovementLogListScreen : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private const int RowsPerPage = 30;


        public FrmStockMovementLogListScreen(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        #region Config
        private void ConfigureContextMenuStrip(DataGridView dgv)
        {
            dgv.ContextMenuStrip = this.cms;
        }
        private void ConfigureGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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

            // ===== Header Style =====
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font(dgv.Font, FontStyle.Bold);

            dgv.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

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

            ConfigureContextMenuStrip(dgv);
        }

        private void ConfigureFilter()
        {
            var items = new List<UcListView.FilterItems>()
                {
                    new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductStockMovementLogListDto), nameof(ProductStockMovementLogListDto.ProductFullName)),
                                                 Value = nameof(ProductStockMovementLogListDto.ProductFullName)},
                };

            ucListView1.ConfigureFilter(items);
        }
        #endregion

        #region DataGetter
        private async Task<List<ProductStockMovementLogListDto>> GetData(ProductStockMovementLogService service,
                                              int PageNumber)
        {
            return await service.GetAllProductMovmentsAsync(PageNumber, RowsPerPage);
        }
        private async Task<List<ProductStockMovementLogListDto>> GetFilteredData(
            ProductStockMovementLogService service,
            string columnName,
            int PageNumber,
            string value,
            DateTime? date)
        {
            return columnName switch
            {
                nameof(ProductStockMovementLogListDto.ProductFullName)
                    => await service.GetAllByProductNameAndDateTimeAsync(PageNumber, RowsPerPage, value, date),


                _ => new List<ProductStockMovementLogListDto>()
            };
        }

        private async Task<int> GetTotalFilteredPages(
            ProductStockMovementLogService service,
            string columnName,
            string value,
            DateTime? date)
        {
            return columnName switch
            {
                nameof(ProductStockMovementLogListDto.ProductFullName)
                    => await service.GetTotalPageByProductNameAndDateAsync(RowsPerPage, value, date),


                _ => 0
            };
        }

        private async Task<int> GetTotalPages(ProductStockMovementLogService service)
        {
            return await service.GetTotalPageNumberAsync(RowsPerPage);
        }
        #endregion



        private async Task DisplayPage(int PageNumber)
        {
            //Call filterMethod With Null Fitler
           await DisplayFilteredPage(PageNumber, null);
        }

        private async Task DisplayFilteredPage(int PageNumber, UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductStockMovementLogService>();

                bool isFiltered = ucListView1.IsDataFiltered && filter != null;

                int totalPages = isFiltered
                    ? await GetTotalFilteredPages(service, filter.ColumnName, filter.Text1Value, filter.dateTime)
                    : await GetTotalPages(service);

                int pageToRequest = Math.Max(1, Math.Min(PageNumber, totalPages));

                var data = isFiltered
                    ? await GetFilteredData(service, filter.ColumnName, pageToRequest, filter.Text1Value, filter.dateTime)
                    : await GetData(service, pageToRequest);

                ucListView1.DisplayData<ProductStockMovementLogListDto>(data, pageToRequest, totalPages);
            }
        }

        private async Task OnFilterClicked(UcListView.Filter filter)
        {
           await DisplayFilteredPage(1, filter);
        }
        private async Task OnFilterCanceled()
        {
           await DisplayPage(1);
        }
        private async Task OnPageChanged(int PageNumber, UcListView.Filter filter)
        {
           await DisplayFilteredPage(PageNumber, filter);
        }
        private async Task OnOperationFinished(int PageNumber, UcListView.Filter filter)
        {
           await DisplayFilteredPage(PageNumber, filter);
        }

        private async void FrmStockMovementLogListScreen_Load(object sender, EventArgs e)
        {
            ucListView1.OnFilterClicked = OnFilterClicked;
            ucListView1.OnFilterCanceled = OnFilterCanceled;
            ucListView1.OnNextPage = OnPageChanged;
            ucListView1.OnPreviousPage = OnPageChanged;
            ucListView1.OnRefreshAfterOperation = OnOperationFinished;

            ucListView1.ConfigureGrid = ConfigureGrid;

           await DisplayPage(1);
            ConfigureFilter();
        }

        


        private void ShowMenustripItem_Click(object sender, EventArgs e)
        {
            var selecteditem =
             ucListView1.GetSelectedItem<ProductStockMovementLogListDto>();


            if (selecteditem == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(ProductStockMovementLog)));
                return;
            }

            string message = $"الإسم الكامل : {selecteditem.ProductFullName} \n\n الملاحظات : {selecteditem.Notes}";
            MessageBox.Show(message);
        }
    }
}

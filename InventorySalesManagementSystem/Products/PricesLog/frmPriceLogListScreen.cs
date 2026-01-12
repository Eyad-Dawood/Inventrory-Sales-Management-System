using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.DTOs.ProductDTO.PriceLogDTO;
using LogicLayer.DTOs.ProductDTO.StockMovementLogDTO;
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

namespace InventorySalesManagementSystem.Products.PricesLog
{
    public partial class frmPriceLogListScreen : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private const int RowsPerPage = 30;

        public frmPriceLogListScreen(IServiceProvider serviceProvider)
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


            // ===== Header Style =====
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font(dgv.Font, FontStyle.Bold);

            dgv.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            ConfigureContextMenuStrip(dgv);
        }
        private void ConfigureFilter()
        {
            var items = new List<UcListView.FilterItems>()
                {
                    new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductPriceLogListDto), nameof(ProductPriceLogListDto.ProductFullName)),
                                                 Value = nameof(ProductPriceLogListDto.ProductFullName)},
                };

            ucListView1.ConfigureFilter(items);
        }
        #endregion

        #region DataGetter
        private List<ProductPriceLogListDto> GetData(ProductPriceLogService service,
                                              int PageNumber)
        {
            return service.GetAllPriceLogs(PageNumber, RowsPerPage);
        }
        private List<ProductPriceLogListDto> GetFilteredData(
            ProductPriceLogService service,
            string columnName,
            int PageNumber,
            string value,
            DateTime? date)
        {
            return columnName switch
            {
                nameof(ProductPriceLogListDto.ProductFullName)
                    => service.GetAllByProductNameAndDateTime(PageNumber, RowsPerPage, value, date),


                _ => new List<ProductPriceLogListDto>()
            };
        }

        private int GetTotalFilteredPages(
            ProductPriceLogService service,
            string columnName,
            string value,
            DateTime? date)
        {
            return columnName switch
            {
                nameof(ProductPriceLogListDto.ProductFullName)
                    => service.GetTotalPageByProductNameAndDate(RowsPerPage, value, date),


                _ => 0
            };
        }

        private int GetTotalPages(ProductPriceLogService service)
        {
            return service.GetTotalPageNumber(RowsPerPage);
        }
        #endregion
        private void DisplayPage(int PageNumber)
        {
            //Call filterMethod With Null Fitler
            DisplayFilteredPage(PageNumber, null);
        }

        private void DisplayFilteredPage(int PageNumber, UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductPriceLogService>();

                bool isFiltered = ucListView1.IsDataFiltered && filter != null;

                int totalPages = isFiltered
                    ? GetTotalFilteredPages(service, filter.ColumnName, filter.Text1Value, filter.dateTime)
                    : GetTotalPages(service);

                int pageToRequest = Math.Max(1, Math.Min(PageNumber, totalPages));

                var data = isFiltered
                    ? GetFilteredData(service, filter.ColumnName, pageToRequest, filter.Text1Value, filter.dateTime)
                    : GetData(service, pageToRequest);

                ucListView1.DisplayData<ProductPriceLogListDto>(data, pageToRequest, totalPages);
            }
        }


        private void OnFilterClicked(UcListView.Filter filter)
        {
            DisplayFilteredPage(1, filter);
        }
        private void OnFilterCanceled()
        {
            DisplayPage(1);
        }
        private void OnPageChanged(int PageNumber, UcListView.Filter filter)
        {
            DisplayFilteredPage(PageNumber, filter);
        }
        private void OnOperationFinished(int PageNumber, UcListView.Filter filter)
        {
            DisplayFilteredPage(PageNumber, filter);
        }

        private void frmPriceLogListScreen_Load(object sender, EventArgs e)
        {
            ucListView1.OnFilterClicked = OnFilterClicked;
            ucListView1.OnFilterCanceled = OnFilterCanceled;
            ucListView1.OnNextPage = OnPageChanged;
            ucListView1.OnPreviousPage = OnPageChanged;
            ucListView1.OnRefreshAfterOperation = OnOperationFinished;

            ucListView1.ConfigureGrid = ConfigureGrid;

            DisplayPage(1);
            ConfigureFilter();
        }


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
    }
}

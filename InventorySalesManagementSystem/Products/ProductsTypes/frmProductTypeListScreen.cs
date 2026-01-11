using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.ProductTypeDTO;
using LogicLayer.Services;
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

namespace InventorySalesManagementSystem.Products.ProductsTypes
{
    public partial class frmProductTypeListScreen : Form
    {
        public ProductTypeListDto SelectedProductType { get; private set; }
        private bool _selectMode = false;

        [Browsable(false)]
        [DefaultValue(false)]
        public bool SelectMode 
        {
            get 
            {
            return _selectMode;
            }
                set
            {
                btnSelect.Visible = value;
                btnSelect.Enabled = value;

            _selectMode = value;
            }
        }
      

        private readonly IServiceProvider _serviceProvider;
        private const int RowsPerPage = 30;

        public frmProductTypeListScreen(IServiceProvider serviceProvider, bool selectMode)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            SelectMode = selectMode;
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

            // ===== ProductTypeId =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductTypeListDto.ProductTypeId),
                DataPropertyName = nameof(ProductTypeListDto.ProductTypeId),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductType), nameof(ProductType.ProductTypeId)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // ===== Name =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductTypeListDto.Name),
                DataPropertyName = nameof(ProductTypeListDto.Name),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductType), nameof(ProductType.ProductTypeName)),
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
                    new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(ProductType), nameof(ProductType.ProductTypeName)),
                                                 Value = nameof(ProductType.ProductTypeName)}                };

            ucListView1.ConfigureFilter(items);
        }
        #endregion

        #region DataGetter
        private List<ProductTypeListDto> GetData(ProductTypeService service,
                                              int PageNumber)
        {
            return service.GetAllProductTypes(PageNumber, RowsPerPage);
        }
        private List<ProductTypeListDto> GetFilteredData(
            ProductTypeService service,
            string columnName,
            int PageNumber,
            string value)
        {
            return columnName switch
            {
                nameof(ProductType.ProductTypeName)
                    => service.GetAllByProductTypeName(PageNumber, RowsPerPage, value),

                _ => new List<ProductTypeListDto>()
            };
        }

        private int GetTotalFilteredPages(
            ProductTypeService service,
            string columnName,
            string value)
        {
            return columnName switch
            {
                nameof(ProductType.ProductTypeName)
                    => service.GetTotalPagesByProductTypeName(value, RowsPerPage),

                _ => 0
            };
        }

        private int GetTotalPages(ProductTypeService service)
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
                var service = scope.ServiceProvider.GetRequiredService<ProductTypeService>();

                bool isFiltered = ucListView1.IsDataFiltered && filter != null;

                int totalPages = isFiltered
                    ? GetTotalFilteredPages(service, filter.ColumnName, filter.FilterValues[0])
                    : GetTotalPages(service);

                int pageToRequest = Math.Max(1, Math.Min(PageNumber, totalPages));

                var data = isFiltered
                    ? GetFilteredData(service, filter.ColumnName, pageToRequest, filter.FilterValues[0])
                    : GetData(service, pageToRequest);

                ucListView1.DisplayData<ProductTypeListDto>(data, pageToRequest, totalPages);
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

        private void frmProductTypeListScreen_Load(object sender, EventArgs e)
        {
            ucListView1.OnFilterClicked = OnFilterClicked;
            ucListView1.OnFilterCanceled = OnFilterCanceled;
            ucListView1.OnNextPage = OnPageChanged;
            ucListView1.OnPreviousPage = OnPageChanged;
            ucListView1.OnRefreshAfterOperation = OnOperationFinished;

            ucListView1.ConfigureGrid = ConfigureGrid;

            DisplayPage(1);
            ConfigureFilter();

            ucListView1.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = frmAddUpdateProductType.CreateForAdd(_serviceProvider);
            frm.ShowDialog();

            ucListView1.RefreshAfterOperation();
        }

        /// <summary>
        /// Get Selected Id From Data Grid View 
        /// </summary>
        /// <returns>-1 if null</returns>
        private int GetSelectedId()
        {
            var selecteditem =
             ucListView1.GetSelectedItem<ProductTypeListDto>();

            if (selecteditem != null)
            {
                return selecteditem.ProductTypeId;
            }

            return -1;
        }

        private void updateMenustripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(ProductType)));
                return;
            }

            try
            {
                var frm = frmAddUpdateProductType.CreateForUpdate(_serviceProvider, id);
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

        private void deleteMenustripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(ProductType)));
                return;
            }

            var selectedProdcutType = ucListView1.GetSelectedItem<ProductTypeListDto>();
            string name = selectedProdcutType?.Name ?? id.ToString();

            string message = $"هل أنت متأكد من حذف الموديل؟\n\n" +
                             $"المعرف: {selectedProdcutType.ProductTypeId}\n" +
                             $"الاسم: >> {name} <<\n\n" +
                             $"تحذير: لا يمكن التراجع عن هذه العملية!";

            if (MessageBox.Show(message, "تأكيد الحذف",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductTypeService>();

                try
                {
                    service.DeleteById(id);
                }
                catch (NotFoundException ex)
                {
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(ProductType)));
                    return;
                }
                catch (OperationFailedException ex)
                {
                    Serilog.Log.Error(ex.InnerException, "Unexcepected Error During Deleting ProductType ");
                    MessageBox.Show(ex.MainBody, ex.Message);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "Unexcepected Error During Deleting ProductType ");
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.OperationFailedErrorMessage());
                }

                ucListView1.RefreshAfterOperation();
            }

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectedProductType = ucListView1.GetSelectedItem<ProductTypeListDto>();

            if (SelectMode && SelectedProductType!=null)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}

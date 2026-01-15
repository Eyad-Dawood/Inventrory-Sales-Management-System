using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.Products;
using InventorySalesManagementSystem.Products.Extra;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.Global.Users;
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

namespace InventorySalesManagementSystem.Products
{
    public partial class frmProductListScreen : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private const int RowsPerPage = 40;
        private const string FilterName = "FullName";

        public frmProductListScreen(IServiceProvider serviceProvider)
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
                Name = nameof(ProductListDto.ProductId),
                DataPropertyName = nameof(ProductListDto.ProductId),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.ProductId)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // ===== ProductTypeName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductListDto.ProductTypeName),
                DataPropertyName = nameof(ProductListDto.ProductTypeName),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductType), nameof(ProductType.ProductTypeName)),
                FillWeight = 35
            });

            // ===== ProductName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductListDto.ProductName),
                DataPropertyName = nameof(ProductListDto.ProductName),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.ProductName)),
                FillWeight = 35
            });


            // ===== MasurementUnitName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductListDto.MesurementUnitName),
                DataPropertyName = nameof(ProductListDto.MesurementUnitName),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(MasurementUnit), nameof(MasurementUnit.UnitName)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });


            // ===== BuyingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductListDto.BuyingPrice),
                DataPropertyName = nameof(ProductListDto.BuyingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.BuyingPrice)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // ===== SellingPrice =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductListDto.SellingPrice),
                DataPropertyName = nameof(ProductListDto.SellingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.SellingPrice)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // ===== Profit (Computed Column) =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductListDto.Profit),
                DataPropertyName = nameof(ProductListDto.Profit),
                HeaderText = "الربح",
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    ForeColor = Color.DarkGreen
                }
            });


            // ===== Quantity =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductListDto.QuantityInStorage),
                DataPropertyName = nameof(ProductListDto.QuantityInStorage),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.QuantityInStorage)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N4",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });


            // ===== IsAvilable =====
            dgv.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = nameof(ProductListDto.IsAvilable),
                DataPropertyName = nameof(ProductListDto.IsAvilable),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.IsAvailable)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
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
                    new UcListView.FilterItems(){DisplayName = "إسم الموديل/المنتج",
                                                 Value = FilterName},
                };

            ucListView1.ConfigureFilter(items);
        }
        #endregion


        #region DataGetter
        private List<ProductListDto> GetData(ProductService service,
                                              int PageNumber)
        {
            return service.GetAllProducts(PageNumber, RowsPerPage);
        }
        private List<ProductListDto> GetFilteredData(
            ProductService service,
            string columnName,
            int PageNumber,
            string ProductTypeName,
            string ProductName)
        {
            return columnName switch
            {
                FilterName
                    => service.GetAllByFullName(PageNumber, RowsPerPage, ProductTypeName, ProductName),

                _ => new List<ProductListDto>()
            };
        }

        private int GetTotalFilteredPages(
            ProductService service,
            string columnName,
            string ProductTypeName,
            string ProductName)
        {
            return columnName switch
            {
                FilterName
                     => service.GetTotalPageByFullName(ProductTypeName, ProductName, RowsPerPage),

                _ => 0
            };
        }

        private int GetTotalPages(ProductService service)
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
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();

                bool isFiltered = ucListView1.IsDataFiltered && filter != null;

                int totalPages = isFiltered
                    ? GetTotalFilteredPages(service, filter.ColumnName, filter.Text1Value, filter.Text2Value)
                    : GetTotalPages(service);

                int pageToRequest = Math.Max(1, Math.Min(PageNumber, totalPages));

                var data = isFiltered
                    ? GetFilteredData(service, filter.ColumnName, pageToRequest, filter.Text1Value, filter.Text2Value)
                    : GetData(service, pageToRequest);

                ucListView1.DisplayData<ProductListDto>(data, pageToRequest, totalPages);
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

        private void frmProductListScreen_Load(object sender, EventArgs e)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = frmAddUpdateProduct.CreateForAdd(_serviceProvider);
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
             ucListView1.GetSelectedItem<ProductListDto>();

            if (selecteditem != null)
            {
                return selecteditem.ProductId;
            }

            return -1;
        }

        private void updateMenustripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                return;
            }

            try
            {
                var frm = frmAddUpdateProduct.CreateForUpdate(_serviceProvider, id);
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
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                return;
            }

            var selectedProduct = ucListView1.GetSelectedItem<ProductListDto>();
            string name = $"{selectedProduct.ProductTypeName}[{selectedProduct?.ProductName}]" ?? id.ToString();

            string message = $"هل أنت متأكد من حذف المنتج؟\n\n" +
                             $"المعرف: {selectedProduct.ProductId}\n" +
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
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();

                try
                {
                    service.DeleteProductById(id);
                }
                catch (NotFoundException ex)
                {
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                    return;
                }
                catch (OperationFailedException ex)
                {
                    Serilog.Log.Error(ex.InnerException, "Unexcepected Error During Deleting Product ");
                    MessageBox.Show(ex.MainBody, ex.Message);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "Unexcepected Error During Deleting Product ");
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.OperationFailedErrorMessage());
                }

                ucListView1.RefreshAfterOperation();
            }
        }

        private void ShowMenustripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                return;
            }

            try
            {
                var frm = new frmShowProduct(_serviceProvider, id);
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

        private void changeIsAvilableStateMenuStripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                return;
            }
            var selectedProduct = ucListView1.GetSelectedItem<ProductListDto>();


            string action = selectedProduct.IsAvilable ? "عدم إتاحة" : "إتاحة";
            if (MessageBox.Show($"هل أنت متأكد من {action} المنتج {selectedProduct.ProductTypeName}[{selectedProduct.ProductName}]؟",
                "تأكيد",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }


            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();
                try
                {
                    service.ChangeAvaliableState(id, !selectedProduct.IsAvilable);
                }
                catch (NotFoundException ex)
                {
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                    return;
                }
                catch (OperationFailedException ex)
                {
                    Serilog.Log.Error(ex.InnerException, "Unexcepected Error During Changing Product Activation State ");
                    MessageBox.Show(ex.MainBody, ex.Message);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "Unexcepected Error During Product Activation State ");
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.OperationFailedErrorMessage());
                }
            }

            ucListView1.RefreshAfterOperation();
        }

        private void AddQuantityMenustripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                return;
            }
            var selectedProduct = ucListView1.GetSelectedItem<ProductListDto>();


            using (var scope = _serviceProvider.CreateScope())
            {
                var UserSession = _serviceProvider.GetRequiredService<UserSession>();

                int userid = UserSession.CurrentUser != null ?
                    UserSession.CurrentUser.UserId
                    :
                    -1;

                var service = scope.ServiceProvider.GetRequiredService<ProductService>();
                try
                {
                    using (frmReadQuantityChangeInfo frm = new frmReadQuantityChangeInfo(frmReadQuantityChangeInfo.State.Add))
                    {

                        if(frm.ShowDialog() == DialogResult.OK)
                        {
                            service.AddQuantity(id,frm.Quantity, userid , frm.Reason, frm.Notes);
                            MessageBox.Show($"تم إضافة كمية \n مقدارها : {frm.Quantity} [{selectedProduct.MesurementUnitName}] \n  للمنتج : ({$"{selectedProduct.ProductTypeName} [{selectedProduct.ProductName}]"})");
                        }
                    }
                }
                catch (NotFoundException ex)
                {
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product))); 
                    return;
                }
                catch (OperationFailedException ex)
                {
                    Serilog.Log.Error(ex.InnerException, "Unexcepected Error During Adding Product Quantity ");
                    MessageBox.Show(ex.MainBody, ex.Message);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "Unexcepected Error During Adding Product Quantity ");
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.OperationFailedErrorMessage());
                }
            }

            ucListView1.RefreshAfterOperation();
        }

        private void WithdrawMenustripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                return;
            }
            var selectedProduct = ucListView1.GetSelectedItem<ProductListDto>();


            using (var scope = _serviceProvider.CreateScope())
            {
                var UserSession = _serviceProvider.GetRequiredService<UserSession>();

                int userid = UserSession.CurrentUser != null ?
                    UserSession.CurrentUser.UserId
                    :
                    -1;

                var service = scope.ServiceProvider.GetRequiredService<ProductService>();
                try
                {
                    using (frmReadQuantityChangeInfo frm = new frmReadQuantityChangeInfo(frmReadQuantityChangeInfo.State.Remove))
                    {

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            service.RemoveQuantity(id, frm.Quantity, userid, frm.Reason,frm.Notes);
                            MessageBox.Show($"تم سحب كمية \n مقدارها : {frm.Quantity} [{selectedProduct.MesurementUnitName}] \n  من المنتج : ({$"{selectedProduct.ProductTypeName} [{selectedProduct.ProductName}]"})");
                        }
                    }
                }
                catch (NotFoundException ex)
                {
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                    return;
                }
                catch (OperationFailedException ex)
                {
                    Serilog.Log.Error(ex.InnerException, "Unexcepected Error During Removing Product Quantity ");
                    MessageBox.Show(ex.MainBody, ex.Message);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "Unexcepected Error During Removing Product Quantity ");
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.OperationFailedErrorMessage());
                }
            }

            ucListView1.RefreshAfterOperation();
        }
    }
}

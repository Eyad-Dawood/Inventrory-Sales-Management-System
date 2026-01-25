using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.General.General_Forms;
using InventorySalesManagementSystem.Products;
using InventorySalesManagementSystem.Products.Extra;
using InventorySalesManagementSystem.UserControles;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.DTOs.WorkerDTO;
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
    public partial class frmProductListScreen : frmBaseListScreen
    {
        private readonly IServiceProvider _serviceProvider;
        protected override ContextMenuStrip GridContextMenu => cms;

        private const string FilterName = "FullName";

        public frmProductListScreen(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lbTitle.Text = "شاشة المنتجات";
            SelectButton = false;
            ucListView1.AllowSecondSearchBox = true;
        }


        #region Configure
        protected override List<UcListView.FilterItems> ConfigureFilter()
        {
            return new List<UcListView.FilterItems>()
                {
                    new UcListView.FilterItems(){DisplayName = "إسم الموديل/المنتج",
                                                 Value = FilterName},
                };
        }
        protected override void ConfigureGrid(DataGridView dgv)
        {
            base.ConfigureGrid(dgv);


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


        }
        #endregion

        #region Hooks
        protected async override Task<int> GetTotalPagesAsync()
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();
                return await service.GetTotalPageNumberAsync(RowsPerPage);
            }
        }

        protected async override Task<int> GetTotalFilteredPagesAsync(UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();

                return filter.ColumnName switch
                {
                    FilterName
                      => await service.GetTotalPageByFullNameAsync(filter.Text1Value, filter.Text2Value, RowsPerPage),

                    _ => 0
                };
            }
        }

        protected async override Task<IEnumerable<object>> GetDataAsync(int page)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();
                return await service.GetAllProductsAsync(page, RowsPerPage);
            }
        }

        protected async override Task<IEnumerable<object>> GetFilteredDataAsync(int page, UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();
                return filter.ColumnName switch
                {
                    FilterName
                    => await service.GetAllByFullNameAsync(page, RowsPerPage, filter.Text1Value, filter.Text2Value),

                    _ => new List<ProductListDto>()
                };
            }
        }
        #endregion

        #region Buttons Event
        protected async override Task HandleAddButtonClicked()
        {
            var frm = await frmAddUpdateProduct.CreateForAdd(_serviceProvider);
            frm.ShowDialog();

            ucListView1.RefreshAfterOperation();
        }
        #endregion

        #region Menu Strip

        private async void updateMenustripItem_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<ProductListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                return;
            }

            try
            {
                var frm = await frmAddUpdateProduct.CreateForUpdate(_serviceProvider, item.ProductId);
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

        private async void deleteMenustripItem_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<ProductListDto>();



            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                return;
            }

            string name = $"{item?.ProductTypeName}[{item?.ProductName}]" ?? "";

            string message = $"هل أنت متأكد من حذف المنتج؟\n\n" +
                             $"المعرف: {item?.ProductId}\n" +
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
                   await service.DeleteProductByIdAsync(item.ProductId);
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
            var item = ucListView1.GetSelectedItem<ProductListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                return;
            }

            try
            {
                var frm = new frmShowProduct(_serviceProvider, item.ProductId);
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

        private async void changeIsAvilableStateMenuStripItem_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<ProductListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                return;
            }


            string action = item.IsAvilable ? "عدم إتاحة" : "إتاحة";
            if (MessageBox.Show($"هل أنت متأكد من {action} المنتج {item.ProductTypeName}[{item?.ProductName}]؟",
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
                   await service.ChangeAvaliableStateAsync(item.ProductId, !item.IsAvilable);
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

        private async void AddQuantityMenustripItem_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<ProductListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                return;
            }


            using (var scope = _serviceProvider.CreateScope())
            {
                var UserSession = scope.ServiceProvider.GetRequiredService<UserSession>();

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
                           await service.AddQuantityAsync(item.ProductId,frm.Quantity, userid , frm.Reason, frm.Notes);
                            MessageBox.Show($"تم إضافة كمية \n مقدارها : {frm.Quantity} [{item.MesurementUnitName}] \n  للمنتج : ({$"{item.ProductTypeName} [{item.ProductName}]"})");
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

        private async void WithdrawMenustripItem_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<ProductListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Product)));
                return;
            }


            using (var scope = _serviceProvider.CreateScope())
            {
                var UserSession = scope.ServiceProvider.GetRequiredService<UserSession>();

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
                           await service.RemoveQuantityAsync(item.ProductId, frm.Quantity, userid, frm.Reason,frm.Notes);
                            MessageBox.Show($"تم سحب كمية \n مقدارها : {frm.Quantity} [{item.MesurementUnitName}] \n  من المنتج : ({$"{item.ProductTypeName} [{item.ProductName}]"})");
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
        #endregion
    }
}

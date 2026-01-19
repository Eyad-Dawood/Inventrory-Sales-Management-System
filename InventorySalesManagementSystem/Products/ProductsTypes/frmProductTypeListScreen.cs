using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.General.General_Forms;
using InventorySalesManagementSystem.UserControles;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.ProductTypeDTO;
using LogicLayer.DTOs.WorkerDTO;
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
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.Products.ProductsTypes
{
    public partial class frmProductTypeListScreen : frmBaseListScreen
    {
        public ProductTypeListDto SelectedProductType { get; private set; }


        private readonly IServiceProvider _serviceProvider;
        protected override ContextMenuStrip GridContextMenu => cms;

        public frmProductTypeListScreen(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SelectButton = true;
            lbTitle.Text = "شاشة الموديلات";
        }

        #region Configure
        protected override List<UcListView.FilterItems> ConfigureFilter()
        {
            return new List<UcListView.FilterItems>()
                {
                    new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(ProductType), nameof(ProductType.ProductTypeName)),
                                                 Value = nameof(ProductType.ProductTypeName)}                
            };
        }
        protected override void ConfigureGrid(DataGridView dgv)
        {
            base.ConfigureGrid(dgv);


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
        }
        #endregion


        #region Hooks
        protected async override Task<int> GetTotalPagesAsync()
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductTypeService>();
                return await service.GetTotalPageNumberAsync(RowsPerPage);
            }
        }

        protected async override Task<int> GetTotalFilteredPagesAsync(UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductTypeService>();

                return filter.ColumnName switch
                {
                    nameof(ProductType.ProductTypeName)
                     => await service.GetTotalPagesByProductTypeNameAsync(filter.Text1Value, RowsPerPage),

                    _ => 0
                };
            }
        }

        protected async override Task<IEnumerable<object>> GetDataAsync(int page)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductTypeService>();
                return await service.GetAllProductTypesAsync(page, RowsPerPage);
            }
        }

        protected async override Task<IEnumerable<object>> GetFilteredDataAsync(int page, UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductTypeService>();
                return filter.ColumnName switch
                {
                    nameof(ProductType.ProductTypeName)
                    => await service.GetAllByProductTypeNameAsync(page, RowsPerPage, filter.Text1Value),

                    _ => new List<ProductTypeListDto>()
                };
            }
        }
        #endregion

        #region Buttons Event
        protected override async Task HandleAddButtonClicked()
        {
            //No Async Methods Here
            var frm = frmAddUpdateProductType.CreateForAdd(_serviceProvider);
            frm.ShowDialog();

            ucListView1.RefreshAfterOperation();
        }

        protected async override Task HandleSelectButtonClicked()
        {
            //No Async Here
            SelectedProductType = ucListView1.GetSelectedItem<ProductTypeListDto>();

            if (SelectedProductType != null)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region Menu Strip
        private async void updateMenustripItem_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<ProductTypeListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(ProductType)));
                return;
            }

            try
            {
                var frm = await frmAddUpdateProductType.CreateForUpdate(_serviceProvider, item.ProductTypeId);
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
            var item = ucListView1.GetSelectedItem<ProductTypeListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(ProductType)));
                return;
            }

            string name = item?.Name ?? "";

            string message = $"هل أنت متأكد من حذف الموديل؟\n\n" +
                             $"المعرف: {item?.ProductTypeId}\n" +
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
                   await service.DeleteByIdAsync(item.ProductTypeId);
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
        #endregion
    }
}

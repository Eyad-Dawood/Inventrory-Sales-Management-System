using InventorySalesManagementSystem.General;
using InventorySalesManagementSystem.People.Towns;
using LogicLayer.DTOs.ProductTypeDTO;
using LogicLayer.DTOs.TownDTO;
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
    public partial class frmAddUpdateProductType : Form
    {
        private Enums.FormStateEnum State { set; get; }
        private IServiceProvider _serviceProvider { set; get; }

        private ProductTypeAddDto _ProductTypeAdd { set; get; }
        private ProductTypeUpdateDto _ProductTypeUpdate { set; get; }

        private frmAddUpdateProductType(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void SetupAdd()
        {
            State = Enums.FormStateEnum.AddNew;

            this.Text = "إضافة موديل";

            _ProductTypeAdd = new ProductTypeAddDto();

            // UI defaults
            lbId.Text = "---";
            txtName.Text = string.Empty;
        }
        private void SetupUpdate(ProductTypeUpdateDto dto)
        {
            State = Enums.FormStateEnum.Update;
            this.Text = "تعديل موديل";
            _ProductTypeUpdate = dto;

            //Load Data
            LoadUpdateData(this._ProductTypeUpdate);
        }

        private void LoadUpdateData(ProductTypeUpdateDto dto)
        {
            this.lbId.Text = dto.ProductTypeId.ToString();
            this.txtName.Text = dto.Name;
        }

        public static frmAddUpdateProductType CreateForAdd(IServiceProvider serviceProvider)
        {
            var form = new frmAddUpdateProductType(serviceProvider);
            form.SetupAdd();
            return form;
        }
        public static async Task<frmAddUpdateProductType> CreateForUpdate(IServiceProvider serviceProvider, int ProductTypeId)
        {
            ProductTypeUpdateDto dto;

            using (var scope = serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductTypeService>();

                dto = await service.GetProductTypeForUpdateAsync(ProductTypeId);
            }

            var form = new frmAddUpdateProductType(serviceProvider);
            form.SetupUpdate(dto);

            return form;
        }


        private void SaveUpdates()
        {
            _ProductTypeUpdate.Name = this.txtName.Text.Trim();
        }

        private void SaveAddNew()
        {
            _ProductTypeAdd.Name = this.txtName.Text.Trim();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task UpdateProductType()
        {
            SaveUpdates();

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<ProductTypeService>();


                   await service.UpdateProductTypeAsync(_ProductTypeUpdate);
                }
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (LogicLayer.Validation.Exceptions.ValidationException ex)
            {
                MessageBox.Show(String.Join("\n", ex.Errors), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (OperationFailedException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(
                          ex,
                          "Unexpected error while Updating Product Type");
                MessageBox.Show("حدث خطأ غير متوقع أثناء التحديث", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("تم التحديث بنجاح");
            this.Close();
        }

        private async Task AddNew()
        {
            SaveAddNew();

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<ProductTypeService>();


                   await service.AddProductTypeAsync(_ProductTypeAdd);
                }
            }
            catch (LogicLayer.Validation.Exceptions.ValidationException ex)
            {
                MessageBox.Show(String.Join("\n", ex.Errors), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (OperationFailedException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Error while adding Product Type");
                MessageBox.Show("حدث خطأ غير متوقع", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show("تمت الإضافة بنجاح");
            this.Close();


        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                btnSave.Enabled = false;
                if (State == Enums.FormStateEnum.AddNew)
                {
                   await AddNew();
                }
                else if (State == Enums.FormStateEnum.Update)
                {
                   await UpdateProductType();
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnSave.Enabled = true;
            }
            
        }
    }
}

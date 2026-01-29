using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.General;
using InventorySalesManagementSystem.MasurementUnits;
using InventorySalesManagementSystem.People;
using InventorySalesManagementSystem.Products.ProductsTypes;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.MasurementUnitDTO;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.DTOs.ProductTypeDTO;
using LogicLayer.Global.Users;
using LogicLayer.Services;
using LogicLayer.Services.Products;
using LogicLayer.Utilities;
using LogicLayer.Validation;
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
    public partial class frmAddUpdateProduct : Form
    {
        int ProductTypeId = -1;

        private Enums.FormStateEnum State { set; get; }

        private ProductAddDto _productAdd { set; get; }
        private ProductUpdateDto _productUpdate { set; get; }

        private IServiceProvider _serviceProvider { set; get; }

        private frmAddUpdateProduct(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private async Task FillUnitsComboBox()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<MasurementUnitService>();
                var units = await service.GetAllMasurementUnitAsync();

                cmpUnit.DataSource = units;
                cmpUnit.DisplayMember = nameof(MasurementUnitListDto.UnitName);
                cmpUnit.ValueMember = nameof(MasurementUnitListDto.MasurementUnitId);
            }
        }

        private async Task SetupAdd()
        {
            State = Enums.FormStateEnum.AddNew;

            lbTitle.Text = "إضافة صنف";
            lbId.Text = "---";

            _productAdd = new ProductAddDto();


           await FillUnitsComboBox();
        }

        private void SetupUpdate(ProductUpdateDto dto,ProductReadDto productReadDto)
        {
            State = Enums.FormStateEnum.Update;

            _productUpdate = dto;

            lbTitle.Text = "تعديل صنف";


            chkAvilable.Enabled = false;
            cmpUnit.Enabled = false;
            txtQuantity.Enabled = false;
            txtProductTypeName.Enabled = false;


            lkSelectProductType.Enabled = false;
            lkAddUnit.Enabled = false;

            LoadUpdateData(productReadDto);
        }

        private void LoadUpdateData(ProductReadDto productReadDto)
        {
            lbId.Text = _productUpdate.ProductId.ToString();

            txtQuantity.Text = productReadDto.QuantityInStorage.ToString();
            txtProductTypeName.Text = productReadDto.ProductTypeName;
            chkAvilable.Checked = productReadDto.IsAvailable;

            cmpUnit.Items.Add(productReadDto.MesurementUnitName);
            cmpUnit.SelectedItem = productReadDto.MesurementUnitName;

            txtProductName.Text = _productUpdate.ProductName.ToString();
            txtBuyingPrice.Text = _productUpdate.BuyingPrice.ToString();
            txtSellingPrice.Text = _productUpdate.SellingPrice.ToString();
            chkAvilable.Checked = _productUpdate.IsAvilable;
        }

        public static async Task<frmAddUpdateProduct> CreateForAdd(IServiceProvider serviceProvider)
        {
            var form = new frmAddUpdateProduct(serviceProvider);
            await form.SetupAdd();
            return form;
        }
        public static async Task<frmAddUpdateProduct> CreateForUpdate(IServiceProvider serviceProvider, int ProductId)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();

                var dto = await service.GetProductForUpdateAsync(ProductId);

                var productRead = await service.GetProductByIdAsync(ProductId); // To Get The Un Editable Data

                frmAddUpdateProduct frm = new frmAddUpdateProduct(serviceProvider);
                 frm.SetupUpdate(dto,productRead);
                return frm;
            }
        }

        private void FillProductAdd()
        {
            _productAdd.ProductName = txtProductName.Text;
            _productAdd.QuantityInStorage = Convert.ToDecimal(txtQuantity.Text);
            _productAdd.BuyingPrice = Convert.ToDecimal(txtBuyingPrice.Text);
            _productAdd.SellingPrice = Convert.ToDecimal(txtSellingPrice.Text);
            _productAdd.ProductTypeId = ProductTypeId;
            _productAdd.IsAvailable = chkAvilable.Checked;
            if (cmpUnit.SelectedValue != null)
            {
                _productAdd.MasurementUnitId = (int)cmpUnit.SelectedValue;
            }
        }
        private void FillProductUpdate()
        {
            _productUpdate.ProductName = txtProductName.Text;
            _productUpdate.BuyingPrice = Convert.ToDecimal(txtBuyingPrice.Text);
            _productUpdate.SellingPrice = Convert.ToDecimal(txtSellingPrice.Text);
        }


        private void ValidationCore(string BuyingPrice, string SellingPrice, string Quantity, bool ValidateQuantity)
        {
            var errors = new List<string>();

            if (!FormatValidation.IsValidDecimal(BuyingPrice))
            {
                string propertyName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Product), nameof(Product.BuyingPrice));
                errors.Add($"{propertyName} تنسيقه غير صحيح");
            }

            if (!FormatValidation.IsValidDecimal(SellingPrice))
            {
                string propertyName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Product), nameof(Product.SellingPrice));
                errors.Add($"{propertyName} تنسيقه غير صحيح");
            }

            if (ValidateQuantity && !FormatValidation.IsValidDecimal(Quantity))
            {
                string propertyName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Product), nameof(Product.QuantityInStorage));
                errors.Add($"{propertyName} تنسيقه غير صحيح");
            }

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }

        private void ValidatePricesLogic()
        {
            if (Convert.ToDecimal(txtBuyingPrice.Text) > Convert.ToDecimal(txtSellingPrice.Text))
            {
                string arabicbuyingprice = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Product), nameof(Product.BuyingPrice));
                string arabicSellingprice = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Product), nameof(Product.SellingPrice));
                throw new OperationFailedException($"{arabicSellingprice} لا يمكن أن يكون أقل من {arabicbuyingprice}");
            }
        }

        private async Task UpdateProduct(ProductService ProductService,int userId)
        {
            //Validate Values Format
            ValidationCore(txtBuyingPrice.Text, txtSellingPrice.Text, txtQuantity.Text, false);

            //Validate Logic (cannot do it before the text values check)
            ValidatePricesLogic();

            FillProductUpdate();


            
           
           await ProductService.UpdateProductAsync(_productUpdate, userId);

            //If Exception Is Thrown it Will Stop Here
            MessageBox.Show($"تم التحديث بنجاح");
            this.Close();
        }
        private async Task AddProduct(ProductService ProductService,int userId)
        {
            //Validate Cmp Units
            if (cmpUnit.SelectedValue == null)
            {
                string arabicEntityName = LogicLayer.Utilities.NamesManager.GetArabicEntityName(typeof(MasurementUnit));
                throw new ValidationException(new List<string>()
                {
                    {$"يجب إختيار {arabicEntityName}"}
                });
            }

            //Validate Model
            if (ProductTypeId <= 0)
            {
                throw new ValidationException(new List<string> { "يجب اختيار موديل المنتج أولاً" });
            }

            //Validate Values Format
            ValidationCore(txtBuyingPrice.Text, txtSellingPrice.Text, txtQuantity.Text, true);

            //Validate Logic (cannot do it before the text values check)
            ValidatePricesLogic();


            FillProductAdd();

           await ProductService.AddProductAsync(_productAdd, userId);

            //If Exception Is Thrown it Will Stop Here
            MessageBox.Show($"تمت الإضافة بنجاح");
            this.Close();
        }


        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                btnSave.Enabled = false;

                using (var scope = _serviceProvider.CreateScope())
                {

                    var UserSession = scope.ServiceProvider.GetRequiredService<UserSession>();

                    int userid = UserSession.CurrentUser != null ?
                        UserSession.CurrentUser.UserId
                        :
                        -1;

                    var ProductService = scope.ServiceProvider.GetRequiredService<ProductService>();

                    if (State == Enums.FormStateEnum.AddNew)
                    {
                        await AddProduct(ProductService, userid);
                    }
                    else if (State == Enums.FormStateEnum.Update)
                    {
                        

                       await UpdateProduct(ProductService, userid);
                    }
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
                MessageBox.Show(ex.MainBody,ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(
                      ex,
                     "Unexpected error while Saving Product");

                MessageBox.Show("حدث خطأ غير متوقع", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnSave.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lkSelectProductType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var frm = new frmProductTypeListScreen(_serviceProvider))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var selectedType = frm.SelectedProductType;
                    this.ProductTypeId = selectedType.ProductTypeId;
                    this.txtProductTypeName.Text = selectedType.Name;
                }
                else
                {
                    this.ProductTypeId = -1;
                    this.txtProductTypeName.Text = "";
                }
            }
        }

        private async void lkAddUnit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = frmAddUpdateMasurementUnit.CreateForAdd(_serviceProvider);
            frm.ShowDialog();

            //Reload Units
           await FillUnitsComboBox();
        }
    }
}

using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.General;
using InventorySalesManagementSystem.Products.ProductsTypes;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.Global.Users;
using LogicLayer.Services.Products;
using LogicLayer.Validation;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace InventorySalesManagementSystem.Products
{
    public partial class frmAddUpdateProduct : Form
    {
        int ProductTypeId = -1;

        private Enums.FormStateEnum State { set; get; }

        private BindingList<ProductAddDto> _productsAdd;
        private BindingList<ProductUpdateDto> _productsUpdate;
        private IServiceProvider _serviceProvider { set; get; }

        private frmAddUpdateProduct(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        #region Config

        private void ConfigAddMode()
        {
            UiFormat.DgvDefualt(dgvData);

            // ===== ProductName =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductAddDto.ProductName),
                DataPropertyName = nameof(ProductAddDto.ProductName),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.ProductName)),
                FillWeight = 35
            });


            // ===== BuyingPrice =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductAddDto.BuyingPrice),
                DataPropertyName = nameof(ProductAddDto.BuyingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.BuyingPrice)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                }
            });

            // ===== SellingPrice =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductAddDto.SellingPrice),
                DataPropertyName = nameof(ProductAddDto.SellingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.SellingPrice)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                }
            });




            // ===== Quantity =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductAddDto.QuantityInStorage),
                DataPropertyName = nameof(ProductAddDto.QuantityInStorage),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.QuantityInStorage)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                }
            });


            // ===== IsAvilable =====
            dgvData.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = nameof(ProductAddDto.IsAvailable),
                DataPropertyName = nameof(ProductAddDto.IsAvailable),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.IsAvailable)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });


            dgvData.DataSource = this._productsAdd;
        }

        private void ConfigUpdateMode()
        {
            UiFormat.DgvDefualt(dgvData);

            // ===== ProductName =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductUpdateDto.ProductName),
                DataPropertyName = nameof(ProductUpdateDto.ProductName),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.ProductName)),
                FillWeight = 35
            });


            // ===== BuyingPrice =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductUpdateDto.BuyingPrice),
                DataPropertyName = nameof(ProductUpdateDto.BuyingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.BuyingPrice)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                }
            });

            // ===== SellingPrice =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductUpdateDto.SellingPrice),
                DataPropertyName = nameof(ProductUpdateDto.SellingPrice),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.SellingPrice)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                }
            });

            // ===== Quantity =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductUpdateDto.Quantity),
                DataPropertyName = nameof(ProductUpdateDto.Quantity),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.QuantityInStorage)),
                FillWeight = 15,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                }
            });


            // ===== IsAvilable =====
            dgvData.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = nameof(ProductUpdateDto.IsAvilable),
                DataPropertyName = nameof(ProductUpdateDto.IsAvilable),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.IsAvailable)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                ReadOnly = true
            });


            dgvData.DataSource = this._productsUpdate;

        }
        #endregion

        private async Task SetupAdd()
        {
            State = Enums.FormStateEnum.AddNew;

            lbTitle.Text = "إضافة أصناف";

            _productsAdd = new BindingList<ProductAddDto>();

            ConfigAddMode();
        }

        private void SetupUpdate(BindingList<ProductUpdateDto> dtos,string ProductTypeName)
        {
            State = Enums.FormStateEnum.Update;

            _productsUpdate = dtos;

            lbTitle.Text = "تعديل أصناف";
            txtProductTypeName.Text = ProductTypeName;

            btnAddProduct.Enabled = false;
            btnAddProduct.Visible = false;

            btnDelete.Visible = false;
            btnDelete.Enabled = false;

            lkSelectProductType.Enabled = false;

            ConfigUpdateMode();
        }

        public static async Task<frmAddUpdateProduct> CreateForAdd(IServiceProvider serviceProvider)
        {
            var form = new frmAddUpdateProduct(serviceProvider);
            await form.SetupAdd();
            return form;
        }
        public static async Task<frmAddUpdateProduct> CreateForUpdate(IServiceProvider serviceProvider, int ProductTypeId)
        {
            using (var scope = serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();
                var TypeService = scope.ServiceProvider.GetRequiredService<ProductTypeService>();

                var type = await TypeService.GetProductTypeByIdAsync(ProductTypeId);
                var dtos = await service.GetProductsForUpdateAggregateByTypeIdAsync(ProductTypeId);

                frmAddUpdateProduct frm = new frmAddUpdateProduct(serviceProvider);
                frm.SetupUpdate(new BindingList<ProductUpdateDto>(dtos),type.Name);
                return frm;
            }
        }


        private void ValidatePricesLogic(decimal buyingPrice, decimal SellingPrice)
        {
            if (buyingPrice > SellingPrice)
            {
                string arabicbuyingprice = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Product), nameof(Product.BuyingPrice));
                string arabicSellingprice = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Product), nameof(Product.SellingPrice));
                throw new OperationFailedException($"{arabicSellingprice} لا يمكن أن يكون أقل من {arabicbuyingprice}");
            }
        }

        private async Task UpdateProduct(ProductService ProductService, int userId)
        {
            foreach (ProductUpdateDto product in _productsUpdate)
            {
                ValidatePricesLogic(product.BuyingPrice, product.SellingPrice);
            }

            await ProductService.UpdateProductAggregateAsync(_productsUpdate.ToList(), userId);

            //If Exception Is Thrown it Will Stop Here
            MessageBox.Show($"تم التحديث بنجاح");
            this.Close();
        }

        private async Task AddProduct(ProductService ProductService, int userId)
        {
            if (ProductTypeId <= 0)
            {
                throw new ValidationException(new List<string> { "يجب اختيار موديل المنتج أولاً" });
            }

            if (_productsAdd.Count == 0)
            {
                throw new ValidationException(new List<string> { "يجب إضافة منتج واحد على الأقل" });
            }

            foreach (ProductAddDto product in _productsAdd)
            {
                ValidatePricesLogic(product.BuyingPrice, product.SellingPrice);

                if (string.IsNullOrEmpty(product.ProductName))
                    throw new ValidationException(new List<string> { "إسم المنتج لا يمكن أن يكون فارغا" });

                product.ProductTypeId = ProductTypeId;

            }
            //Validate Model



            await ProductService.AddProductAggregateAsync(_productsAdd.ToList(), userId);

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

                using (var scope = _serviceProvider.CreateAsyncScope())
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
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (ProductTypeId <= 0)
            {
                MessageBox.Show("يجب اختيار موديل المنتج أولاً");
                return;
            }

            if (State == Enums.FormStateEnum.AddNew)
            {
                _productsAdd.Add(new ProductAddDto() { IsAvailable = true });
            }
        }

        private void dgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("من فضلك أدخل رقم صحيح");
            e.Cancel = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           var item =  dgvData.CurrentRow?.DataBoundItem as ProductAddDto;

            if (item != null)
            {
                _productsAdd.Remove(item);
            }
        }
    }
}

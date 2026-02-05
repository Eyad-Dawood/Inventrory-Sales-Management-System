using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.General;
using InventorySalesManagementSystem.Invoices.SoldProducts.UserControles.helpers;
using InventorySalesManagementSystem.Products;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.DTOs.ProductTypeDTO;
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

namespace InventorySalesManagementSystem.Invoices.SoldProducts.UserControles
{
    public partial class ucProductSelector : UserControl
    {

        private IServiceProvider _serviceProvider;

        private bool _IsRefundMode = false;
        private BindingList<SoldProductAddDto> _SoldProductAdd;
        private BindingList<SoldProductSaleDetailsListDto> _SoldProductRefund;
        private Dictionary<int, List<ProductNameAndIdListDto>> _cachedProducts = new();


        #region Get
        public List<SoldProductAddDto> GetSoldProducts()
        {
            if (_IsRefundMode)
            {
                return _SoldProductRefund.Select(p => new SoldProductAddDto()
                {
                    IsAvilable = p.IsAvilable,
                    PricePerUnit = p.SellingPricePerUnit,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    QuantityInStorage = p.QuantityInStorage,
                    TakeBatchId = 0 // to be set later
                })
                .ToList();
            }
            else
            {
                return _SoldProductAdd.ToList();
            }
        }
        #endregion

        #region Popup
        private PopupKeyFilter _popupKeyFilter;
        private void InitPopup()
        {
            popupList.Visible = false;
            popupList.DisplayMember = nameof(ProductTypeListDto.Name);
            popupList.Click += popupList_Click;
            popupList.KeyDown += popupList_KeyDown;
            popupList.DrawItem += popupList_DrawItem;


            this.Controls.Add(popupList);
            popupList.BringToFront();

            _popupKeyFilter = new PopupKeyFilter(
                popupList,
                CommitSelectedModel,
                () => popupList.Visible = false
            );

            Application.AddMessageFilter(_popupKeyFilter);
        }
        private void popupList_Click(object sender, EventArgs e)
        {
            CommitSelectedModel();
        }
        private void popupList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CommitSelectedModel();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                popupList.Visible = false;
                dgvData.Focus();
                e.Handled = true;
            }
        }
        private async void CommitSelectedModel()
        {
            if (popupList.SelectedItem is not ProductTypeListDto selected)
                return;

            var cell = dgvData.CurrentCell;

            //Store The Name In Tag -> Cell
            cell.Tag = selected.Name;

            // Show
            cell.Value = selected.Name;

            // Store The Id In Tag -> Row
            dgvData.CurrentRow.Tag = selected.ProductTypeId;

            popupList.Visible = false;


            //Load Produts Based On Id
            await LoadProductsForRow(dgvData.CurrentRow, selected.ProductTypeId);

            dgvData.BeginEdit(true);
        }
        private async Task LoadProductsForRow(DataGridViewRow row, int productTypeId)
        {
            List<ProductNameAndIdListDto> originalList;

            if (_cachedProducts.ContainsKey(productTypeId))
            {
                originalList = _cachedProducts[productTypeId];
            }
            else
            {
                using (var scope = _serviceProvider.CreateAsyncScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<ProductService>();
                    originalList = await service.GetProductsByTypeIdAsync(productTypeId);
                }
                _cachedProducts[productTypeId] = originalList;
            }

            var displayList = new List<ProductNameAndIdListDto>(originalList);

            displayList.Insert(0, new ProductNameAndIdListDto
            {
                ProductId = 0,
                ProductName = "اختر المنتج"
            });

            //ComboBoxCOlumn
            var productCell =
                row.Cells[nameof(ProductNameAndIdListDto.ProductName)]
                as DataGridViewComboBoxCell;

            productCell.Value = 0;

            productCell.DisplayMember = nameof(ProductNameAndIdListDto.ProductName);
            productCell.ValueMember = nameof(ProductNameAndIdListDto.ProductId);
            productCell.DataSource = displayList;

            UpdateTotal();
        }
        private void popupList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            e.DrawBackground();

            var item = (ProductTypeListDto)popupList.Items[e.Index];

            using var brush = new SolidBrush(e.ForeColor);
            e.Graphics.DrawString(
                item.Name,
                e.Font,
                brush,
                e.Bounds
            );

            e.DrawFocusRectangle();
        }
        private void ShowPopupList(List<ProductTypeListDto> data, TextBox txt)
        {
            if (data == null || data.Count == 0)
            {
                popupList.Visible = false;
                return;
            }

            popupList.DataSource = data;

            Rectangle cellRect = dgvData.GetCellDisplayRectangle(
                dgvData.CurrentCell.ColumnIndex,
                dgvData.CurrentCell.RowIndex,
                true);

            Point location = dgvData.PointToScreen(
                new Point(cellRect.Left, cellRect.Bottom));

            popupList.Location = this.PointToClient(location);
            popupList.Width = cellRect.Width;

            int maxVisibleItems = 6;
            popupList.Height = Math.Min(data.Count, maxVisibleItems) * popupList.ItemHeight;

            popupList.Visible = true;
        }
        #endregion

        #region Setup
        public void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _SoldProductAdd = new BindingList<SoldProductAddDto>();
            ConfigAddMode();
            this.Enabled = true;
        }
        public void Initialize(IServiceProvider serviceProvider, List<SoldProductSaleDetailsListDto> products)
        {
            _serviceProvider = serviceProvider;
            _SoldProductRefund = new BindingList<SoldProductSaleDetailsListDto>(products);
            ConfigRefundMode();
            this.Enabled = true;
        }
        public void RefundMode(bool Allow)
        {
            _IsRefundMode = Allow;
            btnAddProduct.Enabled = !Allow;
            btnAddRecord.Enabled = !Allow;
        }
        public ucProductSelector()
        {
            InitializeComponent();
            InitPopup();
        }
        #endregion

        #region Total
        private void UpdateTotal()
        {
            //Update Total
            if (_IsRefundMode)
            {
                lbTotal.Text = _SoldProductRefund.Sum(x => x.Total).ToString("N2");
            }
            else
            {
                lbTotal.Text = _SoldProductAdd.Sum(x => x.Total).ToString("N2");
            }
        }
        public void UpdateTotal(decimal Discount)
        {
            decimal Requierd = _SoldProductAdd.Sum(x => x.Total);

            if (Discount > Requierd)
            {
                throw new OperationFailedException("لا يمكن أن يكون الخصم أكبر من المبلغ المطلوب");
            }

            lbTotal.Text = (_SoldProductAdd.Sum(x => x.Total) - Discount).ToString("N2");
        }
        #endregion

        #region Configure
        private void ConfigAddMode()
        {
            UiFormat.DgvDefualt(dgvData);


            // ===== First We Have The Model name to search with =====

            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(ProductTypeListDto.Name),
                DataPropertyName = null, // dont link it , its only for search
                HeaderText = "الموديل",
                FillWeight = 30
            });


            List<ProductNameAndIdListDto> temp = new List<ProductNameAndIdListDto>()
            {
                new ProductNameAndIdListDto(){ProductId = 0,ProductName = "فارغ"}
            };

            dgvData.Columns.Add(new DataGridViewComboBoxColumn
            {
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.ProductName)),
                DisplayMember = nameof(ProductNameAndIdListDto.ProductName),
                ValueMember = nameof(ProductNameAndIdListDto.ProductId),
                DataSource = temp,

                Name = nameof(ProductNameAndIdListDto.ProductName),
                DataPropertyName = nameof(SoldProductAddDto.ProductId), // dont bind it 

                FillWeight = 25,

                //Data Source to be set later when we need
            });


            // ===== Quantity =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductAddDto.Quantity),
                DataPropertyName = nameof(SoldProductAddDto.Quantity),
                HeaderText = "الكمية",
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                }
            });



            // ===== PricePerUnit =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductAddDto.PricePerUnit),
                DataPropertyName = nameof(SoldProductAddDto.PricePerUnit),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.SellingPrice)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                },
                ReadOnly = true
            });




            // ===== Quantity =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductAddDto.QuantityInStorage),
                DataPropertyName = nameof(SoldProductAddDto.QuantityInStorage),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.QuantityInStorage)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                },
                ReadOnly = true
            });

            // ===== Total =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductAddDto.Total),
                DataPropertyName = nameof(SoldProductAddDto.Total),
                HeaderText = "إجمالي",
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                },
                ReadOnly = true
            });

            dgvData.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = nameof(SoldProductAddDto.IsAvilable),
                DataPropertyName = nameof(SoldProductAddDto.IsAvilable),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.IsAvailable)),
                FillWeight = 15,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                ReadOnly = true
            });

            dgvData.DataSource = this._SoldProductAdd;
        }
        private void ConfigRefundMode()
        {
            UiFormat.DgvDefualt(dgvData);


            // ===== First We Have The Model name to search with =====

            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductSaleDetailsListDto.ProductTypeName),
                DataPropertyName = nameof(SoldProductSaleDetailsListDto.ProductTypeName), // dont link it , its only for search
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(ProductType), nameof(ProductType.ProductTypeName)),
                FillWeight = 30,
                ReadOnly = true
            });


            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.ProductName)),
                Name = nameof(SoldProductSaleDetailsListDto.ProductName),
                DataPropertyName = nameof(SoldProductSaleDetailsListDto.ProductName), // dont bind it 
                FillWeight = 25,
                ReadOnly = true
            });


            // ===== Quantity =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductSaleDetailsListDto.Quantity),
                DataPropertyName = nameof(SoldProductSaleDetailsListDto.Quantity),
                HeaderText = "الكمية",
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                }
            });



            // ===== PricePerUnit =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductSaleDetailsListDto.SellingPricePerUnit),
                DataPropertyName = nameof(SoldProductSaleDetailsListDto.SellingPricePerUnit),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.SellingPrice)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                },
                ReadOnly = true
            });




            // ===== Quantity =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductSaleDetailsListDto.QuantityInStorage),
                DataPropertyName = nameof(SoldProductSaleDetailsListDto.QuantityInStorage),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.QuantityInStorage)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                },
                ReadOnly = true
            });

            // ===== Total =====
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(SoldProductSaleDetailsListDto.Total),
                DataPropertyName = nameof(SoldProductSaleDetailsListDto.Total),
                HeaderText = "إجمالي",
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                },
                ReadOnly = true
            });

            dgvData.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = nameof(SoldProductSaleDetailsListDto.IsAvilable),
                DataPropertyName = nameof(SoldProductSaleDetailsListDto.IsAvilable),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Product), nameof(Product.IsAvailable)),
                FillWeight = 15,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                ReadOnly = true
            });

            dgvData.DataSource = this._SoldProductRefund;
        }
        #endregion

        #region dgvEvents
        private void dgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("من فضلك أدخل رقم صحيح");
            e.Cancel = true;
        }
        private void dgvData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            var columnName = dgvData.Columns[e.ColumnIndex].Name;

            if (columnName != nameof(ProductTypeListDto.Name))
            {
                popupList.Visible = false;
            }
        }
        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name != nameof(ProductTypeListDto.Name))
                return;

            popupList.Visible = false;

            var cell = dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Tag is string cachedName)
            {
                cell.Value = cachedName;
            }
        }
        private async void dgvData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //If Its In Search Column
            if (dgvData.Columns[e.ColumnIndex].Name == nameof(ProductNameAndIdListDto.ProductName))
            {
                var row = dgvData.Rows[e.RowIndex];
                var item = row.DataBoundItem as SoldProductAddDto;

                if (item == null)
                    return;

                // if its still Defulat
                if (item.ProductId == 0)
                    return;

                //If User Selected Item , Load its price, quantity etc ...
                await LoadProductData(item);

                // Update Ui
                dgvData.InvalidateRow(e.RowIndex);
                UpdateTotal();
            }
            else if (dgvData.Columns[e.ColumnIndex].Name == nameof(SoldProductAddDto.Quantity))
            {
                //Update Ui
                dgvData.InvalidateRow(e.RowIndex);
                UpdateTotal();
            }
        }
        #endregion

        #region Data
        private async Task LoadProductData(SoldProductAddDto item)
        {
            List<ProductNameAndIdListDto> originalList;


            using var scope = _serviceProvider.CreateAsyncScope();
            var productService = scope.ServiceProvider.GetRequiredService<ProductService>();

            var product = await productService.GetProductByIdAsync(item.ProductId);

            if (product == null)
                return;


            item.PricePerUnit = product.SellingPrice;
            item.QuantityInStorage = product.QuantityInStorage;

            item.Quantity = 1;

        }
        #endregion
        
        #region Buttons
        private async Task PerformAddProduct()
        {
            var frm = await frmAddUpdateProduct.CreateForAdd(_serviceProvider);
            frm.ShowDialog();
        }
        private async void btnAddProduct_Click(object sender, EventArgs e)
        {
            await PerformAddProduct();
        }
        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            _SoldProductAdd.Add(new SoldProductAddDto() { ProductId = 0 });
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_IsRefundMode)
            {
                var item = dgvData.CurrentRow?.DataBoundItem as SoldProductSaleDetailsListDto;


                if (item != null)
                {
                    _SoldProductRefund.Remove(item);
                }
            }
            else
            {
                var item = dgvData.CurrentRow?.DataBoundItem as SoldProductAddDto;

                if (item != null)
                {
                    _SoldProductAdd.Remove(item);
                }
            }
        }
        #endregion

        #region SearchBox

        private void dgvData_EditingControlShowing(
            object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvData.CurrentCell.OwningColumn.Name == nameof(ProductTypeListDto.Name)
        && e.Control is TextBox txt)
            {
                txt.TextChanged -= ModelSearch_TextChanged;
                txt.TextChanged += ModelSearch_TextChanged;

                txt.KeyDown -= ModelSearch_KeyDown;
                txt.KeyDown += ModelSearch_KeyDown;
            }
        }
        private async void ModelSearch_TextChanged(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            if (txt.Text.Length < 3)
            {
                popupList.Visible = false;
                return;
            }

            List<ProductTypeListDto> productTypes;
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductTypeService>();
                productTypes = await service
                    .GetAllByProductTypeNameAsync(1, 6, txt.Text);
            }

            ShowPopupList(productTypes, txt);
        }
        private void ModelSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (!popupList.Visible)
                return;

            if (e.KeyCode == Keys.Down)
            {
                popupList.Focus();
                popupList.SelectedIndex = Math.Max(0, popupList.SelectedIndex);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                popupList.Focus();
                if (popupList.SelectedIndex > 0)
                    popupList.SelectedIndex--;
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                CommitSelectedModel();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                popupList.Visible = false;
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #endregion

    }
}

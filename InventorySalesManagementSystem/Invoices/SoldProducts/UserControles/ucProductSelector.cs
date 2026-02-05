using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Invoices;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.Products;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.DTOs.InvoiceDTO;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.DTOs.InvoiceDTO.TakeBatches;
using LogicLayer.DTOs.ProductDTO;
using LogicLayer.DTOs.ProductTypeDTO;
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

namespace InventorySalesManagementSystem.Invoices.SoldProducts.UserControles
{
    public partial class ucProductSelector : UserControl
    {
        private bool _suppressComboEvents = false;

        private IServiceProvider _serviceProvider;
        private int RowsPerPage => 15;

        private bool _IsRefundMode = false;


        private string _previousSearch1 = "";
        private string _previousSearch2 = "";

        private BindingList<SoldProductAddDto> _SoldProductAdd;

        public void RefundMode(bool Allow)
        {
            _IsRefundMode = Allow;
            btnAddProduct.Enabled = !Allow;
        }


        private void InitPopup()
        {
            popupList.Visible = false;
            popupList.DisplayMember = nameof(ProductTypeListDto.Name);
            popupList.Click += popupList_Click;
            popupList.KeyDown += popupList_KeyDown;
            popupList.DrawItem += popupList_DrawItem;

            this.Controls.Add(popupList);
            popupList.BringToFront();
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

            var row = dgvData.CurrentRow;

            // حط الاسم في الخلية
            dgvData.CurrentCell.Value = selected.Name;

            // لو محتاج الـ ID لاحقًا
            dgvData.CurrentRow.Tag = selected.ProductTypeId;

            popupList.Visible = false;

            await LoadProductsForRow(row, selected.ProductTypeId);

            // انقل الفوكس لعمود المنتج
            dgvData.CurrentCell = row.Cells[nameof(ProductNameAndIdListDto.ProductName)];


        }
        private async Task LoadProductsForRow(DataGridViewRow row, int productTypeId)
        {
            List<ProductNameAndIdListDto> products;

            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();
                products = await service.GetProductsByTypeIdAsync(productTypeId);
            }

            // عمود المنتج (ComboBox)
            var productCell =
                row.Cells[nameof(ProductNameAndIdListDto.ProductName)]
                as DataGridViewComboBoxCell;

            products.Insert(0, new ProductNameAndIdListDto
            {
                ProductId = 0,
                ProductName = "اختر المنتج"
            });

            productCell.Value = 0;

            productCell.DisplayMember = nameof(ProductNameAndIdListDto.ProductName);
            productCell.ValueMember = nameof(ProductNameAndIdListDto.ProductId);
            productCell.DataSource = products;

            UpdateTotal();
        }

        private void UpdateTotal()
        {
            //Update Total
            lbTotal.Text = _SoldProductAdd.Sum(x => x.Total).ToString("N2");
        }

        public ucProductSelector()
        {
            InitializeComponent();
            InitPopup();
        }

        public BindingList<SoldProductAddDto> GetSoldProducts()
        {
            return _SoldProductAdd;
        }


        public void Initialize(IServiceProvider serviceProvider)
        {
            //Add mode

            _serviceProvider = serviceProvider;
            _SoldProductAdd = new BindingList<SoldProductAddDto>();
            // Configure DataGridView
            ConfigAddMode();
            this.Enabled = true;
        }

        public void Initialize(IServiceProvider serviceProvider, List<SoldProductSaleDetailsListDto> products)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Initialize(serviceProvider);


            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }


        #region Configure
        private void ConfigAddMode()
        {
            // Defualt Settings
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();


            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.ReadOnly = false;
            dgvData.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvData.MultiSelect = false;

            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvData.ColumnHeadersDefaultCellStyle.Font =
                new Font(dgvData.Font, FontStyle.Bold);

            dgvData.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvData.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;


            dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;



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

            dgvData.DataSource = this._SoldProductAdd;
        }
        #endregion

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
            var item = dgvData.CurrentRow?.DataBoundItem as SoldProductAddDto;

            if (item != null)
            {
                _SoldProductAdd.Remove(item);
            }
        }

        private void dgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("من فضلك أدخل رقم صحيح");
            e.Cancel = true;
        }

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

        private void ModelSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (!popupList.Visible) return;

            if (e.KeyCode == Keys.Down)
            {
                popupList.Focus();
                popupList.SelectedIndex = 0;
                e.Handled = true;
            }
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

        private void dgvData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            var columnName = dgvData.Columns[e.ColumnIndex].Name;

            if (columnName != nameof(ProductTypeListDto.Name))
            {
                popupList.Visible = false;
            }
        }

        private async void dgvData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // نتأكد إن التغيير في عمود المنتج
            if (dgvData.Columns[e.ColumnIndex].Name == nameof(ProductNameAndIdListDto.ProductName))
            {
                var row = dgvData.Rows[e.RowIndex];
                var item = row.DataBoundItem as SoldProductAddDto;

                if (item == null)
                    return;

                // لو لسه "اختر المنتج"
                if (item.ProductId == 0)
                    return;

                await LoadProductData(item);

                // عشان يحدّث الـ UI
                dgvData.InvalidateRow(e.RowIndex);
                UpdateTotal();
            }

            else if (dgvData.Columns[e.ColumnIndex].Name == nameof(SoldProductAddDto.Quantity))
            {
                dgvData.InvalidateRow(e.RowIndex);
                UpdateTotal();
            }

        }

        private async Task LoadProductData(SoldProductAddDto item)
        {
            using var scope = _serviceProvider.CreateAsyncScope();
            var productService = scope.ServiceProvider.GetRequiredService<ProductService>();

            var product = await productService.GetProductByIdAsync(item.ProductId);

            if (product == null)
                return;

            // تعبئة البيانات
            item.PricePerUnit = product.SellingPrice;
            item.QuantityInStorage = product.QuantityInStorage;

            item.Quantity = 1;

        }
    }
}

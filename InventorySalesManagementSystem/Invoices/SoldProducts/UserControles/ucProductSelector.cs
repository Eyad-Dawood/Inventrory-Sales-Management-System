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



        public ucProductSelector()
        {
            InitializeComponent();
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

            dgvData.Columns.Add(new DataGridViewComboBoxColumn
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
                && e.Control is ComboBox cb)
            {
                cb.DropDownStyle = ComboBoxStyle.DropDown;
                cb.AutoCompleteMode = AutoCompleteMode.None;
                cb.FlatStyle = FlatStyle.Popup;

                cb.TextUpdate -= ModelCombo_TextUpdate;
                cb.TextUpdate += ModelCombo_TextUpdate;

                cb.SelectedIndexChanged -= Combo_SelectedIndexChanged;
                cb.SelectedIndexChanged += Combo_SelectedIndexChanged;
            }
        }

        private void Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressComboEvents)
                return; // تجاهل أي اختيار داخلي
        }


        private async void ModelCombo_TextUpdate(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string searchText = cb.Text;

            if (searchText.Length < 3)
                return;

            int rowIndex = dgvData.CurrentCell.RowIndex;
            var row = dgvData.Rows[rowIndex];
            var cell = (DataGridViewComboBoxCell)row.Cells[nameof(ProductTypeListDto.Name)];

            _suppressComboEvents = true;   // 🔒 اقفل التفاعل

            List<ProductTypeListDto> productTypes;
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductTypeService>();
                productTypes = await service
                    .GetAllByProductTypeNameAsync(1, 30, searchText);
            }

            cb.BeginUpdate();

            // ⚠️ الترتيب مهم
            cell.DisplayMember = nameof(ProductTypeListDto.Name);
            cell.ValueMember = nameof(ProductTypeListDto.ProductTypeId);
            cell.DataSource = productTypes;

            // ❌ امنع أي اختيار
            cb.SelectedIndex = -1;

            // 🔁 رجّع النص كما هو
            cb.Text = searchText;
            cb.SelectionStart = searchText.Length;
            cb.SelectionLength = 0;

            cb.EndUpdate();

            _suppressComboEvents = false;  // 🔓 افتح التفاعل

            cb.DroppedDown = true;
        }

    }
}

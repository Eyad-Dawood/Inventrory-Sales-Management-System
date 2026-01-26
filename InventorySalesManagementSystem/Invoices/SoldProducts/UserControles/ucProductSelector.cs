using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.DTOs.InvoiceDTO.SoldProducts;
using LogicLayer.DTOs.ProductDTO;
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
        private IServiceProvider _serviceProvider;
        private int RowsPerPage => 15;


        private string _previousSearch1 = "";
        private string _previousSearch2 = "";


        public ucProductSelector()
        {
            InitializeComponent();
            ucListView1.DataGridViewControl.KeyDown += DataGridViewControl_KeyDown;
        }

        private void DataGridViewControl_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.PerformClick();
                e.SuppressKeyPress = true; // Stop the beeb sound
            }
        }

        private bool IsProductAlreadyAdded(int productId)
        {
            return flwpSoldProducts.Controls
                .OfType<ucSoldProductCard>()
                .Any(c => c.GetSoldProductData().ProductId == productId);
        }

        private void OnRemoveButtonClicked(ucSoldProductCard obj)
        {
            flwpSoldProducts.Controls.Remove(obj);

            obj.Dispose();
        }
        private void OnQantityChanged()
        {
            //Update Total

            lbTotal.Text = $"{flwpSoldProducts.Controls.OfType<ucSoldProductCard>().Sum(c => c.TotalPrice):N2}";
        }
        private void AddSoldProductCard(ProductListDto product)
        {
            ucSoldProductCard ucSoldProductCard = new ucSoldProductCard();

            ucSoldProductCard.Enabled = false;
            ucSoldProductCard.Location = new Point(3, 3);
            ucSoldProductCard.Name = "ucSoldProductCard1";
            ucSoldProductCard.Size = new Size(973, 35);
            ucSoldProductCard.TabIndex = 0;
            ucSoldProductCard.OnRemoveButtonClicked += OnRemoveButtonClicked;
            ucSoldProductCard.OnQuantityChanged += OnQantityChanged;


            //This Order , So Total is calculated after adding the control
            flwpSoldProducts.Controls.Add(ucSoldProductCard);


            ucSoldProductCard.LoadData(product.ProductTypeName, product.ProductName, product.ProductId, product.QuantityInStorage, product.SellingPrice, product.MesurementUnitName);

        }
        public void Initialize(IServiceProvider serviceProvider)
        {

            _serviceProvider = serviceProvider;
            // Configure DataGridView
            ConfigureGrid(ucListView1.DataGridViewControl);
            // Subscribe to events
            ucListView1.OnFilterClicked += HandleFilterClicked;
            ucListView1.OnNextPage += HandlePageChanged;
            ucListView1.OnPreviousPage += HandlePageChanged;
            ucListView1.OnRefreshAfterOperation += HandleOperationFinished;
            // Initial Display
            _ = DisplayPageAsync(1);

            this.Enabled = true;

        }



        #region Configure
        protected void ConfigureGrid(DataGridView dgv)
        {
            // Defualt Settings
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font(dgv.Font, FontStyle.Bold);

            dgv.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;


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
        }
        #endregion


        public List<SoldProductAddDto> GetSoldProducts()
        {
            var result = new List<SoldProductAddDto>();

            foreach (ucSoldProductCard c in flwpSoldProducts.Controls.OfType<ucSoldProductCard>())
            {
                result.Add(c.GetSoldProductData());
            }
            return result;
        }


        #region MainLogic

        protected async Task<int> GetTotalPagesAsync()
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();
                return await service.GetTotalPagesByActivationState(ActivationState:true,RowsPerPage);
            }
        }

        protected async Task<int> GetTotalFilteredPagesAsync()
        {
            if (String.IsNullOrWhiteSpace(txtSearchValue1.Text) && String.IsNullOrWhiteSpace(txtSearchValue2.Text))
            {
                return await GetTotalPagesAsync();
            }

            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();

                return await service.GetTotalPageByFullNameAsync(txtSearchValue1.Text.Trim(), txtSearchValue2.Text.Trim(), RowsPerPage , ActivationState:true);

            }
        }

        protected async Task<IEnumerable<object>> GetDataAsync(int page)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ProductService>();
                return await service.GetAllByActivationStateAsync(page,RowsPerPage,ActivationState:true);
            }
        }

        protected async Task<IEnumerable<object>> GetFilteredDataAsync(int page)
        {
            if (String.IsNullOrWhiteSpace(txtSearchValue1.Text) && String.IsNullOrWhiteSpace(txtSearchValue2.Text))
            {
                return await GetDataAsync(page);
            }


            using (var scope = _serviceProvider.CreateAsyncScope())
            {

                var service = scope.ServiceProvider.GetRequiredService<ProductService>();

                return await service.GetAllByFullNameAsync(page, RowsPerPage, txtSearchValue1.Text.Trim(), txtSearchValue2.Text.Trim(),ActivationState:true);
            }
        }


        #endregion

        #region Paging Core
        private async Task DisplayPageAsync(int page)
        {
            int totalPages = await GetTotalFilteredPagesAsync();


            page = Math.Max(1, Math.Min(page, totalPages));

            var data =
                 await GetFilteredDataAsync(page);

            ucListView1.DisplayData<object>(data, page, totalPages);
        }
        #endregion

        #region Events
        private async Task HandleFilterClicked(UcListView.Filter filter)
        {
            await DisplayPageAsync(1);
        }

        private async Task HandlePageChanged(int page, UcListView.Filter filter)
        {
            await DisplayPageAsync(page);
        }

        private async Task HandleOperationFinished(int page, UcListView.Filter filter)
        {
            await DisplayPageAsync(page);
        }
        #endregion

        private void txtSearchValue1_TextChanged(object sender, EventArgs e)
        {
            //Restart the timer

            timer1.Stop();
            timer1.Start();
        }

        private void txtSearchValue2_TextChanged(object sender, EventArgs e)
        {
            //Restart the timer

            timer1.Stop();
            timer1.Start();
        }


        private async Task PerformDisplayPage(int page)
        {
            await DisplayPageAsync(page);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop(); // Important , stop the timer , so it doesnt call the db every interval even if user didnt change the text

            bool isSearchChanged = (_previousSearch1.Trim() != txtSearchValue1.Text) ||
                                   (_previousSearch2.Trim() != txtSearchValue2.Text);
            ;

            _previousSearch1 = txtSearchValue1.Text.Trim();
            _previousSearch2 = txtSearchValue2.Text.Trim();

            if (isSearchChanged)
                _ = PerformDisplayPage(1);
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedProduct = ucListView1.GetSelectedItem<ProductListDto>();

            if (selectedProduct == null)
            {
                MessageBox.Show("يرجى اختيار منتج من القائمة أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsProductAlreadyAdded(selectedProduct.ProductId))
            {
                MessageBox.Show("هذا المنتج تمت إضافته بالفعل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            AddSoldProductCard(selectedProduct);
            txtSearchValue1.Clear();
            txtSearchValue1.Focus();
        }
        private void txtSearchValue1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.PerformClick();
                e.SuppressKeyPress = true; // Stop the beeb sound
            }
        }
        private void txtSearchValue2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                ucListView1.DataGridViewControl.Focus();
                e.SuppressKeyPress = true;
            }
        }

    }
}

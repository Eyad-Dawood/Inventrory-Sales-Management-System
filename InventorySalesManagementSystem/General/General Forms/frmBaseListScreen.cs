using DataAccessLayer.Entities;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.DTOs.WorkerDTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.General.General_Forms
{
    public partial class frmBaseListScreen : Form
    {
        private bool _selectbutton = true;
        
        [Browsable(true)]
        [DefaultValue(true)]
        public bool SelectButton 
        {
            get
            {
                return _selectbutton;
            }
            set
            {
                _selectbutton = value;
                btnSelect.Visible = value;
                btnSelect.Enabled = value;
            }
        }


        private bool _addbutton = true;

        [Browsable(true)]
        [DefaultValue(true)]
        public bool AddButton
        {
            get
            {
                return _addbutton;
            }
            set
            {
                _addbutton = value;
                btnAdd.Visible = value;
                btnAdd.Enabled = value;
            }
        }


        protected virtual int RowsPerPage => 30;
        protected virtual ContextMenuStrip GridContextMenu => null;

        public frmBaseListScreen()
        {
            InitializeComponent();
        }


        #region Config
        protected virtual void ConfigureGrid(DataGridView dgv)
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

            dgv.ContextMenuStrip = GridContextMenu;
        }

        protected virtual List<UcListView.FilterItems> ConfigureFilter()
        {
            return new List<UcListView.FilterItems>();
        }

        private void ApplyFilter()
        {
            ucListView1.ConfigureFilter(ConfigureFilter());
        }
        #endregion

        #region Core Life Cycle
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitializeEvents();
            InitializeUI();
            _ = InitializeAsync();
        }

        private void InitializeEvents()
        {
            ucListView1.OnFilterClicked = HandleFilterClicked;
            ucListView1.OnFilterCanceled = HandleFilterCanceled;
            ucListView1.OnNextPage = HandlePageChanged;
            ucListView1.OnPreviousPage = HandlePageChanged;
            ucListView1.OnRefreshAfterOperation = HandleOperationFinished;
        }

        private void InitializeUI()
        {
            ucListView1.ConfigureGrid = ConfigureGrid;
            ApplyFilter();
        }

        private async Task InitializeAsync()
        {
            try
            {
                await DisplayPageAsync(1);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Failed to initialize list screen");
                MessageBox.Show("حدث خطأ أثناء تحميل البيانات");
            }
        }

        #endregion

        #region Events
        private async Task HandleFilterClicked(UcListView.Filter filter)
        {
            await DisplayFilteredPageAsync(1, filter);
        }

        private async Task HandleFilterCanceled()
        {
            await DisplayPageAsync(1);
        }

        private async Task HandlePageChanged(int page, UcListView.Filter filter)
        {
            await DisplayFilteredPageAsync(page, filter);
        }

        private async Task HandleOperationFinished(int page, UcListView.Filter filter)
        {
            await DisplayFilteredPageAsync(page, filter);
        }

        private async void BtnAdd_Click(object sender, EventArgs e)
        {
          await HandleAddButtonClicked();
        }
        private async void BtnSelect_Click(object sender, EventArgs e)
        {
           await HandleSelectButtonClicked();
        }
        #endregion

        #region Paging Core
        private async Task DisplayPageAsync(int page)
        {
            await DisplayFilteredPageAsync(page, null);
        }
        private async Task DisplayFilteredPageAsync(int page, UcListView.Filter filter)
        {
            bool isFiltered = ucListView1.IsDataFiltered && filter != null;

            int totalPages = isFiltered
                ? await GetTotalFilteredPagesAsync(filter)
                : await GetTotalPagesAsync();

            page = Math.Max(1, Math.Min(page, totalPages));

            var data = isFiltered
                ? await GetFilteredDataAsync(page, filter)
                : await GetDataAsync(page);

            ucListView1.DisplayData<object>(data, page, totalPages);
        }

        #endregion

        #region Hooks
        protected async virtual Task<int> GetTotalPagesAsync()
                => throw new NotImplementedException(
                    $"{GetType().Name} must override GetTotalPagesAsync");
        protected async virtual Task<int> GetTotalFilteredPagesAsync(UcListView.Filter filter)
            => throw new NotImplementedException(
                $"{GetType().Name} must override GetTotalFilteredPagesAsync");
        protected async virtual Task<IEnumerable<object>> GetDataAsync(int page)
            => throw new NotImplementedException(
                $"{GetType().Name} must override GetDataAsync");
        protected async virtual Task<IEnumerable<object>> GetFilteredDataAsync(int page, UcListView.Filter filter)
            => throw new NotImplementedException(
                $"{GetType().Name} must override GetFilteredDataAsync");
        protected async virtual Task HandleAddButtonClicked()
                        => throw new NotImplementedException(
                $"{GetType().Name} must override HandleAddButtonClicked");
        protected async virtual Task HandleSelectButtonClicked()
        => throw new NotImplementedException(
                $"{GetType().Name} must override HandleSelectButtonClicked");

        #endregion

    }
}

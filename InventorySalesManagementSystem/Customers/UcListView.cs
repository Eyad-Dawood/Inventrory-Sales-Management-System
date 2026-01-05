using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventorySalesManagementSystem.Customers
{
    public partial class UcListView : UserControl
    {
        public UcListView()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
        }



        public class FilterItems
        {
            public string DisplayName { get; set; }
            public string Value { get; set; }
        }
        public class Filter
        {
            public string ColumnName { get; set; }
            public string FilterValue { get; set; }
        }
        private IReadOnlyList<FilterItems> _filterItems;
        private bool _allowFilter = true;

        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowFilter
        {
            get
            {
                return _allowFilter;
            }
            set
            {
                pnUpperBar.Enabled = value;
                pnUpperBar.Visible = value;
                _allowFilter = value;
            }
        }
        private bool IsFilterItemsConfigured = false;
        public bool IsDataFiltered = false;
        public Action<Filter> OnFilterApplied;
        public Action OnFilterCanceled;


        [Browsable(false)]
        public Filter CurrentFilter
        {
            get
            {
                if (IsFilterItemsConfigured
                    && cmpSearchBy.Items.Count > 0
                    && !string.IsNullOrWhiteSpace(txtSearchValue.Text)
                    && cmpSearchBy.SelectedValue != null)
                {
                    return new Filter
                    {
                        ColumnName = cmpSearchBy.SelectedValue.ToString(),
                        FilterValue = txtSearchValue.Text
                    };
                }
                else
                    return null;
            }
        }
        public void ConfigureFilter(IReadOnlyList<FilterItems> items)
        {
            if (items == null || items.Count == 0)
            {
                return;
            }

            IsFilterItemsConfigured = true;
            _filterItems = items;

            cmpSearchBy.DisplayMember = nameof(FilterItems.DisplayName);
            cmpSearchBy.ValueMember = nameof(FilterItems.Value);
            cmpSearchBy.DataSource = items;

        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (IsFilterItemsConfigured &&
                cmpSearchBy.Items.Count > 0
                && cmpSearchBy.SelectedValue != null
                && !string.IsNullOrWhiteSpace(txtSearchValue.Text))
            {
                var filter = new Filter()
                {
                    ColumnName = cmpSearchBy.SelectedValue.ToString(),
                    FilterValue = txtSearchValue.Text
                };
                OnFilterApplied?.Invoke(filter);

                //Disable Filter Controle

                //Inform Me 
                IsDataFiltered = true;

                cmpSearchBy.Enabled = false;
                txtSearchValue.Enabled = false;
                btnFilter.Enabled = false;
                btnCancelFilter.Enabled = true;
            }
        }
        public void RemoveFilter()
        {
            if (IsFilterItemsConfigured)
            {
                IsDataFiltered = false;

                OnFilterCanceled?.Invoke();

                txtSearchValue.Text = string.Empty;

                //Enable Filter Controles

                //Inform Me
                IsDataFiltered = false;

                cmpSearchBy.Enabled = true;
                txtSearchValue.Enabled = true;
                btnFilter.Enabled = true;
                btnCancelFilter.Enabled = false;
            }
        }
        private void btnCancelFilter_Click(object sender, EventArgs e)
        {
            RemoveFilter();
        }

        //************************************************************//

        private IReadOnlyList<object> _data;

        private int _CurrentPageNumber = 1;
        private int _TotalPages = 0;
        private bool _allowPagin = true;

        [Browsable(true)]
        [DefaultValue(true)]
        public bool AllowPaging
        {
            get
            {
                return _allowPagin;
            }
            set
            {
                _allowPagin = value;
                ChangePagginUI(_allowPagin);
            }
        }

        private bool IsGridConfigured = false;

        public Action<DataGridView> ConfigureGrid;
        public Action<int> OnNextPage;
        public Action<int> OnPreviousPage;

        private void ChangePagginUI(bool value)
        {
            lbPageNumber.Visible = value;
            lbPageNumber.Enabled = value;

            btnNext.Visible = value;
            btnNext.Enabled = value;

            btnPrevious.Visible = value;
            btnPrevious.Enabled = value;
        }
        private void ExcecuteOnNextPage()
        {
            if (_allowPagin)
            {
                if (_CurrentPageNumber >= _TotalPages)
                {
                    return;
                }

                OnNextPage?.Invoke(_CurrentPageNumber + 1);
            }
        }
        private void ExcecuteOnPreviousPage()
        {
            if (_allowPagin)
            {
                if (_CurrentPageNumber == 1)
                {
                    return;
                }
                OnPreviousPage?.Invoke(_CurrentPageNumber - 1);
            }
        }
        private void ExcecuteConfigureGrid()
        {
            if (!IsGridConfigured)
            {
                ConfigureGrid?.Invoke(this.dgvData);
                IsGridConfigured = true;
            }
        }
        private void DisplayInternal<T>(IEnumerable<T> data)
        {
            if (data == null)
            {
                data = new List<T>();
            }

            _data = data.Cast<object>().ToList();
            dgvData.DataSource = _data;
        }
        private void UpdatePagingUI()
        {
            lbrowsCount.Text = $@"{_data.Count.ToString()} عنصر";
            lbPageNumber.Text = $@"{_CurrentPageNumber}/{_TotalPages}";
        }
        private void UpdateNonPagingUI()
        {
            lbrowsCount.Text = _data.Count.ToString();
        }
        public void DisplayData<T>(IEnumerable<T> data, int PageNumber, int TotalPages)
        {
            if (_allowPagin)
            {
                DisplayInternal(data);

                _CurrentPageNumber = PageNumber;
                _TotalPages = Math.Max(TotalPages, 1);


                UpdatePagingUI();


                ExcecuteConfigureGrid();
            }
        }
        public void DisplayData<T>(IEnumerable<T> data, int PageNumber)
        {
            if (_allowPagin)
            {
                DisplayInternal(data);

                _CurrentPageNumber = PageNumber;

                UpdatePagingUI();


                ExcecuteConfigureGrid();
            }
        }
        public void DisplayData<T>(IEnumerable<T> data)
        {
            if (!_allowPagin)
            {
                _data = data.Cast<object>().ToList();
                dgvData.DataSource = _data;

                UpdateNonPagingUI();


                ExcecuteConfigureGrid();
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            ExcecuteOnNextPage();
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            ExcecuteOnPreviousPage();
        }

    }
}

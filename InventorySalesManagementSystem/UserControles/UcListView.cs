using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventorySalesManagementSystem.UserControles
{
    public partial class UcListView : UserControl
    {
        //UI Properties
        #region UIProperties
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
                _allowFilter = value;
                ChangeFilterUI(_allowFilter);
            }
        }

        private bool _allowSecondSearchBox = false;
        [Browsable(true)]
        [DefaultValue(false)]
        public bool AllowSecondSearchBox
        {
            get
            {
                return _allowSecondSearchBox;
            }
            set
            {
                _allowSecondSearchBox = value;
                ChangeSecondFilterUi(_allowSecondSearchBox);
            }
        }

        private bool _allowDatePic = false;
        [Browsable(true)]
        [DefaultValue(false)]
        public bool AllowDatePic
        {
            get
            {
                return _allowDatePic;
            }
            set
            {
                _allowDatePic = value;
                ChangeDatePicFilterUi(_allowDatePic);
            }
        }

        #endregion


        enum RefreshDataOperation { NextPage = 0, PreviousPage = 1, DirectFilter = 2, DirectFilterCancel = 3, Operation = 4 }
        public UcListView()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
        }



        // Pseudocode / Plan (detailed):
        // - Inspect the incoming 'operation' value.
        // - For each operation type provide an empty switch case so the caller can add logic later.
        //   * PageChange:
        //       - This will be used when user navigates (Next/Previous).
        //       - Expected actions to implement later: update current page number, request new page data
        //         (respecting any active filter), update UI paging controls.
        //   * DirictFilter:
        //       - This will be used when a direct filter is applied.
        //       - Expected actions to implement later: reset to first page, apply filter criteria, request data,
        //         update UI to reflect filtered state.
        //   * DirictFilterCancel:
        //       - This will be used when the filter is canceled.
        //       - Expected actions to implement later: clear filter state, reset to first page, request raw data,
        //         re-enable filter controls, update UI.
        //   * Operation:
        //       - This will be used when data changes (add/update/delete).
        //       - Expected actions to implement later: refresh the data for the last known page (LastPageNumber),
        //         re-fetch counts/total pages from DB, maintain current filter if any, update UI.
        // - After the switch we keep existing comments / notes as placeholders for additional logic.
        // - Each case contains a placeholder comment and a 'break;' so the developer can fill the implementation later.

        private void RefreshData(RefreshDataOperation operation)
        {
            switch (operation)
            {
                case RefreshDataOperation.NextPage:
                    ExcecuteOnNextPage();
                    break;
                case RefreshDataOperation.PreviousPage:
                    ExcecuteOnPreviousPage();
                    break;

                case RefreshDataOperation.DirectFilter:
                    ApplyDirictFilter();
                    break;

                case RefreshDataOperation.DirectFilterCancel:
                    CancelFilter();
                    break;

                case RefreshDataOperation.Operation:
                    RefreshDataAfterOperation();
                    break;

                default:
                    // No operation / unrecognized -> do nothing
                    break;
            }
        }
        private void ExcecuteOnNextPage()
        {
            if (_allowPagin)
            {
                if (_CurrentPageNumber >= _TotalPages)
                {
                    return;
                }

                OnNextPage?.Invoke(_CurrentPageNumber + 1, GetFilter);
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

                OnPreviousPage?.Invoke(_CurrentPageNumber - 1, GetFilter);
            }
        }
        private void ApplyDirictFilter()
        {
            if (IsFilterSettingsValid)
            {
                SetFilterState(true);

                OnFilterClicked?.Invoke(GetFilter);
            }
        }
        private void CancelFilter()
        {
            if (_IsFilterItemsConfigured && IsDataFiltered)
            {
                SetFilterState(false);
                OnFilterCanceled?.Invoke();

                //Update Txtboxes
                txtSearchValue1.Text = string.Empty;
                txtSearchValue2.Text = string.Empty;

            }
        }
        private void RefreshDataAfterOperation()
        {
            //لو رجعت لقيت الداتا مفلترة خلاص اعمل زيها
            if (IsDataFiltered)
            {
                if (IsFilterSettingsValid)
                {
                    SetFilterState(true);

                    OnRefreshAfterOperation?.Invoke(_CurrentPageNumber, GetFilter);
                }
            }
            //لو رجعت لقيتها مش مفلترة بردو اعمل زيها
            else
            {
                SetFilterState(false);

                OnRefreshAfterOperation?.Invoke(_CurrentPageNumber, GetFilter);
            }
        }

        private void SetFilterState(bool filtered)
        {
            IsDataFiltered = filtered;
        }
        private void ChangeFilterUI(bool filtered)
        {
            pnUpperBar.Enabled = filtered;
            pnUpperBar.Visible = filtered;
        }
        private void ChangeSecondFilterUi(bool Allow)
        {
            txtSearchValue2.Enabled = Allow;
            txtSearchValue2.Visible = Allow;

            dtpLogDate.Enabled = !Allow;
            dtpLogDate.Visible = !Allow;

            chkUseDateFilter.Enabled = !Allow;
            chkUseDateFilter.Visible = !Allow;

            if (Allow)
                pnUpperButtons.Width -= txtSearchValue2.Width + 10;
            else
                pnUpperButtons.Width += txtSearchValue2.Width + 10;

        }

        private void ChangeDatePicFilterUi(bool Allow)
        {
            dtpLogDate.Enabled = Allow;
            dtpLogDate.Visible = Allow;

            chkUseDateFilter.Enabled = Allow;
            chkUseDateFilter.Visible = Allow;

            txtSearchValue2.Enabled = !Allow;
            txtSearchValue2.Visible = !Allow;

            if (Allow)
                pnUpperButtons.Width -= dtpLogDate.Width + chkUseDateFilter.Width + 10;
            else
                pnUpperButtons.Width += dtpLogDate.Width + chkUseDateFilter.Width + 10;

        }

        private void ChangePagginUI(bool value)
        {
            lbPageNumber.Visible = value;
            lbPageNumber.Enabled = value;

            btnNext.Visible = value;
            btnNext.Enabled = value;

            btnPrevious.Visible = value;
            btnPrevious.Enabled = value;
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



        public Action<DataGridView> ConfigureGrid;

        public Func<int, Filter, Task> OnNextPage;
        public Func<int, Filter, Task> OnPreviousPage;
        public Func<Filter, Task> OnFilterClicked;
        public Func<Task> OnFilterCanceled;
        public Func<int, Filter, Task> OnRefreshAfterOperation;

        private bool _IsFilterItemsConfigured = false;
        public bool IsDataFiltered = false;
        private bool IsGridConfigured = false;
        private bool IsFilterSettingsValid
        {
            get
            {
                if (!_IsFilterItemsConfigured ||
                    cmpSearchBy.Items.Count == 0 ||
                    cmpSearchBy.SelectedValue == null)
                    return false;

                bool hasTxt1 = !string.IsNullOrWhiteSpace(txtSearchValue1.Text);
                bool hasTxt2 = !string.IsNullOrWhiteSpace(txtSearchValue2.Text);

                if (_allowSecondSearchBox)
                {
                    return hasTxt1 || hasTxt2;
                }

                if (_allowDatePic)
                {
                    if (chkUseDateFilter.Checked)
                        return true;

                    return hasTxt1;
                }

                return hasTxt1;
            }
        }

        public DataGridView DataGridViewControl => dgvData;



        private int _CurrentPageNumber = 1;
        private int _TotalPages = 0;


        private IReadOnlyList<object> _data;


        private Filter? GetFilter
        {
            get
            {
                if (IsFilterSettingsValid)
                {
                    return new Filter
                    {
                        ColumnName = cmpSearchBy.SelectedValue.ToString(),
                        Text1Value = txtSearchValue1.Text.Trim(),
                        Text2Value = _allowSecondSearchBox ? txtSearchValue2.Text.Trim() : string.Empty,
                        dateTime = chkUseDateFilter.Checked ? dtpLogDate.Value : null
                    };
                }
                else
                    return null;
            }
        }
        public T? GetSelectedItem<T>() where T : class
        {
            return dgvData.CurrentRow?.DataBoundItem as T;
        }



        public class FilterItems
        {
            public string DisplayName { get; set; }
            public string Value { get; set; }
        }
        public class Filter
        {
            public string ColumnName { get; set; }
            public string Text1Value { get; set; }
            public string Text2Value { get; set; }
            public DateTime? dateTime { get; set; }
        }


        public void ConfigureFilter(IReadOnlyList<FilterItems> items)
        {
            if (items == null || items.Count == 0)
            {
                return;
            }

            _IsFilterItemsConfigured = true;

            cmpSearchBy.DisplayMember = nameof(FilterItems.DisplayName);
            cmpSearchBy.ValueMember = nameof(FilterItems.Value);
            cmpSearchBy.DataSource = items;

        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            RefreshData(RefreshDataOperation.DirectFilter);
        }
        private void btnCancelFilter_Click(object sender, EventArgs e)
        {
            RefreshData(RefreshDataOperation.DirectFilterCancel);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            RefreshData(RefreshDataOperation.NextPage);
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            RefreshData(RefreshDataOperation.PreviousPage);
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
        public void DisplayData<T>(IEnumerable<T> data, int PageNumber, int TotalPages)
        {
            if (_allowPagin)
            {
                DisplayInternal(data);

                _TotalPages = Math.Max(TotalPages, 1);
                _CurrentPageNumber = Math.Min(_TotalPages, PageNumber);

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


        public void RefreshAfterOperation()
        {
            RefreshData(RefreshDataOperation.Operation);
        }

        private void dgvData_CellMouseDoun(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvData.ClearSelection();
                dgvData.CurrentCell = dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dgvData.Rows[e.RowIndex].Selected = true;
            }
        }

        private void UcListView_Load(object sender, EventArgs e)
        {
            dtpLogDate.Value = DateTime.Now;
            dtpLogDate.MaxDate = DateTime.Now;
        }

        
    }
}

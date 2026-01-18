using DataAccessLayer.Entities;
using InventorySalesManagementSystem.UserControles;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.WorkerDTO;
using LogicLayer.Services;
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

namespace InventorySalesManagementSystem.Customers
{
    public partial class frmCustomerListScreen : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private const int RowsPerPage = 30;


        public frmCustomerListScreen(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        #region Config
        private void ConfigureContextMenuStrip(DataGridView dgv)
        {
            dgv.ContextMenuStrip = this.cms;
        }
        private void ConfigureGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ===== CustomerId =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(CustomerListDto.CustomerId),
                DataPropertyName = nameof(CustomerListDto.CustomerId),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Customer), nameof(Customer.CustomerId)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // ===== FullName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(CustomerListDto.FullName),
                DataPropertyName = nameof(CustomerListDto.FullName),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Person), nameof(Person.FullName)),
                FillWeight = 35
            });

            // ===== PhoneNumber =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(CustomerListDto.PhoneNumber),
                DataPropertyName = nameof(CustomerListDto.PhoneNumber),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Person), nameof(Person.PhoneNumber)),
                FillWeight = 15
            });

            // ===== Balance =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(CustomerListDto.Balance),
                DataPropertyName = nameof(CustomerListDto.Balance),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Customer), nameof(Customer.Balance)),
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // ===== TownName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(CustomerListDto.TownName),
                DataPropertyName = nameof(CustomerListDto.TownName),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Town), nameof(Town.TownName)),
                FillWeight = 25
            });

            // ===== IsActive =====
            dgv.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = nameof(CustomerListDto.IsActive),
                DataPropertyName = nameof(CustomerListDto.IsActive),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Customer), nameof(Customer.IsActive)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // ===== Header Style =====
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font(dgv.Font, FontStyle.Bold);

            dgv.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;



            ConfigureContextMenuStrip(dgv);
        }
        private void ConfigureFilter()
        {
            var items = new List<UcListView.FilterItems>()
                {
                    new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Person), nameof(Customer.Person.FullName)),
                                                 Value = nameof(Customer.Person.FullName)},
                     new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Town), nameof(Customer.Person.Town.TownName)),
                                                 Value = nameof(Customer.Person.Town.TownName)}
                };

            ucListView1.ConfigureFilter(items);
        }
        #endregion

        #region DataGetter
        private async Task<List<CustomerListDto>> GetData(CustomerService service,
                                              int PageNumber)
        {
            return await service.GetAllCustomersAsync(PageNumber, RowsPerPage);
        }
        private async Task<List<CustomerListDto>> GetFilteredData(
            CustomerService service,
            string columnName,
            int PageNumber,
            string value)
        {
            return columnName switch
            {
                nameof(Customer.Person.FullName)
                    => await service.GetAllByFullNameAsync(PageNumber, RowsPerPage, value),

                nameof(Customer.Person.Town.TownName)
                    => await service.GetAllByTownNameAsync(PageNumber, RowsPerPage, value),

                _ => new List<CustomerListDto>()
            };
        }

        private async Task<int> GetTotalFilteredPages(
            CustomerService service,
            string columnName,
            string value)
        {
            return columnName switch
            {
                nameof(Customer.Person.FullName)
                    => await service.GetTotalPageByFullNameAsync(value, RowsPerPage),

                nameof(Customer.Person.Town.TownName)
                    =>  await service.GetTotalPageByTownNameAsync(value, RowsPerPage),

                _ => 0
            };
        }

        private async Task<int> GetTotalPages(CustomerService service)
        {
            return await service.GetTotalPageNumberAsync(RowsPerPage);
        }
        #endregion


        private async Task DisplayPage(int PageNumber)
        {
            //Call filterMethod With Null Fitler
           await DisplayFilteredPage(PageNumber, null);
        }

        private async Task DisplayFilteredPage(int PageNumber, UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                bool isFiltered = ucListView1.IsDataFiltered && filter != null;

                int totalPages = isFiltered
                    ? await GetTotalFilteredPages(service, filter.ColumnName, filter.Text1Value)
                    : await GetTotalPages(service);

                int pageToRequest = Math.Max(1, Math.Min(PageNumber, totalPages));

                var data = isFiltered
                    ? await GetFilteredData(service, filter.ColumnName, pageToRequest, filter.Text1Value)
                    : await GetData(service, pageToRequest);

                ucListView1.DisplayData<CustomerListDto>(data, pageToRequest, totalPages);
            }
        }

        private async Task OnFilterClicked(UcListView.Filter filter)
        {
           await DisplayFilteredPage(1, filter);
        }
        private async Task OnFilterCanceled()
        {
           await DisplayPage(1);
        }
        private async Task OnPageChanged(int PageNumber, UcListView.Filter filter)
        {
           await DisplayFilteredPage(PageNumber, filter);
        }
        private async Task OnOperationFinished(int PageNumber, UcListView.Filter filter)
        {
           await DisplayFilteredPage(PageNumber, filter);
        }

        private async void frmCustomerListScreen_Load(object sender, EventArgs e)
        {
            ucListView1.OnFilterClicked = OnFilterClicked;
            ucListView1.OnFilterCanceled = OnFilterCanceled;
            ucListView1.OnNextPage = OnPageChanged;
            ucListView1.OnPreviousPage = OnPageChanged;
            ucListView1.OnRefreshAfterOperation = OnOperationFinished;

            ucListView1.ConfigureGrid = ConfigureGrid;

           await DisplayPage(1);
            ConfigureFilter();
        }



        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = await frmAddUpdateCustomer.CreateForAdd(_serviceProvider);
            frm.ShowDialog();

            ucListView1.RefreshAfterOperation();
        }



        /// <summary>
        /// Get Selected Id From Data Grid View 
        /// </summary>
        /// <returns>-1 if null</returns>
        private int GetSelectedId()
        {
            var selecteditem =
             ucListView1.GetSelectedItem<CustomerListDto>();

            if (selecteditem != null)
            {
                return selecteditem.CustomerId;
            }

            return -1;
        }

        private async void updateMenustripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Customer)));
                return;
            }

            try
            {
                var frm = await frmAddUpdateCustomer.CreateForUpdate(_serviceProvider, id);
                frm.ShowDialog();
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ");
            }

            ucListView1.RefreshAfterOperation();
        }

        private async void deleteMenustripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Customer)));
                return;
            }

            var selectedCustomer = ucListView1.GetSelectedItem<CustomerListDto>();
            string name = selectedCustomer?.FullName ?? id.ToString();

            string message = $"هل أنت متأكد من حذف العميل؟\n\n" +
                             $"المعرف: {selectedCustomer.CustomerId}\n" +
                             $"الاسم: >> {name} <<\n\n" +
                             $"تحذير: لا يمكن التراجع عن هذه العملية!";

            if (MessageBox.Show(message, "تأكيد الحذف",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                try
                {
                   await service.DeleteCustomerAsync(id);
                }
                catch (NotFoundException ex)
                {
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Customer)));
                    return;
                }
                catch (OperationFailedException ex)
                {
                    Serilog.Log.Error(ex.InnerException, "Unexcepected Error During Deleting Customer ");
                    MessageBox.Show(ex.MainBody, ex.Message);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "Unexcepected Error During Deleting Customer ");
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.OperationFailedErrorMessage());
                }

                ucListView1.RefreshAfterOperation();
            }
        }

        private void ShowMenustripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Customer)));
                return;
            }

            try
            {
                var frm = new frmShowCustomer(_serviceProvider, id);
                frm.ShowDialog();
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ");
            }
        }

        private async void changeActivationStateMenuStripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Customer)));
                return;
            }
            var selectedCustomer = ucListView1.GetSelectedItem<CustomerListDto>();


            string action = selectedCustomer.IsActive ? "إيقاف تنشيط" : "تنشيط";
            if (MessageBox.Show($"هل أنت متأكد من {action} العميل {selectedCustomer.FullName}؟",
                "تأكيد",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }


            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();
                try
                {
                   await service.ChangeActivationStateAsync(id, !selectedCustomer.IsActive);
                }
                catch (NotFoundException ex)
                {
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Customer)));
                    return;
                }
                catch (OperationFailedException ex)
                {
                    Serilog.Log.Error(ex.InnerException, "Unexcepected Error During Changing Customer Activation State ");
                    MessageBox.Show(ex.MainBody, ex.Message);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "Unexcepected Error During Customer Activation State ");
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.OperationFailedErrorMessage());
                }
            }

            ucListView1.RefreshAfterOperation();
        }

        
    }
}

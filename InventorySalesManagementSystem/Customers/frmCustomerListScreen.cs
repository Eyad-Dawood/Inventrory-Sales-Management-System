using DataAccessLayer.Entities;
using LogicLayer.DTOs.CustomerDTO;
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
using static InventorySalesManagementSystem.Customers.UcListView;

namespace InventorySalesManagementSystem.Customers
{
    public partial class frmCustomerListScreen : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private const int RowsPerPage = 20;


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
        private List<CustomerListDto> GetData(CustomerService service,
                                              int PageNumber)
        {
            return service.GetAllCustomers(PageNumber, RowsPerPage);
        }
        private List<CustomerListDto> GetFilteredData(
            CustomerService service,
            string columnName,
            int PageNumber,
            string value)
        {
            return columnName switch
            {
                nameof(Customer.Person.FullName)
                    => service.GetAllByFullName(PageNumber, RowsPerPage, value),

                nameof(Customer.Person.Town.TownName)
                    => service.GetAllByTownName(PageNumber, RowsPerPage, value),

                _ => new List<CustomerListDto>()
            };
        }

        private int GetTotalFilteredPages(
            CustomerService service,
            string columnName,
            string value)
        {
            return columnName switch
            {
                nameof(Customer.Person.FullName)
                    => service.GetTotalPageByFullName(value, RowsPerPage),

                nameof(Customer.Person.Town.TownName)
                    => service.GetTotalPageByTownName(value, RowsPerPage),

                _ => 0
            };
        }

        private int GetTotalPages(CustomerService service)
        {
            return service.GetTotalPageNumber(RowsPerPage);
        }
        #endregion


        private void DisplayFilteredPage(int PageNumber, UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<CustomerService>();

                if (ucListView1.IsDataFiltered)
                {
                    ucListView1.DisplayData<CustomerListDto>(GetFilteredData(service, filter.ColumnName, PageNumber, filter.FilterValue),
                         PageNumber,
                         GetTotalFilteredPages(service, filter.ColumnName, filter.FilterValue));
                }
                else
                {
                    ucListView1.DisplayData<CustomerListDto>(GetData(service, PageNumber),
                        PageNumber,
                        GetTotalPages(service));
                }
            }
        }
        private void DisplayPage(int PageNumber)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<CustomerService>();

                ucListView1.DisplayData<CustomerListDto>(GetData(service, PageNumber),
                    PageNumber,
                    GetTotalPages(service));
            }
        }


        private void OnFilterClicked(UcListView.Filter filter)
        {
            DisplayFilteredPage(1, filter);
        }
        private void OnFilterCanceled()
        {
            DisplayPage(1);
        }
        private void OnPageChanged(int PageNumber, UcListView.Filter filter)
        {
            DisplayFilteredPage(PageNumber, filter);
        }
        private void OnOperationFinished(int PageNumber, UcListView.Filter filter)
        {
            DisplayFilteredPage(PageNumber, filter);
        }

        private void frmCustomerListScreen_Load(object sender, EventArgs e)
        {
            ucListView1.OnFilterClicked = OnFilterClicked;
            ucListView1.OnFilterCanceled = OnFilterCanceled;
            ucListView1.OnNextPage = OnPageChanged;
            ucListView1.OnPreviousPage = OnPageChanged;
            ucListView1.OnRefreshAfterOperation = OnOperationFinished;

            ucListView1.ConfigureGrid = ConfigureGrid;

            DisplayPage(1);
            ConfigureFilter();
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = frmAddUpdateCustomer.CreateForAdd(_serviceProvider);
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

        private void updateMenustripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id < 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Customer)));
                return;
            }

            var frm = frmAddUpdateCustomer.CreateForUpdate(_serviceProvider, id);
            frm.ShowDialog();

            ucListView1.RefreshAfterOperation();
        }

        private void deleteMenustripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id < 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Customer)));
                return;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                try
                {
                    service.DeleteCustomer(id);
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
                    Serilog.Log.Error(ex,"Unexcepected Error During Deleting Customer ");
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.OperationFailedErrorMessage());
                }

                ucListView1.RefreshAfterOperation();
            }
        }
    }
}

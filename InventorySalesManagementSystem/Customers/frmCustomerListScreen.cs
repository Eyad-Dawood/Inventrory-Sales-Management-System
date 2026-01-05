using DataAccessLayer.Entities;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.Services;
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
        }


        //
        private List<CustomerListDto> GetFilteredData(
            CustomerService service,
            string columnName,
            int PageNumber,
            int RowsPerPage,
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
            int RowsPerPage,
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

        //For Next And Previous , So I Dont Need To Calculate Total Page Here
        //Also , I Commit To The Filter Applied Inside The Control
        private void ChangePage(int newPageNumber)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                if (ucListView1.IsDataFiltered)
                {
                    var filter = ucListView1.CurrentFilter;

                    if (filter != null)
                    {
                        List<CustomerListDto> results = GetFilteredData(service,
                                                             filter.ColumnName,
                                                             newPageNumber,
                                                             RowsPerPage,
                                                             filter.FilterValue);


                        ucListView1.DisplayData<CustomerListDto>(results,newPageNumber);
                    }
                }
                else
                {
                    var data = service.GetAllCustomers(newPageNumber, RowsPerPage);

                    ucListView1.DisplayData<CustomerListDto>(data, newPageNumber);
                }
            }
        }

        //This For Listing Filtered Data With Pages (Alowas Loads Page Number 1)
        private void LoadFilteredPage(UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                List<CustomerListDto> results = GetFilteredData(service,
                                                            filter.ColumnName,
                                                            1,
                                                            RowsPerPage,
                                                            filter.FilterValue);
                int TotalPage = GetTotalFilteredPages(service,
                                                       filter.ColumnName,
                                                       RowsPerPage,
                                                       filter.FilterValue);


                ucListView1.DisplayData<CustomerListDto>(results, 1, TotalPage);
            }
        }

        //This Loads The Raw Data In Pages Without ANy FIlters
        private void LoadInitialPage()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                int TotalPages = service.GetTotalPageNumber(RowsPerPage);
                var data = service.GetAllCustomers(1, RowsPerPage);


                ucListView1.DisplayData<CustomerListDto>(data, 1, TotalPages);
            }
        }

        public void OnNext(int newpageNumber)
        {
            ChangePage(newpageNumber);
        }
        public void OnPrev(int newpageNumber)
        {
            ChangePage(newpageNumber);
        }
        public void ApplyFilter(UcListView.Filter filter)
        {
            LoadFilteredPage(filter);
        }
        public void CancelFilter()
        {
            LoadInitialPage();
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

        private void frmCustomerListScreen_Load(object sender, EventArgs e)
        {
            ucListView1.ConfigureGrid = ConfigureGrid;
            ucListView1.OnNextPage = OnNext;
            ucListView1.OnPreviousPage = OnPrev;
            ucListView1.OnFilterApplied = ApplyFilter;
            ucListView1.OnFilterCanceled = CancelFilter;


            LoadInitialPage();
            ConfigureFilter();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = frmAddUpdateCustomer.CreateForAdd(_serviceProvider);
            frm.ShowDialog();

            ucListView1.RemoveFilter();
            LoadInitialPage();
        }
    }
}

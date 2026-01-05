using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repos;
using InventorySalesManagementSystem.Customers;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.Services;
using LogicLayer.Validation.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventorySalesManagementSystem
{
    public partial class Form1 : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private int RowsPerPage = 20; // Sample Data

        public Form1(IServiceProvider serviceProvider)
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

        private void ChangePage(int newPageNumber)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                if (ucListView1.IsDataFiltered)
                {
                    var Filter = ucListView1.CurrentFilter;

                    if (Filter != null)
                    {
                        if (Filter.ColumnName == nameof(Customer.Person.FullName))
                        {
                            var data = service.GetAllByFullName(newPageNumber, RowsPerPage, Filter.FilterValue);

                            ucListView1.DisplayData<CustomerListDto>(data, newPageNumber);
                        }
                        else if (Filter.ColumnName == nameof(Customer.Person.Town.TownName))
                        {
                            var data = service.GetAllByTownName(newPageNumber, RowsPerPage, Filter.FilterValue);

                            ucListView1.DisplayData<CustomerListDto>(data, newPageNumber);
                        }
                    }
                }
                else
                {
                    var data = service.GetAllCustomers(newPageNumber, RowsPerPage);

                    ucListView1.DisplayData<CustomerListDto>(data, newPageNumber);
                }
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
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                List<CustomerListDto> results;

                if (filter.ColumnName == nameof(Customer.Person.FullName))
                {
                    results = service.GetAllByFullName(1, RowsPerPage, filter.FilterValue);
                    int TotalPage = service.GetTotalPageByFullName(filter.FilterValue, RowsPerPage);
                    ucListView1.DisplayData<CustomerListDto>(results, 1, TotalPage);
                }
                else if (filter.ColumnName == nameof(Customer.Person.Town.TownName))
                {
                    results = service.GetAllByTownName(1, RowsPerPage, filter.FilterValue);
                    int TotalPage = service.GetTotalPageByTownName(filter.FilterValue, RowsPerPage);
                    ucListView1.DisplayData<CustomerListDto>(results, 1, TotalPage);
                }
            }
        }
        public void CancelFilter()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                int TotalPages = service.GetTotalPageNumber(RowsPerPage);

                var data = service.GetAllCustomers(1, RowsPerPage);

                ucListView1.DisplayData<CustomerListDto>(data, 1, TotalPages);
            }
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
        private void button1_Click(object sender, EventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                int TotalPages = service.GetTotalPageNumber(RowsPerPage);
                var data = service.GetAllCustomers(1, RowsPerPage);

                ConfigureFilter();
                ucListView1.DisplayData<CustomerListDto>(data, 1, TotalPages);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ucListView1.ConfigureGrid = ConfigureGrid;
            ucListView1.OnNextPage = OnNext;
            ucListView1.OnPreviousPage = OnPrev;
            ucListView1.OnFilterApplied = ApplyFilter;
            ucListView1.OnFilterCanceled = CancelFilter;
        }

        private void ucListView1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ucAddUpdatePerson1.Start(_serviceProvider);
        }
    }
}

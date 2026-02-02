using DataAccessLayer.Entities;
using InventorySalesManagementSystem.General.General_Forms;
using InventorySalesManagementSystem.UserControles;
using InventorySalesManagementSystem.Workers;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.DTOs.ProductTypeDTO;
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
    public partial class frmCustomerListScreen : frmBaseListScreen
    {
        private readonly IServiceProvider _serviceProvider;
        protected override ContextMenuStrip GridContextMenu => cms;
        public CustomerListDto SelectedCustomer { get; private set; }
        private const string PersonFilter = nameof(Customer.Person.FullName);
        private const string TownFilter = nameof(Customer.Person.Town.TownName);

        public frmCustomerListScreen(IServiceProvider serviceProvider,bool selectButton)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            SelectButton = selectButton;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lbTitle.Text = "شاشة العملاء";
        }


        #region Configure
        protected override List<UcListView.FilterItems> ConfigureFilter()
        {
            return new List<UcListView.FilterItems>()
                {
                    new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Person), nameof(Customer.Person.FullName)),
                                                 Value = PersonFilter},
                     new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Town), nameof(Customer.Person.Town.TownName)),
                                                 Value = TownFilter}
                };
        }
        protected override void ConfigureGrid(DataGridView dgv)
        {
            base.ConfigureGrid(dgv);


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
        }
        #endregion

        #region Hooks
        protected async override Task<int> GetTotalPagesAsync()
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();
                return await service.GetTotalPageNumberAsync(RowsPerPage);
            }
        }
        protected async override Task<IEnumerable<object>> GetDataAsync(int page)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();
                return await service.GetAllCustomersAsync(page, RowsPerPage);
            }
        }

        protected async override Task<IEnumerable<object>> GetFilteredDataAsync(int page, UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();
                return filter.ColumnName switch
                {
                    PersonFilter
                        => await service.GetAllByFullNameAsync(page, RowsPerPage, filter.Text1Value),

                    TownFilter
                        => await service.GetAllByTownNameAsync(page, RowsPerPage, filter.Text1Value),

                    _ => new List<CustomerListDto>()
                };
            }
        }
        protected async override Task<int> GetTotalFilteredPagesAsync(UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                return filter.ColumnName switch
                {
                    PersonFilter
                        => await service.GetTotalPageByFullNameAsync(filter.Text1Value, RowsPerPage),

                    TownFilter
                        => await service.GetTotalPageByTownNameAsync(filter.Text1Value, RowsPerPage),

                    _ => 0
                };
            }
        }

        #endregion

        #region Buttons Event
        protected async override Task HandleAddButtonClicked()
        {
            var frm = await frmAddUpdateCustomer.CreateForAdd(_serviceProvider);
            frm.ShowDialog();

            ucListView1.RefreshAfterOperation();
        }
        protected async override Task HandleSelectButtonClicked()
        {
            //No Async Here
            SelectedCustomer = ucListView1.GetSelectedItem<CustomerListDto>();

            if (SelectedCustomer != null)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region Menu Strip
        private async void updateMenustripItem_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<CustomerListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Customer)));
                return;
            }

            try
            {
                var frm = await frmAddUpdateCustomer.CreateForUpdate(_serviceProvider,item.CustomerId);
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
            var item = ucListView1.GetSelectedItem<CustomerListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Customer)));
                return;
            }

            string name = item?.FullName ?? "";

            string message = $"هل أنت متأكد من حذف العميل؟\n\n" +
                             $"المعرف: {item?.CustomerId}\n" +
                             $"الاسم: >> {name} <<\n\n" +
                             $"تحذير: لا يمكن التراجع عن هذه العملية!";

            if (MessageBox.Show(message, "تأكيد الحذف",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();

                try
                {
                   await service.DeleteCustomerAsync(item.CustomerId);
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
            var item = ucListView1.GetSelectedItem<CustomerListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Customer)));
                return;
            }

            try
            {
                var frm = new frmShowCustomer(_serviceProvider, item.CustomerId);
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
            var item = ucListView1.GetSelectedItem<CustomerListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Customer)));
                return;
            }


            string action = item.IsActive ? "إيقاف تنشيط" : "تنشيط";
            if (MessageBox.Show($"هل أنت متأكد من {action} العميل {item.FullName}؟",
                "تأكيد",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }


            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<CustomerService>();
                try
                {
                   await service.ChangeActivationStateAsync(item.CustomerId, !item.IsActive);
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
        #endregion
    }
}

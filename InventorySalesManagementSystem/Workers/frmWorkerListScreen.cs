using DataAccessLayer.Entities;
using InventorySalesManagementSystem.Customers;
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

namespace InventorySalesManagementSystem.Workers
{

    public partial class frmWorkerListScreen : frmBaseListScreen
    {

        private readonly IServiceProvider _serviceProvider;
        protected override ContextMenuStrip GridContextMenu => cms;
        public WorkerListDto SelectedWorker { get; private set; }


        public frmWorkerListScreen(IServiceProvider serviceProvider,bool selectButton)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            SelectButton = selectButton;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lbTitle.Text = "شاشة العمال";
        }

        #region Configure
        protected override List<UcListView.FilterItems> ConfigureFilter()
        {
            return new List<UcListView.FilterItems>()
                {
                    new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Person), nameof(Worker.Person.FullName)),
                                                 Value = nameof(Worker.Person.FullName)},
                     new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Town), nameof(Worker.Person.Town.TownName)),
                                                 Value = nameof(Worker.Person.Town.TownName)}
                };
        }
        protected override void ConfigureGrid(DataGridView dgv)
        {
            base.ConfigureGrid(dgv);


            // ===== WorkerId =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(WorkerListDto.WorkerId),
                DataPropertyName = nameof(WorkerListDto.WorkerId),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Worker), nameof(Worker.WorkerId)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // ===== FullName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(WorkerListDto.FullName),
                DataPropertyName = nameof(WorkerListDto.FullName),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Person), nameof(Person.FullName)),
                FillWeight = 35
            });

            // ===== PhoneNumber =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(WorkerListDto.PhoneNumber),
                DataPropertyName = nameof(WorkerListDto.PhoneNumber),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Person), nameof(Person.PhoneNumber)),
                FillWeight = 15
            });

            // ===== Craft =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(WorkerListDto.Craft),
                DataPropertyName = nameof(WorkerListDto.Craft),
                HeaderText = LogicLayer.Utilities.NamesManager
                    .GetArabicPropertyName(typeof(Worker), nameof(Worker.Craft)),
                FillWeight = 25
            });


            // ===== TownName =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = nameof(WorkerListDto.TownName),
                DataPropertyName = nameof(WorkerListDto.TownName),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Town), nameof(Town.TownName)),
                FillWeight = 25
            });

            // ===== IsActive =====
            dgv.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = nameof(WorkerListDto.IsActive),
                DataPropertyName = nameof(WorkerListDto.IsActive),
                HeaderText = LogicLayer.Utilities.NamesManager
                .GetArabicPropertyName(typeof(Worker), nameof(Worker.IsActive)),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
        }
        #endregion

        #region Hooks
        protected async override Task<int> GetTotalPagesAsync()
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<WorkerService>();
                return await service.GetTotalPageNumberAsync(RowsPerPage);
            }
        }

        protected async override Task<int> GetTotalFilteredPagesAsync(UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<WorkerService>();

                return filter.ColumnName switch
                {
                    nameof(Worker.Person.FullName)
                        => await service.GetTotalPageByFullNameAsync(filter.Text1Value, RowsPerPage),

                    nameof(Worker.Person.Town.TownName)
                        => await service.GetTotalPageByTownNameAsync(filter.Text1Value, RowsPerPage),

                    _ => 0
                };
            }
        }

        protected async override Task<IEnumerable<object>> GetDataAsync(int page)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<WorkerService>();
                return await service.GetAllWorkersAsync(page, RowsPerPage);
            }
        }

        protected async override Task<IEnumerable<object>> GetFilteredDataAsync(int page, UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<WorkerService>();
                return filter.ColumnName switch
                {
                    nameof(Worker.Person.FullName)
                        => await service.GetAllByFullNameAsync(page, RowsPerPage, filter.Text1Value),

                    nameof(Worker.Person.Town.TownName)
                        => await service.GetAllByTownNameAsync(page, RowsPerPage, filter.Text1Value),

                    _ => new List<WorkerListDto>()
                };
            }
        }
        #endregion

        #region Buttons Event
        protected async override Task HandleAddButtonClicked()
        {
            var frm = await frmAddUpdateWorker.CreateForAdd(_serviceProvider);
            frm.ShowDialog();

            ucListView1.RefreshAfterOperation();
        }

        protected async override Task HandleSelectButtonClicked()
        {
            //No Async Here
            SelectedWorker = ucListView1.GetSelectedItem<WorkerListDto>();

            if (SelectedWorker != null)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region Menu Strip
        private async void updateMenustripItem_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<WorkerListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Worker)));
                return;
            }

            try
            {
                var frm = await frmAddUpdateWorker.CreateForUpdate(_serviceProvider,item.WorkerId);
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

        private void ShowMenustripItem_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<WorkerListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Worker)));
                return;
            }

            try
            {
                var frm = new frmShowWorker(_serviceProvider,item.WorkerId);
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

        private async void deleteMenustripItem_Click_1(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<WorkerListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Worker)));
                return;
            }

            string name = item?.FullName ?? "";

            string message = $"هل أنت متأكد من حذف العامل؟\n\n" +
                             $"المعرف: {item?.WorkerId??-1}\n" +
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
                var service = scope.ServiceProvider.GetRequiredService<WorkerService>();

                try
                {
                   await service.DeleteWorkerAsync(item.WorkerId);
                }
                catch (NotFoundException ex)
                {
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Worker)));
                    return;
                }
                catch (OperationFailedException ex)
                {
                    Serilog.Log.Error(ex.InnerException, "Unexcepected Error During Deleting Worker ");
                    MessageBox.Show(ex.MainBody, ex.Message);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "Unexcepected Error During Deleting Worker ");
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.OperationFailedErrorMessage());
                }

                ucListView1.RefreshAfterOperation();
            }
        }

        private async void changeActivationStateMenuStripItem_Click(object sender, EventArgs e)
        {
            var item = ucListView1.GetSelectedItem<WorkerListDto>();

            if (item == null)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Worker)));
                return;
            }

            string action = item.IsActive ? "إيقاف تنشيط" : "تنشيط";
            if (MessageBox.Show($"هل أنت متأكد من {action} العامل {item.FullName}؟",
                "تأكيد", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }


            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<WorkerService>();
                try
                {
                   await service.ChangeActivationStateAsync(item.WorkerId, !item.IsActive);
                }
                catch (NotFoundException ex)
                {
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Worker)));
                    return;
                }
                catch (OperationFailedException ex)
                {
                    Serilog.Log.Error(ex.InnerException, "Unexcepected Error During Changing Worker Activation State ");
                    MessageBox.Show(ex.MainBody, ex.Message);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "Unexcepected Error During Changing Worker Activation State ");
                    MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.OperationFailedErrorMessage());
                }
            }

            ucListView1.RefreshAfterOperation();
        }
        #endregion

    }
}

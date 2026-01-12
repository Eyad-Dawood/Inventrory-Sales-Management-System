using DataAccessLayer.Entities;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.UserControles;
using InventorySalesManagementSystem.Workers;
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
namespace InventorySalesManagementSystem.Workers
{

    public partial class frmWorkerListScreen : Form
    {

        private readonly IServiceProvider _serviceProvider;
        private const int RowsPerPage = 30;


        public frmWorkerListScreen(IServiceProvider serviceProvider)
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
                    new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Person), nameof(Worker.Person.FullName)),
                                                 Value = nameof(Worker.Person.FullName)},
                     new UcListView.FilterItems(){DisplayName = LogicLayer.Utilities.NamesManager.GetArabicPropertyName(typeof(Town), nameof(Worker.Person.Town.TownName)),
                                                 Value = nameof(Worker.Person.Town.TownName)}
                };

            ucListView1.ConfigureFilter(items);
        }
        #endregion

        #region DataGetter
        private List<WorkerListDto> GetData(WorkerService service,
                                              int PageNumber)
        {
            return service.GetAllWorkers(PageNumber, RowsPerPage);
        }
        private List<WorkerListDto> GetFilteredData(
            WorkerService service,
            string columnName,
            int PageNumber,
            string value)
        {
            return columnName switch
            {
                nameof(Worker.Person.FullName)
                    => service.GetAllByFullName(PageNumber, RowsPerPage, value),

                nameof(Worker.Person.Town.TownName)
                    => service.GetAllByTownName(PageNumber, RowsPerPage, value),

                _ => new List<WorkerListDto>()
            };
        }

        private int GetTotalFilteredPages(
            WorkerService service,
            string columnName,
            string value)
        {
            return columnName switch
            {
                nameof(Worker.Person.FullName)
                    => service.GetTotalPageByFullName(value, RowsPerPage),

                nameof(Worker.Person.Town.TownName)
                    => service.GetTotalPageByTownName(value, RowsPerPage),

                _ => 0
            };
        }

        private int GetTotalPages(WorkerService service)
        {
            return service.GetTotalPageNumber(RowsPerPage);
        }
        #endregion


        private void DisplayPage(int PageNumber)
        {
            //Call filterMethod With Null Fitler
            DisplayFilteredPage(PageNumber, null);
        }

        private void DisplayFilteredPage(int PageNumber, UcListView.Filter filter)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<WorkerService>();

                bool isFiltered = ucListView1.IsDataFiltered && filter != null;

                int totalPages = isFiltered
                    ? GetTotalFilteredPages(service, filter.ColumnName, filter.Text1Value)
                    : GetTotalPages(service);

                int pageToRequest = Math.Max(1, Math.Min(PageNumber, totalPages));

                var data = isFiltered
                    ? GetFilteredData(service, filter.ColumnName, pageToRequest, filter.Text1Value)
                    : GetData(service, pageToRequest);

                ucListView1.DisplayData<WorkerListDto>(data, pageToRequest, totalPages);
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

        private void frmWorkerListScreen_Load(object sender, EventArgs e)
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
            var frm = frmAddUpdateWorker.CreateForAdd(_serviceProvider);
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
             ucListView1.GetSelectedItem<WorkerListDto>();

            if (selecteditem != null)
            {
                return selecteditem.WorkerId;
            }

            return -1;
        }

        private void updateMenustripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Worker)));
                return;
            }

            try
            {
                var frm = frmAddUpdateWorker.CreateForUpdate(_serviceProvider, id);
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
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Worker)));
                return;
            }

            try
            {
                var frm = new frmShowWorker(_serviceProvider, id);
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

        private void deleteMenustripItem_Click_1(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Worker)));
                return;
            }

            var selectedWorker = ucListView1.GetSelectedItem<WorkerListDto>();
            string name = selectedWorker?.FullName ?? id.ToString();

            string message = $"هل أنت متأكد من حذف العامل؟\n\n" +
                             $"المعرف: {selectedWorker.WorkerId}\n" +
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
                var service = scope.ServiceProvider.GetRequiredService<WorkerService>();

                try
                {
                    service.DeleteWorker(id);
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

        private void changeActivationStateMenuStripItem_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();

            if (id <= 0)
            {
                MessageBox.Show(LogicLayer.Validation.ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(typeof(Worker)));
                return;
            }
            var selectedWorker = ucListView1.GetSelectedItem<WorkerListDto>();


            string action = selectedWorker.IsActive ? "إيقاف تنشيط" : "تنشيط";
            if (MessageBox.Show($"هل أنت متأكد من {action} العامل {selectedWorker.FullName}؟",
                "تأكيد", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }


            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<WorkerService>();
                try
                {
                    service.ChangeActivationState(id, !selectedWorker.IsActive);
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

       
    }
}

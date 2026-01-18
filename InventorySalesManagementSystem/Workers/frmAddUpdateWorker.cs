using DataAccessLayer.Entities;
using InventorySalesManagementSystem.Customers;
using InventorySalesManagementSystem.General;
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
    public partial class frmAddUpdateWorker : Form
    {
        private Enums.FormStateEnum State { set; get; }

        private WorkerAddDto _workerAdd { get; set; }
        private WorkerUpdateDto _workerUpdate { get; set; }

        private IServiceProvider _serviceProvider { get; set; }

        private frmAddUpdateWorker(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }



        private async Task SetupAdd()
        {
            State = Enums.FormStateEnum.AddNew;

            lbTitle.Text = "إضافة عامل";

            _workerAdd = new WorkerAddDto();

            lbId.Text = "---";
           await uc_AddUpdatePerson1.Start(_serviceProvider);
        }

        private async Task SetupUpdate(WorkerUpdateDto dto)
        {
            State = Enums.FormStateEnum.Update;

            lbTitle.Text = "تعديل عامل";

            _workerUpdate = dto;

           await LoadUpdateData();
        }
        private async Task LoadUpdateData()
        {
            lbId.Text = _workerUpdate.WorkerId.ToString();
           await uc_AddUpdatePerson1.Start(_serviceProvider, _workerUpdate.PersonUpdateDto);

            if (_workerUpdate.Craft.HasFlag(DataAccessLayer.Entities.WorkersCraftsEnum.Painter))
                chkPainter.Checked = true;
            if (_workerUpdate.Craft.HasFlag(DataAccessLayer.Entities.WorkersCraftsEnum.Carpenter))
                chkCarpenter.Checked = true;
        }


        public static async Task<frmAddUpdateWorker> CreateForAdd(IServiceProvider serviceProvider)
        {
            var form = new frmAddUpdateWorker(serviceProvider);
           await form.SetupAdd();
            return form;
        }
        public static async Task<frmAddUpdateWorker> CreateForUpdate(IServiceProvider serviceProvider, int WorkerId)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<WorkerService>();

                var dto = await service.GetWorkerForUpdateAsync(WorkerId);

                frmAddUpdateWorker frm = new frmAddUpdateWorker(serviceProvider);
               await frm.SetupUpdate(dto);
                return frm;
            }
        }


        private void FillWorkerAdd()
        {
            //reset
            _workerAdd.Craft = GetSelectedCraftsFromUI();
            _workerAdd.PersonAddDto = uc_AddUpdatePerson1.GetAddPerson();
        }
        private void FillWorkerUpdate()
        {
            _workerUpdate.Craft = GetSelectedCraftsFromUI();
            _workerUpdate.PersonUpdateDto = uc_AddUpdatePerson1.GetUpdatePerson();
        }

        private WorkersCraftsEnum GetSelectedCraftsFromUI()
        {
            WorkersCraftsEnum crafts = WorkersCraftsEnum.None;

            if (chkCarpenter.Checked)
                crafts |= WorkersCraftsEnum.Carpenter;

            if (chkPainter.Checked)
                crafts |= WorkersCraftsEnum.Painter;

            return crafts;
        }
        private async Task UpdateWorker(WorkerService WorkerService)
        {
            FillWorkerUpdate();

            //Validate Values Format
            LogicLayer.Validation.Custom_Validation.PersonFormatValidation.ValidateValues(_workerUpdate.PersonUpdateDto);


           await WorkerService.UpdateWorkerAsync(_workerUpdate);

            //If Exception Is Thrown it Will Stop Here
            MessageBox.Show($"تم التحديث بنجاح");
            this.Close();
        }
        private async Task AddWorker(WorkerService WorkerService)
        {
            FillWorkerAdd();

            //Validate Values Format
            LogicLayer.Validation.Custom_Validation.PersonFormatValidation.ValidateValues(_workerAdd.PersonAddDto);


           await WorkerService.AddWorkerAsync(_workerAdd);

            //If Exception Is Thrown it Will Stop Here
            MessageBox.Show($"تمت الإضافة بنجاح");
            this.Close();
        }




        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if(!(chkCarpenter.Checked||chkPainter.Checked))
            {
                MessageBox.Show("يجب إختيار مهنة واحدة على الأقل", "فشل التحقق", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var WorkerService = scope.ServiceProvider.GetRequiredService<WorkerService>();

                    if (State == Enums.FormStateEnum.AddNew)
                    {
                       await AddWorker(WorkerService);
                    }
                    else if (State == Enums.FormStateEnum.Update)
                    {
                        await UpdateWorker(WorkerService);
                    }
                }
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (LogicLayer.Validation.Exceptions.ValidationException ex)
            {
                MessageBox.Show(String.Join("\n", ex.Errors), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(
                      ex,
                     "Unexpected error while Saving Worker");

                MessageBox.Show("حدث خطأ غير متوقع", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}

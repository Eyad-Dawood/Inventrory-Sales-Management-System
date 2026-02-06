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
using LogicLayer.Utilities;

namespace InventorySalesManagementSystem.Workers
{
    public partial class ucWorkerShow : UserControl
    {
        private IServiceProvider _serviceProvider;

        public ucWorkerShow()
        {
            InitializeComponent();
        }

        private void FillWorkerData(WorkerReadDto dto)
        {
            lbID.Text = dto.WorkerId.ToString();
            lbName.Text = dto.FullName;
            lbNational.Text = UiFormat.FormatNullableValue(dto.NationalNumber);
            lbPhone.Text = UiFormat.FormatNullableValue(dto.PhoneNumber);
            lbTown.Text = dto.TownName;



            lbCraft.Text = dto.Craft.ToDisplayText();

            chkIsActive.Checked = dto.IsActive;

        }

        public async Task ShowData(IServiceProvider serviceProvider, int workerId)
        {
            _serviceProvider = serviceProvider;

            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<WorkerService>();
                try
                {
                    var worker = await service.GetWorkerByIdAsync(workerId);
                    this.Enabled = true;
                    FillWorkerData(worker);

                }
                catch (NotFoundException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "During Finding Worker {WorkerId} In Show Data Function", workerId);
                    throw;
                }
            }
        }

        
    }
}

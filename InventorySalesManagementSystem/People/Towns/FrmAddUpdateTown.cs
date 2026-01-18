using DataAccessLayer.Entities;
using InventorySalesManagementSystem.General;
using LogicLayer.DTOs.PersonDTO;
using LogicLayer.DTOs.TownDTO;
using LogicLayer.Services;
using LogicLayer.Validation.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace InventorySalesManagementSystem.People.Towns
{
    public partial class FrmAddUpdateTown : Form
    {
        private Enums.FormStateEnum State { set; get; }
        private IServiceProvider _serviceProvider { set; get; }


        private TownAddDto _townAdd { set; get; }
        private TownUpdateDto _townUpdate { set; get; }





        private FrmAddUpdateTown(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
        }

        private void SetupAdd()
        {
            State = Enums.FormStateEnum.AddNew;

            this.Text = "إضافة بلد/مدينة";

            _townAdd = new TownAddDto();

            // UI defaults
            lb_TownId.Text = "---";
            txtTownName.Text = string.Empty;
        }
        private void SetupUpdate(TownUpdateDto dto)
        {
            State = Enums.FormStateEnum.Update;
            this.Text = "تعديل بلد/مدينة";
            _townUpdate = dto;

            //Load Data
            LoadUpdateData(this._townUpdate);
        }

        private void LoadUpdateData(TownUpdateDto dto)
        {
            this.lb_TownId.Text = dto.TownId.ToString();
            this.txtTownName.Text = dto.TownName;
        }

        public static FrmAddUpdateTown CreateForAdd(IServiceProvider serviceProvider)
        {
            var form = new FrmAddUpdateTown(serviceProvider);
            form.SetupAdd();
            return form;
        }

        public static async Task<FrmAddUpdateTown> CreateForUpdate(IServiceProvider serviceProvider, int townId)
        {
            TownUpdateDto dto;

            using (var scope = serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<TownService>();

                dto = await service.GetTownForUpdateAsync(townId);
            }

            var form = new FrmAddUpdateTown(serviceProvider);
            form.SetupUpdate(dto);

            return form;
        }


        private void SaveUpdates()
        {
            _townUpdate.TownName = this.txtTownName.Text.Trim();
        }
        private void SaveAddNew()
        {
            _townAdd.TownName = this.txtTownName.Text.Trim();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private async Task UpdateTown()
        {
            SaveUpdates();

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<TownService>();


                   await service.UpdateTownAsync(_townUpdate);
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
            catch (OperationFailedException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(
                          ex,
                          "Unexpected error while Updating Town");
                MessageBox.Show("حدث خطأ غير متوقع أثناء التحديث", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("تم التحديث بنجاح");
            this.Close();
        }
        private async Task AddNew()
        {
            SaveAddNew();

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<TownService>();


                  await  service.AddTownAsync(_townAdd);
                }
            }
            catch (LogicLayer.Validation.Exceptions.ValidationException ex)
            {
                MessageBox.Show(String.Join("\n", ex.Errors), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (OperationFailedException ex)
            {
                MessageBox.Show(ex.MainBody, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Error while adding Town");
                MessageBox.Show("حدث خطأ غير متوقع", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show("تمت الإضافة بنجاح");
            this.Close();


        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (State == Enums.FormStateEnum.AddNew)
            {
               await AddNew();
            }
            else if (State == Enums.FormStateEnum.Update)
            {
               await UpdateTown();
            }
        }
    }
}

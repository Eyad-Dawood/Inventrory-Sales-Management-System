using InventorySalesManagementSystem.General;
using InventorySalesManagementSystem.People.Towns;
using LogicLayer.DTOs.MasurementUnitDTO;
using LogicLayer.DTOs.TownDTO;
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

namespace InventorySalesManagementSystem.MasurementUnits
{
    public partial class frmAddUpdateMasurementUnit : Form
    {
        private Enums.FormStateEnum State { set; get; }
        private IServiceProvider _serviceProvider { set; get; }


        private MasurementUnitAddDto _UnitAdd { get; set; }
        private MasurementUnitUpdateDto _UnitUpdate { get; set; }


        public frmAddUpdateMasurementUnit(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void SetupAdd()
        {
            State = Enums.FormStateEnum.AddNew;

            this.Text = "إضافة وحدة قياس";

            _UnitAdd = new MasurementUnitAddDto();

            // UI defaults
            lb_TownId.Text = "---";
            txtTownName.Text = string.Empty;
        }
        private void SetupUpdate(MasurementUnitUpdateDto dto)
        {
            State = Enums.FormStateEnum.Update;
            this.Text = "تعديل وحدة قياس";
            _UnitUpdate = dto;

            //Load Data
            LoadUpdateData(this._UnitUpdate);
        }
        private void LoadUpdateData(MasurementUnitUpdateDto dto)
        {
            this.lb_TownId.Text = dto.MasurementUnitId.ToString();
            this.txtTownName.Text = dto.MasurementUnitName;
        }

        public static frmAddUpdateMasurementUnit CreateForAdd(IServiceProvider serviceProvider)
        {
            var form = new frmAddUpdateMasurementUnit(serviceProvider);
            form.SetupAdd();
            return form;
        }

        public static frmAddUpdateMasurementUnit CreateForUpdate(IServiceProvider serviceProvider, int townId)
        {
            MasurementUnitUpdateDto dto;

            using (var scope = serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<MasurementUnitService>();

                dto = service.GetUnitForUpdate(townId);
            }

            var form = new frmAddUpdateMasurementUnit(serviceProvider);
            form.SetupUpdate(dto);

            return form;
        }

        private void SaveUpdates()
        {
            _UnitUpdate.MasurementUnitName = this.txtTownName.Text.Trim();
        }
        private void SaveAddNew()
        {
            _UnitAdd.Name = this.txtTownName.Text.Trim();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateMasurementUnit()
        {
            SaveUpdates();

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<MasurementUnitService>();


                    service.UpdateMasurementUnit(_UnitUpdate);
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


        private void AddNew()
        {
            SaveAddNew();

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<MasurementUnitService>();


                    service.AddMasuremetUnit(_UnitAdd);
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
                Serilog.Log.Error(ex, "Error while adding Measurement Unit");
                MessageBox.Show("حدث خطأ غير متوقع", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show("تمت الإضافة بنجاح");
            this.Close();


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (State == Enums.FormStateEnum.AddNew)
            {
                AddNew();
            }
            else if (State == Enums.FormStateEnum.Update)
            {
                UpdateMasurementUnit();
            }
        }
    }
}

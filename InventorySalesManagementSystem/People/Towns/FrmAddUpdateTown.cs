using DataAccessLayer.Entities;
using InventorySalesManagementSystem.General;
using LogicLayer.DTOs.PersonDTO;
using LogicLayer.DTOs.TownDTO;
using LogicLayer.Services;
using LogicLayer.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
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
        private TownService _townService { set; get; }


        private TownAddDto _townAdd { set; get; }
        private TownUpdateDto _townUpdate { set; get; }





        private FrmAddUpdateTown(TownService townService)
        {
            InitializeComponent();

            _townService = townService;
        }
        
        private void SetupAdd()
        {
            State = Enums.FormStateEnum.AddNew;

            this.Text = "إضافة بلد/مدينة";

            _townAdd = new TownAddDto();

            // UI defaults
            lb_TownId.Text = "??";
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

        public static FrmAddUpdateTown CreateForAdd(TownService service)
        {
            var form = new FrmAddUpdateTown(service);
            form.SetupAdd();
            return form;
        }

        public static FrmAddUpdateTown CreateForUpdate(TownService service, int townId)
        {
                var dto = service.GetTownForUpdate(townId);

                var form = new FrmAddUpdateTown(service);
                form.SetupUpdate(dto);
                return form;
        }


        private void SaveUpdates()
        {
            _townUpdate.TownName = this.txtTownName.Text;
        }
        private void SaveAddNew()
        {
            _townAdd.TownName = this.txtTownName.Text;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void UpdateTown()
        {
            SaveUpdates();

            try
            {
                _townService.UpdateTown(_townUpdate);
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

        private void AddnNew()
        {
            SaveAddNew();

            try
            {
                _townService.AddTown(_townAdd);
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

            MessageBox.Show("تمت الإضافة بنجاح");
            this.Close();


        }
        private void btnSave_Click(object sender, EventArgs e)
        {
           if(State == Enums.FormStateEnum.AddNew)
            {
                AddnNew();
            }
            else if(State == Enums.FormStateEnum.Update)
            {
                UpdateTown();
            }
        }
    }
}

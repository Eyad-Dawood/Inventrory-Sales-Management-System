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

        private TownAddDto _townAdd { set; get; }
        private TownUpdateDto _townUpdate { set; get; }


        private TownService _townService { set; get; }


        
       
        public FrmAddUpdateTown()
        {
            InitializeComponent();
        }



        private void AddNewModeOn()
        {
            State = Enums.FormStateEnum.AddNew;
            this.Text = "إضافة بلد/مدينة";


            _townAdd = new TownAddDto();

            LoadAddNewScreen();
        }
        private void LoadAddNewScreen()
        {
            lb_TownId.Text = "??";

            txtTownName.Text = "";
        }
        public void Start(TownService townService)
        {
            _townService = townService;

            this.Enabled = true;

            AddNewModeOn();
        }


        private void UpdateModeOn()
        {
            State = Enums.FormStateEnum.Update;
            this.Text = "تعديل بلد/مدينة";


            LoadUpdateScreen();
        }

        private void LoadUpdateScreen()
        {
            lb_TownId.Text = _townUpdate.TownId.ToString();

            txtTownName.Text = _townUpdate.TownName;
        }

        public void Start(TownService townService, int townId)
        {
            


            _townService = townService;
            _townUpdate = _townService.GetTownForUpdate(townId);
            
            this.Enabled = true;

            UpdateModeOn();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillTownAdd()
        {
            _townAdd.TownName = txtTownName.Text;
        }
        private void FillTownUpdate()
        {
            _townUpdate.TownName = txtTownName.Text;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (State == Enums.FormStateEnum.AddNew)
                {
                    FillTownAdd();
                    _townService.AddTown(_townAdd);

                }
                else if (State == Enums.FormStateEnum.Update)
                {
                    FillTownUpdate();
                    _townService.UpdateTown(_townUpdate);

                }
            }
            catch(NotFoundException ex)
            {
                MessageBox.Show(ex.message, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch(LogicLayer.Validation.Exceptions.ValidationException ex)
            {
                MessageBox.Show(String.Join("\n",ex.Errors), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            this.Close();
        }



    }
}

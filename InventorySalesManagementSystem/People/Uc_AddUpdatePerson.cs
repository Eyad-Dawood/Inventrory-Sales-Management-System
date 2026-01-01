using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repos;
using InventorySalesManagementSystem.General;
using InventorySalesManagementSystem.People.Towns;
using LogicLayer.DTOs.PersonDTO;
using LogicLayer.DTOs.TownDTO;
using LogicLayer.Services;
using LogicLayer.Services.Helpers;
using LogicLayer.Validation.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.People
{
    public partial class Uc_AddUpdatePerson : UserControl
    {
        private Enums.FormStateEnum State { set; get; }

        private PersonAddDto _personAdd { set; get; }
        private PersonUpdateDto _personUpdate { set; get; }
        

        private IDbContextFactory<InventoryDbContext> _dbFactory { set; get; }
        private List<TownListDto> _towns { set; get; }

        public Uc_AddUpdatePerson()
        {
            InitializeComponent();

        }

        private void LoadTowns()
        {
            if (_dbFactory == null)
            {
                throw new InvalidOperationException("Town Service not initialized");
            }

            using (var dbContext = _dbFactory.CreateDbContext())
            {
                var service = ServiceHelper.CreateTownService(dbContext);

                _towns = service.GetAllTowns();
            }

            cmpTown.DisplayMember = nameof(TownListDto.TownName);
            cmpTown.ValueMember = nameof(TownListDto.TownID);
            cmpTown.DataSource = _towns;
        }

        private void AddNewModeOn()
        {
            State = Enums.FormStateEnum.AddNew;
            _personAdd = new PersonAddDto();

            LoadAddNewScreen();
        }
        private void LoadAddNewScreen()
        {
            LoadTowns();

            lb_PerosnId.Text = "??";
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtFourthName.Text = "";
            txtNationalNumber.Text = "";
            txtPhoneNumber.Text = "";

            cmpTown.SelectedIndex = -1;
        }


        private void UpdateModeOn()
        {
            State = Enums.FormStateEnum.Update;

            LoadUpdateScreen();
        }
        private void LoadUpdateScreen()
        {
            LoadTowns();

            lb_PerosnId.Text = _personUpdate.PersonId.ToString();

            txtFirstName.Text = _personUpdate.FirstName;
            txtSecondName.Text = _personUpdate.SecondName;
            txtThirdName.Text = _personUpdate.ThirdName;
            txtFourthName.Text = _personUpdate.FourthName;
            txtNationalNumber.Text = _personUpdate.NationalNumber;
            txtPhoneNumber.Text = _personUpdate.PhoneNumber;

            if (cmpTown.Items.Count > 0)
            {
                cmpTown.SelectedValue = _personUpdate.TownID;
            }
        }


        private void FillAddPerson()
        {
            _personAdd.FirstName = txtFirstName.Text;
            _personAdd.SecondName = txtSecondName.Text;
            _personAdd.ThirdName = txtThirdName.Text;
            _personAdd.FourthName = txtFourthName.Text;

            _personAdd.PhoneNumber = txtPhoneNumber.Text;

            _personAdd.NationalNumber = txtNationalNumber.Text;


            if (cmpTown.SelectedValue is int townId)
            {
                _personAdd.TownID = townId;
            }
        }

        private void FillUpdatePerson()
        {
            _personUpdate.FirstName = txtFirstName.Text;
            _personUpdate.SecondName = txtSecondName.Text;
            _personUpdate.ThirdName = txtThirdName.Text;
            _personUpdate.FourthName = txtFourthName.Text;

            _personUpdate.PhoneNumber = txtPhoneNumber.Text;

            _personUpdate.NationalNumber = txtNationalNumber.Text;

            if (cmpTown.SelectedValue is int townId)
            {
                _personUpdate.TownID = townId;
            }
        }

        public void Start(IDbContextFactory<InventoryDbContext> dbFactory)
        {
            _dbFactory = dbFactory;

            pnAllControles.Enabled = true;

            AddNewModeOn();
        }

        public void Start(IDbContextFactory<InventoryDbContext> dbFactory, PersonUpdateDto personDto)
        {
            if (personDto == null)
                throw new NotFoundException(typeof(PersonUpdateDto));

            _dbFactory = dbFactory;
            _personUpdate = personDto;

            pnAllControles.Enabled = true;

            UpdateModeOn();
        }

        public PersonAddDto GetAddPerson()
        {
            if (State == Enums.FormStateEnum.AddNew)
            {
                FillAddPerson();

                return _personAdd;
            }
            else
            {
                return new PersonAddDto();
            }
        }

        public PersonUpdateDto GetUpdatePerson()
        {
            if (State == Enums.FormStateEnum.Update)
            {
                FillUpdatePerson();

                return _personUpdate;
            }
            else
            {
                return new PersonUpdateDto();
            }
        }

        private void lkAddTown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmAddUpdateTown frm = new FrmAddUpdateTown();

            using (var dbContext = _dbFactory.CreateDbContext())
            {
                var service = ServiceHelper.CreateTownService(dbContext);

                try
                {
                    frm.Start(service);
                }
                catch(NotFoundException ex)
                {
                    MessageBox.Show(ex.message, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frm.ShowDialog();
            }

            LoadTowns();
        }
    }
}

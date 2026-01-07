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

namespace InventorySalesManagementSystem.People
{
    public partial class UcAddUpdatePerson : UserControl
    {
        private Enums.FormStateEnum State { set; get; }

        private PersonAddDto _personAdd { set; get; }
        private PersonUpdateDto _personUpdate { set; get; }
        

        private IServiceProvider _serviceProvider { set; get; }
        private List<TownListDto> _towns { set; get; }

        public UcAddUpdatePerson()
        {
            InitializeComponent();
            LoadDefualtValues();
        }


        public void Start(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            State = Enums.FormStateEnum.AddNew;
            _personAdd = new PersonAddDto();
            this.pnAllControles.Enabled = true;

            EnsureTownsLoaded();
            LoadAddNewScreen();
        }
        public void Start(IServiceProvider serviceProvider, PersonUpdateDto Dto)
        {
            if (Dto != null)
            {
                _serviceProvider = serviceProvider;


                State = Enums.FormStateEnum.Update;
                _personUpdate = Dto;
                this.pnAllControles.Enabled = true;

                EnsureTownsLoaded();
                LoadUpdateScreen();
            }
        }

        public PersonAddDto GetAddPerson()
        {

            if (State != Enums.FormStateEnum.AddNew || !this.pnAllControles.Enabled)
            {
                throw new InvalidOperationException("Cannot Access PersonAdd While In UpdateMode Or Control Is Disabled");
            }

            FillAddPerson();
            return _personAdd;
        }
        public PersonUpdateDto GetUpdatePerson()
        {

            if (State != Enums.FormStateEnum.Update || !this.pnAllControles.Enabled)
            {
                throw new InvalidOperationException("Cannot Access PersonUpdate While In AddMode Or Control Is Disabled");
            }

            FillUpdatePerson();
            return _personUpdate;
        }

        private void EnsureTownsLoaded()
        {
            if (_towns != null && _towns.Count > 0)
                return;

            LoadTowns();
        }
        private void LoadTowns()
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<TownService>();

                _towns = service.GetAllTowns();
            }

            cmpTown.DisplayMember = nameof(TownListDto.TownName);
            cmpTown.ValueMember = nameof(TownListDto.TownID);
            cmpTown.DataSource = _towns;

            cmpTown.SelectedIndex = -1;
        }

        private void LoadDefualtValues()
        {
            lb_PerosnId.Text = "---";
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtFourthName.Text = "";
            txtNationalNumber.Text = "";
            txtPhoneNumber.Text = "";

            cmpTown.SelectedIndex = -1;
        }

        private void LoadAddNewScreen()
        {

            LoadDefualtValues();
        }
        private void LoadUpdateScreen()
        {
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
            _personAdd.FirstName = txtFirstName.Text.Trim();
            _personAdd.SecondName = txtSecondName.Text.Trim();
            _personAdd.ThirdName = txtThirdName.Text.Trim();
            _personAdd.FourthName = txtFourthName.Text.Trim();

            _personAdd.PhoneNumber = txtPhoneNumber.Text.Trim();

            _personAdd.NationalNumber = txtNationalNumber.Text.Trim();


            if (cmpTown.SelectedValue is int townId)
            {
                _personAdd.TownID = townId;
            }
        }
        private void FillUpdatePerson()
        {
            _personUpdate.FirstName = txtFirstName.Text.Trim();
            _personUpdate.SecondName = txtSecondName.Text.Trim();
            _personUpdate.ThirdName = txtThirdName.Text.Trim();
            _personUpdate.FourthName = txtFourthName.Text.Trim();

            _personUpdate.PhoneNumber = txtPhoneNumber.Text.Trim();

            _personUpdate.NationalNumber = txtNationalNumber.Text.Trim();

            if (cmpTown.SelectedValue is int townId)
            {
                _personUpdate.TownID = townId;
            }
        }


        private void lkAddTown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FrmAddUpdateTown frm = FrmAddUpdateTown.CreateForAdd(_serviceProvider);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(
                       ex,
                       "Unexpected error while opening Add Town form");
            }
            LoadTowns();
        }
    }
}

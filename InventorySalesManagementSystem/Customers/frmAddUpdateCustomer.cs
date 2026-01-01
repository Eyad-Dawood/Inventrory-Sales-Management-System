using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repos;
using InventorySalesManagementSystem.General;
using LogicLayer.DTOs.CustomerDTO;
using LogicLayer.Services;
using LogicLayer.Services.Helpers;
using LogicLayer.Validation.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySalesManagementSystem.Customers
{
    public partial class frmAddUpdateCustomer : Form
    {
        private Enums.FormStateEnum State { set; get; }

        private CustomerAddDto _customerAdd { get; set; }
        private CustomerUpdateDto _customerUpdate { get; set; }

        private IDbContextFactory<InventoryDbContext> _dbFactory { set; get; }

        public frmAddUpdateCustomer()
        {
            InitializeComponent();
        }

        private void AddNewModeOn()
        {
            State = Enums.FormStateEnum.AddNew;
            lbTitle.Text = "إضافة زبون";

            _customerAdd = new CustomerAddDto();

            LoadAddNewScreen();
        }
        private void LoadAddNewScreen()
        {
            uc_AddUpdatePerson1.Start(_dbFactory);
        }
        public void Start(IDbContextFactory<InventoryDbContext> dbFactory)
        {
            _dbFactory = dbFactory;

            this.Enabled = true;

            AddNewModeOn();

        }



        private void UpdateModeOn()
        {
            State = Enums.FormStateEnum.Update;
            lbTitle.Text = "تعديل الزبون";

            LoadUpdateScreen();
        }
        private void LoadUpdateScreen()
        {
            

            lb_CustomerId.Text = _customerUpdate.CustomerId.ToString();

            uc_AddUpdatePerson1.Start(_dbFactory, _customerUpdate.PersonUpdateDto);
        }
        public void Start(IDbContextFactory<InventoryDbContext> dbFactory, int customerId)
        {
            _dbFactory = dbFactory;

            using (var dbContext = _dbFactory.CreateDbContext())
            {
                var Customerservice = ServiceHelper.CreateCustomerService(dbContext);

                _customerUpdate = Customerservice.GetCustomerForUpdate(customerId);
            }


             this.Enabled = true;

             UpdateModeOn();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillCustomerAdd()
        {
            _customerAdd.PersonAddDto = uc_AddUpdatePerson1.GetAddPerson();
        }
        private void FillCustomerUpdate()
        {
            _customerUpdate.PersonUpdateDto = uc_AddUpdatePerson1.GetUpdatePerson();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dbContext = _dbFactory.CreateDbContext())
                {

                    var CustomerService = ServiceHelper.CreateCustomerService(dbContext);

                    if (State == Enums.FormStateEnum.AddNew)
                    {
                        FillCustomerAdd();

                        CustomerService.AddCustomer(_customerAdd);
                    }
                    else if (State == Enums.FormStateEnum.Update)
                    {
                        FillCustomerUpdate();

                        CustomerService.UpdateCustomer(_customerUpdate);
                    }
                }
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(ex.message, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (LogicLayer.Validation.Exceptions.ValidationException ex)
            {
                MessageBox.Show(String.Join("\n", ex.Errors), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string OperationName = State == Enums.FormStateEnum.AddNew?
                                    "الإضافة":
                                    "التعديل";


            MessageBox.Show($"تم {OperationName} بنجاح");

            this.Close();
        }
    }
}
